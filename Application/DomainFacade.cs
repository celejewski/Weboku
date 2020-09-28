using Application.Data;
using Application.Enums;
using Application.Filters;
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

        private IGrid _grid;
        private IGrid _gridToShare;
        public IGrid Grid
        {
            get
            {
                if( ModalState == ModalState.Share )
                {
                    return _gridToShare;
                }
                if( ModalState == ModalState.Paste )
                {
                    return _pastedGrid;
                }
                return _grid;
            }
            set
            {
                _grid = value;
            }
        }
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
                if( _modalState == ModalState.Share )
                {
                    _gridToShare = TransformGrid();
                }
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
            _storageManager.Save(new StorageDto(_grid, Difficulty));
        }

        public void Load()
        {
            var storageDto = _storageManager.Load();
            _grid = storageDto.Grid;
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

        public IGrid TransformGrid()
        {
            return ShareManager.TransformGrid(_grid, SharedFields);
        }

        public string SharedOutput => ShareManager.SerializeGridToShareableFormat(Grid, SharedConverter, BaseUri);

        private SharedConverter _sharedConverter = SharedConverter.MyLink;
        public SharedConverter SharedConverter
        {
            get => _sharedConverter;
            set
            {
                _sharedConverter = value;
                _gridToShare = TransformGrid();
                ValueAndCandidateChanged();
            }
        }

        private SharedFields _sharedFields = SharedFields.Everything;
        public SharedFields SharedFields
        {
            get => _sharedFields;
            set
            {
                _sharedFields = value;
                _gridToShare = TransformGrid();
                ValueAndCandidateChanged();
            }
        }

        private string BaseUri { get; set; }

        private IFilter _filter = new SelectedValueFilter(1);
        public IFilter Filter
        {
            get
            {
                if( ModalState == ModalState.Share )
                {
                    return new SharedFilter();
                }
                return _filter;
            }
            set
            {
                _filter = value;
                OnFilterChanged?.Invoke();
            }
        }

        public void SetFilter(IFilter filter) => Filter = filter;
        public event Action OnFilterChanged;

        private string _pasted = new string('0', 81);
        public string Pasted
        {
            get => _pasted;
            set
            {
                _pasted = value;
                PastedIsValid = _defaultSerializer.IsValidFormat(_pasted);
                _pastedGrid = PastedIsValid ? _defaultSerializer.Deserialize(Pasted) : new Grid();
                ValueAndCandidateChanged();
            }
        }

        public bool PastedIsValid;
        private IGrid _pastedGrid = new Grid();

        private readonly IGridSerializer _defaultSerializer = GridSerializerFactory.Make(GridSerializerName.Default);
        public void StartNewGameFromPasted()
        {
            if( !PastedIsValid ) throw new ApplicationException($"Can not start game from invalid pasted = {Pasted}.");
            StartNewGame(_pastedGrid);
        }
    }
}
