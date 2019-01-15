using System;
namespace GNET
{
    public class Item : Marshal
	{
        public const int BIND = 0x00000001; // 不可交易给玩家，不可卖店
        public const int YUANBAO = 0x0000002; // 用元宝购买而来
        public const int ONSTALL = 0x0000004; // 摆摊出售中
        public const int ONEQUIP = 0x0000008; // 装备
        public const int CANNOTONSTALL = 0x10; // 不能卖店
        public const int LOCK = 0x0000020; // 锁定

	    public int id; // 物品id
    	public int key; // key
        public short timer; // 每天使用次数
    	public short number; // 数量
    	public Octets extdata; // 额外数据

        public Item()
        {
            this.id = 0;
            this.key = 0;
            this.timer = 0;
            this.number = 0;
            extdata = new Octets();
        }

        public Item(int _id_, int _key_, short _timer_,short _number_, Octets _extdata_)
        {
            this.id = _id_;
            this.key = _key_;
            this.timer = _timer_;
            this.number = _number_;
            this.extdata = _extdata_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(id);
            _os_.marshal(key);
            _os_.marshal(timer);
            _os_.marshal(number);
            _os_.marshal(extdata);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            id = _os_.unmarshal_int();
            key = _os_.unmarshal_int();
            timer = _os_.unmarshal_short();
            number = _os_.unmarshal_short();
            extdata = _os_.unmarshal_Octets();
            return _os_;
		}

	}
}
