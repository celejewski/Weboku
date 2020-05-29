using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Providers
{
    public interface IProvider
    {
        event Action OnChanged;
    }
}
