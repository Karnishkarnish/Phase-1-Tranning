using System.ComponentModel.DataAnnotations;

namespace Day13api.Models
{
    public class StateInfo
    {
        [Key]
        public int Id { get; set; }
        public string? State { get; set; }
        public string? ChiefMinister { get; set; }
        public string? Language { get; set; }
    }
}
