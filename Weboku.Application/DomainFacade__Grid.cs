using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Validators;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private Grid _grid;
        private Grid _solutionGrid = new();
        private Grid _customGrid = new();

        private Grid Grid
        {
            get
            {
                return ModalState switch
                {
                    ModalState.Share => _shareManager.Grid,
                    ModalState.Paste => _pasteManager.Grid,
                    ModalState.CustomSudoku => _customGrid,
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
            if (Grid == _grid)
            {
                return !Grid.HasValue(position)
                       || Grid.GetValue(position) == _solutionGrid.GetValue(position);
            }

            return Grid.IsValueLegal(position);
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
            GridChanged();
        }

        public void ClearAllCandidates()
        {
            _historyManager.Save(Grid);
            Grid.ClearAllCandidates();
            GridChanged();
        }

        public void RestartGrid()
        {
            _historyManager.Save(Grid);
            _grid.Restart();
            GridChanged();
        }

        public void RestartGame()
        {
            RestartGrid();
            ClearAllColors();
            StartTimer();
        }

        public bool IsCustomGridValid => ValidatorGrid.AreAllValueslegal(_customGrid);
    }
}