using Core.Data;
using System.Collections.Generic;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints
{
    /// <summary>
    /// Contains methods for displaying user information about ISolvingTechnique.
    /// </summary>
    public class Displayer
    {
        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Desccription { get; set; }

        public void Reset() { }
        public void Show() { IsVisible = true; }
        public void Hide() { IsVisible = false; }
        public void HighlightRow(Position position) { }
        public void HighlightCol(Position position) { }
        public void HighlightBlock(Position position) { }

        public void Mark(Color color, IEnumerable<Position> positions) { }
        public void Mark(Color color, Position position) { }
        public void Mark(Color color, Position position, InputValue value) { }

        public void SetTitle(string text) { Title = text; }
        public void SetDescription(string text) { Desccription = text; }
    }
}
