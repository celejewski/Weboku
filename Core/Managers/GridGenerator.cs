using Core.Data;
using Core.Exceptions;
using Core.Serializers;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Core.Managers
{
    internal static class GridGenerator
    {
        public static async Task<IGrid> Make(Difficulty difficulty)
        {
            if( difficulty == Difficulty.Unknown )
            {
                throw new SudokuCoreException($"Can not use difficulty = {difficulty} to create make new grid.");
            }

            using HttpClient _httpClient = new HttpClient();
            try
            {
                var sudoku = await _httpClient.GetFromJsonAsync<Sudoku>($"http://andzej-002-site2.ftempurl.com/sudokugenerator/{difficulty}");
                var serializer = new HodokuGridSerializer();
                return serializer.Deserialize(sudoku.Given);
            }
            catch( Exception ex )
            {
                throw new SudokuCoreException($"Failed to make grid with difficulty = {difficulty}.", ex);
            }
        }
    }
}
