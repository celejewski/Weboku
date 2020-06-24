using Core.Data;
using Core.Generators;
using System.Text;
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
            input = input.Replace('.', '0');
            var grid = _generator.Empty();
            foreach( var pos in Position.All )
            {
                var value = int.Parse(input[pos.y * 9 + pos.x].ToString());
                if( value != 0 )
                {
                    grid.SetIsGiven(pos, true);
                    grid.SetValue(pos, value);
                }
                else
                {
                    grid.SetValue(pos, InputValue.Empty);
                }
            }
            return grid;
        }

        public bool IsValidText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            text = text.Replace('.', '0');
            return text.Length == 81
                && !Regex.IsMatch(text, @"[^\d]");
        }

        public string ToText(IGrid grid)
        {
            var sb = new StringBuilder();
            foreach( var pos in Position.All )
            {
                sb.Append(grid.GetValue(pos));
            }
            return sb.ToString();
        }


    }
}
