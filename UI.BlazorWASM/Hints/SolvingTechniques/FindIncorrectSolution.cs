using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FindIncorrectSolution : ISolvingTechnique
    {
        private readonly Informer _informer;

        public FindIncorrectSolution(Informer informer)
        {
            _informer = informer;
        }

        public bool CanExecute(Informer informer)
        {
            return GetInvalidSolutionsPosition().Any();
        }

        public void Display(Displayer displayer, Informer informer)
        {
            displayer.SetTitle("There are some invalid inputs");
            displayer.SetDescription("There are some invalid inputs.");
            displayer.MarkCells(Enums.Color.Illegal, GetInvalidSolutionsPosition());
        }

        public void Execute(Executor executor)
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
