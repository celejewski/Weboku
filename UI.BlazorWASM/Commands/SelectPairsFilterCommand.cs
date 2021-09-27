﻿using System.Threading.Tasks;
using Application;
using Application.Filters;

namespace Weboku.UserInterface.Commands
{
    public class SelectPairsFilterCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public SelectPairsFilterCommand(DomainFacade filterProvider)
        {
            _domainFacade = filterProvider;
        }

        public Task Execute()
        {
            _domainFacade.SetFilter(new PairFilter());
            return Task.CompletedTask;
        }
    }
}