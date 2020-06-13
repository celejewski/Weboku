using Core.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Converters
{
    class HodokuGridConverterV2
    {
        public HodokuGridConverterV2()
        {
        }

        public IGridV2 FromText(string input)
        {
            input = input.Replace('.', '0');
            IGridV2 grid = new GridV2();
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var value = int.Parse(input[y * 9 + x].ToString());
                    if( value != 0 )
                    {
                        grid.SetIsGiven(x, y, true);
                        grid.SetValue(x, y, (InputValue) value);
                    }
                    else
                    {
                        grid.SetValue(x, y, 0);
                    }
                }
            }
            return grid;
        }

        public bool IsValidText(string text)
        {
            text = text.Replace('.', '0');
            return text.Length == 81
                && !Regex.IsMatch(text, @"[^\d]");
        }

        public string ToText(IGridV2 grid)
        {
            var sb = new StringBuilder();
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    sb.Append((int) grid.GetValue(x, y));
                }
            }
            return sb.ToString();
        }



    }
}
