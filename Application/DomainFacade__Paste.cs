namespace Application
{
    public sealed partial class DomainFacade
    {
        public string Pasted
        {
            get => _pasteManager.Pasted;
            set
            {
                _pasteManager.Pasted = value;
                GridChanged();
            }
        }

        public bool PastedIsValid => _pasteManager.IsValid;
    }
}
