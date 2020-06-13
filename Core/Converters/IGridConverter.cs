using Core.Data;

namespace Core.Converters
{
    public interface IGridConverter
    {
        string ToText(IGridV2 grid);
        IGridV2 FromText(string text);
        bool IsValidText(string text);
    }
}
