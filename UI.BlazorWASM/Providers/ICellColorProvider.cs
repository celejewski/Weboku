using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public interface ICellColorProvider : IProvider
    {
        Color GetColor(int x, int y);

        string GetCssClass(int x, int y);
        void SetColor(int x, int y, Color color);
        void ToggleColor(int x, int y, Color color);

        void ClearAll();
    }
}
