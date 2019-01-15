package chuhan.gsp.hero;

import chuhan.gsp.Dictionary;
import chuhan.gsp.MsgType;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.item.hero01;
import chuhan.gsp.mail.PAddMail;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;




public class PAddHero extends xdb.Procedure{
	private final long roleid;
	private final int heroId;
	private final int herolevel;
	
	public int herokey;
	public String SYS_DEFAULT;
	
	public PAddHero(long roleid, int heroId, int herolevel) {
		this.roleid = roleid;
		this.heroId = heroId;
		this.herolevel = herolevel;
		SYS_DEFAULT = "addhero";
	}
	
	public PAddHero(long roleid, int heroId, int herolevel,String strsys) {
		this.roleid = roleid;
		this.heroId = heroId;
		this.herolevel = herolevel;
		this.SYS_DEFAULT = strsys;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		xbean.Properties xprop = xtable.Properties.get(roleid);
		if(xprop == null)
		{
			throw new IllegalArgumentException("构造角色时，角色 "+roleid+" 不存在。");
//			return false;
		}
		
		HeroColumn herocol = HeroColumn.getHeroColumn(roleid, false);
		hero01 addHeroInit = ConfigManager.getInstance().getConf(hero01.class).get(heroId);
		if(addHeroInit == null){
			return false;
		}
		if(herocol.isHaveSameHero(addHeroInit)){
			DropManager.getInstance().dropAddByOther(addHeroInit.getSyntheticItemid(), addHeroInit.getSyntheticCount()/2, 
					0,0,roleid, SYS_DEFAULT);
			return true;
		}
		/*
		//判断是否走邮件
		chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleid, false);
		if(proprole.getMaxHeroSize() <= herocol.getxcolumn().getHeroes().size())
		{
			java.util.LinkedList<Integer> herolist = new java.util.LinkedList<Integer>();
			herolist.addFirst(heroId);
			PAddMail paddmail = new PAddMail(roleid,Dictionary.SYS_TITLE,SYS_DEFAULT,
					0,0,herolist,null,0,0);
			paddmail.call();
			return true;
		}
		*/
		xbean.Hero xhero = Hero.createHero(heroId, herolevel);
		if(xhero == null)
		{
			Message.psendMsgNotify(roleid, 135);
			return false;
		}
		
		//添加英雄默认皮肤
		HeroSkinColumn skincol = HeroSkinColumn.getHeroSkinColumn(roleid, false);
		if(skincol.getSkin(xhero.getHeroviewid()) == null)
			skincol.addSkin(xhero.getHeroviewid(),true);
		
		Hero hero = herocol.addHero(xhero);
		herokey = hero.getxHeroInfo().getKey();
		psendWhileCommit(roleid, new SRefreshHero(hero.getProtocolHero()));
				
		return true;
	}
	
}
