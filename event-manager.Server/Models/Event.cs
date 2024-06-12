using System.ComponentModel.DataAnnotations;

namespace EventStore.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}