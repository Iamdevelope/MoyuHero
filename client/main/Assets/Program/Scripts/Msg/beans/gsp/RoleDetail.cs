using System.Collections;
using System.Collections.Generic;
namespace GNET
{
    public class RoleDetail : Marshal
	{
        public long roleid; // ID
        public string name; // 名称
        public byte isgm; // 0:no,1:yes
        public short level; // 等级
        public int exp; // 经验
        public byte viplv; // vip等级
        public int vipexp; // vip经验
        public short ti;
        public int titime;
        public long money; // 游戏币
        public int yuanbao; // 元宝
        public int battlenum; // 关卡记录
        public long servertime; // 服务器时间
        public byte timezone; // 服务器时区[-12,+12]
        public LinkedList<Hero> heroes;
        public LinkedList<Troop> troops;
        public Hashtable baginfo;   //玩家背包信息
        public int hammer;      //锤子

        public int freegoldtime; // 免费金币剩余时间
        public int freeybtime; // 免费元宝剩余时间

        public int goldbuynum; // 金币购买次数
        public int tibuynum; // 体力购买次数

        public int signnum7; // 连续签到ID
        public int signnum28; // 累计签到ID

        public byte mailsize; // 邮件数量
        public short buybagnum; // 购买背包上线次数
        public short buyherobagnum; // 扩充英雄背包次数

        public int smid; // 神秘关卡或商店ID
        public int smtime; // 神秘剩余时间
        public int smzhangjie; // 神秘所属章节

        public int shenglingzq; // 圣灵之泉
        public int ronglian; // 熔炼点
        public int huangjinxz; // 黄金勋章
        public int baijinxz; // 白金勋章
        public int qingtongxz; // 青铜勋章
        public int chitiexz; // 赤铁勋章
        public int jyjiejing; // 经验结晶

        public byte troopnum; // 默认编队号
        public LinkedList<int> heroskins; // 皮肤列表
        public Hashtable artifacts; // key是Artifacttype,value是神器的详细信息
        public Hashtable shopbuys; // key是shopid,value是Shopbuy的详细信息

        public int sweepnum; // 今日扫荡剩余次数
        public int sweepbuynum; // 今日扫荡购买剩余次数
        public int mszqgetnum; // 缪斯奏曲：个位为中午，十位为晚上

        public int qiyuannum; // 累计祈愿台次数
        public int qiyuanallnum; // 祈愿回合次数，第一次为3，完成后均为5
        public int isqiyuantoday; // 个位是今日是否祈愿，十位为是否断签，0是否，1为是

        public short txti; // 探险体力
        public int txtitime; // 探险体力更新时间剩余

        public LinkedList<int> newyindao; // 新手引导
        public Hashtable smshop; // 神秘商店随机出的物品（key为随机商店物品id，int为是否购买，0为未购买）

        public RoleDetail()         
        {
            name = "";
            heroes = new LinkedList<Hero>();
            troops = new LinkedList<Troop>();
            baginfo = new Hashtable();           
            heroskins = new LinkedList<int>();
            artifacts = new Hashtable();
            shopbuys = new Hashtable();
            newyindao = new LinkedList<int>();
            smshop = new Hashtable();
        }

