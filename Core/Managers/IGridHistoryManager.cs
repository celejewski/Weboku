using Core.Data;

namespace Core.Managers
{
    public interface IGridHistoryManager
    {
        void AttachTo(IGrid grid);
        void Save();
        void Undo();
        void Redo();
        void ClearUndo();
        void ClearRedo();

        bool IsAttached { get; }
        bool CanUndo { get; }
        bool CanRedo { get; }
    }
}
