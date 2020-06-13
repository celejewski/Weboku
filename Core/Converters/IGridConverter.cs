using Core.Data;

namespace Core.Converters
{
    public interface IGridConverter
    {
        string ToText(IGrid grid);
        IGrid FromText(string text);
        bool IsValidText(string text);
    }
}
