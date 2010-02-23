﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SyncButler.ProgramEnvironment
{
    class SettingsConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("allowAutoSyncForConflictFreeTasks")]
        public bool AllowAutoSyncForConflictFreeTasks
        {
            get
            {
                return (bool) this["allowAutoSyncForConflictFreeTasks"];
            }
            set
            {
                this["allowAutoSyncForConflictFreeTasks"] = value;
            }
        }
    }
}
