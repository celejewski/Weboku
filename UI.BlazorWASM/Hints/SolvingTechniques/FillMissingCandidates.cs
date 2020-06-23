using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FillMissingCandidates : BaseSolvingTechnique
    {
        public FillMissingCandidates() : base("fill-missing-candidates")
        {
        }
        public override bool CanExecute(Informer informer)
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var position = new Position(x, y);
                    var solution = informer.GetSolution(position);
                    if (!informer.HasValue(position) 
                        && !informer.HasCandidate(position, solution) )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.FillAllLegalCandidates();
        }
    }
}
