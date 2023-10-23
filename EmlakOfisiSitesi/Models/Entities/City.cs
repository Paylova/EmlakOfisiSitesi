﻿using EmlakOfisiSitesi.Models.Entities.Comman;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
