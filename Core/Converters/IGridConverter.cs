using Core.Data;

namespace Core.Converters
{
    interface IGridConverter
    {
        string ToText(Grid grid, GridConverterFormat format);
        Grid FromText(string text);
    }
}
