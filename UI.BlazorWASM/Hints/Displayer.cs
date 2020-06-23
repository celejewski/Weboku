using AKSoftware.Localization.MultiLanguages;
using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Hints
{
    /// <summary>
    /// Contains methods for displaying user information about ISolvingTechnique.
    /// </summary>
    public class Displayer : IProvider
    {
        private readonly CellColorProvider _cellColorProvider;
        private readonly CandidatesMarkProvider _candidatesMarkProvider;
        private readonly NumpadMenuBuilder _numpadMenuBuilder;
        private readonly Informer _informer;
        private readonly MarkInputProvider _markInputProvider;

        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ILanguageContainerService Loc { get; }

        public bool[] IsRowHighlighted = new bool[9];
        public bool[] IsColHighlighted = new bool[9];
        public bool[] IsBlockHighlighted = new bool[9];

        public event Action OnChanged;

        public Displayer(
            CellColorProvider cellColorProvider,
            CandidatesMarkProvider candidatesMarkProvider,
            NumpadMenuBuilder numpadMenuBuilder,
            Informer informer,
            MarkInputProvider markInputProvider,
            ILanguageContainerService loc)
        {
            _cellColorProvider = cellColorProvider;
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
            for( int i = 0; i < 9; i++ )
            {
                IsRowHighlighted[i] = false;
                IsColHighlighted[i] = false;
                IsBlockHighlighted[i] = false;
            }
            _markInputProvider.ClearColors();
            _cellColorProvider.ClearAll();
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

        public void HighlightRow(Position position) => IsRowHighlighted[position.Y] = true;
        public void HighlightCol(Position position) => IsColHighlighted[position.X] = true;
        public void HighlightBlock(Position position) => HighlightBlock(position.Block);

        public void HighlightBlock(int block) => IsBlockHighlighted[block] = true;

        public void HighlightHouse(Position position, House house)
        {
            switch( house )
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
            foreach( var house in houses )
            {
                HighlightHouse(position, house);
            }
        }

        public void MarkCell(Color color, Position position) => _cellColorProvider.SetColor(position.X, position.Y, color);
        public void MarkCells(Color color, IEnumerable<Position> positions)
        {
            foreach( var position in positions )
            {
                MarkCell(color, position);
            }
        }
        public void MarkCandidate(Color color, Position position, InputValue value)
        {
            _candidatesMarkProvider.SetColor(position.X, position.Y, (int) value, color);
        }

        public void MarkCandidates(Color color, IEnumerable<Position> positions, InputValue inputValue)
        {
            foreach( var position in positions )
            {
                MarkCandidate(color, position, inputValue);
            }
        }

        public void MarkInput(Color color, Position position) => _markInputProvider.SetColor(position, color);


        public void Mark(Color color, Position position, InputValue value)
        {
            MarkCell(color, position);
            MarkCandidate(color, position, value);
        }

        public void Mark(Color color, IEnumerable<Position> positions, InputValue value)
        {
            MarkCells(color, positions);
            MarkCandidates(color, positions, value);
        }

        public void Mark(Color color, IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            foreach( var value in values )
            {
                Mark(color, positions, value);
            }
        }

        public void MarkIfHasCandidate(Color color, IEnumerable<Position> positions, InputValue value)
        {
            Mark(color, positions.Where(pos => _informer.HasCandidate(pos, value)), value);
        }

        public void MarkIfHasCandidates(Color color, IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            foreach( var value in values )
            {
                MarkIfHasCandidate(color, positions, value);
            }
        }

        public void MarkIfInputEquals(Color color, IEnumerable<Position> positions, InputValue value)
        {
            foreach( var position in positions )
            {
                if (_informer.GetValue(position) == value)
                {
                    MarkInput(color, position);
                }
            }
        }

        public void MarkInputOrCandidate(Color color, IEnumerable<Position> positions, InputValue candidate)
        {
            foreach( var pos in positions )
            {
                if( !_informer.HasValue(pos) )
                {
                    MarkCandidate(color, pos, candidate);
                }
                else
                {
                    MarkInput(color, pos);
                }
            }
        }

        public void SetValueFilter(InputValue input)
        {
            _ = _numpadMenuBuilder.SelectValue((int) input).Execute();
        }
        public static string Format(House house)
        {
            return house switch
            {
                House.None => "none",
                House.Row => "row",
                House.Col => "column",
                House.Block => "block",
                _ => throw new ArgumentException("House not supported by HintHelper.Format")
            };
        }

        public static string Format(House house, Position position)
        {
            return house switch
            {
                House.Row => $"row {position.Y + 1}",
                House.Col => $"column {position.X + 1}",
                House.Block => $"block {position.Block + 1}",
                _ => "none"
            };
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
