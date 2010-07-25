namespace OpenRasta.Demo.Handlers
{
    using OpenRasta.Demo.Resources;
    using OpenRasta.Web;

    public class ShoppingListHandler
    {
        public static readonly ShoppingList ShoppingList = CreateDefault();

        private static ShoppingList CreateDefault()
        {
            var result = new ShoppingList
                {
                    new ShoppingListItem
                        {
                            Description = "Large Eggs",
                            Quantity = "6",
                            Notes = "Free range please!",
                            Image = "/images/eggs.jpg"
                        },
                    new ShoppingListItem { Description = "Pints of Milk", Quantity = "4", Image = "/images/milk.jpg" },
                    new ShoppingListItem { Description = "Bag of Apples", Quantity = "1" },
                    new ShoppingListItem { Description = "Boxes of Wine", Quantity = "4" }
                };

            return result;
        }

        public ShoppingList Get()
        {
            return ShoppingList;
        }

        public OperationResult Post(ShoppingListItem item)
        {
            ShoppingList.Add(item);
            return new OperationResult.SeeOther { RedirectLocation = typeof(ShoppingList).CreateUri() };
        }
    }
}