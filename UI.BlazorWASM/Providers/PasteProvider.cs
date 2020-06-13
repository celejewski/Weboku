using Core.Converters;
using Core.Data;
using System;

namespace UI.BlazorWASM.Providers
{
    public class PasteProvider : IProvider
    {
        private readonly IGridConverter _converter;

        public bool IsValidText { get; private set; }
        public IGrid Grid { get; private set; }
        private string _pasted;
        public string Pasted 
        { 
            get => _pasted;
            set
            {
                _pasted = value;
                if( IsValid )
                {
                    Grid = _converter.FromText(_pasted);
                }
                OnChanged?.Invoke();
            }
        }

        public bool IsValid => IsValidText = _converter.IsValidText(_pasted);

        public PasteProvider(ChainGridConverter chainGridConverter)
        {
            _converter = chainGridConverter;
            Pasted = new string('0', 81);   
        }

        public event Action OnChanged;
    }
}
