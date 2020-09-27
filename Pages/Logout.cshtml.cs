using AWO_Orders.Models;

namespace AWO_Orders.Pages
{
    public class LogoutModel : BasePageModel
    {
        public void OnGet()
        {
            Logout();
        }
    }
}