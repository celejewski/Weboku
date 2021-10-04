using System;
using System.Threading.Tasks;
using Weboku.Application.Enums;
using Weboku.Application.Interfaces;
using Weboku.Application.Managers;
using Weboku.Application.Managers.GridGenerators;
using Weboku.Core.Data;
using Weboku.Core.Exceptions;
using Weboku.Core.Hints;
using Weboku.Core.Hints.SolvingTechniques;
using Weboku.Core.Serializers;
using Weboku.Core.Solvers;
using Weboku.Core.Validators;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private readonly ToolManager _toolManager;
        private readonly HistoryManager _historyManager;
        private readonly HintsProvider _hintsProvider;
        private readonly StorageManager _storageManager;
        private readonly ShareManager _shareManager;
        private readonly PasteManager _pasteManager;
        private readonly ISolver _solver;
        private readonly IGridGenerator _gridGenerator;
        private readonly ColorManager _colorManager;

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
            _solver = new BruteForceSolver();
            _gridGenerator = new PredefinedGridGenerator();
            _colorManager = new ColorManager();
            _colorManager.OnChanged += () => OnColorChanged?.Invoke();
            _gameTimerManager = new GameTimerManager();

            _modalStateManager = new();
            _modalStateManager.OnModalStateChanged += HandleModalStateChanged;
            SetModalState(ModalState.Loading);
        }

        public void StartNewGame(Grid grid, Difficulty difficulty = Difficulty.Unknown)
        {
            ValidatorGrid.EnsureGridIsValid(grid);
            _grid = grid.Clone();
            _solutionGrid = _solver.SolveGivens(_grid);
            Difficulty = difficulty;
            GridChanged();
            _historyManager.ClearRedo();
            _historyManager.ClearUndo();
            SetModalState(ModalState.None);
            ClearAllColors();
            _gameTimerManager.StartTimer();
        }

        public void StartNewGame(string givens)
        {
            var serializer = GridSerializerFactory.Make(GridSerializerName.Default);
            if (!serializer.IsValidFormat(givens))
            {
                throw new SudokuCoreException($"Game can not start. Givens can not be deserialized to valid grid. Passed givens = {givens}");
            }

            var grid = serializer.Deserialize(givens);
            StartNewGame(grid, Difficulty.Unknown);
        }

        public async Task StartNewGame(Difficulty difficulty)
        {
            var grid = await _gridGenerator.Make(difficulty).ConfigureAwait(true);
            StartNewGame(grid, difficulty);
        }

        public void StartNewGameFromPasted()
        {
            if (!PastedIsValid) throw new ApplicationException($"Can not start game from invalid pasted = {Pasted}.");
            StartNewGame(_pasteManager.Pasted);
        }

        public void StartNewCustomGame()
        {
            if (!IsCustomGridValid) throw new ApplicationException($"Can not start game because some inputs are invalid.");
            foreach (var position in Position.Positions)
            {
                if (_customGrid.HasValue(position))
                {
                    _customGrid.SetIsGiven(position, true);
                }
            }

            StartNewGame(_customGrid);
            _customGrid = new Grid();
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

            if (_grid is not null && _grid.IsSudokuSolved())
            {
                _gameTimerManager.StopTimer();
                if (CurrentModalState == ModalState.None)
                {
                    SetModalState(ModalState.EndGame);
                }
            }
        }
    }
}