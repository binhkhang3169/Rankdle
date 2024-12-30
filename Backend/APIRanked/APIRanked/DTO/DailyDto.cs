using APIRanked.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIRanked.DTO
{
    public class DailyDto
    {
        public int Quiz1 { get; set; }
        public int Quiz2 { get; set; }
        public int Quiz3 { get; set; }
        public int TypeId { get; set; }
        public DateOnly Date { get; set; }

    }
}

