using System;
namespace GNET
{
    public class Monthcard : Marshal
	{
        public int monthcardid; // �¿�id
        public long overtime; // ����ʱ��
        public int istodayget; // �����Ƿ���ȡ��0δ��ȡ��1����ȡ

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
