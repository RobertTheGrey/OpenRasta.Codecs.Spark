namespace OpenRasta.Codecs.Spark.Extensions
{
    #region Using Directives

    using System.Collections.Generic;
    using global::Spark.Parser.Markup;
    using OpenRasta.Codecs.Spark.Extensions.Specifications;

    #endregion

    internal class TextAreaValueReplacement : SpecifiedReplacement
    {
        public TextAreaValueReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
        {
        }

        public override void DoReplace(ElementNode node, IList<Node> body)
        {
            body.Clear();
            Node newBody = node.GetAttribute("for").Value.GetPropertyValueNode();
            body.Add(newBody);
        }
    }
}