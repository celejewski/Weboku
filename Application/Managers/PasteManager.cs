using Core.Data;
using Core.Serializers;

namespace Application.Managers
{
    public class PasteManager
    {
        public PasteManager()
        {
            Pasted = new string('0', 81);
        }

        private readonly IGridSerializer _gridSerializer = GridSerializerFactory.Make(GridSerializerName.Default);
        private string _pasted;
        public string Pasted
        {
            get => _pasted;
            set
            {
                _pasted = value;
                IsValid = _gridSerializer.IsValidFormat(_pasted);
                Grid = IsValid
                    ? _gridSerializer.Deserialize(Pasted)
                    : new Grid();
            }
        }

        public bool IsValid { get; private set; }
        public IGrid Grid { get; private set; }
    }
}
