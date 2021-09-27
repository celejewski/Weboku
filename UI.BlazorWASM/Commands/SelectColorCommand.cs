using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Enums;

namespace Weboku.UserInterface.Commands
{
    public class SelectColorCommand : ICommand
    {
        private readonly Color _color;
        private readonly DomainFacade _domainFacade;

        public SelectColorCommand(Color color, DomainFacade domainFacade)
        {
            _color = color;
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.SelectPrimaryColor(_color);
            _domainFacade.SelectSecondaryColor(_color);
            return Task.CompletedTask;
        }
    }
}