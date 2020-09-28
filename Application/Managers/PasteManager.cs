using Core.Data;
using Core.Serializers;
using Core.Validators;

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
                var isValidFormat = _gridSerializer.IsValidFormat(_pasted);
                Grid = isValidFormat
                    ? _gridSerializer.Deserialize(Pasted)
                    : new Grid();
                IsValid = isValidFormat && ValidatorGrid.AreAllGivensLegal(Grid);
            }
        }

        public bool IsValid { get; private set; }
        public Grid Grid { get; private set; }
    }
}
