﻿using Core.Data;
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
        private readonly CommandProvider _commandProvider;
        private readonly NumpadMenuBuilder _numpadMenuBuilder;
        private readonly Informer _informer;

        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Desccription { get; set; }
        public bool[] IsRowHighlighted = new bool[9];
        public bool[] IsColHighlighted = new bool[9];
        public bool[] IsBlockHighlighted = new bool[9];

        public event Action OnChanged;

        public Displayer(
            CellColorProvider cellColorProvider,
            CandidatesMarkProvider candidatesMarkProvider,
            CommandProvider commandProvider,
            NumpadMenuBuilder numpadMenuBuilder,
            Informer informer)
        {
            _cellColorProvider = cellColorProvider;
            _candidatesMarkProvider = candidatesMarkProvider;
            _commandProvider = commandProvider;
            _numpadMenuBuilder = numpadMenuBuilder;
            _informer = informer;
        }

        public void Reset()
        {
            Title = string.Empty;
            Desccription = string.Empty;
            for( int i = 0; i < 9; i++ )
            {
                IsRowHighlighted[i] = false;
                IsColHighlighted[i] = false;
                IsBlockHighlighted[i] = false;
            }
            _cellColorProvider.ClearAll();
            _candidatesMarkProvider.ClearColors();
        }
        public void Show()
        {
            IsVisible = true;
            OnChanged?.Invoke();
        }
        public void Hide()
        {
            IsVisible = false;
            Reset();
            OnChanged?.Invoke();
        }
        public void HighlightRow(Position position)
        {
            IsRowHighlighted[position.Y] = true;
        }
        public void HighlightCol(Position position)
        {
            IsColHighlighted[position.X] = true;
        }
        public void HighlightBlock(Position position)
        {
            HighlightBlock(position.Block);
        }

        public void HighlightBlock(int block)
        {
            IsBlockHighlighted[block] = true;
        }

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

        public void Mark(Color color, IEnumerable<Position> positions, InputValue value)
        {
            MarkCells(color, positions);
            MarkCandidates(color, positions, value);
        }
        public void MarkIfHasCandidate(Color color, IEnumerable<Position> positions, InputValue value)
        {
            Mark(color, positions.Where(pos => _informer.HasCandidate(pos, value)), value);
        }

        public void MarkCells(Color color, IEnumerable<Position> positions)
        {
            foreach( var position in positions )
            {
                MarkCell(color, position);
            }
        }

        public void MarkCell(Color color, Position position)
        {
            _cellColorProvider.SetColor(position.X, position.Y, color);
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

        public void SetTitle(string text) { Title = text; }
        public void SetDescription(string text) { Desccription = text; }

        public void SetValueFilter(InputValue input)
        {
            _numpadMenuBuilder.SelectValue((int) input).Execute();
        }
    }
}
