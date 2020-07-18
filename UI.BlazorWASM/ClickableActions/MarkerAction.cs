﻿using Core.Data;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class MarkerAction : IClickableAction
    {
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridProvider _gridProvider;

        public MarkerAction(
            IGridHistoryManager gridHistoryManager, 
            CellColorProvider cellColorProvider, 
            IGridProvider gridProvider
            )
        {
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _gridProvider = gridProvider;
        }
        public void LeftClickAction(ClickableActionArgs args)
        {
            if( args.MouseEventArgs.CtrlKey )
            {
                _cellColorProvider.ToggleColor(args.Pos, args.Color1);
                return;
            }

            if( _gridProvider.GetIsGiven(args.Pos) )
            {
                return;
            }

            if( args.Value == InputValue.Empty 
                || !_gridProvider.HasValue(args.Pos))
            {
                _gridHistoryManager.Save();
                _gridProvider.SetValue(args.Pos, args.Value);
            }
            else if( _gridProvider.GetValue(args.Pos) == args.Value )
            {
                _gridHistoryManager.Save();
                _gridProvider.SetValue(args.Pos, InputValue.Empty);
            }
        }
        public void RightClickAction(ClickableActionArgs args)
        {
            if( args.MouseEventArgs.CtrlKey )
            {
                _cellColorProvider.ToggleColor(args.Pos, args.Color2);
                return;
            }

            if( _gridProvider.HasValue(args.Pos) 
                || args.Value == InputValue.Empty )
            {
                return;
            }

            _gridHistoryManager.Save();
            _gridProvider.ToggleCandidate(args.Pos, args.Value);
        }
    }
}