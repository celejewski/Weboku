using Core.Data;

namespace Core.Converters
{
    interface IGridConverter
    {
        string ToText(IGrid grid, GridConverterFormat format);
        IGrid FromText(string text);
    }
}
