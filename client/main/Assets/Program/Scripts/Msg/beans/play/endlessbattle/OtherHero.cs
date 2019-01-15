using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;

namespace GNET
{
    public class OtherHero : Marshal
	{
        public int heroid; // Ӣ�����ID
        public int exp; // ��ǰ����
        public int herolevel; // Ӣ�۵ȼ�
        public int hp; // Ѫ��
        public int physicalattack; // ������
        public int physicaldefence; // �������
        public int magicattack; // ħ������
        public int magicdefence; // ħ������
        public int skill1; // ����1��ţ�δ��ͨΪ0��
        public int skill2; // ����2��ţ�δ��ͨΪ0��
        public int skill3; // ����3��ţ�δ��ͨΪ0��
        public int heroviewid; // Ӣ�����
        

      

        public OtherHero()
        {
        }

        public OtherHero(int _heroid_, int _exp_, int _herolevel_, int _hp_, int _physicalattack_, int _physicaldefence_, 
            int _magicattack_, int _magicdefence_, int _skill1_, int _skill2_, int _skill3_, int _heroviewid_
                          )
        {
            this.heroid = _heroid_;
            this.exp = _exp_;
            this.herolevel = _herolevel_;
            this.hp = _hp_;
            this.physicalattack = _physicalattack_;
            this.physicaldefence = _physicaldefence_;
            this.magicattack = _magicattack_;
            this.magicdefence = _magicdefence_;
            this.skill1 = _skill1_;
            this.skill2 = _skill2_;
            this.skill3 = _skill3_;
            this.heroviewid = _heroviewid_;
       
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(heroid);
            _os_.marshal(exp);
            _os_.marshal(herolevel);
            _os_.marshal(hp);
            _os_.marshal(physicalattack);
            _os_.marshal(physicaldefence);
            _os_.marshal(magicattack);
            _os_.marshal(magicdefence);
            _os_.marshal(skill1);
            _os_.marshal(skill2);
            _os_.marshal(skill3);
            _os_.marshal(heroviewid);   
        
    		return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            heroid = _os_.unmarshal_int();
            exp = _os_.unmarshal_int();
            herolevel = _os_.unmarshal_int();
            hp = _os_.unmarshal_int();
            physicalattack = _os_.unmarshal_int();
            physicaldefence = _os_.unmarshal_int();
            magicattack = _os_.unmarshal_int();
            magicdefence = _os_.unmarshal_int();
            skill1 = _os_.unmarshal_int();
            skill2 = _os_.unmarshal_int();
            skill3 = _os_.unmarshal_int();
            heroviewid = _os_.unmarshal_int();
            return _os_;
		}

	}
}
