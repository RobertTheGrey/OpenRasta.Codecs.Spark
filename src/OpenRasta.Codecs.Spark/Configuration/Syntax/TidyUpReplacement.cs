namespace OpenRasta.Codecs.Spark.Extensions
{
    #region Using Directives

    using System.Collections.Generic;
    using global::Spark.Parser.Markup;
    using OpenRasta.Codecs.Spark.Extensions.Specifications;

    #endregion

    internal class TidyUpReplacement : SpecifiedReplacement
    {
        public TidyUpReplacement(ReplacementSpecification replacementSpecification)
            : base(replacementSpecification)
        {
        }

        public override void DoReplace(ElementNode node, IList<Node> body)
        {
            node.RemoveAttributesByName(ReplacementSpecification.OriginalAttributeName);
        }
    }
}