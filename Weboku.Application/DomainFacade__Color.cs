using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        public Color GetCellColor(Position position) => _colorManager.GetCellColor(position);
        public void SetCellColor(Position position, Color color) => _colorManager.SetCellColor(position, color);
        public void ClearAllCellColors() => _colorManager.ClearAllCellColors();


        public event Action OnCellColorChanged
        {
            add => _colorManager.OnCellColorChanged += value;
            remove => _colorManager.OnCellColorChanged -= value;
        }


        public void SetInputColor(Position position, Color color) => _colorManager.SetInputColor(position, color);

        public Color GetInputColor(Position position)
        {
            if (!IsValueLegal(position)) return Color.Illegal;

            return _colorManager.GetInputColor(position);
        }

        public void ClearInputColors() => _colorManager.ClearInputColors();

        public event Action OnInputColorChanged
        {
            add => _colorManager.OnInputColorChanged += value;
            remove => _colorManager.OnInputColorChanged -= value;
        }

        public void SetCandidateColor(Position position, Value value, Color color) => _colorManager.SetCandidateColor(position, value, color);

        public void ClearCandidatesColors() => _colorManager.ClearCandidatesColors();

        public Color GetCandidateColor(Position position, Value value)
        {
            var isCandidateLegal = !HasCandidate(position, value) || IsCandidateLegal(position, value);

            if (!isCandidateLegal) return Color.Illegal;

            return _colorManager.GetCandidateColor(position, value);
        }

        public event Action OnCandidateColorChanged
        {
            add => _colorManager.OnCandidateColorChanged += value;
            remove => _colorManager.OnCandidateColorChanged -= value;
        }
    }
}