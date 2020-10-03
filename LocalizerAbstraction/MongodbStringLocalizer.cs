using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using MongoDB.Driver;
using MongodbCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LocalizerAbstraction
{ 
    public class MongodbStringLocalizer : IStringLocalizer
    {
        private readonly string _typeName;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMongoCollection<BsonDocument> _mongoCollection;

        public MongodbStringLocalizer(string typeName, IServiceProvider serviceProvider)
        {
            _typeName = typeName;
            _serviceProvider = serviceProvider;
            var options = serviceProvider.GetService<MongodbLocalizerOptions>();
            //连接数据库
            var client = new MongoClient(options.ConnectionString);
            //获取database
            var mydb = client.GetDatabase(options.DatabaseName);
            //获取collection
            _mongoCollection = mydb.GetCollection<BsonDocument>(options.CollectionName);
        }

        private LocalizedString getLocallizedString(string name, params object[] arguments)
        {
            var filter = Builders<BsonDocument>.Filter;
            var docs = _mongoCollection.Find(filter.Eq("name", name) & filter.Eq("typename", _typeName) & filter.Eq("culture", CultureInfo.CurrentCulture.Name)).FirstOrDefault();
            if (docs != null && docs.Contains("value"))
            {
                var value = docs["value"].AsString;
                return new LocalizedString(name, string.Format(value, arguments));
            }
            return new LocalizedString(name, "");
        }
        public LocalizedString this[string name]
        {
            get
            {
                return getLocallizedString(name);

            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return getLocallizedString(name, arguments);
            }
        }


        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var list = new List<LocalizedString>();
            var filter = Builders<BsonDocument>.Filter;
            var docs = _mongoCollection.Find(filter.Eq("culture", CultureInfo.CurrentCulture.Name) & filter.Eq("typename", _typeName));

            docs.ForEachAsync(s =>
            {
                var localizedString = new LocalizedString(s.GetValue("name").AsString, s.GetValue("value").AsString);
                list.Add(localizedString);
            });
            return list;
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.CurrentCulture = culture;


            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            return new MongodbStringLocalizer(_typeName, _serviceProvider);
        }
    }


}
