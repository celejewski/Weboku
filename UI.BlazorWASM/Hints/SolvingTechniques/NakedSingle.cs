using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedSingle : BaseSingleTechnique
    {
        public NakedSingle(int x, int y, int value, CandidatesMarkProvider candidatesMarkProvider, IGridProvider gridProvider)
            : base(x, y, value, candidatesMarkProvider, gridProvider)
        {
        }

        public override string Name => "Naked single";

        public override string Desc => $"There is only {_value} candidate left in {_x} {_y}.";
    }
}
