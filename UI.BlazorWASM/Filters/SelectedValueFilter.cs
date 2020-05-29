using Core.Data;

namespace UI.BlazorWASM.Filters
{
    public class SelectedValueFilter : IFilter
    {
        private readonly int _value;

        public SelectedValueFilter(int value)
        {
            _value = value;
        }

        public string IsFiltered(ICell cell)
        {
            if (cell.Input.Value == _value)
            {
                return FilterStyleClass.Primary;
            }

            if (cell.Candidates.ContainsKey(_value))
            {
                return FilterStyleClass.Secondary;
            }

            return FilterStyleClass.None;
        }
    }
}
