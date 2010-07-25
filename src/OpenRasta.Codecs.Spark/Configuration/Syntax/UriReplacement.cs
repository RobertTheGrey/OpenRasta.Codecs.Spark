namespace OpenRasta.Codecs.Spark.Extensions
{
    #region Using Directives

    using System.Collections.Generic;
    using global::Spark.Parser.Markup;
    using OpenRasta.Codecs.Spark.Extensions.Specifications;

    #endregion

    internal class UriReplacement : SpecifiedReplacement
    {
        public UriReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
        {
        }

        public override void DoReplace(ElementNode node, IList<Node> body)
        {
            this.RemoveReplacedAttribute(node);
            Node uriSnippet = this.RemoteAndEvalUriSnippet(node);

            node.Attributes.Add(
                new AttributeNode(
                    ReplacementSpecification.NewAttributeName,
                    new List<Node> { uriSnippet }));
        }

        private Node RemoteAndEvalUriSnippet(ElementNode elementNode)
        {
            string attribValue = elementNode.GetAttributeValue(ReplacementSpecification.OriginalAttributeName);
            elementNode.RemoveAttributesByName(ReplacementSpecification.OriginalAttributeName);
            
            return attribValue.GetCreateUriSnippet(this.IsTypeReplacement());
        }

        private bool IsTypeReplacement()
        {
            return ReplacementSpecification.OriginalAttributeName.ToUpper() == "TOTYPE";
        }

        private void RemoveReplacedAttribute(ElementNode elementNode)
        {
            elementNode.RemoveAttributesByName(ReplacementSpecification.NewAttributeName);
        }
    }
}