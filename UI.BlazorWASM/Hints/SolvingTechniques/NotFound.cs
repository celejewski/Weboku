namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NotFound : BaseSolvingTechnique
    {
        public NotFound()
            : base("no-hint")
        {

        }
        public override bool CanExecute(Informer informer)
        {
            return true;
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);
            displayer.SetDescription(DescriptionKey);
        }

        public override void Execute(Executor executor, Informer informer)
        {
        }
    }
}
