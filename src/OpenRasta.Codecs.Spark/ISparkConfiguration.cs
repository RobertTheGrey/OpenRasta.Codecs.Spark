namespace OpenRasta.Codecs.Spark
{
    using global::Spark;

    public interface ISparkConfiguration
    {
        ISparkServiceContainer Container { get; }
    }
}