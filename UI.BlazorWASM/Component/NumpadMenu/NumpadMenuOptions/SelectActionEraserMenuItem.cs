namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionEraserMenuItem : NumpadMenuLabel
    {
        public SelectActionEraserMenuItem(MenuOptionSettings menuOptionSettings)
            : base(menuOptionSettings)
        {
        }

        public string Label => "fas fa-eraser";
        public override string Tooltip => "select-action-eraser__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}