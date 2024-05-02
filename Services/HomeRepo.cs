namespace Nonamii.Services
{
    public class HomeRepo : IHomeRepo
    {
        private readonly ApplicationDbContext _db;

        public HomeRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get a list of ALL MenuItems in table.
        public async Task<List<MenuItem>> GetMenuItems(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            var menuItems = await (from menuItem in _db.MenuItems
                                   join itemSize in _db.MenuItemSizes
                                   on menuItem.Id equals itemSize.MenuItemId
                               where (string.IsNullOrWhiteSpace(searchTerm) || (menuItem != null && menuItem.Name.StartsWith(searchTerm))) && itemSize.SizeId == 1


                               select new MenuItem
                               {
                                   Id = menuItem.Id,
                                   Image = menuItem.Image,
                                   Name = menuItem.Name,
                                   Description = menuItem.Description,
                                   Price = itemSize.Price
                               }
                         ).ToListAsync();

            return menuItems;
        }
    }
}
