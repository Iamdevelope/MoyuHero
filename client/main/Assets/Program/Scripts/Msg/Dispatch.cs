using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;
using DreamFaction.UI;

namespace GNET
{
	public partial class Dispatch: Protocol
	{

        public HashSet<int> linksids;
        public int ptype;
        public Octets pdata;

        public const int PROTOCOL_TYPE = 65540;

        public Dispatch()
            : base(PROTOCOL_TYPE)
		 {
             linksids = new HashSet<int>();
             ptype = 0;
		 } 

		public override object Clone()
		{
            Dispatch obj = new Dispatch();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.compact_uint32(linksids.Count);
            foreach (var _v_ in linksids)
            {
			    os.marshal(_v_);
		    }
            os.marshal(ptype);
            os.marshal(pdata);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            for (int _size_ = os.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = os.unmarshal_int();
                linksids.Add(_v_);
            }

            ptype = os.unmarshal_int();
            pdata = os.unmarshal_Octets();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 65536; }

        public override void Process() 
        {
            Protocol stub = Protocol.Create(ptype);
            if (stub == null)
            {
                // 通知UI显示错误提示信息
                Debug.LogError("没有定义消息包[type:"+ ptype + "]");
                return;
            }
            try
            {
                OctetsStream octstram = OctetsStream.wrap(this.pdata);
                stub.unmarshal(octstram);
                stub.Process();
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.ToString());
            }
            finally
            {
                // 关闭菊花
                IOControler.GetInstance().stopLoading();
            }
        }

	}	
}
