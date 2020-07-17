using Core.Data;
using System.Collections;
using System.Collections.Generic;

namespace SmartSolver.SolvingTechniques
{
    public interface ISolvingTechniquesFactory
    {
        public ISolvingTechnique FullHouse(Position pos, InputValue value);
        public ISolvingTechnique NakedSingle(Position pos, InputValue value);
        public ISolvingTechnique NakedPair(IEnumerable<Position> positions, IEnumerable<InputValue> values);
        public ISolvingTechnique NakedSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values);
        public ISolvingTechnique HiddenSingle(Position pos, InputValue value);
        public ISolvingTechnique HiddenPair(IEnumerable<Position> positions, IEnumerable<InputValue> values);
        public ISolvingTechnique HiddenSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values);
        public ISolvingTechnique LockedCandidatesPointing();
        public ISolvingTechnique LockedCandidatesClaiming();
        public ISolvingTechnique XWing();
        public ISolvingTechnique XYWing();
        public ISolvingTechnique Skyscrapper();
        
    }
}
