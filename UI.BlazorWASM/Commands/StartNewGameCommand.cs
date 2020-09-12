﻿using Core;
using Core.Data;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameCommand : ICommand
    {
        private readonly Difficulty _difficulty;
        private readonly DomainFacade _domainFacade;
        private readonly StartGameCommand _startGameCommand;

        public StartNewGameCommand(
            Difficulty difficulty,
            DomainFacade domainFacade,
            StartGameCommand startGameCommand)
        {
            _difficulty = difficulty;
            _domainFacade = domainFacade;
            _startGameCommand = startGameCommand;
        }

        public async Task Execute()
        {
            _domainFacade.StartNewGame(_difficulty);
            await _startGameCommand.Execute();
        }
    }
}
