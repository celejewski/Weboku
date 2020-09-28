using Application.Enums;
using Application.Filters;
using System;

namespace Application
{
    public sealed partial class DomainFacade
    {
        private IFilter _filter = new SelectedValueFilter(1);
        public IFilter Filter
        {
            get
            {
                if( ModalState == ModalState.Share ) return _shareManager.Filter;
                return _filter;
            }
            private set
            {
                _filter = value;
                OnFilterChanged?.Invoke();
            }
        }

        public void SetFilter(IFilter filter) => Filter = filter;
        public event Action OnFilterChanged;
    }
}
