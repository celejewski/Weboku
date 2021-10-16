using System;
using System.Linq;
using System.Text.RegularExpressions;
using Weboku.Core.Data;
using Weboku.Core.Exceptions;

namespace Weboku.Core.Serializers
{
    public class HodokuGridSerializer : IGridSerializer
    {
        public Grid Deserialize(string input)
        {
            try
            {
                var givens = input.Replace('.', '0');
                var grid = new Grid();
                foreach (var pos in Position.Positions)
                {
                    var value = int.Parse(givens.Substring(pos.Y * 9 + pos.X, 1));
                    if (value != 0)
                    {
                        grid.SetValue(pos, value);
                        grid.SetIsGiven(pos, true);
                    }
                }

                return grid;
            }
            catch (Exception ex)
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

        public string Serialize(Grid grid)
        {
            return string.Concat(Position.Positions.Select(pos => grid.GetValue(pos)));
        }
    }
}