        public RoleDetail(long _roleid_, string _name_, 
            byte _isgm_, short _level_, int _exp_,
            byte _viplv_, int _vipexp_,
            byte _ti_, int _titime_, long _money_, int _yuanbao_,
            int _battlenum_, long _servertime_, byte _timezone_, 
            LinkedList<Hero> _heroes_, 
            LinkedList<Troop> _troops_,
            Hashtable _baginfo_,
            int _hammer_,
            int _freegoldtime_, int _freeybtime_, 
            int _goldbuynum_, int _tibuynum_,
            int _signnum7_, int _signnum28_, byte _mailsize_,
            short _buybagnum_, short _buyherobagnum_, int _smid_, int _smtime_, int _smzhangjie_,
            int _shenglingzq_, int _ronglian_, int _huangjinxz_, int _baijinxz_, int _qingtongxz_, int _chitiexz_, int _jyjiejing_,
            byte _troopnum_, LinkedList<int> _heroskins_, Hashtable _artifacts_, Hashtable _shopbuys_,
            int _sweepnum_, int _sweepbuynum_, int _mszqgetnum_, int _qiyuannum_, int _qiyuanallnum_, int _isqiyuantoday_,
            short _txti_, int _txtitime_, LinkedList<int> _newyindao_, Hashtable _smshop_)
        {
            this.roleid = _roleid_;
            this.name = _name_;
            this.isgm = _isgm_;
            this.level = _level_;
            this.exp = _exp_;
            this.viplv = _viplv_;
            this.vipexp = _vipexp_;
            this.ti = _ti_;
            this.titime = _titime_;
            this.money = _money_;
            this.yuanbao = _yuanbao_;
            this.battlenum = _battlenum_;
            this.servertime = _servertime_;
            this.timezone = _timezone_;
            this.heroes = _heroes_;
            this.troops = _troops_;
            this.baginfo = _baginfo_;
            this.hammer = _hammer_;
            this.freegoldtime = _freegoldtime_;
            this.freeybtime = _freeybtime_;
            this.goldbuynum = _goldbuynum_;
            this.tibuynum = _tibuynum_;
            this.signnum7 = _signnum7_;
            this.signnum28 = _signnum28_;
            this.mailsize = _mailsize_;
            this.buybagnum = _buybagnum_;
            this.buyherobagnum = _buyherobagnum_;
            this.smid = _smid_;
            this.smtime = _smtime_;
            this.smzhangjie = _smzhangjie_;
            this.shenglingzq = _shenglingzq_;
            this.ronglian = _ronglian_;
            this.huangjinxz = _huangjinxz_;
            this.baijinxz = _baijinxz_;
            this.qingtongxz = _qingtongxz_;
            this.chitiexz = _chitiexz_;
            this.jyjiejing = _jyjiejing_;
            this.troopnum = _troopnum_;
            this.heroskins = _heroskins_;
            this.artifacts = _artifacts_;
            this.shopbuys = _shopbuys_;
            this.sweepnum = _sweepnum_;
            this.sweepbuynum = _sweepbuynum_;
            this.mszqgetnum = _mszqgetnum_;
            this.qiyuannum = _qiyuannum_;
            this.qiyuanallnum = _qiyuanallnum_;
            this.isqiyuantoday = _isqiyuantoday_;
            this.txti = _txti_;
            this.txtitime = _txtitime_;
            this.newyindao = _newyindao_;
            this.smshop = _smshop_;
        }

		public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(roleid);
    		_os_.marshal(name);
    		_os_.marshal(isgm);
    		_os_.marshal(level);
    		_os_.marshal(exp);
    		_os_.marshal(viplv);
            _os_.marshal(vipexp);
    		_os_.marshal(ti);
            _os_.marshal(titime);
    		_os_.marshal(money);
    		_os_.marshal(yuanbao);
            _os_.marshal(battlenum);
    		_os_.marshal(servertime);
    		_os_.marshal(timezone);

            _os_.compact_uint32(heroes.Count);
            LinkedListNode<Hero> firstNode = heroes.First;
            while (firstNode != null)
            {
                _os_.marshal(heroes.First.Value);

                heroes.RemoveFirst();
                firstNode = heroes.First;
            }

            _os_.compact_uint32(troops.Count);
            LinkedListNode<Troop> firstNode2 = troops.First;
            while (firstNode2 != null)
            {
                _os_.marshal(troops.First.Value);

                troops.RemoveFirst();
                firstNode2 = troops.First;
            }

            _os_.compact_uint32(baginfo.Count);  //玩家背包
            foreach (DictionaryEntry de in baginfo)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((int)de.Value);
            }

            _os_.marshal(hammer);
            _os_.marshal(freegoldtime);
            _os_.marshal(freeybtime);
            _os_.marshal(goldbuynum);
            _os_.marshal(tibuynum);
            _os_.marshal(signnum7);
            _os_.marshal(signnum28);
            _os_.marshal(mailsize);
            _os_.marshal(buybagnum);
            _os_.marshal(buyherobagnum);
            _os_.marshal(smid);
            _os_.marshal(smtime);
            _os_.marshal(smzhangjie);
            _os_.marshal(shenglingzq);
            _os_.marshal(ronglian);
            _os_.marshal(huangjinxz);
            _os_.marshal(baijinxz);
            _os_.marshal(qingtongxz);
            _os_.marshal(chitiexz);
            _os_.marshal(jyjiejing);
            _os_.marshal(troopnum);

            _os_.compact_uint32(heroskins.Count);
            LinkedListNode<int> firstNode3 = heroskins.First;
            while (firstNode3 != null)
            {
                _os_.marshal(heroskins.First.Value);

                heroskins.RemoveFirst();
                firstNode3 = heroskins.First;
            }

