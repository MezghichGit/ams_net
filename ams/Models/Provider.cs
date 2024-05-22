using System.ComponentModel.DataAnnotations;

namespace ams.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
