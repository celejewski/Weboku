//using System;
//using UI.BlazorWASM.Hints;
//using UI.BlazorWASM.Hints.SolvingTechniques;
//using UI.BlazorWASM.Managers;

//namespace UI.BlazorWASM.Providers
//{
//    public class HintsProvider : IProvider
//    {
//        private bool _showHints = false;
//        public bool ShowHints 
//        {
//            get => _showHints;
//            set
//            {
//                _showHints = value;
//                OnChanged?.Invoke();
//            }
//        }

//        public string TechniqueName => NextTechnique.Name;
//        public string TechniqueDesc => NextTechnique.Desc;
//        private ISolvingTechnique NextTechnique =>_hodokuParser.GetNextTechnique(_sudokuProvider.Sudoku.Steps);

//        public bool[] IsBlockHighlighted { get; private set; } = new bool[9];
//        public bool[] IsColHighlighted { get; private set; } = new bool[9];
//        public bool[] IsRowHighlighted { get; private set; } = new bool[9];
//        public HintsHelper HintsHelper { get; }

//        public CandidatesMarkProvider CandidatesMarkProvider;
//        private readonly HodokuParser _hodokuParser;
//        private readonly ISudokuProvider _sudokuProvider;
//        private readonly IGridHistoryManager _gridHistoryManager;
//        private readonly CellColorProvider _cellColorProvider;

//        public event Action OnChanged;

//        public HintsProvider(
//            CandidatesMarkProvider candidatesMarkProvider, 
//            HodokuParser hodokuParser,
//            ISudokuProvider sudokuProvider,
//            HintsHelper hintsHelper,
//            IGridHistoryManager gridHistoryManager,
//            CellColorProvider cellColorProvider)
//        {
//            CandidatesMarkProvider = candidatesMarkProvider;
//            _hodokuParser = hodokuParser;
//            _sudokuProvider = sudokuProvider;
//            HintsHelper = hintsHelper;
//            _gridHistoryManager = gridHistoryManager;
//            _cellColorProvider = cellColorProvider;
//        }

//        public void Display()
//        {
//            if( NextTechnique == null )
//            {
//                return;
//            }

//            ShowHints = true;
//            Clear();
//            NextTechnique.Display(this);
//            OnChanged?.Invoke();
//        }

//        public void Execute()
//        {
//            Hide();
//            _gridHistoryManager.Save();
//            NextTechnique.Execute(_sudokuProvider);
//        }

//        public void Hide()
//        {
//            Clear();
//            ShowHints = false;
//        }

//        private void Clear()
//        {
//            _cellColorProvider.ClearAll();
//            CandidatesMarkProvider.ClearColors();
//            ClearHighlight();
//        }

//        private void ClearHighlight()
//        {
//            for( int i = 0; i < 9; i++ )
//            {
//                IsBlockHighlighted[i] = false;
//                IsRowHighlighted[i] = false;
//                IsColHighlighted[i] = false;
//            }
//        }

//    }
//}
