﻿namespace StageCompanion.Models
{
    public class Invitation : BaseDataModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BandId { get; set; }
        public bool Accepted { get; set; }
    }
}
