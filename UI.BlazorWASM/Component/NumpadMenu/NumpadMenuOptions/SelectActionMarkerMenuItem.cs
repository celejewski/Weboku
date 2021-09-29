namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionMarkerMenuItem : NumpadMenuLabel
    {
        public SelectActionMarkerMenuItem(MenuOptionSettings menuOptionSettings)
            : base(menuOptionSettings)
        {
        }

        public string Label => "fas fa-marker";
        public override string Tooltip => "select-action-marker__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}