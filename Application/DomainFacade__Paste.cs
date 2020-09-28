using Core.Data;
using Core.Serializers;

namespace Application
{
    public sealed partial class DomainFacade
    {

        private string _pasted = new string('0', 81);
        public string Pasted
        {
            get => _pasted;
            set
            {
                _pasted = value;
                PastedIsValid = _defaultSerializer.IsValidFormat(_pasted);
                _pastedGrid = PastedIsValid ? _defaultSerializer.Deserialize(Pasted) : new Grid();
                ValueAndCandidateChanged();
            }
        }

        public bool PastedIsValid;
        private IGrid _pastedGrid = new Grid();

        private readonly IGridSerializer _defaultSerializer = GridSerializerFactory.Make(GridSerializerName.Default);
    }
}
