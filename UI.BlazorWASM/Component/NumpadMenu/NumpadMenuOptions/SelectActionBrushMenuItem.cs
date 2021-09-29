namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionBrushMenuItem : NumpadMenuLabel
    {
        public SelectActionBrushMenuItem(MenuOptionSettings menuOptionSettings)
            : base(menuOptionSettings)
        {
        }

        public string Label => "fas fa-paint-roller";
        public override string Tooltip => "select-action-brush__tooltip";

        public override bool IsSelectable => true;
    }
}