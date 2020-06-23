﻿using AKSoftware.Localization.MultiLanguages;

namespace UI.BlazorWASM.Helpers
{
    public static class LanguageContainerExtensions
    {
        public static string Format(this ILanguageContainerService LanguageContainer, string key, params string[] args)
        {
            return string.Format(LanguageContainer.Keys[key], args);
        }
    }
}
