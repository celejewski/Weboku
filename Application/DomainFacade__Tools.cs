﻿using Core.Data;

namespace Application
{
    public sealed partial class DomainFacade
    {
        public void UseMarker(Position position, Value value)
        {
            _historyManager.Save(Grid);
            _toolManager.UseMarker(Grid, position, value);
            ValueAndCandidateChanged();
        }

        public void UsePencil(Position position, Value value)
        {
            _historyManager.Save(Grid);
            _toolManager.UsePencil(Grid, position, value);
            CandidateChanged();
        }
        public void UseEraser(Position position)
        {
            _historyManager.Save(Grid);
            _toolManager.UseEraser(Grid, position);
            ValueAndCandidateChanged();
        }
    }
}
