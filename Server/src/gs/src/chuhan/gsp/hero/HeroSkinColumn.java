package chuhan.gsp.hero;

import java.util.LinkedList;
import java.util.List;

import chuhan.gsp.item.artresource31;
import chuhan.gsp.item.hero01;
import chuhan.gsp.log.Logger;
import chuhan.gsp.main.ConfigManager;



public class HeroSkinColumn {	
	public static Logger logger = Logger.getLogger(HeroSkinColumn.class);
	
	final public long roleId;
	final xbean.HeroSkinColumn xcolumn;
	final boolean readonly;
	
	
	public static HeroSkinColumn getHeroSkinColumn(long roleId, boolean readonly)
	{
		if(xtable.Properties.select(roleId) == null)
			throw new IllegalArgumentException("构造TroopColumn时，角色 "+roleId+" 不存在。");
		
		xbean.HeroSkinColumn heroskincol = null;
		if(readonly)
			heroskincol = xtable.Heroskincolumns.select(roleId);
		else
			heroskincol = xtable.Heroskincolumns.get(roleId);
		if(heroskincol == null)
		{
			if(readonly)
				heroskincol = xbean.Pod.newHeroSkinColumnData();
			else
			{
				heroskincol = xbean.Pod.newHeroSkinColumn();
				xtable.Heroskincolumns.insert(roleId, heroskincol);
			}
		}
		return new HeroSkinColumn(roleId, heroskincol, readonly);
	}
	
	
	
	private HeroSkinColumn(long roleId, xbean.HeroSkinColumn xcolumn, boolean readonly) {
		this.roleId = roleId;
		this.xcolumn = xcolumn;
		this.readonly = readonly;
	}
	
	public List<Integer> getProtocolHeroSkins()
	{
		//测试数据
		if(getSkin(1404000200) == null)
			addSkin(1404000200,false);
		if(getSkin(1404000300) == null)
			addSkin(1404000300,false);
		
		List<Integer> datas = new LinkedList<Integer>();
		for(xbean.HeroSkin heroskin : xcolumn.getHeroskins()){
			datas.add(heroskin.getHeroskinid());
		}
		return datas;
	}
	
	/**
	 * 添加皮肤数据
	 * @param skinId
	 * @return
	 */
	public boolean addSkin(int skinId,boolean isSend){
		artresource31 heroskin = ConfigManager.getInstance().getConf(artresource31.class).get(skinId);
		if(heroskin == null)
			return false;
		if(this.getSkin(skinId) != null)
			return false;
		xbean.HeroSkin newSkin = xbean.Pod.newHeroSkin();
		newSkin.setHeroskinid(skinId);
		xcolumn.getHeroskins().add(newSkin);
		if(isSend)
			this.sendAdd(skinId);
		return true;
	}
	
	/**
	 * 发送添加皮肤
	 * @param skinId
	 */
	private void sendAdd(int skinId){
		SAddSkin snd = new SAddSkin();
		snd.skinid = skinId;
		xdb.Procedure.psendWhileCommit(roleId, snd);
	}
	
	
	/**
	 * 获取皮肤
	 * @param skinId
	 * @return
	 */
	public xbean.HeroSkin getSkin(int skinId){
		for(xbean.HeroSkin heroskin : xcolumn.getHeroskins()){
			if(heroskin.getHeroskinid() == skinId)
				return heroskin;
		}
		return null;
	}
	
	/**
	 * 获取皮肤基础数据
	 * @param skinId
	 * @return
	 */
	public artresource31 getSkinInit(int skinId){
		return ConfigManager.getInstance().getConf(artresource31.class).get(skinId);
	}
	
	
	
	
}
