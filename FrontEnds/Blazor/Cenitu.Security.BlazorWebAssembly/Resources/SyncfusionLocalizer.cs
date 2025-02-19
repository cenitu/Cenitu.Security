﻿using Syncfusion.Blazor;

namespace Cenitu.Security.BlazorWebAssembly.Resources
{
    public class SyncfusionLocalizer : ISyncfusionStringLocalizer
    {
        public string GetText(string key)
        {
            return this.ResourceManager.GetString(key);
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                // Replace the ApplicationNamespace with your application name.
                return SfResources.ResourceManager;

                //For .Net Maui Blazor App
                // Replace the ApplicationNamespace with your application name.
                //return ApplicationNamespace.LocalizationResources.SfResources.ResourceManager;
            }
        }
    }
}
