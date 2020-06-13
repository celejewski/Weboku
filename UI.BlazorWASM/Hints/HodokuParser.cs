using System;
using System.Collections;
using System.Collections.Generic;
using UI.BlazorWASM.Hints.SolvingTechniques;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    public class HodokuParser
    {
        private readonly IGridProvider _gridProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly SudokuProvider _sudokuProvider;
        private readonly CandidatesMarkProvider _candidatesMarkProvider;
        public HintsProvider HintsProvider { get; set; }
        private readonly HintsHelper _hintsHelper;

        public HodokuParser(IGridProvider gridProvider, CellColorProvider cellColorProvider, SudokuProvider sudokuProvider, CandidatesMarkProvider candidatesMarkProvider, HintsHelper hintsHelper)
        {
            _gridProvider = gridProvider;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
            _candidatesMarkProvider = candidatesMarkProvider;
            _hintsHelper = hintsHelper;
        }
        public ISolvingTechnique GetNextTechnique(IEnumerable<string> steps)
        {
            if( FindInvalidInputs() != null )
            {
                return FindInvalidInputs();
            }

            if( FindCandidates() != null )
            {
                return FindCandidates();
            }

            Func<string, ISolvingTechnique>[] techniques =
            {
                NakedSingle,
                HiddenSingle,
                //FullHouse,
            };

            foreach( var step in steps )
            {
                foreach( var technique in techniques )
                {
                    var result = technique(step);
                    if( result != null && result.CanExecute() )
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
            if( !step.Contains("Naked Single") )
            {
                return null;
            }

            // r1c6=6
            var cell = step.Substring(step.Length - 6, 6);
            var (x, y) = HintsHelper.GetCoords(cell);
            var value = HintsHelper.GetDigit(cell, 5);
            return new NakedSingle(x, y, value, _candidatesMarkProvider, _gridProvider);
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
            return new HiddenSingle(x, y, value, _candidatesMarkProvider, _gridProvider, HintsProvider, _hintsHelper);

        }

        //    public ISolvingTechnique FullHouse(string step)
        //    {

        //        //Naked Single: r1c6=6
        //        if( !step.Contains("Full House") )
        //        {
        //            return null;
        //        }

        //        // r1c6=6
        //        var cell = step.Substring(step.Length - 6, 6);
        //        var (x, y) = HintsHelper.GetCoords(cell);
        //        var value = HintsHelper.GetDigit(cell, 5);
        //        return new FullHouse(x, y, value);
        //    }

        public ISolvingTechnique FindCandidates()
        {
            var technique = new FindCandidates(_sudokuProvider, _gridProvider);
            return technique.CanExecute() ? technique : null;
        }

        public ISolvingTechnique FindInvalidInputs()
        {
            var technique = new FindInvalidInputs(_sudokuProvider, _cellColorProvider, _gridProvider);
            return technique.CanExecute() ? technique : null;
        }
    }
}
