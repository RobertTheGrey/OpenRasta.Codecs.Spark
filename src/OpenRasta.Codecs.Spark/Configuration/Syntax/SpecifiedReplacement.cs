namespace OpenRasta.Codecs.Spark.Extensions
{
    #region Using Directives

    using System.Collections.Generic;
    using global::Spark.Parser.Markup;
    using OpenRasta.Codecs.Spark.Extensions.Specifications;

    #endregion

    internal abstract class SpecifiedReplacement : IReplacement
    {
        private readonly ReplacementSpecification replacementSpecification;

        protected SpecifiedReplacement(ReplacementSpecification replacementSpecification)
        {
            this.replacementSpecification = replacementSpecification;
        }

        protected ReplacementSpecification ReplacementSpecification
        {
            get { return this.replacementSpecification; }
        }

        public abstract void DoReplace(ElementNode node, IList<Node> body);

        protected static void AddAttribute(ElementNode elementNode, string attributeName, Node childNode)
        {
            elementNode.RemoveAttributesByName(attributeName);
            elementNode.Attributes.Add(
                new AttributeNode(
                    attributeName,
                    new List<Node> { childNode }));
        }
    }
}