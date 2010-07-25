namespace OpenRasta.Codecs.Spark
{
    #region Using Directives

    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using global::Spark;
    using OpenRasta.Collections.Specialized;
    using OpenRasta.DI;
    using OpenRasta.Diagnostics;
    using OpenRasta.IO;
    using OpenRasta.Web;

    #endregion

    [MediaType("application/xhtml+xml;q=0.9", "xhtml"), MediaType("text/html", "html"),
     MediaType("application/vnd.openrasta.htmlfragment+xml;q=0.5")]
    public class SparkCodec : IMediaTypeWriter
    {
        private static readonly string[] DEFAULT_VIEW_NAMES = new[] { "index", "default", "view", "get" };

        private readonly IRequest request;

        private readonly IDependencyResolver resolver;
        private readonly ISparkConfiguration sparkConfiguration;

        public SparkCodec(IRequest request, ISparkConfiguration sparkConfiguration, IDependencyResolver resolver)
        {
            this.request = request;
            this.resolver = resolver;
            this.sparkConfiguration = sparkConfiguration;
        }

        public ILogger Log { get; set; }

        public IDictionary<string, string> Configuration { get; private set; }

        object ICodec.Configuration
        {
            get
            {
                return this.Configuration;
            }

            set
            {
                if (value != null)
                {
                    this.Configuration = value.ToCaseInvariantDictionary();
                }
            }
        }

        public static string GetViewVPath(IDictionary<string, string> codecConfiguration, string[] codecUriParameters)
        {
            // if no pages were defined, return 501 not implemented
            if (codecConfiguration == null || codecConfiguration.Count == 0)
            {
                return null;
            }

            // if no request parameters, take the default or return null
            if (codecUriParameters == null || codecUriParameters.Length == 0)
            {
                foreach (string defaultViewName in DEFAULT_VIEW_NAMES)
                {
                    if (codecConfiguration.Keys.Contains(defaultViewName))
                    {
                        return codecConfiguration[defaultViewName];
                    }
                }
            }
            else
            {
                string requestParameter = codecUriParameters[codecUriParameters.Length - 1];
                
                if (codecConfiguration.Keys.Contains(requestParameter))
                {
                    return codecConfiguration[requestParameter];
                }
            }
            return null;
        }

        public void WriteTo(object entity, IHttpEntity response, string[] codecParameters)
        {
            var codecParameterList = new List<string>(codecParameters);

            if (!string.IsNullOrEmpty(this.request.UriName))
            {
                codecParameterList.Add(this.request.UriName);
            }

            string templateAddress = GetViewVPath(this.Configuration, codecParameterList.ToArray());
            
            this.RenderTemplate(response, templateAddress, entity);
        }

        private static void RenderToResponse(IHttpEntity response, ISparkView templateBase)
        {
            Encoding targetEncoding = Encoding.UTF8;
            response.ContentType.CharSet = targetEncoding.HeaderName;
            TextWriter writer = null;
            
            bool ownsWriter = false;
            try
            {
                if (response is ISupportsTextWriter)
                {
                    writer = ((ISupportsTextWriter)response).TextWriter;
                }
                else
                {
                    writer = new DeterministicStreamWriter(response.Stream, targetEncoding, StreamActionOnDispose.None);
                    ownsWriter = true;
                }
                templateBase.RenderView(writer);
            }
            finally
            {
                if (ownsWriter)
                {
                    writer.Dispose();
                }
            }
        }

        private void RenderTemplate(IHttpEntity response, string templateAddress, object entity)
        {
            SparkViewDescriptor descriptor = new SparkViewDescriptor().AddTemplate(templateAddress);
            var engine = this.sparkConfiguration.Container.GetService<ISparkViewEngine>();
            var view = (SparkResourceView)engine.CreateInstance(descriptor);
            
            view.ViewData = new ViewData(entity);
            view.Resolver = this.resolver;
            
            try
            {
                RenderToResponse(response, view);
            }
            finally
            {
                engine.ReleaseInstance(view);
            }
        }
    }
}