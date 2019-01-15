using System;
using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class Hero : Marshal
	{
        public int key; // Ӣ��ΨһID
        public int heroid; // Ӣ��ID
        public int heroviewid; // Ӣ����ʾID����ͬ�Ǽ���ͬ��Ӣ��ID��ͬ��
        public int heroexp; // Ӣ�۱�������
        public int herolevel; // Ӣ�۵ȼ�
        public int heroallexp; // Ӣ���ܾ���
        public int qianghualevel; // ǿ���ȼ�
        public int weapon; // ����
        public int barde; // ����
        public int ornament; // ��Ʒ
        public int qhadd; // ǿ���ӳ�
        public int peiyang1; // ����1��ţ�Ĭ��Ϊ0��
        public int peiyang2; // ����2��ţ�Ĭ��Ϊ0��
        public int peiyang3; // ����3��ţ�Ĭ��Ϊ0��
        public int peiyang4; // ����4��ţ�Ĭ��Ϊ0��
        public int skill1; // ����1��ţ�δ��ͨΪ0��
        public int skill2; // ����2��ţ�δ��ͨΪ0��
        public int skill3; // ����3��ţ�δ��ͨΪ0��
        public Hashtable items;     //����װ��

        public int herojinjiestar; // �����Ǽ�
        public int herojinjiesmall; // ���׽׼�
        public int heropinji; // Ʒ�ʣ���Ʒ��Ӣ�����ID��
        public string heroskill; // ���ܣ�:�ָ����λ�ü�¼���ܵȼ���
        public string heromishu; // ������:һ���ָ|�����ָ����λ�ü�¼�����ȼ����������飩
        public string heropeiyang; // ������:�ָ����λ�ü�¼�����ȼ���
        public string heroequip; // װ����:һ���ָ|�����ָ����λ�ü�¼װ��ID��ǿ���ȼ���

        public Hero()
        {
            items = new Hashtable();
        }

        public Hero(int _key_, int _heroid_, int _heroviewid_, int _heroexp_, int _herolevel_, int _heroallexp_, int _qianghualevel_, int _weapon_, int _barde_, int _ornament_, int _qhadd_, int _peiyang1_, int _peiyang2_, int _peiyang3_, int _peiyang4_, int _skill1_, int _skill2_, int _skill3_,
            Hashtable _items_, int _herojinjiestar_, int _herojinjiesmall_, int _heropinji_, string _heroskill_, string _heromishu_, string _heropeiyang_, string _heroequip_)
        {
            this.key = _key_;
            this.heroid = _heroid_;
            this.heroviewid = _heroviewid_;
            this.heroexp = _heroexp_;
            this.herolevel = _herolevel_;
            this.heroallexp = _heroallexp_;
            this.qianghualevel = _qianghualevel_;
            this.weapon = _weapon_;
            this.barde = _barde_;
            this.ornament = _ornament_;
            this.qhadd = _qhadd_;
            this.peiyang1 = _peiyang1_;
            this.peiyang2 = _peiyang2_;
            this.peiyang3 = _peiyang3_;
            this.peiyang4 = _peiyang4_;
            this.skill1 = _skill1_;
            this.skill2 = _skill2_;
            this.skill3 = _skill3_;
            this.items = _items_;

            this.herojinjiestar = _herojinjiestar_;
            this.herojinjiesmall = _herojinjiesmall_;
            this.heropinji = _heropinji_;
            this.heroskill = _heroskill_;
            this.heromishu = _heromishu_;
            this.heropeiyang = _heropeiyang_;
            this.heroequip = _heroequip_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(key);
            _os_.marshal(heroid);
            _os_.marshal(heroviewid);
            _os_.marshal(heroexp);
            _os_.marshal(herolevel);
            _os_.marshal(heroallexp);
            _os_.marshal(qianghualevel);
            _os_.marshal(weapon);
            _os_.marshal(barde);
            _os_.marshal(ornament);
            _os_.marshal(qhadd);
            _os_.marshal(peiyang1);
            _os_.marshal(peiyang2);
            _os_.marshal(peiyang3);
            _os_.marshal(peiyang4);
            _os_.marshal(skill1);
            _os_.marshal(skill2);
            _os_.marshal(skill3);

            _os_.compact_uint32(items.Count);
            foreach (DictionaryEntry de in items)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((int)de.Value);
            }

            _os_.marshal(herojinjiestar);
            _os_.marshal(herojinjiesmall);
            _os_.marshal(heropinji);
            _os_.marshal(heroskill);
            _os_.marshal(heromishu);
            _os_.marshal(heropeiyang);
            _os_.marshal(heroequip);
            
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            key = _os_.unmarshal_int();
            heroid = _os_.unmarshal_int();
            heroviewid = _os_.unmarshal_int();
            heroexp = _os_.unmarshal_int();
            herolevel = _os_.unmarshal_int();
            heroallexp = _os_.unmarshal_int();
            qianghualevel = _os_.unmarshal_int();
            weapon = _os_.unmarshal_int();
            barde = _os_.unmarshal_int();
            ornament = _os_.unmarshal_int();
            qhadd = _os_.unmarshal_int();
            peiyang1 = _os_.unmarshal_int();
            peiyang2 = _os_.unmarshal_int();
            peiyang3 = _os_.unmarshal_int();
            peiyang4 = _os_.unmarshal_int();
            skill1 = _os_.unmarshal_int();
            skill2 = _os_.unmarshal_int();
            skill3 = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int mapkey, mapvalue;
                mapkey = _os_.unmarshal_int();
                mapvalue = _os_.unmarshal_int();
                items.Add(mapkey, mapvalue);
            }

            herojinjiestar = _os_.unmarshal_int();
            herojinjiesmall = _os_.unmarshal_int();
            heropinji = _os_.unmarshal_int();
            heroskill = _os_.unmarshal_String();
            heromishu = _os_.unmarshal_String();
            heropeiyang = _os_.unmarshal_String();
            heroequip = _os_.unmarshal_String();
            return _os_;
		}

	}
}
