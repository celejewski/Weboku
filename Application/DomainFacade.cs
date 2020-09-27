using Application.Managers;
using Core.Data;
using Core.Exceptions;
using Core.Hints;
using Core.Hints.SolvingTechniques;
using Core.Serializers;
using System;
using System.Threading.Tasks;

namespace Application
{
    public class DomainFacade
    {
        private readonly GridManager _gridManager;
        private readonly ToolManager _toolManager;
        private readonly GridHistoryManager _gridHistoryManager;
        private readonly HintsProvider _hintsProvider;

        public DomainFacade()
        {
            _gridManager = new GridManager();
            _toolManager = new ToolManager();
            _gridHistoryManager = new GridHistoryManager(_gridManager);
            _hintsProvider = new HintsProvider();
        }
        public Value GetValue(Position pos)
        {
            return _gridManager.GetValue(pos);
        }

        public bool IsGiven(Position position)
        {
            return _gridManager.IsGiven(position);
        }

        public bool HasCandidate(Position position, Value value)
        {
            return _gridManager.HasCandidate(position, value);
        }

        public bool HasValue(Position position)
        {
            return _gridManager.HasValue(position);
        }

        public bool IsValueLegal(Position position)
        {
            return _gridManager.IsValueLegal(position);
        }

        public bool IsCandidateLegal(Position position, Value value)
        {
            return _gridManager.IsCandidateLegal(position, value);
        }

        public int GetCandidatesCount(Position position)
        {
            return _gridManager.GetCandidatesCount(position);
        }

        public void StartNewGame(IGrid grid, Difficulty difficulty = Difficulty.Unknown)
        {
            _gridManager.Grid = grid;
            _gridManager.Difficulty = difficulty;
            _gridManager.ValueAndCandidateChanged();
        }

        public Difficulty Difficulty
        {
            get => _gridManager.Difficulty;
            set => _gridManager.Difficulty = value;
        }

        public void StartNewGame(string givens)
        {
            var serializer = GridSerializerFactory.Make(GridSerializerName.Default);
            if( !serializer.IsValidFormat(givens) )
            {
                throw new SudokuCoreException($"Game can not start. Givens can not be deserialized to valid grid. Passed givens = {givens}");
            }
            var grid = serializer.Deserialize(givens);
            StartNewGame(grid);
        }

        public async Task StartNewGame(Difficulty difficulty)
        {
            var grid = await GridGenerator.Make(difficulty).ConfigureAwait(true);
            StartNewGame(grid, difficulty);
        }

        public void UseMarker(Position position, Value value)
        {
            _gridHistoryManager.Save();
            _toolManager.UseMarker(_gridManager.Grid, position, value);
            _gridManager.ValueAndCandidateChanged();
        }

        public void UsePencil(Position position, Value value)
        {
            _gridHistoryManager.Save();
            _toolManager.UsePencil(_gridManager.Grid, position, value);
            _gridManager.CandidateChanged();
        }
        public void UseEraser(Position position)
        {
            _gridHistoryManager.Save();
            _toolManager.UseEraser(_gridManager.Grid, position);
            _gridManager.ValueAndCandidateChanged();
        }

        public void FillAllLegalCandidates()
        {
            _gridHistoryManager.Save();
            _gridManager.FillAllLegalCandidates();
            _gridManager.CandidateChanged();
        }

        public event Action OnValueChanged
        {
            add { _gridManager.OnValueChanged += value; }
            remove { _gridManager.OnValueChanged -= value; }
        }

        public event Action OnCandidateChanged
        {
            add { _gridManager.OnCandidateChanged += value; }
            remove { _gridManager.OnCandidateChanged -= value; }
        }

        public event Action OnValueOrCandidateChanged
        {
            add { _gridManager.OnValueOrCandidateChanged += value; }
            remove { _gridManager.OnValueOrCandidateChanged -= value; }
        }

        public void ClearAllCandidates()
        {
            _gridHistoryManager.Save();
            _gridManager.Grid.ClearAllCandidates();
        }

        public IGrid Grid
        {
            get => _gridManager.Grid;
            set => _gridManager.Grid = value;
        }

        public void RestartGrid()
        {
            _gridHistoryManager.Save();
            foreach( var position in Position.Positions )
            {
                if( !_gridManager.Grid.GetIsGiven(position) )
                {
                    _gridManager.Grid.SetValue(position, Value.None);
                }
            }

            _gridManager.Grid.ClearAllCandidates();
            _gridManager.ValueAndCandidateChanged();
        }

        public void Undo()
        {
            if( _gridHistoryManager.CanUndo )
            {
                _gridHistoryManager.Undo();
            }
        }

        public void Redo()
        {
            if( _gridHistoryManager.CanRedo )
            {
                _gridHistoryManager.Redo();
            }
        }

        public bool CanRedo => _gridHistoryManager.CanRedo;
        public bool CanUndo => _gridHistoryManager.CanUndo;
        public event Action OnHistoryChanged
        {
            add { _gridHistoryManager.OnChanged += value; }
            remove { _gridHistoryManager.OnChanged -= value; }
        }

        public ISolvingTechnique GetNextHint()
        {
            return _hintsProvider.GetNextHint(_gridManager.Grid);
        }

        public void ExecuteNextHint()
        {
            _gridHistoryManager.Save();
            var nextHint = _hintsProvider.GetNextHint(_gridManager.Grid);
            nextHint.Execute(_gridManager.Grid);
            _gridManager.ValueAndCandidateChanged();
        }
    }
}
