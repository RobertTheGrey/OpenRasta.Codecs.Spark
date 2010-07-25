namespace OpenRasta.Demo
{
    #region Using Directives

    using OpenRasta.Codecs.Spark.Configuration;
    using OpenRasta.Configuration;
    using OpenRasta.Demo.Handlers;
    using OpenRasta.Demo.Resources;
    using OpenRasta.Web.UriDecorators;

    #endregion

    public class Configuration : IConfigurationSource
    {
        public void Configure()
        {
            // OR resources config has to be done in the scope below, afterwards it's made immutable/read-only
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Uses.UriDecorator<HttpMethodOverrideUriDecorator>();
                ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();
                ResourceSpace.Uses.SparkCodec();

                ResourceSpace.Has.ResourcesOfType<Home>()
                    .AtUri("/home")
                    .AndAt("/")
                    .HandledBy<HomeHandler>()
                    .AndRenderedBySpark("home.spark");

                ResourceSpace.Has.ResourcesOfType<ShoppingList>()
                    .AtUri("/shoppinglist")
                    .HandledBy<ShoppingListHandler>()
                    .AndRenderedBySpark("shoppingList.spark");

                ResourceSpace.Has.ResourcesOfType<ShoppingListItem>()
                    .AtUri("/shoppinglistitem/{description}")
                    .HandledBy<ShoppingListItemHandler>()
                    .AndRenderedBySpark("shoppinglistitem.spark");
            }
        }
    }
}