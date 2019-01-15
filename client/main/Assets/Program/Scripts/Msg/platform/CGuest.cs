using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameEventSystem;
using LitJson;

namespace Platform
{
    public class CGuest : PlatformBase
	{
        public string device_key;

        public const int PROTOCOL_TYPE = 10001;
        public CGuest()
            : base(PROTOCOL_TYPE)
        {
          
        }

        public override void marshal( ref JsonData _os_ )
        {
            _os_["device_key"] = device_key;
        }

        public override void unmarshal(JsonData _os_) { }
        public override void Process() { }
	}	
}
