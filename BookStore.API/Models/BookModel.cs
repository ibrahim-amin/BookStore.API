using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Title")]

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
