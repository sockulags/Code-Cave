using lucas_2._0.Models;
using Microsoft.AspNetCore.Mvc;

namespace lucas_2._0.ViewComponents
{
    public class MenuListViewComponent : ViewComponent
    {
        List<MenuList> MenuItems = new List<MenuList>();
        private readonly MenuList _menuList;

        public MenuListViewComponent(MenuList menuList)
        {
            _menuList = menuList;
            MenuItems = _menuList.MenuListItems();
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = MenuItems;
            return await Task.FromResult((IViewComponentResult)View("MenuList", model));
        }
    }
}
