namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NotFound : BaseSolvingTechnique
    {
        public NotFound()
            : base("No hint found")
        {

        }
        public override bool CanExecute(Informer informer)
        {
            return true;
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            displayer.SetTitle(_title);
            displayer.SetDescription("There is no hint avaliable for this sudoku.");
        }

        public override void Execute(Executor executor, Informer informer)
        {
        }
    }
}
