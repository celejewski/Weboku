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

        public ISolvingTechnique LockedCandidatesClaiming()
        {
            throw new NotImplementedException();
        }

        public ISolvingTechnique LockedCandidatesPointing()
        {
            throw new NotImplementedException();
        }

        public ISolvingTechnique NakedPair(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            throw new NotImplementedException();
        }

        public ISolvingTechnique NakedSingle(Position pos, InputValue value)
        {
            return new NakedSingle(pos, value);
        }

        public ISolvingTechnique NakedSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            throw new NotImplementedException();
        }

        public ISolvingTechnique Skyscrapper()
        {
            throw new NotImplementedException();
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
