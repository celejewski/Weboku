using Core.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Core.Generators
{
    public class RESTSudokuGenerator : ISudokuGenerator
    {
        private readonly HttpClient _http;

        public RESTSudokuGenerator(HttpClient http)
        {
            _http = http;
        }

        public async Task<Sudoku> Generate(string difficulty)
        {
            try
            {
                return await _http.GetFromJsonAsync<Sudoku>($"http://andzej-002-site2.ftempurl.com/sudokugenerator/{difficulty}");
            }
            catch
            {
                return new Sudoku();
            }
        }
    }
}
