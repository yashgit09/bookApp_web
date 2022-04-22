using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using schema = Schema.NET;
namespace BookTrackingApplication.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }

        public string CategoryTypeNameToken { get; set; }

        public CategoryType CategoryType { get; set; }

       
    }
}
