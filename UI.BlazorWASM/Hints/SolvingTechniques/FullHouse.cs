using Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FullHouse : BaseSingleTechnique
    {
        public FullHouse(int x, int y, int value)
            : base(x, y, value)
        {

        }

        public override string Name => "Full House";

        public override string Desc => "There is only one empty cell left in house.";

        public override void Display(HintsProvider hintsProvider)
        {
            base.Display(hintsProvider);

            var helper = hintsProvider.HintsHelper;
            if (IsFullHouse(helper.GetCellsInBlock(_x, _y)))
            {
                hintsProvider.IsBlockHighlighted[HintsHelper.GetBlock(_x, _y)] = true;
            }
            else if (IsFullHouse(helper.GetCellsInCol(_x)))
            {
                hintsProvider.IsColHighlighted[_x] = true;
            }
            else if (IsFullHouse(helper.GetCellsInRow(_y)))
            {
                hintsProvider.IsRowHighlighted[_y] = true;
            }
        }

        private bool IsFullHouse(IEnumerable<ICell> cells)
        {
            return cells.Count(cell => cell.Input.Value != 0) == 8;
        }
    }
}
