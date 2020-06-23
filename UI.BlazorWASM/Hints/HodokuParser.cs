using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UI.BlazorWASM.Hints.SolvingTechniques;

namespace UI.BlazorWASM.Hints
{
    public class HodokuParser
    {
        public IEnumerable<ISolvingTechnique> GetSolvingTechniques(IEnumerable<string> steps)
        {
            Func<string, ISolvingTechnique>[] techniques =
            {
                SingleOrDefault,
                LockedCandidatesPointingOrDefault,
                LockedCandidatesClaimingOrDefault,
                SubsetOrDefault,
                NotFound,
            };

            foreach( var step in steps )
            {
                foreach( var technique in techniques )
                {
                    var result = technique(step);
                    if( result != null )
                    {
                        yield return result;
                        break;
                    }
                }
            }
        }

        private ISolvingTechnique NotFound(string step)
        {
            Console.WriteLine($"Unknown step: {step}");
            return new NotFound();
        }

        private ISolvingTechnique SingleOrDefault(string step)
        {
            (string name, Type type)[] singles = {
                ("Naked Single", typeof(NakedSingle)),
                ("Hidden Single", typeof(HiddenSingle)),
                ("Full House", typeof(FullHouse)),
            };

            var technique = singles.FirstOrDefault(single => step.Contains(single.name));
            if( technique == default )
            {
                return default;
            }

            var info = step.Substring(step.Length - 6, 6);
            var position = ParsePosition(info);
            var value = ParseValue(info, 5);
            return (ISolvingTechnique) Activator.CreateInstance(technique.type, position, value);
        }

        private ISolvingTechnique LockedCandidatesPointingOrDefault(string step)
        {
            // Locked Candidates Type 1 (Pointing): 8 in b3 => r1c136 <> 8
            // Locked Candidates Type 1 (Pointing): 7 in b3 => r569c7 <> 7
            if( !step.Contains("Locked Candidates Type 1 (Pointing)") )
            {
                return null;
            }

            var info = step.Substring("Locked Candidates Type 1 (Pointing): ".Length);
            var value = ParseValue(info, 0);
            var block = ParseIndex(info, 6);

            var positionsMatch = Regex.Match(info, @"(r[\d]+c[\d]+)");
            if( !positionsMatch.Success )
            {
                return null;
            }
            var positions = GetPositions(positionsMatch.Value);

            return new LockedCandidatesPointing(block, value, positions);
        }

        private ISolvingTechnique LockedCandidatesClaimingOrDefault(string step)
        {
            // Locked Candidates Type 2 (Claiming): 2 in r6 => r4c456,r5c5<>2
            if( !step.Contains("Locked Candidates Type 2 (Claiming)", StringComparison.Ordinal) )
            {
                return null;
            }

            // 2 in r6 => r4c456,r5c5<>2
            var info = step.Substring("Locked Candidates Type 2 (Claiming): ".Length);
            var value = ParseValue(info, 0);
            var house = info[5] == 'r' ? House.Row : House.Col;
            var positionsMatch = Regex.Match(info, @"=> (.*)<>\d");
            if( !positionsMatch.Success )
            {
                return null;
            }
            var positions = GetPositions(positionsMatch.Groups[1].Value);

            return new LockedCandidatesClaiming(value, positions, house);
        }
        private ISolvingTechnique SubsetOrDefault(string step)
        {
            (string name, Type type)[] subsets = {
                ("Locked Pair", typeof(NakedPair)),
                ("Locked Triple", typeof(NakedSubset)),
                ("Locked Quadruple", typeof(NakedSubset)),
                ("Naked Pair", typeof(NakedPair)),
                ("Naked Triple", typeof(NakedSubset)),
                ("Naked Quadruple", typeof(NakedSubset)),
                ("Hidden Pair", typeof(HiddenPair)),
                ("Hidden Triple", typeof(HiddenSubset)),
                ("Hidden Quadruple", typeof(HiddenSubset)),
            };

            var subset = subsets.FirstOrDefault(subset => step.Contains(subset.name));
            if( subset == default )
            {
                return default;
            }

            var match = Regex.Match(step, ": (.*)in (.*) =>");

            var valuesText = match.Groups[1].Value;
            var values = Enumerable.Range(0, valuesText.Length / 2)
                .Select(pos => ParseValue(valuesText, pos * 2));

            var positionsText = match.Groups[2].Value;
            var positions = GetPositions(positionsText);
            return (ISolvingTechnique) Activator.CreateInstance(subset.type, positions, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">For example "r1c6"</param>
        /// <returns>For "r1c6" will return x=5, y=0</returns>
        public static Position ParsePosition(string text)
        {
            var x = ParseDigit(text, 3) - 1;
            var y = ParseDigit(text, 1) - 1;
            return new Position(x, y);
        }

        public static int ParseDigit(string text, int pos)
        {
            return int.Parse(text.Substring(pos, 1));
        }

        public static int ParseDigit(char c)
        {
            return ParseDigit(c.ToString(), 0);
        }

        public static int ParseIndex(string text, int pos)
        {
            return ParseDigit(text, pos) - 1;
        }

        public static int ParseIndex(char c)
        {
            return ParseDigit(c) - 1;
        }

        public static InputValue ParseValue(string text, int pos)
        {
            return (InputValue) ParseDigit(text, pos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">"r1c136,r569c7"</param>
        /// <returns></returns>
        public static IEnumerable<Position> GetPositions(string text)
        {
            foreach( var input in text.Split(',') )
            {
                // r12c136
                var splited = Regex.Match(input, @"r(\d+)c(\d+)");
                var rows = splited.Groups[1].Value.Select(t => ParseIndex(t));
                var cols = splited.Groups[2].Value.Select(t => ParseIndex(t));

                foreach( var col in cols )
                {
                    foreach( var row in rows )
                    {
                        yield return new Position(col, row);
                    }
                }
            }
        }
    }
}
