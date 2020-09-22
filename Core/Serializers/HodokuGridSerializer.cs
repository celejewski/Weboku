using Core.Data;
using Core.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Serializers
{
    internal class HodokuGridSerializer : IGridSerializer
    {
        public IGrid Deserialize(string input)
        {
            try
            {
                var givens = input.Replace('.', '0');
                var grid = new Grid();
                foreach( var pos in Position.Positions )
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
            catch( Exception ex )
            {
                throw new GridSerializationException($"Exception in {nameof(HodokuGridSerializer)} occured during {nameof(Deserialize)} with value \"{input}\" ", ex);
            }
        }

        public bool IsValidFormat(string text)
        {
            return !string.IsNullOrEmpty(text)
                && text.Length == 81
                && !Regex.IsMatch(text.Replace('.', '0'), @"[^\d]");
        }

        public string Serialize(IGrid grid)
        {
            return string.Concat(Position.Positions.Select(pos => grid.GetValue(pos)));
        }
    }
}
