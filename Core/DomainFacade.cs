using Core.Data;
using Core.Exceptions;
using Core.Managers;
using Core.Serializers;
using System;
using System.Threading.Tasks;

namespace Core
{
    public class DomainFacade
    {
        private readonly GridManager _gridManager;
        private readonly ToolManager _toolManager;

        public DomainFacade()
        {
            _gridManager = new GridManager();
            _toolManager = new ToolManager();
        }
        public InputValue GetInputValue(Position pos)
        {
            return _gridManager.GetInputValue(pos);
        }

        public bool IsGiven(Position pos)
        {
            return _gridManager.IsGiven(pos);
        }

        public bool HasCandidate(Position pos, InputValue value)
        {
            return _gridManager.HasCandidate(pos, value);
        }

        public bool HasValue(Position pos)
        {
            return _gridManager.HasValue(pos);
        }

        public bool IsValueLegal(Position pos)
        {
            return _gridManager.IsValueLegal(pos);
        }

        public bool IsCandidateLegal(Position pos, InputValue value)
        {
            return _gridManager.IsCandidateLegal(pos, value);
        }

        public int GetCandidatesCount(Position pos)
        {
            return _gridManager.GetCandidatesCount(pos);
        }

        public void StartNewGame(IGrid grid)
        {
            _gridManager.Grid = grid;
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


        public void UseMarker(Position pos, InputValue value)
        {
            _toolManager.UseMarker(_gridManager.Grid, pos, value);
            _gridManager.ValueAndCandidateChanged();
        }

        public void UsePencil(Position pos, InputValue value)
        {
            _toolManager.UsePencil(_gridManager.Grid, pos, value);
            _gridManager.CandidateChanged();
        }
        public void UseEraser(Position pos)
        {
            _toolManager.UseEraser(_gridManager.Grid, pos);
            _gridManager.ValueAndCandidateChanged();
        }

        public void FillAllLegalCandidates()
        {
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
            _gridManager.Grid.ClearAllCandidates();
        }

        public IGrid Grid
        {
            get => _gridManager.Grid;
            set => _gridManager.Grid = value;
        }

        public void RestartGrid()
        {
            foreach( var pos in Position.All )
            {
                if( !_gridManager.Grid.GetIsGiven(pos) )
                {
                    _gridManager.Grid.SetValue(pos, InputValue.None);
                }
            }

            _gridManager.Grid.ClearAllCandidates();
        }

        public async Task StartNewGame(Difficulty difficulty)
        {
            var grid = await GridGenerator.Make(difficulty);
            StartNewGame(grid);
        }
    }
}
