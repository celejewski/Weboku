using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Services
{
    public class RESTGridGenerator : IGridGenerator
    {
        private readonly HttpClient _http;

        public RESTGridGenerator(HttpClient http)
        {
            _http = http;
        }

        public async Task<Grid> New(string difficulty)
        {
            try
            {
                var sudoku = await _http.GetFromJsonAsync<Sudoku>($"http://andzej-002-site2.ftempurl.com/sudokugenerator/{difficulty}");
                return new Grid(sudoku.Given);
            }
            catch
            {
                return new Grid();
            }
        }
    }
}
