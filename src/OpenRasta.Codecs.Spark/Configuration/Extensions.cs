namespace OpenRasta.Codecs.Spark.Configuration
{
    #region Using Directives

    using OpenRasta.Configuration.Fluent;
    using OpenRasta.DI;

    #endregion

    public static class Extensions
    {
        public static ICodecDefinition AndRenderedBySpark(this ICodecParentDefinition codecParentDefinition, string pageVirtualPath)
        {
            return codecParentDefinition.TranscodedBy<SparkCodec>(new { index = pageVirtualPath });
        }

        public static void SparkCodec(this IUses uses)
        {
            DependencyManager.GetService<IDependencyResolver>().AddDependency(
                                                                    typeof(ISparkConfiguration), 
                                                                    typeof(SparkConfiguration), 
                                                                    DependencyLifetime.Singleton);
        }
    }
}