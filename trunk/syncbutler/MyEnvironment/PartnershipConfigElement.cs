﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SyncButler.MyEnvironment
{
    public class PartnershipConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("leftUniqueIdent", IsRequired = true)]
        public String LeftUniqueIdent
        {
            get
            {
                return (String) this["leftUniqueIdent"];
            }
            set
            {
                this["leftUniqueIdent"] = value;
            }
        }

        [ConfigurationProperty("leftPath", DefaultValue = "\\")]
        public String LeftPath
        {
            get
            {
                return (String) this["leftPath"];
            }
            set
            {
                this["leftPath"] = value;
            }
        }

        [ConfigurationProperty("rigthUniqueIdent", IsRequired = true)]
        public String RigthUniqueIdent
        {
            get
            {
                return (String) this["rigthUniqueIdent"];
            }
            set
            {
                this["rigthUniqueIdent"] = value;
            }
        }

        [ConfigurationProperty("rightPath", DefaultValue = "\\")]
        public String RightPath
        {
            get
            {
                return (String) this["rightPath"];
            }
            set
            {
                this["rightPath"] = value;
            }
        }

        [ConfigurationProperty("exclusionList")]
        public List<String> ExclusionList
        {
            get
            {
                return (List<String>) this["exclusionList"];
            }
            set
            {
                this["exclusionList"] = value;
            }
        }

        [ConfigurationProperty("inclusionList")]
        public List<String> InclusionList
        {
            get
            {
                return (List<String>) this["inclusionList"];
            }
            set
            {
                this["inclusionList"] = value;
            }
        }

        [ConfigurationProperty("ignoredList")]
        public List<String> IgnoredList
        {
            get
            {
                return (List<String>) this["ignoredList"];
            }
            set
            {
                this["ignoredList"] = value;
            }
        }
    }
}
