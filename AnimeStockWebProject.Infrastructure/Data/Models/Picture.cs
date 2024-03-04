using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class Picture
    {
        public int Id { get; init; }

        [Required]
        public string Path { get; set; } = null!;
        [ForeignKey(nameof(Book))]
        public int? BookId { get; set; }
        public Book? Book { get; set; }
        [ForeignKey(nameof(Game))]
        public int? GameId { get; set; }
        public Game? Game { get; set; }
        public bool IsDeleted { get; set; }

    }
}
