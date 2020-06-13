using System;
using System.Collections;
using System.Collections.Generic;
using UI.BlazorWASM.Hints.SolvingTechniques;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    public class HodokuParser
    {
        public IEnumerable<ISolvingTechnique> GetSolvingTechniques(IEnumerable<string> steps)
        {
            Func<string, ISolvingTechnique>[] techniques =
            {
                NakedSingle,
                HiddenSingle, 
                FullHouse,
                Unknown,
            };

            foreach( var step in steps )
            {
                foreach( var technique in techniques )
                {
                    var result = technique(step);
                    if (result != null)
                    {
                        yield return result;
                    }
                }
            }
        }

        public ISolvingTechnique Unknown(string step)
        {
            Console.WriteLine($"Unknown step: {step}");
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
            var pos = HintsHelper.GetCoords(cell);
            var value = HintsHelper.GetValue(cell, 5);
            return new NakedSingle(pos, value);
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
            var position = HintsHelper.GetCoords(cell);
            var value = HintsHelper.GetValue(cell, 5);
            return new HiddenSingle(position, value);

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
            var position = HintsHelper.GetCoords(cell);
            var value = HintsHelper.GetValue(cell, 5);
            return new FullHouse(position, value);
        }
    }
}
