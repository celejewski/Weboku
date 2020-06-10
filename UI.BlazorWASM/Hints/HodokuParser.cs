using System;
using System.Collections;
using System.Collections.Generic;
using UI.BlazorWASM.Hints.SolvingTechniques;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    public class HodokuParser
    {
        private readonly ISudokuProvider _sudokuProvider;

        public HodokuParser(ISudokuProvider sudokuProvider)
        {
            _sudokuProvider = sudokuProvider;
        }
        public ISolvingTechnique GetNextTechnique(IEnumerable<string> steps)
        {
            Func<string, ISolvingTechnique>[] techniques =
            {
                NakedSingle,
                HiddenSingle,
                FullHouse,
            };

            foreach( var step in steps )
            {
                foreach( var technique in techniques )
                {
                    var result = technique(step);
                    if (result != null && result.CanExecute(_sudokuProvider))
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        public ISolvingTechnique NakedSingle(string step)
        {
            //Naked Single: r1c6=6
            if( !step.Contains("Naked Single"))
            {
                return null;
            }

            // r1c6=6
            var cell = step.Substring(step.Length - 6, 6);
            var (x, y) = HintsHelper.GetCoords(cell);
            var value = HintsHelper.GetDigit(cell, 5);
            return new NakedSingle(x, y, value);
        }

        public ISolvingTechnique HiddenSingle(string step)
        {
            //Naked Single: r1c6=6
            if( !step.Contains("Hidden Single") )
            {
                return null;
            }

            // r1c6=6
            var cell = step.Substring(step.Length - 6, 6);
            var (x, y) = HintsHelper.GetCoords(cell);
            var value = HintsHelper.GetDigit(cell, 5);
            return new HiddenSingle(x, y, value);

        }

        public ISolvingTechnique FullHouse(string step)
        {

            //Naked Single: r1c6=6
            if( !step.Contains("Full House") )
            {
                return null;
            }

            // r1c6=6
            var cell = step.Substring(step.Length - 6, 6);
            var (x, y) = HintsHelper.GetCoords(cell);
            var value = HintsHelper.GetDigit(cell, 5);
            return new FullHouse(x, y, value);
        }
    }
}
