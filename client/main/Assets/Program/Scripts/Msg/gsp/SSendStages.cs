using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;

namespace GNET
{
	public partial class SSendStages: Protocol
	{

        public const int PROTOCOL_TYPE = 787934;

        public LinkedList<StageInfo> stages;

        public SSendStages()
            : base(PROTOCOL_TYPE)
        {
            stages = new LinkedList<StageInfo>();
        }

		public override object Clone()
		{
            SSendStages obj = new SSendStages();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{

            _os_.compact_uint32(stages.Count);
            LinkedListNode<StageInfo> firstNode = stages.First;
            while (firstNode != null)
            {
                _os_.marshal(stages.First.Value);

                stages.RemoveFirst();
                firstNode = stages.First;
            }
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{

            for (int size = _os_.uncompact_uint32(); size > 0; --size)
            {
                StageInfo _v_ = new StageInfo();
                _v_.unmarshal(_os_);
                stages.AddFirst(_v_);
            }

            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size < 0 || size <= 65535; }

        public override void Process() 
        {
            BattleStageMgr pStageData = ObjectSelf.GetInstance().BattleStageData;
            LogManager.LogToFile("start chapter ...");
            foreach (StageInfo item in stages)
            {
                LogManager.LogToFile("start chapter  : "+item.id);
                if (pStageData.m_BattleStageList.ContainsKey(item.id))
                {
                    //初始化关卡数据的章节ID不能重复 [4/7/2015 Zmy]
                    continue;
                    //LogManager.LogError("!!!!!!!!!Error:Init Stages Picec Data Key repeat");
                }
                else
                {
                    pStageData.CopyBattleStageData(item);
                }
                
            }

        }
	}	
}
