using Core.Data;
using System.Collections.Generic;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Component;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    /// <summary>
    /// Contains methods for displaying user information about ISolvingTechnique.
    /// </summary>
    public class Displayer
    {
        private readonly CellColorProvider _cellColorProvider;
        private readonly CandidatesMarkProvider _candidatesMarkProvider;

        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Desccription { get; set; }

        public Displayer(
            CellColorProvider cellColorProvider,
            CandidatesMarkProvider candidatesMarkProvider)
        {
            _cellColorProvider = cellColorProvider;
            _candidatesMarkProvider = candidatesMarkProvider;
        }

        public void Reset() 
        {
            _cellColorProvider.ClearAll();
            _candidatesMarkProvider.ClearColors();
        }
        public void Show() 
        { 
            IsVisible = true; 
        }
        public void Hide() 
        {
            IsVisible = false;
            Reset();
        }
        public void HighlightRow(Position position) { }
        public void HighlightCol(Position position) { }
        public void HighlightBlock(Position position) { }

        public void Mark(Color color, IEnumerable<Position> positions) { }
        public void Mark(Color color, Position position) 
        {
            _cellColorProvider.SetColor(position.X, position.Y, color);
        }
        public void Mark(Color color, Position position, InputValue value) 
        {
            _candidatesMarkProvider.SetColor(position.X, position.Y, (int) value, color);
        }

        public void SetTitle(string text) { Title = text; }
        public void SetDescription(string text) { Desccription = text; }
    }
}
