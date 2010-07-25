namespace OpenRasta.Demo.Handlers
{
    #region Using Directives

    using System.Linq;

    using OpenRasta.Data;
    using OpenRasta.Demo.Resources;
    using OpenRasta.Web;

    #endregion

    public class ShoppingListItemHandler
    {
        public ShoppingListItem Get(string description)
        {
            return this.GetItem(description);
        }

        private ShoppingListItem GetItem(string description)
        {
            return ShoppingListHandler.ShoppingList.Where(x => x.Description.ToUpper() == description.ToUpper()).FirstOrDefault();
        }

        public OperationResult Post(string description, ChangeSet<ShoppingListItem> changes)
        {
            var item = this.GetItem(description);
            changes.Apply(item);

            return new OperationResult.SeeOther { RedirectLocation = item.CreateUri() };
        }
    }
}
