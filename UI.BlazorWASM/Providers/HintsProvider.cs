﻿using Core;
using System;
using UI.BlazorWASM.Hints;
using UI.BlazorWASM.Hints.SolvingTechniqueDisplayers;

namespace UI.BlazorWASM.Providers
{
    public class HintsProvider : IProvider
    {
        private readonly Informer _informer;
        private readonly Displayer _displayer;
        private readonly DomainFacade _domainFacade;

        public event Action OnChanged;

        public HintsState State { get; private set; }
        public void SetState(HintsState value)
        {
            State = value;
            OnChanged?.Invoke();
        }

        public bool HasExplanation => _currentTechnique.HasExplanation;
        public bool HasNextExplanation => _currentTechnique.HasNextExplanation;
        public bool HasPreviousExplanation => _currentTechnique.HasPreviousExplanation;

        private ISolvingTechniqueDisplayer _currentTechnique;
        private ISolvingTechniqueDisplayer GetNextTechnique()
        {
            return DisplayTechniqueFactory.MakeDisplayer(_informer, _displayer, _domainFacade.GetNextHint());
        }

        public HintsProvider(Informer informer, Displayer displayer, DomainFacade domainFacade)
        {
            _informer = informer;
            _displayer = displayer;
            _domainFacade = domainFacade;
        }


        public void ShowHint()
        {
            _displayer.Clear();
            GetNextTechnique().DisplayHint();
            _displayer.Show();
            SetState(HintsState.ShowHint);
        }

        public void ShowNextStep()
        {
            _currentTechnique = GetNextTechnique();
            _displayer.Clear();
            _currentTechnique.DisplaySolution();
            _displayer.Show();
            SetState(HintsState.ShowNextStep);
        }

        public void ShowExplanation()
        {
            _currentTechnique.DisplayExplanation();
            SetState(HintsState.ShowExplanation);
        }

        public void ShowNextExplanation()
        {
            _currentTechnique.NextExplanation();
            ShowExplanation();
        }

        public void ShowPreviousExplanation()
        {
            _currentTechnique.PreviousExplanation();
            ShowExplanation();
        }

        public void Execute()
        {
            _domainFacade.ExecuteNextHint();
            _displayer.Hide();
            SetState(HintsState.ShowEmpty);
        }

        public void Close()
        {
            _displayer.Clear();
            _displayer.Hide();
            SetState(HintsState.Hide);
        }

    }
}
