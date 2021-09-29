using Weboku.Core.Data;
using Weboku.Core.Serializers;
using Weboku.Core.Validators;

namespace Weboku.Application.Managers
{
    internal sealed class PasteManager
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