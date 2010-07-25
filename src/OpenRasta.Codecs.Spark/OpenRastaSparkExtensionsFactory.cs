namespace OpenRasta.Codecs.Spark
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using global::Spark;
    using global::Spark.Compiler.NodeVisitors;
    using global::Spark.Parser.Markup;
    using OpenRasta.Codecs.Spark.Extensions;
    using OpenRasta.Codecs.Spark.Extensions.Specifications;

    #endregion

    internal class OpenRastaSparkExtensionsFactory : ISparkExtensionFactory
    {
        public ISparkExtension CreateExtension(VisitorContext context, ElementNode node)
        {
            IEnumerable<IReplacement> replacements = GetApplicableReplacements(node);
    
            if (replacements.Any())
            {
                return new SparkExtension(node, replacements);
            }
            
            return null;
        }

        private static IEnumerable<IReplacement> GetApplicableReplacements(ElementNode node)
        {
            var result = new List<IReplacement>();
            
            result.AddRange(UriReplacementSpecifications.GetMatching(node));
            result.AddRange(FormReplacementSpecifications.GetMatching(node));
            
            return result;
        }
    }
}