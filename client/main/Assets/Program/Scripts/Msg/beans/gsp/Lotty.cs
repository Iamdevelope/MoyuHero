using System;
namespace GNET
{
    public class Lotty : Marshal
	{
		public int freetime; // ?????????(?)
		public int firstget; // ????????
		public int dreamexp; // ???
		public int dreamfree; // ????????
		public int dream; // ??????
		public int normalrecruitnum; // ????????
		public int normalrecruittime; // ??????????(?)
		public int toprecruitnum; // ????????
		public int toprecruittime; // ??????????(?)
		public int toprecruitheronum; // ????????,??????????
		public int toptentime; // ?????????,?(?????10??)10??????(????A????)+1???(???????)???

        public Lotty()
        {
            freetime = 0;
            firstget = 0;
            dreamexp = 0;
            dreamfree = 0;
			dream = 0;
			normalrecruitnum = 0;
			normalrecruittime = 0;
			toprecruitnum = 0;
			toprecruittime = 0;
			toprecruitheronum = 0;
			toptentime = 0;
        }

		public Lotty(int _freetime_, int _firstget_, int _dreamexp_, int _dreamfree_, int _dream_, int _normalrecruitnum_, int _normalrecruittime_, int _toprecruitnum_, int _toprecruittime_, int _toprecruitheronum_, int _toptentime_) {
			this.freetime = _freetime_;
			this.firstget = _firstget_;
			this.dreamexp = _dreamexp_;
			this.dreamfree = _dreamfree_;
			this.dream = _dream_;
			this.normalrecruitnum = _normalrecruitnum_;
			this.normalrecruittime = _normalrecruittime_;
			this.toprecruitnum = _toprecruitnum_;
			this.toprecruittime = _toprecruittime_;
			this.toprecruitheronum = _toprecruitheronum_;
			this.toptentime = _toptentime_;
		}

		public override OctetsStream marshal(OctetsStream _os_) {
			_os_.marshal(freetime);
			_os_.marshal(firstget);
			_os_.marshal(dreamexp);
			_os_.marshal(dreamfree);
			_os_.marshal(dream);
			_os_.marshal(normalrecruitnum);
			_os_.marshal(normalrecruittime);
			_os_.marshal(toprecruitnum);
			_os_.marshal(toprecruittime);
			_os_.marshal(toprecruitheronum);
			_os_.marshal(toptentime);
			return _os_;
		}

		public override OctetsStream unmarshal(OctetsStream _os_) {
			freetime = _os_.unmarshal_int();
			firstget = _os_.unmarshal_int();
			dreamexp = _os_.unmarshal_int();
			dreamfree = _os_.unmarshal_int();
			dream = _os_.unmarshal_int();
			normalrecruitnum = _os_.unmarshal_int();
			normalrecruittime = _os_.unmarshal_int();
			toprecruitnum = _os_.unmarshal_int();
			toprecruittime = _os_.unmarshal_int();
			toprecruitheronum = _os_.unmarshal_int();
			toptentime = _os_.unmarshal_int();
			return _os_;
		}

	}
}
