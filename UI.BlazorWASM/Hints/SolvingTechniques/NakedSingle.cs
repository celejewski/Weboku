using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedSingle : HintHandler
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly IFilterProvider _filterProvider;
        private readonly NumpadMenuBuilder _numpadMenuBuilder;

        public NakedSingle(ISudokuProvider sudokuProvider, ICellColorProvider cellColorProvider, IFilterProvider filterProvider, NumpadMenuBuilder numpadMenuBuilder)
        {
            _sudokuProvider = sudokuProvider;
            _cellColorProvider = cellColorProvider;
            _filterProvider = filterProvider;
            _numpadMenuBuilder = numpadMenuBuilder;
        }

        // Naked Single: r4c7=1
        // Hidden Single: r9c1=3 
        // Full House: r6c8=1Z
        public override async Task Execute(string step, IEnumerator<string> enumerator)
        {
            if( step.Contains("Naked Single: ")
                || step.Contains("Hidden Single: ")
                || step.Contains("Full House: "))
            {
                var split = step.Split("=");
                var value = int.Parse(split[1]);
                var target = split[0][^4..];
                var technique = split[0].Split(":")[0];

                var x = int.Parse(target[3..4]) - 1;
                var y = int.Parse(target[1..2]) - 1;

                Console.WriteLine($"{technique} target {target} has value {value} {x}, {y}");
                if (_sudokuProvider.Cells[x,y].Input.Value == 0)
                {
                    Console.WriteLine($"{x}, {y}");

                    await _numpadMenuBuilder.SelectValue(value).Execute();
                    _cellColorProvider.SetColor(x, y, Enums.CellColor.Legal);
                    return;
                }
            }
            
            await _next?.Execute(step, enumerator);
        }
    }
}
