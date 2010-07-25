namespace OpenRasta.Codecs.Spark.Extensions.Specifications
{
    using global::Spark.Parser.Markup;

    internal class ReplacementSpecification
    {
        private readonly string elementName;
        private readonly string newAttributeName;
        private readonly string originalAttributeName;

        public ReplacementSpecification(string elementName, string originalAttributeName)
            : this(elementName, originalAttributeName, string.Empty)
        {
        }

        public ReplacementSpecification(string elementName, string originalAttributeName, string newAttributeName)
        {
            this.elementName = elementName;
            this.originalAttributeName = originalAttributeName;
            this.newAttributeName = newAttributeName;
        }

        public string NewAttributeName
        {
            get { return this.newAttributeName; }
        }

        public string OriginalAttributeName
        {
            get { return this.originalAttributeName; }
        }

        public bool IsMatch(ElementNode node)
        {
            return node.IsTag(this.elementName) && node.HasAttribute(this.originalAttributeName);
        }
    }
}