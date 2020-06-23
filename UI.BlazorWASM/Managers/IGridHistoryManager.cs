using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Managers
{
    public interface IGridHistoryManager : IProvider
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
