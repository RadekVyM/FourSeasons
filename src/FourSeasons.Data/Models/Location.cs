﻿namespace FourSeasons.Data.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public virtual List<Season> Seasons { get; set; }
    }
}
