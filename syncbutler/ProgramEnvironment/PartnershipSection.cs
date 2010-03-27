﻿using System;
using System.Collections.Generic;
using System.Configuration;

namespace SyncButler.ProgramEnvironment
{
    /// <summary>
    /// This stores the list of partnerships, in XML descriptor form
    /// </summary>
    public class PartnershipSection : ConfigurationSection
    {
        /// <summary>
        /// This sectional attributes contains all the states related to
        /// Partnerships (particularly holding on to a list of them)
        /// </summary>
        [ConfigurationProperty("partnershipList")]
        public PartnershipCollection PartnershipList
        {
            get
            {
                return ((PartnershipCollection)this["partnershipList"]);
            }
            set
            {
                this["partnershipList"] = value;
            }
        }

        /// <summary>
        /// This sectional attributes contains all the states related to Mini
        /// Partnerships (particularly holding on to a list of them)
        /// </summary>
        [ConfigurationProperty("miniPartnershipList")]
        public PartnershipCollection MiniPartnershipList
        {
            get
            {
                return ((PartnershipCollection)this["miniPartnershipList"]);
            }
            set
            {
                this["miniPartnershipList"] = value;
            }
        }
    }
}
