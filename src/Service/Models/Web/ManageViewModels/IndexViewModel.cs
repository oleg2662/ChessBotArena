namespace BoardGame.Service.Models.Web.ManageViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string StatusMessage { get; set; }

        public bool Bot { get; set; }
    }
}
