namespace OpenRasta.Codecs.Spark.Extensions
{
    using System.Collections.Generic;

    using global::Spark.Parser.Markup;
    
    using OpenRasta.Codecs.Spark.Extensions.Specifications;

    internal class InputNameReplacement : SpecifiedReplacement, IReplacement
    {
        public InputNameReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
        {
        }

        public override void DoReplace(ElementNode node, IList<Node> body)
        {
            AttributeNode forAttribute = node.GetAttribute(ReplacementSpecification.OriginalAttributeName);
            AddAttribute(node, "name", new ExpressionNode(forAttribute.Value.GetPropertyNameSnippet()));
        }
    }
}