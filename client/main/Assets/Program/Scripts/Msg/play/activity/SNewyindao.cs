using DreamFaction.GameEventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GNET
{
	public partial class SNewyindao: Protocol
	{
        public const int END_OK = 1; // ³É¹¦
        public const int END_ERROR = 2; // Ê§°Ü

	    public int result;
        public int num;

        public const int PROTOCOL_TYPE = 789045;

        public SNewyindao()
            : base(PROTOCOL_TYPE)
		 {
		 } 

		public override object Clone()
		{
            SNewyindao obj = new SNewyindao();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(num);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            if(result == END_OK)
            {
                if (num != 300103)
                    GuideManager.GetInstance().guideIDList.Add(num);    
            }
            else if(result == END_ERROR)
            {
                Debug.Log("SNewyindao " + num.ToString() + "faile");
            }
        }
	}	
}
