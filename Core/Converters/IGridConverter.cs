using Core.Data;

namespace Core.Converters
{
    public interface IGridConverter
    {
        string ToText(IGrid grid, IncludedFields format);
        IGrid FromText(string text);
    }
}
