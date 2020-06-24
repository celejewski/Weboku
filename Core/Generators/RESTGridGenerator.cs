using Core.Converters;
using Core.Data;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Core.Generators
{
    public class RESTGridGenerator : IGridGenerator
    {
        private readonly HttpClient _http;
        private readonly IEmptyGridGenerator _emptyGridGenerator;
        private readonly IGridConverter _converter;

        public RESTGridGenerator(HttpClient http, IEmptyGridGenerator emptyGridGenerator, HodokuGridConverter converter)
        {
            _http = http;
            _emptyGridGenerator = emptyGridGenerator;
            _converter = converter;
        }

        public IGrid Empty()
        {
            return _emptyGridGenerator.Empty();
        }

        public async Task<IGrid> WithGiven(string difficulty)
        {
            try
            {
                var sudoku = await _http.GetFromJsonAsync<Sudoku>($"http://andzej-002-site2.ftempurl.com/sudokugenerator/{difficulty}");
                return _converter.FromText(sudoku.Given);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Empty();
            }
        }
    }
}
