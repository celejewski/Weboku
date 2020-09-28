﻿using Application.Enums;
using Core.Data;

namespace Application
{
    public sealed partial class DomainFacade
    {
        private IGrid _grid;
        private IGrid _customGrid = new Grid();
        private IGrid Grid
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
            OnGridChanged();
        }
        public void RestartGrid()
        {
            _historyManager.Save(Grid);
            _grid.Restart();
            GridChanged();
        }
    }
}
