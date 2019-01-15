using System;
namespace GNET
{
    public class StageBattle : Marshal
	{
       	public int id;
	    public byte maxstar; // 0-3
	    public short fightnum; // 已战次数
        public int buybattlenum; // 已购买次数
        public int resetnum; // 已重置次数
        public int sweepnum; // 已扫荡次数

        public StageBattle()
        {
 
        }

        public StageBattle(int _id_, byte _maxstar_, short _fightnum_, int _buybattlenum_, int _resetnum_, int _sweepnum_)
        {
            this.id = _id_;
            this.maxstar = _maxstar_;
            this.fightnum = _fightnum_;
            this.buybattlenum = _buybattlenum_;
            this.resetnum = _resetnum_;
            this.sweepnum = _sweepnum_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(id);
            _os_.marshal(maxstar);
            _os_.marshal(fightnum);
            _os_.marshal(buybattlenum);
            _os_.marshal(resetnum);
            _os_.marshal(sweepnum);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            id = _os_.unmarshal_int();
            maxstar = _os_.unmarshal_byte();
            fightnum = _os_.unmarshal_short();
            buybattlenum = _os_.unmarshal_int();
            resetnum = _os_.unmarshal_int();
            sweepnum = _os_.unmarshal_int();
            return _os_;
		}

	}
}
