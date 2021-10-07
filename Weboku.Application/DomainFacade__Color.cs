using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        public Color GetCellColor(Position position) => _colorManager.GetCellColor(position);
        public void ClearAllCellColors() => _colorManager.ClearAllCellColors();


        public event Action OnCellColorChanged
        {
            add => _colorManager.OnCellColorChanged += value;
            remove => _colorManager.OnCellColorChanged -= value;
        }


        public Color GetInputColor(Position position)
        {
            return IsValueLegal(position)
                ? _colorManager.GetInputColor(position)
                : Color.Illegal;
        }


        public event Action OnInputColorChanged
        {
            add => _colorManager.OnInputColorChanged += value;
            remove => _colorManager.OnInputColorChanged -= value;
        }


        public Color GetCandidateColor(Position position, Value value)
        {
            var isCandidateLegal = !HasCandidate(position, value) || IsCandidateLegal(position, value);

            return isCandidateLegal
                ? _colorManager.GetCandidateColor(position, value)
                : Color.Illegal;
        }

        public event Action OnCandidateColorChanged
        {
            add => _colorManager.OnCandidateColorChanged += value;
            remove => _colorManager.OnCandidateColorChanged -= value;
        }
    }
}