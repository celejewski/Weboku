using Core.Data;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Filters;

namespace UI.BlazorWASM.ViewModels
{
    public class SelectValue : INumpadMenuItem
    {
        private readonly int _value;
        private readonly ICell[,] _cells;
        private readonly EventCallback<IFilter> _filterChanged;
        private readonly EventCallback<IClickableAction> _clickableActionChanged;
        private readonly StandardAction _standardAction;

        public SelectValue(int value, ICell[,] cells, EventCallback<IFilter> filterChanged, EventCallback<IClickableAction> clickableActionChanged, StandardAction standardAction )
        {
            _value = value;
            _cells = cells;
            _filterChanged = filterChanged;
            _clickableActionChanged = clickableActionChanged;
            _standardAction = standardAction;
        }

        public bool IsDimmed
        {
            get
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int x = 0; x < 9; x++ )
                    {
                        if( _cells[x, y].Candidates.ContainsKey(_value) )
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public string Label => _value.ToString();

        public bool CanExecute => true;

        public bool IsSelectable => true;

        public async Task Execute()
        {
            await _filterChanged.InvokeAsync(new SelectedValueFilter(_value));
            await _clickableActionChanged.InvokeAsync(_standardAction);
            _standardAction.SelectedValue = _value;
        }
    }
}
