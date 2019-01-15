package chuhan.gsp.hero;



import java.util.List;
import java.util.Map;

import chuhan.gsp.item.artifact33;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.util.ParserString;


public class HeroArtifact {
	
	public static final Logger logger = Logger.getLogger(HeroArtifact.class);
		
	private final xbean.Artifact xartifact;
	public final boolean readonly;
	private artifact33 iartifact;
	private List<Integer> attriTypeList;
	private List<Integer> attriValueList;
	private List<Integer> heroIDList;
	private List<Integer> heroNumList;
	private List<Integer> weightList;

	
	
	public static HeroArtifact getHeroArtifact(xbean.Artifact xartifact, boolean readonly)
	{
		return new HeroArtifact(xartifact, readonly);
	}
	
	public xbean.Artifact getxArtifactInfo()
	{
		return xartifact;
	}
	
	public artifact33 getiArtifactInfo()
	{
		return iartifact;
	}
	
	public HeroArtifact(xbean.Artifact xartifact, boolean readonly) {
		iartifact = ConfigManager.getInstance().getConf(artifact33.class).get(xartifact.getArtifactid());
		this.xartifact = xartifact;
		this.readonly = readonly;
		init();
	}
	
	public void init(){
		if(iartifact != null){
			attriTypeList = ParserString.parseString2Int(iartifact.getAttriType());
			attriValueList = ParserString.parseString2Int(iartifact.getAttriValue());
			heroIDList = ParserString.parseString2Int(iartifact.getHeroID());
			heroNumList = ParserString.parseString2Int(iartifact.getHeroNum());
			weightList = ParserString.parseString2Int(iartifact.getWeight());
		}
	}
	
	public chuhan.gsp.Artifact getProtocolArtifact(){
		chuhan.gsp.Artifact artifact = new chuhan.gsp.Artifact();
		artifact.artifacttype = xartifact.getArtifacttype();
		artifact.artifactid = xartifact.getArtifactid();
		artifact.heronum1 = xartifact.getHeronum1();
		artifact.heronum2 = xartifact.getHeronum2();
		artifact.heronum3 = xartifact.getHeronum3();
		artifact.heronum4 = xartifact.getHeronum4();
		artifact.heronum5 = xartifact.getHeronum5();

		return artifact;
	}
	
	public static chuhan.gsp.Artifact getProtocolArtifact(xbean.Artifact xartifact){
		chuhan.gsp.Artifact artifact = new chuhan.gsp.Artifact();
		artifact.artifacttype = xartifact.getArtifacttype();
		artifact.artifactid = xartifact.getArtifactid();
		artifact.heronum1 = xartifact.getHeronum1();
		artifact.heronum2 = xartifact.getHeronum2();
		artifact.heronum3 = xartifact.getHeronum3();
		artifact.heronum4 = xartifact.getHeronum4();
		artifact.heronum5 = xartifact.getHeronum5();

		return artifact;
	}
	
	/**
	 * 升级更换相应内容
	 */
	public void levelUpChange(Long roleId){
		java.util.TreeMap<Integer,artifact33> artifactMap = ConfigManager.getInstance().getConf(artifact33.class);
		boolean isFull = true;
		for(Map.Entry<Integer, artifact33> entry : artifactMap.entrySet()){
			if(entry.getValue().getLevel() == this.getiArtifactInfo().getLevel() + 1 &&
					entry.getValue().getType() == this.getiArtifactInfo().getType() ){
				iartifact = entry.getValue();
				this.xartifact.setArtifactid(entry.getValue().getId());
				this.xartifact.setHeronum1(0);
				this.xartifact.setHeronum2(0);
				this.xartifact.setHeronum3(0);
				this.xartifact.setHeronum4(0);
				this.xartifact.setHeronum5(0);
				init();
				artifactLevelUp(roleId);
				isFull = false;
				break;
			}
		}
		if(isFull){
			//跑马灯
			ActivityManager.getInstance().addMsgNotice(roleId,this.getiArtifactInfo().id,ActivityManager.SHENQIFULL,
					this.getiArtifactInfo().getName());
		}
	}
	
	/**
	 * 判断如果满级则升级
	 */
	public void levelUp(Long roleId){
		if(heroNumList == null || heroNumList.size() == 0)
			return;
		boolean isFull = true;
		for(int i = 0;i<heroNumList.size();i++){
			if( this.getHeroNum(i+1) < heroNumList.get(i) ){
				isFull = false;
				break;
			}
		}
		if(isFull){
			levelUpChange(roleId);
		}
	}
	
	
	/**
	 * 神器注入英雄
	 * @param heroid
	 * @return
	 */
	public boolean addHero(int heroid){
		if(this.iartifact == null)
			return false;
		if(heroIDList == null || heroNumList == null|| heroIDList.size() != heroNumList.size())
			return false;
		for(int i = 0;i< heroIDList.size();i++){
			if( isRightHeroid(heroid,heroIDList.get(i)) && getHeroNum(i+1) != -1 &&
					getHeroNum(i+1) < this.heroNumList.get(i) ){
				return addHeroNum(i+1);
			}
		}
		return false;
	}
	
	/**
	 * 神器判断注入英雄类型
	 * @param heroid
	 * @param needheroid
	 * @return
	 */
	public boolean isRightHeroid(int heroid, int needheroid){
		if(heroid == needheroid)
			return true;
		if(needheroid / 10 == heroid / 10 && heroid % 10 > needheroid % 10){
			return true;
		}
		return false;
	}
	
	/**
	 * 根据序列得到已注入数量
	 * @param place
	 * @return
	 */
	public int getHeroNum(int place){
		switch(place){
		case 1:
			return this.xartifact.getHeronum1();
		case 2:
			return this.xartifact.getHeronum2();
		case 3:
			return this.xartifact.getHeronum3();
		case 4:
			return this.xartifact.getHeronum4();
		case 5:
			return this.xartifact.getHeronum5();
			default:
				return -1;
		}
	}
	
	/**
	 * 添加英雄计数
	 * @param place
	 * @return
	 */
	public boolean addHeroNum(int place){
		switch(place){
		case 1:
			this.xartifact.setHeronum1(this.xartifact.getHeronum1() + 1);
			return true;
		case 2:
			this.xartifact.setHeronum2(this.xartifact.getHeronum2() + 1);
			return true;
		case 3:
			this.xartifact.setHeronum3(this.xartifact.getHeronum3() + 1);
			return true;
		case 4:
			this.xartifact.setHeronum4(this.xartifact.getHeronum4() + 1);
			return true;
		case 5:
			this.xartifact.setHeronum5(this.xartifact.getHeronum5() + 1);
			return true;
			default:
				return false;
		}
	}
	
	/**
	 * 刷新单个神器数据
	 * @param roleId
	 */
	public void refreshArtifact(long roleId){
		SRefreshArtifact snd = new SRefreshArtifact();
		snd.artifactinfo = this.getProtocolArtifact();
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	/**
	 * 神器升级通知
	 * @param roleId
	 */
	private void artifactLevelUp(long roleId){
		SArtifactLevelUp snd = new SArtifactLevelUp();
		snd.artifacttype = this.xartifact.getArtifacttype();
		snd.artifactid = this.xartifact.getArtifactid();
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	
}
