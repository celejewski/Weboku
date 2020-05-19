﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints
{
    public class PrintTechniqueNameHandler : HintHandler
    {
        public override void Execute(string step, IEnumerator<string> enumerator)
        {
            Console.WriteLine(step + " PrintTechniqueName ");
            _next?.Execute(step, enumerator);
        }
    }
}
