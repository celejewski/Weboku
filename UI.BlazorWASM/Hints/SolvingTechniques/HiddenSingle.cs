using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class HiddenSingle : BaseSingleTechnique
    {
        public HiddenSingle(int x, int y, int value)
            : base(x, y, value)
        {
        }

        public override string Name => "Hidden Single";

        public override string Desc => $"There is hidden single {_value} is only candidate in house.";

        public override void Display(HintsProvider hintsProvider)
        {
            base.Display(hintsProvider);

            if( hintsProvider.HintsHelper.GetCandidatesCountInBlock(_x, _y, _value) == 1 )
            {
                hintsProvider.IsBlockHighlighted[HintsHelper.GetBlock(_x, _y)] = true;
            }
            else if( hintsProvider.HintsHelper.GetCandidatesCountInRow(_y, _value) == 1 )
            {
                hintsProvider.IsRowHighlighted[_y] = true;
            }
            else if( hintsProvider.HintsHelper.GetCandidatesCountInCol(_x, _value) == 1 )
            {
                hintsProvider.IsColHighlighted[_x] = true;
            }

            hintsProvider.ShowHints = true;
        }
    }
}
