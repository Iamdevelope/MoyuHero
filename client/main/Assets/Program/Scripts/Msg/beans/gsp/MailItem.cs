using System;
namespace GNET
{
    public class MailItem : Marshal
	{
        public int objectid; // 物品ID
        public int dropnum; // 数量
        public int dropparameter1; // 附加条件1
        public int dropparameter2; // 附加条件2

        public MailItem()
        {
        }

        public MailItem(int _objectid_, int _dropnum_, int _dropparameter1_, int _dropparameter2_)
        {
            this.objectid = _objectid_;
            this.dropnum = _dropnum_;
            this.dropparameter1 = _dropparameter1_;
            this.dropparameter2 = _dropparameter2_;
        }

		public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(objectid);
		    _os_.marshal(dropnum);
		    _os_.marshal(dropparameter1);
		    _os_.marshal(dropparameter2);
	
			return _os_;
		}

		public override OctetsStream unmarshal(OctetsStream _os_)
		{
            objectid = _os_.unmarshal_int();
		    dropnum = _os_.unmarshal_int();
		    dropparameter1 = _os_.unmarshal_int();
		    dropparameter2 = _os_.unmarshal_int();
			return _os_;
		}

	}
}
