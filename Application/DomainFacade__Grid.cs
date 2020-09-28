using Application.Enums;
using Core.Data;

namespace Application
{
    public sealed partial class DomainFacade
    {
        private IGrid _grid;
        private IGrid Grid
        {
            get
            {
                return ModalState switch
                {
                    ModalState.Share => _shareManager.Grid,
                    ModalState.Paste => _pastedGrid,
                    _ => _grid
                };
            }
        }

        public Value GetValue(Position pos)
        {
            return Grid.GetValue(pos);
        }

        public bool IsGiven(Position position)
        {
            return Grid.GetIsGiven(position);
        }

        public bool HasCandidate(Position position, Value value)
        {
            return Grid.HasCandidate(position, value);
        }

        public bool HasValue(Position position)
        {
            return Grid.HasValue(position);
        }

        public bool IsValueLegal(Position position)
        {
            return Grid.IsCandidateLegal(position, Grid.GetValue(position));
        }

        public bool IsCandidateLegal(Position position, Value value)
        {
            return Grid.IsCandidateLegal(position, value);
        }

        public int GetCandidatesCount(Position position)
        {
            return Grid.GetCandidates(position).Count();
        }

        public void FillAllLegalCandidates()
        {
            _historyManager.Save(Grid);
            Grid.FillAllLegalCandidates();
            CandidateChanged();
        }
        public void ClearAllCandidates()
        {
            _historyManager.Save(Grid);
            Grid.ClearAllCandidates();
            OnCandidateChanged();
        }
        public void RestartGrid()
        {
            _historyManager.Save(Grid);
            foreach( var position in Position.Positions )
            {
                if( !Grid.GetIsGiven(position) )
                {
                    Grid.SetValue(position, Value.None);
                }
            }

            Grid.ClearAllCandidates();
            ValueAndCandidateChanged();
        }
    }
}
