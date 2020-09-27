using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MongodbCore;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LocalizerAbstraction
{

    public class MongodbStringLocalizer : IStringLocalizer
    {

        public MongodbStringLocalizer()
        {

        }
        public LocalizedString this[string name] => new LocalizedString("", "");

        public LocalizedString this[string name, params object[] arguments] => new LocalizedString("", "");

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return new List<LocalizedString>() { new LocalizedString("", "") };
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.CurrentCulture = culture;


            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            return new MongodbStringLocalizer();
        }
    }


}
