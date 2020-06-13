using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class HiddenSingle : BaseSingleTechnique
    {
        private readonly HintsProvider _hintsProvider;
        private readonly HintsHelper _hintsHelper;

        public HiddenSingle(int x, int y, int value, CandidatesMarkProvider candidatesMarkProvider, IGridProvider gridProvider, HintsProvider hintsProvider, HintsHelper hintsHelper)
            : base(x, y, value, candidatesMarkProvider, gridProvider)
        {
            _hintsProvider = hintsProvider;
            _hintsHelper = hintsHelper;
        }

        public override string Name => "Hidden Single";

        public override string Desc => $"There is hidden single {_value} is only candidate in house.";

        public override void Display()
        {
            base.Display();

            if( _hintsHelper.GetCandidatesCountInBlock(_x, _y, _value) == 1 )
            {
                _hintsProvider.IsBlockHighlighted[HintsHelper.GetBlock(_x, _y)] = true;
            }
            else if( _hintsHelper.GetCandidatesCountInRow(_y, _value) == 1 )
            {
                _hintsProvider.IsRowHighlighted[_y] = true;
            }
            else if( _hintsHelper.GetCandidatesCountInCol(_x, _value) == 1 )
            {
                _hintsProvider.IsColHighlighted[_x] = true;
            }
        }
    }
}
