using System;
namespace GNET
{
    public class Monthcard : Marshal
	{
        public int monthcardid; // 月卡id
        public long overtime; // 到期时间
        public int istodayget; // 今天是否领取：0未领取，1已领取

        public Monthcard()
        {

        }

        public Monthcard(int _monthcardid_, long _overtime_, int _istodayget_)
        {
		    this.monthcardid = _monthcardid_;
		    this.overtime = _overtime_;
		    this.istodayget = _istodayget_;
	    }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(monthcardid);
            _os_.marshal(overtime);
            _os_.marshal(istodayget);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            monthcardid = _os_.unmarshal_int();
            overtime = _os_.unmarshal_long();
            istodayget = _os_.unmarshal_int();
            return _os_;
		}

	}
}
