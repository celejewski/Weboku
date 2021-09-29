using Weboku.Core.Data;

namespace Weboku.Application.Data
{
    internal sealed class StorageDto
    {
        public StorageDto(Grid grid, Difficulty difficulty)
        {
            Grid = grid;
            Difficulty = difficulty;
        }

        public Grid Grid { get; }
        public Difficulty Difficulty { get; }
    }
}