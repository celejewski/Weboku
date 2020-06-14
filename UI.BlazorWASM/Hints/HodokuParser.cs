using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using UI.BlazorWASM.Helpers;
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
                LockedCandidatesPointing,
                LockedCandidatesClaiming,
                LockedPair,
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
                        break;
                    }
                }
            }
        }

        public ISolvingTechnique Unknown(string step)
        {
            Console.WriteLine($"Unknown step: {step}");
            return new NotFound();
        }

        public ISolvingTechnique NakedSingle(string step)
        {
            //Naked Single: r1c6=6
            if( !step.Contains("Naked Single") )
            {
                return null;
            }

            // r1c6=6
            var info = step.Substring(step.Length - 6, 6);
            var pos = HintsHelper.GetPosition(info);
            var value = HintsHelper.GetValue(info, 5);
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
            var position = HintsHelper.GetPosition(cell);
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
            var position = HintsHelper.GetPosition(cell);
            var value = HintsHelper.GetValue(cell, 5);
            return new FullHouse(position, value);
        }

        public ISolvingTechnique LockedCandidatesPointing(string step)
        {
            // Locked Candidates Type 1 (Pointing): 8 in b3 => r1c136 <> 8
            // Locked Candidates Type 1 (Pointing): 7 in b3 => r569c7 <> 7
            if (!step.Contains("Locked Candidates Type 1 (Pointing)"))
            {
                return null;
            }

            var info = step.Substring("Locked Candidates Type 1 (Pointing): ".Length);
            var value = HintsHelper.GetValue(info, 0);
            var block = HintsHelper.GetIndex(info, 6);

            var positionsMatch = Regex.Match(info, @"(r[\d]+c[\d]+)"); 
            if (!positionsMatch.Success)
            {
                return null;
            }
            var positions = HintsHelper.GetPositions(positionsMatch.Value);

            return new LockedCandidatesPointing(block, value, positions);
        }

        public ISolvingTechnique LockedCandidatesClaiming(string step)
        {
            // Locked Candidates Type 2 (Claiming): 2 in r6 => r4c456,r5c5<>2
            if (!step.Contains("Locked Candidates Type 2 (Claiming)", StringComparison.Ordinal))
            {
                return null;
            }

            // 2 in r6 => r4c456,r5c5<>2
            var info = step.Substring("Locked Candidates Type 2 (Claiming): ".Length);
            var value = HintsHelper.GetValue(info, 0);
            var house = info[5] == 'r' ? House.Row : House.Col;
            var positionsMatch = Regex.Match(info, @"=> (.*)<>\d");
            if (!positionsMatch.Success)
            {
                return null;
            }
            var positions = HintsHelper.GetPositions(positionsMatch.Groups[1].Value);

            return new LockedCandidatesClaiming(value, positions, house);
        }

        public ISolvingTechnique LockedPair(string step)
        {
            // Locked Pair: 2,3 in r46c3 => r159c3,r46c1,r56c2 <> 3, r6c2,r79c3 <> 2
            // Locked Pair: 6,9 in r8c13 => r8c456,r9c3<>9, r7c1,r8c56<>6
            if( !step.Contains("Locked Pair: ") )
            {
                return null;
            }

            Console.WriteLine(step);
            var info = step.Substring("Locked Pair: ".Length);
            var value1 = HintsHelper.GetValue(info, 0);
            var value2 = HintsHelper.GetValue(info, 2);

            var positions = HintsHelper.GetPositions(info.Substring(7, 5)).ToList();

            return new LockedPair(positions[0], positions[1], value1, value2);
        }
    }
}
