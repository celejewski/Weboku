﻿using Core.Data;
using System.Threading.Tasks;

namespace Core.Generators
{
    public abstract class BaseGridGenerator : IGridGenerator
    {
        protected readonly IEmptyGridGenerator _emptyGridGenerator;

        protected BaseGridGenerator(IEmptyGridGenerator emptyGridGenerator) 
        {
            _emptyGridGenerator = emptyGridGenerator;
        }

        public IGridV2 Empty() => _emptyGridGenerator.Empty();
        public abstract Task<IGridV2> WithGiven(string difficulty);
    }
}
