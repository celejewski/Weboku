using Core.Converters;
using Core.Data;
using Core.Generators;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Services
{
    public class RESTGridGenerator : BaseGridGenerator
    {
        private readonly HttpClient _http;
        private readonly HodokuGridConverter _converter;

        public RESTGridGenerator(HttpClient http, HodokuGridConverter converter, IEmptyGridGenerator emptyGridGenerator)
            :base(emptyGridGenerator)
        {
            _http = http;
            _converter = converter;
        }

        public override async Task<IGrid> WithGiven(string difficulty)
        {
            try
            {
                var sudoku = await _http.GetFromJsonAsync<Sudoku>($"http://andzej-002-site2.ftempurl.com/sudokugenerator/{difficulty}");
                return _converter.FromText(sudoku.Given);
            }
            catch
            {
                return _emptyGridGenerator.Empty();
            }
        }
    }
}
