using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class TwoStringKiteDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Informer _informer;
        private readonly Displayer _displayer;
        private readonly TwoStringKite _twoStringKite;

        public TwoStringKiteDisplayer(Informer informer, Displayer displayer, TwoStringKite twoStringKite)
            : base(informer, displayer, twoStringKite, "two-string-kite")
        {
            _informer = informer;
            _displayer = displayer;
            _twoStringKite = twoStringKite;
        }

        public override void DisplaySolution()
        {
            base.DisplaySolution();
            _displayer.Mark(Enums.Color.Legal, _twoStringKite.LegalPositions, _twoStringKite.Value);
            _displayer.Mark(Enums.Color.Info, _twoStringKite.InfoPositions, _twoStringKite.Value);
            _displayer.Mark(Enums.Color.Illegal, _twoStringKite.PositionToRemove, _twoStringKite.Value);
            _displayer.SetValueFilter(_twoStringKite.Value);
        }
    }
}
