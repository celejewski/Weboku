using Application.Enums;
using Application.Interfaces;
using Application.Managers;
using Core.Data;
using Core.Exceptions;
using Core.Hints;
using Core.Hints.SolvingTechniques;
using Core.Serializers;
using Core.Validators;
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
        private readonly PasteManager _pasteManager;

        public Difficulty Difficulty;
        public event Action OnGridChanged;
        public DomainFacade(IStorageProvider storageProvider, string baseUri)
        {
            _grid = new Grid();
            _toolManager = new ToolManager();
            _historyManager = new HistoryManager();
            _hintsProvider = new HintsProvider();
            _storageManager = new StorageManager(storageProvider);
            _shareManager = new ShareManager(baseUri);
            _pasteManager = new PasteManager();
        }
        public void StartNewGame(IGrid grid, Difficulty difficulty = Difficulty.Unknown)
        {
            ValidatorGrid.EnsureGridIsValid(grid);
            _grid = grid.Clone();
            Difficulty = difficulty;
            GridChanged();
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

        public void StartNewGameFromPasted()
        {
            if( !PastedIsValid ) throw new ApplicationException($"Can not start game from invalid pasted = {Pasted}.");
            StartNewGame(_pasteManager.Pasted);
        }

        public void StartNewCustomGame()
        {
            foreach( var position in Position.Positions )
            {
                if( _customGrid.HasValue(position) )
                {
                    _customGrid.SetIsGiven(position, true);
                }
            }
            StartNewGame(_customGrid);
            _customGrid = new Grid();
        }

        private ModalState _modalState;
        public ModalState ModalState
        {
            get => _modalState;
            set
            {
                if( _modalState == value ) return;
                _modalState = value;
                if( _modalState == ModalState.Share )
                {
                    _shareManager.UpdateGrid(_grid);
                }
                GridChanged();
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
            GridChanged();
        }

        private void GridChanged()
        {
            OnGridChanged?.Invoke();
        }
    }
}
