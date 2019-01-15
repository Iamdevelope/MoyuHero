using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
	public partial class SOpenMohe: Protocol
	{

        public const int END_OK = 1; // 成功
	    public const int END_ERROR = 2; // 失败

	    public int endtype;
        public int moheid; // 成功开启的魔盒ID
        public Hashtable moheshop; // 魔盒列表



        public const int PROTOCOL_TYPE = 787947;

        public SOpenMohe()
            : base(PROTOCOL_TYPE)
		 {
             endtype = 0;
             moheid = 0;
             moheshop = new Hashtable();

		 } 

		public override object Clone()
		{
            SOpenMohe obj = new SOpenMohe();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(endtype);
            _os_.marshal(moheid);

            _os_.compact_uint32(moheshop.Count);  //魔盒
            foreach (DictionaryEntry de in moheshop)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((Mohe)de.Value);
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            endtype = _os_.unmarshal_int();
            moheid = _os_.unmarshal_int();

            //魔盒
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                key = _os_.unmarshal_int();

                Mohe _v_ = new Mohe();
                _v_.unmarshal(_os_);
                moheshop.Add(key, _v_);
            }

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
            if (endtype == END_OK)
            {
                int count = 0;
                UI_SealBox.Inst.MoheListClear();
                int nCount = DataTemplate.GetInstance().m_BossboxTable.getDataCount();
                for (int i = 1; i <= nCount; i++)
                {
                    if (moheshop.ContainsKey(i))
                    {
                        Mohe data = moheshop[i] as Mohe;
                        UI_SealBox.Inst.MoheListAdd(data);
                        if (data.place == 0)
                        {
                            count += 1;
                        }
                    }
                }
                UI_SealBox.Inst.SetCurOpenNum(count);
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_SealBox);
                
                //if (moheshop.ContainsKey(moheid))
                //{
                //    Mohe data = moheshop[moheid] as Mohe;
                //    if (data != null && data.isopen == 1)
                //    {
                //        UI_SealBox._inst.SetCurMohe(data);
                //    }
                //    if (data.place == 0)
                //    {
                //        count += 1;
                //    }
                //}

            }
            else
            {
                
            }
        }
	}	
}
