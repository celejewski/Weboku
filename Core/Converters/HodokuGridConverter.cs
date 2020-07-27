﻿using Core.Data;
using Core.Generators;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Converters
{
    public class HodokuGridConverter : IGridConverter
    {
        private readonly IEmptyGridGenerator _generator;

        public HodokuGridConverter(IEmptyGridGenerator generator)
        {
            _generator = generator;
        }

        public IGrid FromText(string input)
        {
            var givens = input.Replace('.', '0');
            var grid = _generator.Empty();
            foreach( var pos in Position.All )
            {
                var value = int.Parse(givens.Substring(pos.y * 9 + pos.x, 1));
                if( value != 0 )
                {
                    grid.SetIsGiven(pos, true);
                    grid.SetValue(pos, value);
                }
            }
            return grid;
        }

        public bool IsValidText(string text)
        {
            return !string.IsNullOrEmpty(text)
                && text.Length == 81
                && !Regex.IsMatch(text.Replace('.', '0'), @"[^\d]");
        }

        public string ToText(IGrid grid)
        {
            return string.Concat(Position.All.Select(pos => grid.GetValue(pos)));
        }
    }
}
