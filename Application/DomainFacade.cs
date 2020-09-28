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
    public sealed partial class DomainFacade
    {
        private readonly ToolManager _toolManager;
        private readonly HistoryManager _historyManager;
        private readonly HintsProvider _hintsProvider;
        private readonly StorageManager _storageManager;
        private readonly ShareManager _shareManager;

        public Difficulty Difficulty;

        public event Action OnValueChanged;
        public event Action OnCandidateChanged;
        public event Action OnValueOrCandidateChanged;
        public DomainFacade(IStorageProvider storageProvider, string baseUri)
        {
            _grid = new Grid();
            _toolManager = new ToolManager();
            _historyManager = new HistoryManager();
            _hintsProvider = new HintsProvider();
            _storageManager = new StorageManager(storageProvider);
            _shareManager = new ShareManager(baseUri);
        }
        public void StartNewGame(IGrid grid, Difficulty difficulty = Difficulty.Unknown)
        {
            _grid = grid;
            Difficulty = difficulty;
            ValueAndCandidateChanged();
        }

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


        private ModalState _modalState;
        public ModalState ModalState
        {
            get => _modalState;
            set
            {
                _modalState = value;
                if( _modalState == ModalState.Share )
                {
                    _shareManager.UpdateGrid(_grid);
                }
                ValueAndCandidateChanged();
            }
        }

        public ISolvingTechnique GetNextHint()
        {
            return _hintsProvider.GetNextHint(Grid);
        }

        public void ExecuteNextHint()
        {
            _historyManager.Save(Grid);
            var nextHint = _hintsProvider.GetNextHint(Grid);
            nextHint.Execute(Grid);
            ValueAndCandidateChanged();
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

        private IFilter _filter = new SelectedValueFilter(1);
        public IFilter Filter
        {
            get
            {
                if( ModalState == ModalState.Share )
                {
                    return _shareManager.Filter;
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
