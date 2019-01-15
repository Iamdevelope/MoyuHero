using System;
namespace GNET
{
    public class Huoyue : Marshal
	{
        public int huoyueid; // ��Ծid
        public int num; // ��������
        public int numall; // �ܴ���
        public int huoyuetype; // ��������
        public int isok; // �Ƿ����

        public Huoyue()
        {
        }

        public Huoyue(int _huoyueid_, int _num_, int _numall_, int _huoyuetype_, int _isok_) {
		this.huoyueid = _huoyueid_;
		this.num = _num_;
		this.numall = _numall_;
		this.huoyuetype = _huoyuetype_;
		this.isok = _isok_;
	}
	

		public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(huoyueid);
            _os_.marshal(num);
            _os_.marshal(numall);
            _os_.marshal(huoyuetype);
            _os_.marshal(isok);
	
			return _os_;
		}

		public override OctetsStream unmarshal(OctetsStream _os_)
		{
            huoyueid = _os_.unmarshal_int();
            num = _os_.unmarshal_int();
            numall = _os_.unmarshal_int();
            huoyuetype = _os_.unmarshal_int();
            isok = _os_.unmarshal_int();
			return _os_;
		}

	}
}
