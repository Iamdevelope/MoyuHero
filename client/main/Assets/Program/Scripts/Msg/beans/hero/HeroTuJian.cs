using System;
namespace GNET
{
    public class HeroTuJian : Marshal
	{
        public int heroid; // Ӣ��ID
        public int flag; // �Ƿ�������0δ����1����

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
