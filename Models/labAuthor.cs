using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace day2.Models
{
    public class labAuthor
    {
        [Key]
        public int id {  get; set; }

        [ForeignKey("Author")]
        public int? Authorid { get; set; }

        public Author? Author { get; set; }
        [ForeignKey("lab")]
        public int? labid { get; set; }

        public lab? lab { get; set; }
    }
}
