using Core.Converters;
using Core.Data;
using Core.Generators;
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
                IsValidText = _converter.IsValidText(_pasted);
                Console.WriteLine("{0} {1}", _pasted, IsValidText);
                if( IsValidText )
                {
                    Grid = _converter.FromText(_pasted);
                }
                OnChanged?.Invoke();
            }
        }

        public PasteProvider(HodokuGridConverter hodokuGridConverter)
        {
            _converter = hodokuGridConverter;
            Pasted = new string('0', 81);   
        }

        public event Action OnChanged;
    }
}
