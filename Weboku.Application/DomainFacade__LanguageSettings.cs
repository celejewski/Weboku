using System;
using System.Globalization;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private const string LanguageNameKey = "LanguageName";

        private void TryRestoreLanguageName()
        {
            if (!_storageProvider.HasKey(LanguageNameKey)) return;

            var name = _storageProvider.Load<string>(LanguageNameKey);
            if (string.IsNullOrEmpty(name)) return;

            SetLanguage(name);
        }


        public event Action OnLanguageChanged;

        public void SetLanguage(string name)
        {
            _storageProvider.Save(LanguageNameKey, name);
            var cultureInfo = new CultureInfo(name);
            LanguageContainerService.SetLanguage(cultureInfo);
            OnLanguageChanged?.Invoke();
        }
    }
}