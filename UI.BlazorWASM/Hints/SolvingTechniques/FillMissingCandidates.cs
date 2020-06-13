using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FillMissingCandidates : ISolvingTechnique
    {
        public bool CanExecute(Informer informer)
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var position = new Position(x, y);
                    var answer = informer.GetSolution(position);
                    System.Console.WriteLine($"{position} {answer}");
                    if (!informer.HasValue(position) 
                        && !informer.HasCandidate(position, answer) )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Display(Displayer displayer)
        {
            displayer.SetTitle("Missing Candidates");
            displayer.SetDescription("Fill all legal candidates before using hints.");
        }

        public void Execute(Executor executor)
        {
            executor.FillAllLegalCandidates();
        }
    }
}
