namespace OpenRasta.Codecs.Spark.Configuration
{
    #region Using Directives

    using System.Collections.Generic;

    using global::Spark;
    using global::Spark.FileSystem;

    #endregion

    public class SparkConfiguration : ISparkConfiguration
    {
        private readonly SparkServiceContainer container;

        public SparkConfiguration()
        {
            this.container = new SparkServiceContainer();
            this.container.SetServiceBuilder<ISparkSettings>(c => this.CreateSparkSettings());
            this.container.SetServiceBuilder<ISparkViewEngine>(c => new SparkViewEngine(c.GetService<ISparkSettings>())
                                                                {
                                                                    ExtensionFactory = new OpenRastaSparkExtensionsFactory()
                                                                });
        }

        public ISparkServiceContainer Container
        {
            get { return this.container; }
        }

        private SparkSettings CreateSparkSettings()
        {
            var settings = new SparkSettings()
                .SetPageBaseType(typeof(SparkResourceView).Name)
                .SetAutomaticEncoding(true)
                .AddNamespace("OpenRasta.Web.Markup")
                .AddNamespace("OpenRasta.Web")
                .AddNamespace("OpenRasta.Codecs.Spark")
                .AddNamespace("System.Linq")
                .AddNamespace("OpenRasta.Codecs.Spark.Extensions")
                .AddViewFolder(ViewFolderType.VirtualPathProvider, new Dictionary<string, string> { { "virtualBaseDir", "~/views/" } });
                
            return settings;
        }
    }
}