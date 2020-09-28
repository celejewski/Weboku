using Application.Enums;

namespace Application
{
    public sealed partial class DomainFacade
    {
        public string SharedOutput => _shareManager.SharedOutput;

        public SharedConverter SharedConverter
        {
            get => _shareManager.SharedConverter;
            set => _shareManager.SharedConverter = value;
        }

        public SharedFields SharedFields
        {
            get => _shareManager.SharedFields;
            set
            {
                _shareManager.SharedFields = value;
                GridChanged();
            }
        }
    }
}
