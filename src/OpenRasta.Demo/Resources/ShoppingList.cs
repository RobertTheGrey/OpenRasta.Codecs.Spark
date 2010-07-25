namespace OpenRasta.Demo.Resources
{
    using System.Collections;
    using System.Collections.Generic;

    public class ShoppingList : IEnumerable<ShoppingListItem>
    {
        private readonly List<ShoppingListItem> list = new List<ShoppingListItem>();

        public IEnumerator<ShoppingListItem> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(ShoppingListItem item)
        {
            this.list.Add(item);
        }
    }
}
