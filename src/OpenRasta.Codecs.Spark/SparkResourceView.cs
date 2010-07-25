namespace OpenRasta.Codecs.Spark
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web;
    
    using global::Spark;
    
    using OpenRasta.DI;
    using OpenRasta.Web.Markup;
    using OpenRasta.Web.Markup.Modules;

    #endregion

    public abstract class SparkResourceView : AbstractSparkView, IXhtmlAnchorSite
    {
        private XhtmlAnchor xhtmlAnchor;
        
        public ViewData ViewData { get; set; }

        public IDependencyResolver Resolver { get; set; }

        public IList<Error> Errors { get; set; }

        public IXhtmlAnchor Xhtml
        {
            get
            {
                // todo: where has my user gone?
                if (this.xhtmlAnchor == null)
                {
                    this.xhtmlAnchor = new XhtmlAnchor(
                        this.Resolver, new XhtmlTextWriter(this.Output), () => HttpContext.Current.User);
                }
                return this.xhtmlAnchor;
            }
        }

        public IDisposable scope(IContentModel element)
        {
            return IXhtmlAnchorSiteExtensions.scope(this, element);
        }
    }
}