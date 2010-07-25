using System.Collections.Generic;
using Spark.FileSystem;

namespace OpenRasta.Codecs.Spark.Tests.Stubs
{
    internal class TestingViewFolder : IViewFolder
    {
        public const string SingleTemplateName = "MyTemplate";
        private readonly string templateSource;

        public TestingViewFolder(string templateSource)
        {
            this.templateSource = templateSource;
        }

        public IViewFile GetViewSource(string path)
        {
            if (path == SingleTemplateName)
            {
                return new TestViewFile(this.templateSource);
            }
            return null;
        }

        public IList<string> ListViews(string path)
        {
            return new List<string> { path };
        }

        public bool HasView(string path)
        {
            return path == SingleTemplateName;
        }
    }
}