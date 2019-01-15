using System;
namespace GNET
{
    public class HeroTuJian : Marshal
	{
        public int heroid; // Ó¢ÐÛID
        public int flag; // ÊÇ·ñÂú¼¶£¬0Î´Âú£¬1Âú¼¶

        public HeroTuJian()
        {
            heroid = 0;
            flag = 0;
        }

        public HeroTuJian(int _heroid_, int _flag__)
        {
            this.heroid = _heroid_;
            this.flag = _flag__;
        }

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(heroid);
            os.marshal(flag);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            heroid = os.unmarshal_int();
            flag = os.unmarshal_int();
			return os;
		}

	}
}
