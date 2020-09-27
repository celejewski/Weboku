using Core.Data;

namespace Application.Data
{
    public class StorageDto
    {
        public StorageDto(IGrid grid, Difficulty difficulty)
        {
            Grid = grid;
            Difficulty = difficulty;
        }

        public IGrid Grid { get; }
        public Difficulty Difficulty { get; }
    }
}
