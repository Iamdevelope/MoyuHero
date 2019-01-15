using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using UnityEngine;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using DreamFaction;
using DreamFaction.UI.Core;
namespace GNET
{
	public partial class STanXianOther: Protocol
	{
        public const int END_OK = 1; // �ɹ�
	    public const int END_ERROR = 2; // ʧ��

        public int result;
        public int endtype;
        public int tanxianid; // ̽��id
        public LinkedList<int> dropidlist; // ����С��ID

        public const int PROTOCOL_TYPE = 788987;

        public STanXianOther()
            : base(PROTOCOL_TYPE)
		 {
             dropidlist = new LinkedList<int>();
             result = 0;
             endtype = 0;
             tanxianid = 0;
		 } 

		public override object Clone()
		{
            STanXianOther obj = new STanXianOther();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            endtype = _os_.unmarshal_int();
            tanxianid = _os_.unmarshal_int();

            _os_.compact_uint32(dropidlist.Count);
            LinkedListNode<int> firstNode = dropidlist.First;
            while (firstNode != null)
            {
                _os_.marshal(dropidlist.First.Value);

                dropidlist.RemoveFirst();
                firstNode = dropidlist.First;
            }
 
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            endtype = _os_.unmarshal_int();
            tanxianid = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_;
                _v_ = _os_.unmarshal_int();
                dropidlist.AddFirst(_v_);
            }
 
            
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            switch (endtype)
            {
                case CTanXianOther.END_GET: // ��ȡ����
                    if (result == END_OK)
                    {
                        //��ʱ��ôд�����ڵ���ͳһ�Ż�;
                        //foreach (int i in dropidlist)
                        //{
                            //InterfaceControler.GetInst().AddMsgBox("��õ���С��id=" + i);
                        //}
                        UI_HomeControler.Inst.AddUI(UI_NormalDrop.UI_ResPath);
                        UI_NormalDrop.Inst.ShowInnerDrop(new List<int>(dropidlist));
                        DreamFaction.GameEventSystem.GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ExploreTeamGetReward);
                    }
                    else if (result == END_ERROR)
                    {
                    }
                    break;
                case CTanXianOther.END_SPEED: // �������
                    if (result == END_OK)
                    {
                        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("explore_bubble16"));
                        DreamFaction.GameEventSystem.GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ExploreTeamTimeUp, tanxianid);
                    }
                    else if (result == END_ERROR)
                    {
                    }
                    break;
                case CTanXianOther.END_NULL: // �ٻ�
                    if (result == END_OK)
                    {
                        int txId = tanxianid;
                        DreamFaction.GameEventSystem.GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ExploreTeamCallBack, txId);
                    }
                    else if (result == END_ERROR)
                    {
                    }
                    break;
                case CTanXianOther.SREFRESH: // ˢ��
                    if (result == END_OK)
                    {
                        int chapterid = tanxianid;
                        DreamFaction.GameEventSystem.GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_ExploreTeamRefreshTasks, chapterid);
                    }
                    else if (result == END_ERROR)
                    {
                    }
                    break;
                default:
                    break;
            }
		}
			
	}	
}
