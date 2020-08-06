using Core.Data;

namespace Core.Converters
{
    public interface IGridConverter
    {
        string Serialize(IGrid grid);
        IGrid Deserialize(string text);
        bool IsValidFormat(string text);
    }
}
