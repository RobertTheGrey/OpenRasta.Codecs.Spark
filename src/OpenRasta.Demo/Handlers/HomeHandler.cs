namespace OpenRasta.Demo.Handlers
{
    using OpenRasta.Demo.Resources;

    public class HomeHandler
    {
        public Home Get()
        {
            return new Home { Title = "Shopping List tool", Strapline = "Because a pen and paper would be too simple..." };
        }
    }
}