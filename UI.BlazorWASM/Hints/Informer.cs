using Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    /// <summary>
    /// Passes user readonly data to determine if ISolvingTechnique can execute.
    /// </summary>
    public class Informer
    {
        private readonly IGridProvider _gridProvider;
        private readonly SudokuProvider _sudokuProvider;

        public Informer(IGridProvider gridProvider, SudokuProvider sudokuProvider)
        {
            _gridProvider = gridProvider;
            _sudokuProvider = sudokuProvider;
        }

        public bool HasValue(Position position) => _gridProvider.HasValue(position.X, position.Y);
        public bool HasCandidate(Position position, InputValue value) => _gridProvider.HasCandidate(position.X, position.Y, value);
        public InputValue GetSolution(Position position) => _sudokuProvider.GetSolution(position.X, position.Y);

        public int GetCandidatesCount(Position position) => _gridProvider.GetCandidatesCount(position.X, position.Y);

        public House FindHouse(Position position, Predicate<IEnumerable<Position>> predicate)
        {
            foreach( var house in new[]{ House.Row, House.Col, House.Block} )
            {
                if (predicate(GetPositionsInHouse(position, house)))
                {
                    return house;
                }
            }
            return House.None;
        }

        public IEnumerable<Position> GetPositionsInHouse(Position position, House house)
        {
            return house switch
            {
                House.None => Enumerable.Empty<Position>(),
                House.Row => GridHelper.GetIndexesFromRow(position.Y),
                House.Col => GridHelper.GetIndexesFromCol(position.X),
                House.Block => GridHelper.GetIndexesFromBlock(position.X, position.Y),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
