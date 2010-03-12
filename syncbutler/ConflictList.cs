﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncButler
{
    public class ConflictList
    {
        public List<Conflict> conflicts { get; set; }
        public string PartnerShipName { get; set; }

        public ConflictList(List<Conflict> conflicts, string PartnerShipName)
        {
            this.conflicts = conflicts;
            this.PartnerShipName = PartnerShipName;
        }
    }
}
