using Blazored.LocalStorage;
using System.Globalization;
using AKSoftware.Localization.MultiLanguages;

namespace UI.BlazorWASM.Providers
{
    public class SettingsProvider
    {
        private readonly LocalStorageService _localStorageService;
        private readonly ILanguageContainerService _languageContainerService;

        public SettingsProvider(LocalStorageService localStorageService, ILanguageContainerService languageContainerService)
        {
            _localStorageService = localStorageService;
            _languageContainerService = languageContainerService;
            if( !localStorageService.ContainKey("LanguageName") )
            {
                return;
            }
            var name = localStorageService.GetItem<string>("LanguageName");
            if( !string.IsNullOrEmpty(name) )
            {
                SetLanguage(name);
            }
        }

        public CultureInfo CultureInfo
        { 
            get; 
            private set; 
        }

        public void SetLanguage(string name)
        {
            CultureInfo = new CultureInfo(name);
            _localStorageService.SetItem("LanguageName", name);
            _languageContainerService.SetLanguage(CultureInfo);
        }

    }
}
