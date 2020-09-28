using Application.Data;

namespace Application
{
    public sealed partial class DomainFacade
    {
        public void Save()
        {
            _storageManager.Save(new StorageDto(_grid, Difficulty));
        }

        public void Load()
        {
            var storageDto = _storageManager.Load();
            _grid = storageDto.Grid;
            Difficulty = storageDto.Difficulty;
            GridChanged();
        }
    }
}
