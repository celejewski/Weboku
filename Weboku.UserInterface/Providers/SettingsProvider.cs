using AKSoftware.Localization.MultiLanguages;
using Blazored.LocalStorage;
using System;
using System.Globalization;

namespace Weboku.UserInterface.Providers
{
    public class SettingsProvider : IProvider
    {
        private readonly ISyncLocalStorageService _localStorageService;
        private readonly ILanguageContainerService _languageContainerService;

        public SettingsProvider(ISyncLocalStorageService localStorageService, ILanguageContainerService languageContainerService)
        {
            _localStorageService = localStorageService;
            _languageContainerService = languageContainerService;
            if (!localStorageService.ContainKey("LanguageName"))
            {
                return;
            }

            var name = localStorageService.GetItem<string>("LanguageName");
            if (!string.IsNullOrEmpty(name))
            {
                SetLanguage(name);
            }
        }

        public CultureInfo CultureInfo { get; private set; }

        public event Action OnHintsChanged;

        public void SetLanguage(string name)
        {
            CultureInfo = new CultureInfo(name);
            _localStorageService.SetItem("LanguageName", name);
            _languageContainerService.SetLanguage(CultureInfo);
            OnHintsChanged?.Invoke();
        }
    }
}