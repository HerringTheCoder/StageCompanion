﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace StageCompanion.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Band
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LeaderId { get; set; }

        public User Leader { get; set; }
    }
}
