package chuhan.gsp.hero;

public class PHeroMSExp extends xdb.Procedure
{
	private final long roleId;
	private final int herokey;
	private final int mslocation; // 秘术位置
	private final java.util.LinkedList<Integer> itemidlist; // 物品配表ID
	private final java.util.LinkedList<Integer> itemnumlist; // 物品数量
	
	public PHeroMSExp(long roleId, int herokey,int _mslocation_, java.util.LinkedList<Integer> itemidlist, java.util.LinkedList<Integer> itemnumlist) {
		this.roleId = roleId;
		this.herokey = herokey;
		this.mslocation = _mslocation_;
		this.itemidlist = itemidlist;
		this.itemnumlist = itemnumlist;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		SHeroMSExp snd = new SHeroMSExp();
		HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);	
		boolean result = herocol.msEntry(herokey,mslocation,itemidlist,itemnumlist);
		if(result){
			snd.result = SHeroMSExp.END_OK;
		}else{
			snd.result = SHeroMSExp.END_NOT_OK;
		}
		
		xdb.Procedure.psend(roleId,snd);
		
		return result;
	}
}
