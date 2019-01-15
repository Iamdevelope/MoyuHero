
package xbean;

public interface lotty extends xdb.Bean {
	public lotty copy(); // deep clone
	public lotty toData(); // a Data instance
	public lotty toBean(); // a Bean instance
	public lotty toDataIf(); // a Data instance If need. else return this
	public lotty toBeanIf(); // a Bean instance If need. else return this

	public int getNormalrecruitnum(); // 普通招募累计次数
	public long getNormalrecruittime(); // 最后普通招募时间
	public int getToprecruitnum(); // 顶级招募累计次数
	public long getToprecruittime(); // 最后顶级招募时间
	public int getToprecruitheronum(); // 顶级招募累计次数，为招十次必得英雄准备
	public int getToptentime(); // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
	public long getFreetime(); // 可以免费抽奖的时间
	public int getFirstget(); // 首抽是否已经完成
	public int getDreamexp(); // 梦想值
	public int getDreamfree(); // 梦想改变是否免费
	public int getDream(); // 梦想兑换展示
	public java.util.Map<Integer, Integer> getSinglelotty(); // 单抽增加值
	public java.util.Map<Integer, Integer> getSinglelottyAsData(); // 单抽增加值
	public java.util.Map<Integer, Integer> getTenlotty(); // 十连抽增加值
	public java.util.Map<Integer, Integer> getTenlottyAsData(); // 十连抽增加值
	public java.util.Map<Integer, Integer> getTensinglelotty(); // 十连抽大奖增加值
	public java.util.Map<Integer, Integer> getTensinglelottyAsData(); // 十连抽大奖增加值
	public java.util.Map<Integer, Integer> getGetherolotty(); // 梦想兑换增加值
	public java.util.Map<Integer, Integer> getGetherolottyAsData(); // 梦想兑换增加值

	public void setNormalrecruitnum(int _v_); // 普通招募累计次数
	public void setNormalrecruittime(long _v_); // 最后普通招募时间
	public void setToprecruitnum(int _v_); // 顶级招募累计次数
	public void setToprecruittime(long _v_); // 最后顶级招募时间
	public void setToprecruitheronum(int _v_); // 顶级招募累计次数，为招十次必得英雄准备
	public void setToptentime(int _v_); // 顶级招募十连抽时间，为（用户第一次10连抽）10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）准备的
	public void setFreetime(long _v_); // 可以免费抽奖的时间
	public void setFirstget(int _v_); // 首抽是否已经完成
	public void setDreamexp(int _v_); // 梦想值
	public void setDreamfree(int _v_); // 梦想改变是否免费
	public void setDream(int _v_); // 梦想兑换展示
}
