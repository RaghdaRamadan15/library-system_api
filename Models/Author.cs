using System.ComponentModel.DataAnnotations;

namespace day2.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
        
        public virtual ICollection<book>? Books { get; set; }
        //public  virtual ICollection<lab>? labs{ get; set; }

        public virtual ICollection<labAuthor>? labAuthors { get; set; }

    }

}
