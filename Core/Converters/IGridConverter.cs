using Core.Data;

namespace Core.Converters
{
    interface IGridConverter
    {
        string ToText(IGrid grid, IncludedFields format);
        IGrid FromText(string text);
    }
}
