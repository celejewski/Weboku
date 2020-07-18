﻿using System.Collections.Generic;

namespace Core.Data
{
    public interface IGrid
    {
        bool GetIsGiven(Position pos);
        void SetIsGiven(Position pos, bool value);

        InputValue GetValue(Position pos);
        void SetValue(Position pos, InputValue value);
        bool HasValue(Position pos);

        bool HasCandidate(Position pos, InputValue value);
        IEnumerable<InputValue> GetCandidates(Position pos);
        int CandidatesCount(Position pos);

        void AddCandidate(Position pos, InputValue value);
        void RemoveCandidate(Position pos, InputValue value);
        void ToggleCandidate(Position pos, InputValue value);
        void FillCandidates();
        void ClearCandidates();
        void ClearCandidates(Position pos);

        IGrid Clone();

        int GetGivensHashcode();
    }
}
