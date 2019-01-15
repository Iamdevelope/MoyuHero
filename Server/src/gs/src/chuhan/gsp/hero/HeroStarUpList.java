package chuhan.gsp.hero;



import chuhan.gsp.log.Logger;
import chuhan.gsp.msg.Message;




public class HeroStarUpList{

	public static Logger logger = Logger.getLogger(HeroStarUpList.class);
	static private HeroStarUpList instance = null;
	
	private HeroStarUpList(){}
	public static HeroStarUpList getInstance() {
		if(instance == null)
		{
			instance = new HeroStarUpList();
		}
		return instance;
	}
	
	private java.util.Hashtable<Long,StarUpInfo> StarUpMap = new java.util.Hashtable<Long,StarUpInfo>();

	public class StarUpInfo
	{
		final int usekey1;
		final int usekey2;
		final java.util.LinkedList<Integer> resultList; 
		int isSee;		//0是未看最后一位不可选,1是看了可选
		StarUpInfo(java.util.LinkedList<Integer> resultList, int usekey1, int usekey2, int isSee)
		{
			this.usekey1 = usekey1;
			this.usekey2 = usekey2;
			this.resultList = resultList;
			this.isSee = isSee;
		}	
	}
	
	public void addStarUpInfo(long roleId, java.util.LinkedList<Integer> resultList, int usekey1, int usekey2, int isSee)
	{
		StarUpInfo starupInfo = new StarUpInfo(resultList,usekey1,usekey2,isSee);
		this.addStarUpInfo(roleId, starupInfo);
	}
	
	private void addStarUpInfo(long roleId, StarUpInfo starupInfo)
	{
		StarUpMap.put(roleId, starupInfo);
	}
	
	public void removeStarUpInfo(long roleId)
	{
		StarUpMap.remove(roleId);
	}
	
	public StarUpInfo getStarUpInfo(long roleId)
	{
		return StarUpMap.get(roleId);
	}
	public boolean buy(long roleId)
	
	{
		StarUpInfo starupInfo = this.getStarUpInfo(roleId);
		if(starupInfo == null)
		{
			Message.psendMsgNotify(roleId, 135);
			return false;
		}
		starupInfo.isSee = 1;
		return true;
	}
	
}



