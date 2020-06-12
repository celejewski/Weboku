using Core.Data;
using System;
using System.Collections.Generic;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Managers
{
    public class GridHistoryManager : IGridHistoryManager
    {
        public GridHistoryManager()
        {
        }

        public bool CanUndo =>false;

        public bool CanRedo => false;

        public event Action OnChanged;

        public void ClearRedo()
        {
        }

        public void ClearUndo()
        {
        }

        public void Redo()
        {
        }

        public void Save()
        {

        }

        public void Undo()
        {
        }

        //private readonly Stack<IGrid> _previousStates = new Stack<IGrid>();
        //private readonly Stack<IGrid> _nextStates = new Stack<IGrid>();

        //public event Action OnChanged;

        //public bool CanUndo { get => _previousStates.Count > 0; }
        //public bool CanRedo { get => _nextStates.Count > 0; }

        //public void Save()
        //{
        //    //_previousStates.Push(_sudokuProvider.GetGridClone());
        //    _nextStates.Clear();
        //    OnChanged?.Invoke();
        //}

        //public void Undo()
        //{
        //    if( CanUndo )
        //    {
        //        var current = _sudokuProvider.GetGridClone();
        //        _nextStates.Push(current);

        //        var previous = _previousStates.Pop();
        //        _sudokuProvider.AssignFrom(previous);
        //        OnChanged?.Invoke();
        //    }
        //}

        //public void Redo()
        //{
        //    if( CanRedo )
        //    {
        //        var current = _sudokuProvider.GetGridClone();
        //        _previousStates.Push(current);

        //        var next = _nextStates.Pop();
        //        _sudokuProvider.AssignFrom(next);
        //        OnChanged?.Invoke();
        //    }
        //}

        //public void ClearUndo()
        //{
        //    _previousStates.Clear();
        //    OnChanged?.Invoke();
        //}

        //public void ClearRedo()
        //{
        //    _nextStates.Clear();
        //    OnChanged?.Invoke();
        //}
    }
}
