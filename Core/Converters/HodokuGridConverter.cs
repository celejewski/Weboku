using Core.Data;
using Core.Generators;
using System.Text;

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
            var grid = _generator.Empty();
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var value = int.Parse(input[y * 9 + x].ToString());
                    if( value != 0 )
                    { 
                        grid.SetGiven(x, y, value);
                    }
                    else
                    {
                        grid.SetValue(x, y, 0);
                    }
                }
            }
            return grid;
        }

        public string ToText(IGrid grid)
        {
            var sb = new StringBuilder();
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    var cell = grid.Cells[x, y];
                    sb.Append(cell.Input.Value);
                }
            }
            return sb.ToString();
        }
    }
}
