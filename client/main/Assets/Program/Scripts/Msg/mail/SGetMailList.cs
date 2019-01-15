using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
namespace GNET
{
	public partial class SGetMailList: Protocol
	{

        public LinkedList<Mail> maillist;
        public int mailsize; // 从第几个开始往后取20个
        public int mailallsize; // 邮件总数

        public const int PROTOCOL_TYPE = 786934;

        public SGetMailList()
            : base(PROTOCOL_TYPE)
		 {
             maillist = new LinkedList<Mail>();
             mailsize = 0;
             mailallsize = 0;
		 } 

		public override object Clone()
		{
            SGetMailList obj = new SGetMailList();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.compact_uint32(maillist.Count);
            LinkedListNode<Mail> firstNode = maillist.First;
            while (firstNode != null)
            {
                _os_.marshal(maillist.First.Value);

                maillist.RemoveFirst();
                firstNode = maillist.First;
            }
            _os_.marshal(mailsize);
            _os_.marshal(mailallsize);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Mail _v_ = new Mail();
                _v_.unmarshal(_os_);
                maillist.AddFirst(_v_);
            }
            mailsize = _os_.unmarshal_int();
            mailallsize = _os_.unmarshal_int();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            ObjectSelf.GetInstance().GetManager().mailallsize = mailallsize;

            switch(ObjectSelf.GetInstance().CurGetDataType)
            {
                case EM_GETMAIL_TYPE.GETNEW:
                    ObjectSelf.GetInstance().GetManager().CopyInfo(maillist);
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_MailReceiveListData);
                    break;
                case EM_GETMAIL_TYPE.GETMORE:
                    ObjectSelf.GetInstance().GetManager().GetMoreCopyInfo(maillist);
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_MailReceiveMore, maillist.Count);
                    break;
                case EM_GETMAIL_TYPE.GETDEL:
                    ObjectSelf.GetInstance().GetManager().CopyInfo(maillist);
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_MailDel);
                    break;
            }
		}
			
	}	
}