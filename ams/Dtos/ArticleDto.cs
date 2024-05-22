using ams.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ams.Dtos
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProviderId { get; set; }
      
    }
}
