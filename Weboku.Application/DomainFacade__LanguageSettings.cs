using System;
using System.Globalization;
using Weboku.Application.Interfaces;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private void TryRestoreLanguageName()
        {
            if (!_storageProvider.HasKey("LanguageName")) return;

            var name = _storageProvider.Load<string>("LanguageName");
            if (string.IsNullOrEmpty(name)) return;

            SetLanguage(name);
        }


        public event Action OnLanguageChanged;

        public void SetLanguage(string name)
        {
            _storageProvider.Save("LanguageName", name);
            var cultureInfo = new CultureInfo(name);
            LanguageContainerService.SetLanguage(cultureInfo);
            OnLanguageChanged?.Invoke();
        }
    }
}