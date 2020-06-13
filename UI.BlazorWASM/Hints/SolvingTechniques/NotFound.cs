using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NotFound : ISolvingTechnique
    {
        public bool CanExecute(Informer informer)
        {
            return true;
        }

        public void Display(Displayer displayer)
        {
            displayer.SetTitle("No Hint Found");
            displayer.SetDescription("There is no hint avaliable for this sudoku.");
        }

        public void Execute(Executor executor)
        {
        }
    }
}
