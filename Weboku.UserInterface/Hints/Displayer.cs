using AKSoftware.Localization.MultiLanguages;
using System;
using System.Collections.Generic;
using System.Linq;
using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.UserInterface.Component.NumpadMenu;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Hints
{
    /// <summary>
    /// Contains methods for displaying user information about ISolvingTechnique.
    /// </summary>
    public class Displayer : IProvider
    {
        private readonly DomainFacade _domainFacade;
        private readonly CandidateColorProvider _candidatesMarkProvider;
        private readonly NumpadMenuBuilder _numpadMenuBuilder;
        private readonly Informer _informer;
        private readonly InputMarkProvider _markInputProvider;

        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ILanguageContainerService Loc { get; }

        public bool[] IsRowHighlighted = new bool[9];
        public bool[] IsColHighlighted = new bool[9];
        public bool[] IsBlockHighlighted = new bool[9];

        public event Action OnChanged;

        public Displayer(
            DomainFacade domainFacade,
            CandidateColorProvider candidatesMarkProvider,
            NumpadMenuBuilder numpadMenuBuilder,
            Informer informer,
            InputMarkProvider markInputProvider,
            ILanguageContainerService loc)
        {
            _domainFacade = domainFacade;
            _candidatesMarkProvider = candidatesMarkProvider;
            _numpadMenuBuilder = numpadMenuBuilder;
            _informer = informer;
            _markInputProvider = markInputProvider;
            Loc = loc;
        }

        public void Clear()
        {
            Title = string.Empty;
            Description = string.Empty;
            for (int i = 0; i < 9; i++)
            {
                IsRowHighlighted[i] = false;
                IsColHighlighted[i] = false;
                IsBlockHighlighted[i] = false;
            }

            _markInputProvider.ClearColors();
            _domainFacade.ClearAllColors();
            _candidatesMarkProvider.ClearColors();
            OnChanged?.Invoke();
        }

        public void SetTitle(string key, params object[] args)
        {
            Title = string.Format(Loc.Keys[key], args);
        }

        public void SetDescription(string key, params object[] args)
        {
            try
            {
                Description = string.Format(Loc.Keys[key], args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(key);
                Console.WriteLine(ex.ToString());
            }
        }

        public void HighlightRow(Position position) => IsRowHighlighted[position.y] = true;
        public void HighlightRow(int y) => IsRowHighlighted[y] = true;
        public void HighlightCol(Position position) => IsColHighlighted[position.x] = true;
        public void HighlightCol(int x) => IsColHighlighted[x] = true;
        public void HighlightBlock(Position position) => HighlightBlock(position.block);

        public void HighlightBlock(int block) => IsBlockHighlighted[block] = true;

        public void HighlightHouse(Position position, House house)
        {
            switch (house)
            {
                case House.None:
                    return;
                case House.Row:
                    HighlightRow(position);
                    return;
                case House.Col:
                    HighlightCol(position);
                    return;
                case House.Block:
                    HighlightBlock(position);
                    return;
            }
        }

        public void HighlightHouses(Position position, IEnumerable<House> houses)
        {
            foreach (var house in houses)
            {
                HighlightHouse(position, house);
            }
        }

        public void MarkCell(Color color, Position position) => _domainFacade.SetColor(position, color);

        public void MarkCells(Color color, IEnumerable<Position> positions)
        {
            foreach (var position in positions)
            {
                MarkCell(color, position);
            }
        }

        public void MarkCandidate(Color color, Position position, Value value)
        {
            _candidatesMarkProvider.SetColor(position, value, color);
        }

        public void MarkCandidates(Color color, IEnumerable<Position> positions, Value value)
        {
            foreach (var position in positions)
            {
                MarkCandidate(color, position, value);
            }
        }

        public void MarkInput(Color color, Position position) => _markInputProvider.SetColor(position, color);


        public void Mark(Color color, Position position, Value value)
        {
            MarkCell(color, position);
            MarkCandidate(color, position, value);
        }

        public void Mark(Color color, IEnumerable<Position> positions, Value value)
        {
            MarkCells(color, positions);
            MarkCandidates(color, positions, value);
        }

        public void Mark(Color color, IEnumerable<Position> positions, IEnumerable<Value> values)
        {
            foreach (var value in values)
            {
                Mark(color, positions, value);
            }
        }

        public void MarkIfHasCandidate(Color color, IEnumerable<Position> positions, Value value)
        {
            Mark(color, positions.Where(pos => _informer.HasCandidate(pos, value)), value);
        }

        public void MarkIfHasCandidates(Color color, IEnumerable<Position> positions, IEnumerable<Value> values)
        {
            foreach (var value in values)
            {
                MarkIfHasCandidate(color, positions, value);
            }
        }

        public void MarkIfInputEquals(Color color, IEnumerable<Position> positions, Value value)
        {
            foreach (var position in positions)
            {
                if (_informer.GetValue(position) == value)
                {
                    MarkInput(color, position);
                }
            }
        }

        public void MarkInputOrCandidate(Color color, IEnumerable<Position> positions, Value candidate)
        {
            foreach (var pos in positions)
            {
                if (!_informer.HasValue(pos))
                {
                    MarkCandidate(color, pos, candidate);
                }
                else
                {
                    MarkInput(color, pos);
                }
            }
        }

        public void SetValueFilter(Value input)
        {
            _ = _numpadMenuBuilder.SelectValue(input).Execute();
        }

        public string Format(House house)
        {
            return house switch
            {
                House.None => Loc.Keys["hints__house--none"],
                House.Row => Loc.Keys["hints__house--row"],
                House.Col => Loc.Keys["hints__house--column"],
                House.Block => Loc.Keys["hints__house--block"],
                _ => throw new ArgumentException("House not supported by HintHelper.Format")
            };
        }

        public string Format(House house, Position position)
        {
            return house switch
            {
                House.Row => $"{Loc.Keys["hints__house-formatted--row"]}{position.y + 1}",
                House.Col => $"{Loc.Keys["hints__house-formatted--col"]}{position.x + 1}",
                House.Block => $"{Loc.Keys["hints__house-formatted--block"]}{position.block + 1}",
                _ => $"{Loc.Keys["hints__house-formatted--none"]}"
            };
        }

        public string Format(IEnumerable<House> houses, Position pos)
        {
            return string.Join(Loc.Keys["hints__houses-formatted--seperator"], houses.Select(house => Format(house, pos)));
        }

        public void Show()
        {
            IsVisible = true;
            OnChanged?.Invoke();
        }

        public void Hide()
        {
            Clear();
            IsVisible = false;
            OnChanged?.Invoke();
        }
    }
}