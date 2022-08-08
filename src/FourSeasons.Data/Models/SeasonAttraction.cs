using System.ComponentModel.DataAnnotations.Schema;

namespace FourSeasons.Data.Models
{
    public class SeasonAttraction
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Season))]
        public Guid SeasonId { get; set; }
        public virtual Season Season { get; set; }
        [ForeignKey(nameof(Attraction))]
        public Guid AttractionId { get; set; }
        public virtual Attraction Attraction { get; set; }
    }
}
