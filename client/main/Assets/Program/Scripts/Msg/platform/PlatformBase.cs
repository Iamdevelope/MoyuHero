using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameEventSystem;
using LitJson;

namespace Platform
{
    public abstract class PlatformBase
	{
        public int m_Type = 0;

        public PlatformBase(int Type)
        {
            m_Type = Type;
        }
        public abstract void marshal(ref JsonData _os_);
        public abstract void unmarshal(JsonData _os_);
        public abstract void Process();

        public static PlatformBase JsonDataObject(int nType)
        {
            switch (nType)
            {
                case 10002:
                    return new SGuest();
                case 10012:
                    return new SExchange();
                default:
                    return null;
            }

        }
	}	
}
