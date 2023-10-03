using System.ComponentModel.DataAnnotations.Schema;

namespace AutoComplete.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public State state { get; set; }
    }
}
