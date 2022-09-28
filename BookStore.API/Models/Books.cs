using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
