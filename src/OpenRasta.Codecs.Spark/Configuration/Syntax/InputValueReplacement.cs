namespace OpenRasta.Codecs.Spark.Extensions
{
    #region Using Directives

    using System.Collections.Generic;

    using global::Spark.Parser.Markup;
    
    using OpenRasta.Codecs.Spark.Extensions.Specifications;

    #endregion

    internal class InputValueReplacement : SpecifiedReplacement
    {
        public InputValueReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
        {
        }

        public override void DoReplace(ElementNode node, IList<Node> body)
        {
            AttributeNode forAttribute = node.GetAttribute(ReplacementSpecification.OriginalAttributeName);
            AddAttribute(node, "value", forAttribute.Value.GetPropertyValueNode());
        }
    }
}