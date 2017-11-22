namespace ValidationTagHelper.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}