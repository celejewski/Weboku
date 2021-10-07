using AKSoftware.Localization.MultiLanguages;
using System;
using System.Collections.Generic;
using System.Linq;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ILanguageContainerService LanguageContainerService { get; }

        public bool[] IsRowHighlighted = new bool[9];
        public bool[] IsColHighlighted = new bool[9];
        public bool[] IsBlockHighlighted = new bool[9];

        public event Action OnHintDisplayerChanged;


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

            ClearInputColors();
            ClearAllCellColors();
            ClearCandidatesColors();
            OnHintDisplayerChanged?.Invoke();
        }

        public void SetTitle(string key, params object[] args)
        {
            Title = string.Format(LanguageContainerService.Keys[key], args);
        }

        public void SetDescription(string key, params object[] args)
        {
            try
            {
                Description = string.Format(LanguageContainerService.Keys[key], args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(key);
                Console.WriteLine(ex.ToString());
            }
        }

        public void HighlightRow(Position position) => IsRowHighlighted[position.y] = true;
        public void HighlightCol(Position position) => IsColHighlighted[position.x] = true;
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

        public void MarkCell(Color color, Position position) => SetCellColor(position, color);

        public void MarkCells(Color color, IEnumerable<Position> positions)
        {
            foreach (var position in positions)
            {
                MarkCell(color, position);
            }
        }

        public void MarkCandidate(Color color, Position position, Value value)
        {
            SetCandidateColor(position, value, color);
        }

        public void MarkCandidates(Color color, IEnumerable<Position> positions, Value value)
        {
            foreach (var position in positions)
            {
                MarkCandidate(color, position, value);
            }
        }

        public void MarkInput(Color color, Position position) => SetInputColor(position, color);


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
            Mark(color, positions.Where(pos => HasCandidate(pos, value)), value);
        }

        public void MarkIfHasCandidates(Color color, IEnumerable<Position> positions, IEnumerable<Value> values)
        {
            foreach (var value in values)
            {
                MarkIfHasCandidate(color, positions, value);
            }
        }

        public void MarkInputOrCandidate(Color color, IEnumerable<Position> positions, Value candidate)
        {
            foreach (var pos in positions)
            {
                if (!HasValue(pos))
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
            SelectValue(input);
        }

        public string Format(House house, Position position)
        {
            return house switch
            {
                House.Row => $"{LanguageContainerService.Keys["hints__house-formatted--row"]}{position.y + 1}",
                House.Col => $"{LanguageContainerService.Keys["hints__house-formatted--col"]}{position.x + 1}",
                House.Block => $"{LanguageContainerService.Keys["hints__house-formatted--block"]}{position.block + 1}",
                _ => $"{LanguageContainerService.Keys["hints__house-formatted--none"]}"
            };
        }

        public string Format(IEnumerable<House> houses, Position pos)
        {
            var separator = LanguageContainerService.Keys["hints__houses-formatted--seperator"];
            var housesAsString = houses.Select(house => Format(house, pos));
            return string.Join(separator, housesAsString);
        }

        public void Show()
        {
            IsVisible = true;
            OnHintDisplayerChanged?.Invoke();
        }

        public void Hide()
        {
            Clear();
            IsVisible = false;
            OnHintDisplayerChanged?.Invoke();
        }
    }
}