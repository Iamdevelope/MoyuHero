using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
using DreamFaction.GameCore;
using System.Linq;
using DreamFaction;
using DreamFaction.Utils;
namespace GNET
{
	public partial class SGetStageReward: Protocol
	{
        public const int END_OK = 1; // 成功
	    public const int END_ERROR = 2; // 失败

        public int result;
        public LinkedList<int> dropidlist; // 掉落小包ID
        public byte boxnum; // 第几个宝箱，从0开始

        public const int PROTOCOL_TYPE = 787952;

        public SGetStageReward()
            : base(PROTOCOL_TYPE)
		 {
             dropidlist = new LinkedList<int>();
             result = 0;
		 } 

		public override object Clone()
		{
            SGetStageReward obj = new SGetStageReward();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);

            _os_.compact_uint32(dropidlist.Count);
            LinkedListNode<int> firstNode = dropidlist.First;
            while (firstNode != null)
            {
                _os_.marshal(dropidlist.First.Value);

                dropidlist.RemoveFirst();
                firstNode = dropidlist.First;
            }

            _os_.marshal(boxnum);
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                dropidlist.AddFirst(_v_);
            }
            boxnum = _os_.unmarshal_byte();
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            if (result == END_ERROR)
            {
                return;
            }

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_ChapterBoxGot, boxnum);

            List<int> _temp = dropidlist.ToList<int>();
            for (int i = 0; i < _temp.Count; i++)
            {
                InnerdropTemplate _it = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(_temp[i]);
                if (_it == null) continue;
                //新手引导先关
                if (_it.getInnerdropid() == 1211010001)
                {
                    if (_it.getObjectid() == 1400000009)
                    {
                        //新手引导相关 介绍经验结晶用途（强制）
                        if (GuideManager.GetInstance().IsContentGuideID(200501) == false)
                        {
                            GuideManager.GetInstance().ShowGuideWithIndex(200501, ShowNewGuide);
                        }
                    }
                }
            }
		}
        /// <summary>
        /// 立即前往回调
        /// </summary>
        private void ShowNewGuide()
        {
            ObjectSelf.GetInstance().m_NewGuidePath = "UI_Home/UI_HeroInfo_2_2";
            SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
        }
			
	}	
}
