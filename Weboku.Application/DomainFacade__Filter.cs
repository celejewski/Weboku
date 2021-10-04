using System;
using System.Linq;
using Weboku.Application.Enums;
using Weboku.Application.Filters;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private IFilter _filter = new SelectedValueFilter(1);

        public IFilter Filter
        {
            get
            {
                if (_modalStateManager.CurrentModalState == ModalState.Share) return new SharedFilter(SharedFields);
                return _filter;
            }
            private set
            {
                _filter = value;
                OnFilterChanged?.Invoke();
            }
        }

        public FilterOption IsFiltered(Position position) => Filter.IsFiltered(Grid, position);

        public void SetFilter(IFilter filter) => Filter = filter;
        public event Action OnFilterChanged;

        public bool CanUsePairFilter()
        {
            foreach (var pos in Position.Positions)
            {
                if (GetCandidatesCount(pos) == 2)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanUseValueFilter(Value value)
        {
            foreach (var row in Position.Rows)
            {
                var isAnyValueIllegal = !row.All(IsValueLegal);
                var legalValuesInRow = row.Count(pos => GetValue(pos) == value);
                if (isAnyValueIllegal || legalValuesInRow != 1) return true;
            }

            return false;
        }
    }
}