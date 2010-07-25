namespace OpenRasta.Codecs.Spark.Extensions
{
    using System.Collections.Generic;

    using global::Spark.Parser.Markup;

    internal interface IReplacement
    {
        void DoReplace(ElementNode node, IList<Node> body);
    }
}