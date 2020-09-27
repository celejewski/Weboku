using Application.Data;
using Application.Enums;
using Application.Interfaces;
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
        private readonly ToolManager _toolManager;
        private readonly GridHistoryManager _gridHistoryManager;
        private readonly HintsProvider _hintsProvider;
        private readonly StorageManager _storageManager;

        public DomainFacade(IStorageProvider storageProvider)
        {
            Grid = new Grid();
            _toolManager = new ToolManager();
            _gridHistoryManager = new GridHistoryManager();
            _hintsProvider = new HintsProvider();
            _storageManager = new StorageManager(storageProvider);
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

        public void StartNewGame(IGrid grid, Difficulty difficulty = Difficulty.Unknown)
        {
            Grid = grid;
            Difficulty = difficulty;
            ValueAndCandidateChanged();
        }

        public Difficulty Difficulty;

        public void StartNewGame(string givens)
        {
            var serializer = GridSerializerFactory.Make(GridSerializerName.Default);
            if( !serializer.IsValidFormat(givens) )
            {
                throw new SudokuCoreException($"Game can not start. Givens can not be deserialized to valid grid. Passed givens = {givens}");
            }
            var grid = serializer.Deserialize(givens);
            StartNewGame(grid, Difficulty.Unknown);
        }

        public async Task StartNewGame(Difficulty difficulty)
        {
            var grid = await GridGenerator.Make(difficulty).ConfigureAwait(true);
            StartNewGame(grid, difficulty);
        }

        public void UseMarker(Position position, Value value)
        {
            _gridHistoryManager.Save(Grid);
            _toolManager.UseMarker(Grid, position, value);
            ValueAndCandidateChanged();
        }

        public void UsePencil(Position position, Value value)
        {
            _gridHistoryManager.Save(Grid);
            _toolManager.UsePencil(Grid, position, value);
            CandidateChanged();
        }
        public void UseEraser(Position position)
        {
            _gridHistoryManager.Save(Grid);
            _toolManager.UseEraser(Grid, position);
            ValueAndCandidateChanged();
        }

        public void FillAllLegalCandidates()
        {
            _gridHistoryManager.Save(Grid);
            Grid.FillAllLegalCandidates();
            CandidateChanged();
        }

        public event Action OnValueChanged;

        public event Action OnCandidateChanged;
        public event Action OnValueOrCandidateChanged;

        public void ClearAllCandidates()
        {
            _gridHistoryManager.Save(Grid);
            Grid.ClearAllCandidates();
            OnCandidateChanged();
        }

        public IGrid Grid;
        public void RestartGrid()
        {
            _gridHistoryManager.Save(Grid);
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

        public void Undo()
        {
            if( _gridHistoryManager.CanUndo )
            {
                Grid = _gridHistoryManager.Undo(Grid);
                ValueAndCandidateChanged();
            }
        }

        public void Redo()
        {
            if( _gridHistoryManager.CanRedo )
            {
                Grid = _gridHistoryManager.Redo(Grid);
                ValueAndCandidateChanged();
            }
        }

        private ModalState _modalState;
        public ModalState ModalState
        {
            get => _modalState;
            set
            {
                _modalState = value;
                ValueAndCandidateChanged();
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
            return _hintsProvider.GetNextHint(Grid);
        }

        public void ExecuteNextHint()
        {
            _gridHistoryManager.Save(Grid);
            var nextHint = _hintsProvider.GetNextHint(Grid);
            nextHint.Execute(Grid);
            ValueAndCandidateChanged();
        }

        public void Save()
        {
            _storageManager.Save(new StorageDto(Grid, Difficulty));
        }

        public void Load()
        {
            var storageDto = _storageManager.Load();
            Grid = storageDto.Grid;
            Difficulty = storageDto.Difficulty;
            ValueChanged();
        }
        private void ValueChanged()
        {
            OnValueChanged?.Invoke();
            OnValueOrCandidateChanged?.Invoke();
        }

        private void CandidateChanged()
        {
            OnCandidateChanged?.Invoke();
            OnValueOrCandidateChanged?.Invoke();
        }
        private void ValueAndCandidateChanged()
        {
            OnCandidateChanged?.Invoke();
            OnValueChanged?.Invoke();
            OnValueOrCandidateChanged?.Invoke();
        }
    }
}
