using System;
namespace GNET
{
    public class Artifact : Marshal
	{
        public int artifacttype; // �������ͣ�key��
        public int artifactid; // ����ID
        public int heronum1; // Ӣ������1
        public int heronum2; // Ӣ������2
        public int heronum3; // Ӣ������3
        public int heronum4; // Ӣ������4
        public int heronum5; // Ӣ������5

        public Artifact()
        {
            
        }

        public Artifact(int _artifacttype_, int _artifactid_, int _heronum1_, int _heronum2_, int _heronum3_, int _heronum4_, int _heronum5_)
        {
            this.artifacttype = _artifacttype_;
            this.artifactid = _artifactid_;
            this.heronum1 = _heronum1_;
            this.heronum2 = _heronum2_;
            this.heronum3 = _heronum3_;
            this.heronum4 = _heronum4_;
            this.heronum5 = _heronum5_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(artifacttype);
            _os_.marshal(artifactid);
            _os_.marshal(heronum1);
            _os_.marshal(heronum2);
            _os_.marshal(heronum3);
            _os_.marshal(heronum4);
            _os_.marshal(heronum5);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            artifacttype = _os_.unmarshal_int();
            artifactid = _os_.unmarshal_int();
            heronum1 = _os_.unmarshal_int();
            heronum2 = _os_.unmarshal_int();
            heronum3 = _os_.unmarshal_int();
            heronum4 = _os_.unmarshal_int();
            heronum5 = _os_.unmarshal_int();
            return _os_;
		}

	}
}
