package chuhan.gsp.hero;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import chuhan.gsp.item.artifact33;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.play.huoyuedu.HuoyueColumns;
import chuhan.gsp.play.tanxian.TanXianColumns;



public class HeroArtifactColumn {	
	public static Logger logger = Logger.getLogger(HeroArtifactColumn.class);
	
	final public long roleId;
	final xbean.ArtifactColumn xcolumn;
	final boolean readonly;
	
	
	public static HeroArtifactColumn getHeroArtifactColumn(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造TroopColumn时，角色 "+roleId+" 不存在。");
		
		xbean.ArtifactColumn artifactcol = null;
		if(readonly)
			artifactcol = xtable.Artifactcolumns.select(roleId);
		else
			artifactcol = xtable.Artifactcolumns.get(roleId);
		if(artifactcol == null)
		{
			if(readonly)
				artifactcol = xbean.Pod.newArtifactColumnData();
			else
			{
				artifactcol = xbean.Pod.newArtifactColumn();
				xtable.Artifactcolumns.insert(roleId, artifactcol);
			}
		}
		return new HeroArtifactColumn(roleId, artifactcol, readonly);
	}
	
	
	
	private HeroArtifactColumn(long roleId, xbean.ArtifactColumn xcolumn, boolean readonly) {
		this.roleId = roleId;
		this.xcolumn = xcolumn;
		this.readonly = readonly;
		init();
	}
	
	public void init(){
		java.util.TreeMap<Integer,artifact33> artifactMap = ConfigManager.getInstance().getConf(artifact33.class);
		for(Map.Entry<Integer, artifact33> entry : artifactMap.entrySet()){
			if(entry.getValue().getLevel() == 1){
				if(getArtifactByType(entry.getValue().getType()) == null){
					xbean.Artifact artifact = new xbean.Pod().newArtifact();
					artifact.setArtifacttype(entry.getValue().getType());
					artifact.setArtifactid(entry.getValue().getId());
					artifact.setHeronum1(0);		//测试数据
					artifact.setHeronum2(0);		//测试数据
					artifact.setHeronum3(0);
					artifact.setHeronum4(0);
					artifact.setHeronum5(0);
					xcolumn.getArtifacts().put(artifact.getArtifacttype(), artifact);
				}
			}
		}
	}
	
	public java.util.HashMap<Integer,chuhan.gsp.Artifact> getProtocolHeroArtifacts(){		
		HashMap<Integer,chuhan.gsp.Artifact> datas = new HashMap<Integer,chuhan.gsp.Artifact>();
		for(Map.Entry<Integer,xbean.Artifact> entry : xcolumn.getArtifacts().entrySet()){
			chuhan.gsp.Artifact artifact = HeroArtifact.getProtocolArtifact(entry.getValue());
			datas.put(artifact.artifacttype, artifact);
		}
		return datas;
	}
	
	public HeroArtifact getArtifactByType(int artifactType){
		xbean.Artifact artifact = xcolumn.getArtifacts().get(artifactType);
		if(artifact != null)
			return HeroArtifact.getHeroArtifact(artifact, readonly);
		return null;
	}
	
	/**
	 * 神器注魂入口
	 * @param artifactType
	 * @param herokeyList
	 * @return
	 */
	public boolean addHeroToArtifact(int artifactType,List<Integer> herokeyList){
		HeroArtifact heroArtifact = this.getArtifactByType(artifactType);
		if(heroArtifact == null){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		//判断是否探险
		TanXianColumns col = TanXianColumns.getTanXianColumn(roleId, false);
		if(col.isHeroTanXian(herokeyList)){
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		
		chuhan.gsp.attr.PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, readonly);
		if(prop.getLevel() < heroArtifact.getiArtifactInfo().getPlayerLevel()){
//test			Message.psendMsgNotify(roleId, 135);
//			return false;
		}
		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
		LinkedList<Integer> delHeroKeyList = new LinkedList<Integer>();
		for(Integer herokey : herokeyList){
			Hero hero = herocol.getHByHKey(herokey);
			if(hero != null){
				if( heroArtifact.addHero(hero.getiHeroInfo().getId()) ){
					delHeroKeyList.add(herokey);
				}
			}
		}
		
		if(delHeroKeyList.size() == 0)
			return false;
		if(herocol.removeByKeyList(delHeroKeyList)){
			heroArtifact.levelUp(roleId);
			heroArtifact.refreshArtifact(roleId);
			
			ActivityManager.getInstance().addHYTaskOver(roleId, HuoyueColumns.SHENQI_ZHUHUN, delHeroKeyList.size());

			return true;
		}
		return false;
	}
		
	
}
