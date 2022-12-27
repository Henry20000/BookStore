using BookStore.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsApprove { get; set; } = false;
        public AppUser User { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
