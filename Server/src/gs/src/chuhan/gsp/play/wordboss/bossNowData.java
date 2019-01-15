package chuhan.gsp.play.wordboss;

public class bossNowData {
	public bossNowData(){}
	public long lasthpall; // 上次总血量
	public int lastiskill; // 上次是否杀掉，0未杀，1已杀
	public long lastkillnum; // 杀掉则为用时（毫秒），未杀则为受到的伤害
	public long newhpall; // 最新总血量
	public long nowhp; // 现在血量
	public int bossid1; // bossid(第一个守门人)
	public int bossid2; // bossid(第一个boss)
	public int bossid3; // bossid(第二个守门人)
	public int bossid4; // bossid(第二个boss)
	public int bossiskill; // 个位为第一个boss，十位为第二个boss,0为未击杀，1为击杀
	public String boss1killname; // 击杀1者名称
	public String boss2killname; // 击杀2者名称
	public long time; // 上次刷新时间
	
	public void copyFrom(xbean.boss boss){
		this.lasthpall = boss.getLasthpall();
		this.lastiskill = boss.getLastiskill();
		this.lastkillnum = boss.getLastkillnum();
		this.newhpall = boss.getNewhpall();
		this.nowhp = boss.getNowhp();
		this.bossid1 = boss.getBossid1();
		this.bossid2 = boss.getBossid2();
		this.bossid3 = boss.getBossid3();
		this.bossid4 = boss.getBossid4();
		this.bossiskill = boss.getBossiskill();
		this.boss1killname = boss.getBoss1killname();
		this.boss2killname = boss.getBoss2killname();
		this.time = boss.getTime();
	}

	public long getLasthpall() {
		return lasthpall;
	}

	public void setLasthpall(long lasthpall) {
		this.lasthpall = lasthpall;
	}

	public int getLastiskill() {
		return lastiskill;
	}

	public void setLastiskill(int lastiskill) {
		this.lastiskill = lastiskill;
	}

	public long getLastkillnum() {
		return lastkillnum;
	}

	public void setLastkillnum(long lastkillnum) {
		this.lastkillnum = lastkillnum;
	}

	public long getNewhpall() {
		return newhpall;
	}

	public void setNewhpall(long newhpall) {
		this.newhpall = newhpall;
	}

	public long getNowhp() {
		return nowhp;
	}

	public void setNowhp(long nowhp) {
		this.nowhp = nowhp;
	}

	public int getBossid1() {
		return bossid1;
	}

	public void setBossid1(int bossid1) {
		this.bossid1 = bossid1;
	}

	public int getBossid2() {
		return bossid2;
	}

	public void setBossid2(int bossid2) {
		this.bossid2 = bossid2;
	}

	public int getBossid3() {
		return bossid3;
	}

	public void setBossid3(int bossid3) {
		this.bossid3 = bossid3;
	}

	public int getBossid4() {
		return bossid4;
	}

	public void setBossid4(int bossid4) {
		this.bossid4 = bossid4;
	}

	public int getBossiskill() {
		return bossiskill;
	}

	public void setBossiskill(int bossiskill) {
		this.bossiskill = bossiskill;
	}

	public String getBoss1killname() {
		return boss1killname;
	}

	public void setBoss1killname(String boss1killname) {
		this.boss1killname = boss1killname;
	}

	public String getBoss2killname() {
		return boss2killname;
	}

	public void setBoss2killname(String boss2killname) {
		this.boss2killname = boss2killname;
	}

	public long getTime() {
		return time;
	}

	public void setTime(long time) {
		this.time = time;
	}
	
	
}
