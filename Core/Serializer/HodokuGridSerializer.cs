using Core.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Serializer
{
    internal class HodokuGridSerializer : IGridSerializer
    {
        public IGrid Deserialize(string input)
        {
            var givens = input.Replace('.', '0');
            var grid = new Grid();
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

        public bool IsValidFormat(string text)
        {
            return !string.IsNullOrEmpty(text)
                && text.Length == 81
                && !Regex.IsMatch(text.Replace('.', '0'), @"[^\d]");
        }

        public string Serialize(IGrid grid)
        {
            return string.Concat(Position.All.Select(pos => grid.GetValue(pos)));
        }
    }
}
