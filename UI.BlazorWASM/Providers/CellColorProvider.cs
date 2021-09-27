using System.Diagnostics;
using Weboku.Application;
using Weboku.Core.Data;
using Weboku.UserInterface.Converters;

namespace Weboku.UserInterface.Providers
{
    public class CellColorProvider
    {
        private readonly DomainFacade _domainFacade;

        public CellColorProvider(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public string GetCssClass(Position position)
        {
            var cellColor = _domainFacade.GetColor(position);
            var cssClass = CellColorConverter.ToCssClass(cellColor);
            Debug.WriteLine(cssClass);
            return cssClass;
        }
    }
}