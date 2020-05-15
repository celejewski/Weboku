using Core.Data;

namespace UI.BlazorWASM.Managers
{
    public interface IGridHistoryManager
    {
        void Save();
        void Undo();
        void Redo();
        void ClearUndo();
        void ClearRedo();

        bool CanUndo { get; }
        bool CanRedo { get; }
    }
}
