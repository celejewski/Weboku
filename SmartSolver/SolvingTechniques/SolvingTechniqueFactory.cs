using Core.Data;
using System;
using System.Collections.Generic;

namespace SmartSolver.SolvingTechniques
{
    public class SolvingTechniqueFactory : ISolvingTechniquesFactory
    {
        public ISolvingTechnique FullHouse(Position pos, InputValue value)
        {
            return new FullHouse(pos, value);
        }

        public ISolvingTechnique HiddenPair(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            return new HiddenPair(positions, values);
        }

        public ISolvingTechnique HiddenSingle(Position pos, InputValue value)
        {
            return new HiddenSingle(pos, value);
        }

        public ISolvingTechnique HiddenSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            return new HiddenSubset(positions, values);
        }

        public ISolvingTechnique LockedCandidatesClaiming(InputValue inputValue, IEnumerable<Position> positionsToRemoveCandidate, House house)
        {
            return new LockedCandidatesClaiming(inputValue, positionsToRemoveCandidate, house);
        }

        public ISolvingTechnique LockedCandidatesPointing(int block, InputValue inputValue, IEnumerable<Position> positionToRemoveFrom)
        {
            return new LockedCandidatesPointing(block, inputValue, positionToRemoveFrom);
        }

        public ISolvingTechnique NakedPair(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            return new NakedPair(positions, values);
        }

        public ISolvingTechnique NakedSingle(Position pos, InputValue value)
        {
            return new NakedSingle(pos, value);
        }

        public ISolvingTechnique NakedSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            throw new NotImplementedException();
        }

        public ISolvingTechnique Skyscrapper(Position base1, Position base2, Position pos1, Position pos2, InputValue value)
        {
            return new Skyscrapper(base1, base2, pos1, pos2, value);
        }

        public ISolvingTechnique XWing()
        {
            throw new NotImplementedException();
        }

        public ISolvingTechnique XYWing()
        {
            throw new NotImplementedException();
        }
    }
}
