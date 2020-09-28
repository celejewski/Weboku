using Core.Data;

namespace Application.Data
{
    public class StorageDto
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
