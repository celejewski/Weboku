using Core.Data;
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
        public ISolvingTechnique LockedCandidatesPointing(int block, InputValue inputValue, IEnumerable<Position> positionToRemoveFrom);
        public ISolvingTechnique LockedCandidatesClaiming(InputValue inputValue, IEnumerable<Position> positionsToRemoveCandidate, House house);
        public ISolvingTechnique XWing();
        public ISolvingTechnique XYWing(Position pivot, Position pos1, Position pos2, InputValue candidate1, InputValue candidate2, IEnumerable<Position> positionsToRemove, InputValue value);
        public ISolvingTechnique Skyscrapper(Position base1, Position base2, Position pos1, Position pos2, InputValue value);
        
    }
}
