using Core.Data;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Converters
{
    public class Base64CandidatesConverter : IGridConverter
    {
        public IGrid FromText(string text)
        {
            var bytes = WebEncoders.Base64UrlDecode(text);
            var bitArray = new BitArray(bytes);

            return BitArrayToGrid(bitArray);
        }

        public bool IsValidText(string text)
        {
            try
            {
                FromText(text);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public string ToText(IGrid grid)
        {
            var bools = GridToBools(grid);
            var bitArray = new BitArray(bools.ToArray());
            var bytes = new byte[bitArray.Length / 8 + 1];
            bitArray.CopyTo(bytes, 0);

            return WebEncoders.Base64UrlEncode(bytes);
        }

        private IEnumerable<bool> GridToBools(IGrid grid)
        {
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    foreach( var bit in CellToBools(grid, x, y) )
                    {
                        yield return bit;
                    }
                }
            }
        }

        /// <summary>
        /// IsGiven == true => true + 4 bits
        /// IsInput == true => false + true + 4 bits
        /// default         => false + false + 9 bits
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private IEnumerable<bool> CellToBools(IGrid grid, int x, int y)
        {

            var isGiven = grid.GetIsGiven(x, y);
            var hasInput = grid.GetValue(x, y) != InputValue.Empty;
            var input = grid.GetValue(x, y);
            if( isGiven )
            {
                yield return true;
                foreach( var bit in ValueToBools(input) )
                {
                    yield return bit;
                }
            }
            else if( hasInput )
            {
                yield return false;
                yield return true;
                foreach( var bit in ValueToBools(input) )
                {
                    yield return bit;
                }
            }
            else
            {
                yield return false;
                yield return false;
                for( int value = 0; value < 9; value++ )
                {
                    yield return grid.HasCandidate(x, y, (InputValue) value + 1);
                }
            }
        }
        private IEnumerable<bool> ValueToBools(InputValue input)
        {
            var binary = Convert.ToString((int) input - 1, 2).PadLeft(4, '0');
            foreach( var digit in binary )
            {
                yield return digit == '1';
            }
        }

        private Grid BitArrayToGrid(BitArray bitArray)
        {
            _counter = 0;
            var grid = new Grid();

            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    SetValue(grid, bitArray, x, y);
                }
            }
            return grid;
        }

        int _counter = 0;
        private void SetValue(Grid grid, BitArray bitArray, int x, int y)
        {
            // IsGiven?
            if( bitArray.Get(_counter++) )
            {
                grid.SetIsGiven(x, y, true);
                grid.SetValue(x, y, GetValue(bitArray));
            }
            // IsInput?
            else if( bitArray.Get(_counter++) )
            {
                grid.SetIsGiven(x, y, false);
                grid.SetValue(x, y, GetValue(bitArray));
            }
            else
            {
                grid.SetIsGiven(x, y, false);
                grid.SetValue(x, y, InputValue.Empty);

                for( int i = 0; i < 9; i++ )
                {
                    if( bitArray.Get(_counter++) )
                    {
                        grid.AddCandidate(x, y, (InputValue) i + 1);
                    }
                }
            }

        }

        private InputValue GetValue(BitArray bitArray)
        {
            var sb = new StringBuilder();
            for( int i = 0; i < 4; i++ )
            {
                sb.Append(bitArray.Get(_counter++) ? "1" : "0");
            }

            var value = (InputValue) (Convert.ToInt32(sb.ToString(), 2) + 1);
            return value;
        }
    }
}
