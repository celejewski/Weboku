using Core.Data;

namespace Core.Serializers
{
    public interface IGridSerializer
    {
        string Serialize(IGrid grid);
        IGrid Deserialize(string text);
        bool IsValidFormat(string text);
    }
}
