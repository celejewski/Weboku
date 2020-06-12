using Core.Converters;
using Core.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Core.Generators
{
    public class RESTGridGeneratorV2
    {
        private readonly HttpClient _http;
        private readonly HodokuGridConverterV2 _converter = new HodokuGridConverterV2(); 

        public RESTGridGeneratorV2(HttpClient http)
        {
            _http = http;
        }

        public async Task<IGridV2> WithGiven(string difficulty)
        {
            try
            {
                var sudoku = await _http.GetFromJsonAsync<Sudoku>($"http://andzej-002-site2.ftempurl.com/sudokugenerator/{difficulty}");
                return _converter.FromText(sudoku.Given);
            }
            catch
            {
                return new GridV2();
            }
        }
    }
}
