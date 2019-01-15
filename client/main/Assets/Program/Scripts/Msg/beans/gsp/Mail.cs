using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class Mail : Marshal
	{
        public int key; // 邮件唯一ID
        public string sender; // 发送者
        public string title; // 邮件标题
        public string msg; // 消息内容
        public LinkedList<int> innerdropidlist; // 掉落包ID
        public LinkedList<MailItem> items; // 掉落物品（非掉落包内容）
        public long endtime; // 开始时间
        public int isopen;      //个位为是否打开，十位为是否领取 0否，1是
        public LinkedList<string> strlist; // 参数列表

        public Mail()         
        {
            sender = "";
            msg = "";
            title = "";
            items = new LinkedList<MailItem>();
            innerdropidlist = new LinkedList<int>();
            strlist = new LinkedList<string>();
        }

        public Mail(int _key_, String _sender_, String _title_, String _msg_, LinkedList<int> _innerdropidlist_,
            LinkedList<MailItem> _items_, long _endtime_, LinkedList<string> _strlist_)
        {
            this.key = _key_;
            this.sender = _sender_;
            this.title = _title_;
            this.msg = _msg_;
            this.innerdropidlist = _innerdropidlist_;
            this.items = _items_;
            this.endtime = _endtime_;
            this.strlist = _strlist_;
        }

		public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(key);
            _os_.marshal(sender);
            _os_.marshal(title);
            _os_.marshal(msg);

            _os_.compact_uint32(innerdropidlist.Count);
            LinkedListNode<int> firstNode = innerdropidlist.First;
            while (firstNode != null)
            {
                _os_.marshal(innerdropidlist.First.Value);

                innerdropidlist.RemoveFirst();
                firstNode = innerdropidlist.First;
            }

            _os_.compact_uint32(items.Count);
            LinkedListNode<MailItem> firstNode2 = items.First;
            while (firstNode2 != null)
            {
                _os_.marshal(items.First.Value);

                items.RemoveFirst();
                firstNode2 = items.First;
            }

            _os_.marshal(endtime);
            _os_.marshal(isopen);

            _os_.compact_uint32(strlist.Count);
            LinkedListNode<string> firstNode3 = strlist.First;
            while (firstNode3 != null)
            {
                _os_.marshal(strlist.First.Value);

                strlist.RemoveFirst();
                firstNode3 = strlist.First;
            }

            return _os_;

		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            key = _os_.unmarshal_int();
            sender = _os_.unmarshal_String();
            title = _os_.unmarshal_String();
            msg = _os_.unmarshal_String();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ = _os_.unmarshal_int();
                innerdropidlist.AddFirst(_v_);
            }

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                MailItem _v_ = new MailItem();
                _v_.unmarshal(_os_);
                items.AddFirst(_v_);
            }
            endtime = _os_.unmarshal_long();
            isopen = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                string _v_ = "";
                _v_ = _os_.unmarshal_String();
                strlist.AddLast(_v_);
            }

            return _os_;
		}

	}
}
