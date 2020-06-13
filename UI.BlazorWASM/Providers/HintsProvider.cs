using System;
using UI.BlazorWASM.Hints;
using UI.BlazorWASM.Hints.SolvingTechniques;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Providers
{
    public class HintsProvider : IProvider
    {
        private bool _showHints = false;
        public bool ShowHints
        {
            get => _showHints;
            set
            {
                _showHints = value;
                OnChanged?.Invoke();
            }
        }

        public string TechniqueName => NextTechnique.Name;
        public string TechniqueDesc => NextTechnique.Desc;
        private ISolvingTechnique NextTechnique => _hodokuParser.GetNextTechnique(_sudokuProvider.Steps);

        public bool[] IsBlockHighlighted { get; private set; } = new bool[9];
        public bool[] IsColHighlighted { get; private set; } = new bool[9];
        public bool[] IsRowHighlighted { get; private set; } = new bool[9];
        public HintsHelper HintsHelper { get; }

        public CandidatesMarkProvider CandidatesMarkProvider;
        private readonly HodokuParser _hodokuParser;
        private readonly IGridProvider _gridProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly CellColorProvider _cellColorProvider;
        private readonly SudokuProvider _sudokuProvider;

        public event Action OnChanged;

        public HintsProvider(
            CandidatesMarkProvider candidatesMarkProvider,
            HodokuParser hodokuParser,
            IGridProvider gridProvider,
            HintsHelper hintsHelper,
            IGridHistoryManager gridHistoryManager,
            CellColorProvider cellColorProvider,
            SudokuProvider sudokuProvider)
        {
            CandidatesMarkProvider = candidatesMarkProvider;
            _hodokuParser = hodokuParser;
            _hodokuParser.HintsProvider = this;
            _gridProvider = gridProvider;
            HintsHelper = hintsHelper;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
        }

        public void Display()
        {
            if( NextTechnique == null )
            {
                return;
            }

            Clear();
            NextTechnique.Display();
            ShowHints = true;
            OnChanged?.Invoke();
        }

        public void Execute()
        {
            Hide();
            _gridHistoryManager.Save();
            NextTechnique.Execute();
        }

        public void Hide()
        {
            Clear();
            ShowHints = false;
        }

        private void Clear()
        {
            _cellColorProvider.ClearAll();
            CandidatesMarkProvider.ClearColors();
            ClearHighlight();
        }

        private void ClearHighlight()
        {
            for( int i = 0; i < 9; i++ )
            {
                IsBlockHighlighted[i] = false;
                IsRowHighlighted[i] = false;
                IsColHighlighted[i] = false;
            }
        }

    }
}
