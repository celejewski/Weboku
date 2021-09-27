using Weboku.Core.Data;

namespace Weboku.Core.Serializers
{
    public interface IGridSerializer
    {
        string Serialize(Grid grid);
        Grid Deserialize(string text);
        bool IsValidFormat(string text);
    }
}