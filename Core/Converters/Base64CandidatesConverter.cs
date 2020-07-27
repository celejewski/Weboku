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
            catch( Exception ex )
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
            foreach( var pos in Position.All )
            {
                foreach( var bit in CellToBools(grid, pos) )
                {
                    yield return bit;
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
        private IEnumerable<bool> CellToBools(IGrid grid, Position pos)
        {
            var isGiven = grid.GetIsGiven(pos);
            var hasInput = grid.GetValue(pos) != InputValue.None;
            var input = grid.GetValue(pos);
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

                foreach( var value in InputValue.NonEmpty )
                {
                    yield return grid.HasCandidate(pos, value);
                }
            }
        }
        private IEnumerable<bool> ValueToBools(InputValue input)
        {
            var binary = Convert.ToString(input - 1, 2).PadLeft(4, '0');
            foreach( var digit in binary )
            {
                yield return digit == '1';
            }
        }

        private Grid BitArrayToGrid(BitArray bitArray)
        {
            _counter = 0;
            var grid = new Grid();

            foreach( var pos in Position.All )
            {
                SetValue(grid, bitArray, pos);
            }
            return grid;
        }

        private int _counter = 0;
        private void SetValue(Grid grid, BitArray bitArray, Position pos)
        {
            // IsGiven?
            if( bitArray.Get(_counter++) )
            {
                grid.SetIsGiven(pos, true);
                grid.SetValue(pos, GetValue(bitArray));
            }
            // IsInput?
            else if( bitArray.Get(_counter++) )
            {
                grid.SetIsGiven(pos, false);
                grid.SetValue(pos, GetValue(bitArray));
            }
            else
            {
                grid.SetIsGiven(pos, false);
                grid.SetValue(pos, InputValue.None);

                foreach( var value in InputValue.NonEmpty )
                {
                    if( bitArray.Get(_counter++) )
                    {
                        grid.AddCandidate(pos, value);
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

            return Convert.ToInt32(sb.ToString(), 2) + 1;
        }
    }
}
