using System.ComponentModel.DataAnnotations;

namespace day2.Models
{
    public class lab
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string address{ get; set; }
        
       // public virtual ICollection<Author>? Authors { get; set; }

        public virtual ICollection<labAuthor>? labAuthors { get; set; }
    }
}