            _os_.compact_uint32(artifacts.Count);  //神器
            foreach (DictionaryEntry de in artifacts)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((Artifact)de.Value);
            }

            _os_.compact_uint32(shopbuys.Count);  //商城
            foreach (DictionaryEntry de in shopbuys)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((Shopbuy)de.Value);
            }

            _os_.marshal(sweepnum);
            _os_.marshal(sweepbuynum);
            _os_.marshal(mszqgetnum);
            _os_.marshal(qiyuannum);
            _os_.marshal(qiyuanallnum);
            _os_.marshal(isqiyuantoday);
            _os_.marshal(txti);
            _os_.marshal(txtitime);

            _os_.compact_uint32(newyindao.Count);
            LinkedListNode<int> firstNode4 = newyindao.First;
            while (firstNode4 != null)
            {
                _os_.marshal(newyindao.First.Value);

                newyindao.RemoveFirst();
                firstNode4 = newyindao.First;
            }

            _os_.compact_uint32(smshop.Count);  //神秘商店物品
            foreach (DictionaryEntry de in smshop)
            {
                _os_.marshal((int)de.Key);
                _os_.marshal((Smshopdata)de.Value);
            }

            return _os_;

		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            roleid = _os_.unmarshal_long();
            name = _os_.unmarshal_String();
            isgm = _os_.unmarshal_byte();
            level = _os_.unmarshal_short();
            exp = _os_.unmarshal_int();
            viplv = _os_.unmarshal_byte();
            vipexp = _os_.unmarshal_int();
            ti = _os_.unmarshal_short();
            titime = _os_.unmarshal_int();
            money = _os_.unmarshal_long();
            yuanbao = _os_.unmarshal_int();
            battlenum = _os_.unmarshal_int();
            servertime = _os_.unmarshal_long();
            timezone = _os_.unmarshal_byte();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Hero _v_ = new Hero();
                _v_.unmarshal(_os_);
                heroes.AddFirst(_v_);
            }

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                Troop _v_ = new Troop();
                _v_.unmarshal(_os_);
                troops.AddFirst(_v_);
            }

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                key = _os_.unmarshal_int();

                Bag _v_ = new Bag();
                _v_.unmarshal(_os_);
                baginfo.Add(key, _v_);
            }

            hammer = _os_.unmarshal_int();
            freegoldtime = _os_.unmarshal_int();
            freeybtime = _os_.unmarshal_int();
            goldbuynum = _os_.unmarshal_int();
            tibuynum = _os_.unmarshal_int();
            signnum7 = _os_.unmarshal_int();
            signnum28 = _os_.unmarshal_int();
            mailsize = _os_.unmarshal_byte();
            buybagnum = _os_.unmarshal_short();
            buyherobagnum = _os_.unmarshal_short();
            smid = _os_.unmarshal_int();
            smtime = _os_.unmarshal_int();
            smzhangjie = _os_.unmarshal_int();
            shenglingzq = _os_.unmarshal_int();
            ronglian = _os_.unmarshal_int();
            huangjinxz = _os_.unmarshal_int();
            baijinxz = _os_.unmarshal_int();
            qingtongxz = _os_.unmarshal_int();
            chitiexz = _os_.unmarshal_int();
            jyjiejing = _os_.unmarshal_int();
            troopnum = _os_.unmarshal_byte();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ = _os_.unmarshal_int();
                heroskins.AddFirst(_v_);
            }

            //神器
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                key = _os_.unmarshal_int();

                Artifact _v_ = new Artifact();
                _v_.unmarshal(_os_);
                artifacts.Add(key, _v_);
            }
            //商城
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                key = _os_.unmarshal_int();

                Shopbuy _v_ = new Shopbuy();
                _v_.unmarshal(_os_);
                shopbuys.Add(key, _v_);
            }
            sweepnum = _os_.unmarshal_int();
            sweepbuynum = _os_.unmarshal_int();
            mszqgetnum = _os_.unmarshal_int();
            qiyuannum = _os_.unmarshal_int();
            qiyuanallnum = _os_.unmarshal_int();
            isqiyuantoday = _os_.unmarshal_int();
            txti = _os_.unmarshal_short();
            txtitime = _os_.unmarshal_int();

            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int _v_ = _os_.unmarshal_int();
                newyindao.AddLast(_v_);
            }

            //神秘商店
            for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_)
            {
                int key;
                key = _os_.unmarshal_int();
                Smshopdata _v_ = new Smshopdata();
                _v_.unmarshal(_os_);

                smshop.Add(key, _v_);
            }

            return _os_;
		}

	}
}
