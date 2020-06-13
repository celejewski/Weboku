using Core.Data;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    /// <summary>
    /// Passes user readonly data to determine if ISolvingTechnique can execute.
    /// </summary>
    public class Informer
    {
        private readonly IGridProvider _gridProvider;

        public Informer(IGridProvider gridProvider)
        {
            _gridProvider = gridProvider;
        }

        public bool HasCandidate(Position position, InputValue value) => _gridProvider.HasCandidate(position.X, position.Y, value);
    }
}
