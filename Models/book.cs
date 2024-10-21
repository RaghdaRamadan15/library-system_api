using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace day2.Models
{
    public class book
    {
        [Key]
        public int Id { get; set; } 
            public string Title { get; set; }
            public string Isbn { get; set; }
            public int PageCount { get; set; }
          
            public decimal Price { get; set; }
            public string Publisher { get; set; }
        [ForeignKey("Authors")]
        public int? AuthorId{ get; set; }
            public Author? Authors { get; set; }

           

    }
}
