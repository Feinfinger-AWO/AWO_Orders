using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWO_Orders.Pages
{
    public class InfoModel : PageModel
    {
        private string _subject;
        private string _nextPage;
        private string _parameter;

        public void OnGet(string subject, string nextPage, string paramId)
        {
            _subject = subject;
            _nextPage = nextPage;
            _parameter = paramId;
        }

        public IActionResult OnPost(string nextPage, string parameter)
        {
            if (parameter == null)
            {
                return RedirectToPage(nextPage);
            }
            else
            {
                return RedirectToPage(nextPage, new { id = parameter });
            }
        }

        public string Subject { get => _subject; set => _subject = value; }
        public string NextPage { get => _nextPage; set => _nextPage = value; }
        public string Parameter { get => _parameter; set => _parameter = value; }
    }
}