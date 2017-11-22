namespace AnchorTagHelper.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}