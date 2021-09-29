namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionPencilMenuItem : NumpadMenuLabel
    {
        public SelectActionPencilMenuItem(MenuOptionSettings menuOptionSettings)
            : base(menuOptionSettings)
        {
        }

        public string Label => "fas fa-pencil-alt";
        public override string Tooltip => "select-action-pencil__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}