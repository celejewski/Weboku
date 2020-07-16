using Core.Data;
using System.Collections;
using System.Collections.Generic;

namespace SmartSolver.SolvingTechniques
{
    public interface ISolvingTechniquesFactory
    {
        public FullHouse FullHouse(Position pos, InputValue value);
        public NakedSingle NakedSingle(Position pos, InputValue value);
        public NakedPair NakedPair(IEnumerable<Position> positions, IEnumerable<InputValue> values);
        public NakedSubset NakedSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values);
        public HiddenSingle HiddenSingle(Position pos, InputValue value);
        public HiddenPair HiddenPair(IEnumerable<Position> positions, IEnumerable<InputValue> values);
        public HiddenSubset HiddenSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values);
        public LockedCandidatesPointing LockedCandidatesPointing();
        public LockedCandidatesClaiming LockedCandidatesClaiming();
        public XWing XWing();
        public XYWing XYWing();
        public Skyscrapper Skyscrapper();
        
    }
}
