namespace OpenRasta.Codecs.Spark.Extensions
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Text;

    using global::Spark;
    using global::Spark.Compiler;
    using global::Spark.Compiler.ChunkVisitors;
    using global::Spark.Compiler.NodeVisitors;
    using global::Spark.Parser.Markup;

    #endregion

    internal class SparkExtension : ISparkExtension
    {
        private readonly IEnumerable<IReplacement> replacements;

        public SparkExtension(ElementNode node, IEnumerable<IReplacement> replacements)
        {
            this.replacements = replacements;
            this.Node = node;
        }

        public ElementNode Node { get; private set; }

        public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
        {
            this.Transform(this.Node, body);

            visitor.Accept(this.Node);
            visitor.Accept(body);

            if (!this.Node.IsEmptyElement)
            {
                visitor.Accept(new EndElementNode(this.Node.Name));
            }
        }

        public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
        {
            visitor.Accept(body);
        }

        protected void Transform(ElementNode elementNode, IList<Node> body)
        {
            foreach (IReplacement replacement in replacements)
            {
                replacement.DoReplace(elementNode, body);
            }
        }
    }
}