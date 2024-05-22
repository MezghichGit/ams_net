using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ams.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }
    }
}
