﻿namespace EmlakOfisiSitesi.ViewModels
{
    public class BuildingAgeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public bool IsAndOver { get; set; }
        public bool? IsActive { get; set; }
    }
}
