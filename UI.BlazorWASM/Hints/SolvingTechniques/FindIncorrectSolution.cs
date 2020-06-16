using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FindIncorrectSolution : BaseSolvingTechnique
    {
        private readonly Informer _informer;

        public FindIncorrectSolution(Informer informer) : base("There are some invalid inputs")
        {
            _informer = informer;
        }

        public override bool CanExecute(Informer informer)
        {
            return GetInvalidSolutionsPosition().Any();
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            displayer.SetTitle(_title);
            displayer.SetDescription("There are some invalid inputs.");
            displayer.MarkCells(Enums.Color.Illegal, GetInvalidSolutionsPosition());
        }

        public override void Execute(Executor executor, Informer informer)
        {
            foreach( var pos in GetInvalidSolutionsPosition() )
            {
                executor.SetInput(Core.Data.InputValue.Empty, pos);
            }
        }

        private IEnumerable<Position> GetInvalidSolutionsPosition()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var pos = new Position(x, y);
                    if( _informer.HasValue(pos) && _informer.GetSolution(pos) != _informer.GetValue(pos) )
                    {
                        yield return pos;
                    }
                }
            }
        }
    }
}
