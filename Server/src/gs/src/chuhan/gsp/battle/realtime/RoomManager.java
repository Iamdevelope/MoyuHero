package chuhan.gsp.battle.realtime;

import java.util.LinkedList;

import chuhan.gsp.DataInit;
import chuhan.gsp.log.Logger;


public class RoomManager{

	public static Logger logger = Logger.getLogger(RoomManager.class);
	static private RoomManager instance = null;
	
	static public boolean IsLoopRoomList = false;
	
	public final int DEFAULE_TROOP_NUM = 0;		//默认战队
	public final int BEGIN_WAIT_TIME = 3000;	//开战等待读秒时间
	public final int WAIT_OTHER_WIN = 15000;	//对方未开始直接胜利时间15秒
	
	private static int room_id = 1;
	
	private RoomManager(){}
	public static RoomManager getInstance() {
		if(instance == null)
		{
			instance = new RoomManager();
		}
		return instance;
	}
	
	private java.util.Hashtable<Long,BattleRole> mWaitMap = new java.util.Hashtable<Long,BattleRole>();
	private java.util.Hashtable<Integer,Room> mRoomMap = new java.util.Hashtable<Integer,Room>();

	//加入等待队列
	public int addWait(long roleid, int ranking, String rolename)
	{
		if(mWaitMap.containsKey(roleid))
		{
			long now = chuhan.gsp.main.GameTime.currentTimeMillis();
			int result = (int) ((now - mWaitMap.get(roleid).starttime)/1000);
			return result;
		}
		BattleRole battleroleinfo = getnewBattleRoleInfo(roleid,ranking,rolename);
		if(battleroleinfo.begin(DEFAULE_TROOP_NUM))
		{
			mWaitMap.put(roleid, battleroleinfo);
			return 0;
		}
		return DataInit.ERROR_RESULT;		
	}
	
	//退出等待队列
	public void removeWait(long roleid)
	{
		mWaitMap.remove(roleid);
	}
	
	//删除房间
	public void removeRoom(int roomkey)
	{
		mRoomMap.remove(roomkey);
	}
	
	public Room getRoom(int roomkey)
	{
		return mRoomMap.get(roomkey);
	}
	
	private BattleRole getnewBattleRoleInfo(long roleid, int ranking, String rolename)
	{
		return new BattleRole(roleid,ranking,rolename);
	}
	
	//匹配对手
	public int RealTimeBattleEntry(long roleid)
	{
		chuhan.gsp.attr.PropRole proprole = chuhan.gsp.attr.PropRole.getPropRole(roleid,false);
		RealTimeRoleColumn realtimerolecol = RealTimeRoleColumn.getRealTimeRole(roleid,false);
		
		BattleRole battleroleother = null;
		if(mWaitMap.size() != 0)
		{
			for(java.util.Map.Entry<Long, BattleRole> battlerole : mWaitMap.entrySet())
			{
				battleroleother = battlerole.getValue();
				break;			
			}
		}			
		
		if(battleroleother == null)
		{
//			return this.addWait(roleid, realtimerolecol.getData().getRealtimerank(), proprole.getProperties().getRolename());
		}
		
		BattleRole battleroleinfo = null;//getnewBattleRoleInfo(roleid, realtimerolecol.getData().getRealtimerank(), proprole.getProperties().getRolename());
		if(battleroleinfo.begin(DEFAULE_TROOP_NUM))
		{
			CreateRoom(battleroleinfo,battleroleother);
			return 0;
		}
		else
		{
			return DataInit.ERROR_RESULT;
		}
	}
	
	//准备开始
	public boolean CreateOk(int roomid,long roleid)
	{
		Room room = this.mRoomMap.get(roomid);
		if(room == null)
			return false;
		if(room.battlerole1.roleid == roleid)
		{
			room.battlerole1.isOk = true;
		}
		if(room.battlerole2.roleid == roleid)
		{
			room.battlerole2.isOk = true;
		}
		if(room.battlerole1.isOk && room.battlerole2.isOk)
		{
			xdb.Procedure.psendWhileCommit(room.battlerole1.roleid, new SRealTimeBegin());
			xdb.Procedure.psendWhileCommit(room.battlerole2.roleid, new SRealTimeBegin());
			room.isOk = true;
			room.begintime = chuhan.gsp.main.GameTime.currentTimeMillis();
		}
		return true;
	}
	
	//创建房间
	public void CreateRoom(BattleRole battlerole1,BattleRole battlerole2)
	{
		Room room = new Room(getroomid(),battlerole1,battlerole2);
		mRoomMap.put(room.roomkey, room);	
		
		SCreateRoom createRoom1 = new SCreateRoom();
		createRoom1.roomkey = room.roomkey;
		createRoom1.maininfo = room.battlerole1.getProBattleRoleInfo();
		createRoom1.otherinfo = room.battlerole2.getProBattleRoleInfo();
		
		SCreateRoom createRoom2 = new SCreateRoom();
		createRoom2.roomkey = room.roomkey;
		createRoom2.maininfo = room.battlerole2.getProBattleRoleInfo();
		createRoom2.otherinfo = room.battlerole1.getProBattleRoleInfo();
		
		xdb.Procedure.psendWhileCommit(createRoom1.maininfo.roleid, createRoom1);
		xdb.Procedure.psendWhileCommit(createRoom2.maininfo.roleid, createRoom2);	
	} 
	
	//分发处理攻击
	public boolean AttackEntry(long roleid, int roomid,LinkedList<BHeroAttack> bherotypelist,int iswin,int attackkey)
	{
		Room room = this.mRoomMap.get(roomid);
		if(room == null)
			return false;

		if(room.isRoleInRoom(roleid))
		{
			return false;
		}
		
		boolean result = isAccackRight(room,bherotypelist);
		if(result)
		{
			SAttackOther send = new SAttackOther();
			send.attackkey = attackkey;
			send.bherotypelist = bherotypelist;
			send.iswin = iswin;
			xdb.Procedure.psendWhileCommit(roleid, send);	
			
			if(room.isAllDie(room.battlerole1) || room.isAllDie(room.battlerole2))
			{
				room.SendEnd();
				this.removeRoom(room.roomkey);
			}
		}
		return result;
	}
	

	
	//判断攻击的正确性
	public boolean isAccackRight(Room room,LinkedList<BHeroAttack> bherotypelist)
	{
		for(BHeroAttack bherotype : bherotypelist)
		{
			if(room.dieHeroList.contains(bherotype.herokey))
				return false;
			if(bherotype.killlist.size() != 0)
			{
				for(int num : bherotype.killlist)
				{
					room.dieHeroList.add(num);
				}
			}
		}
		
		return true;
	}
	
	synchronized public int getroomid()
	{
		return this.room_id++;
	}
	
	public void WaitOtherWin()
	{
		logger.debug("WaitOtherWin begin");
		this.IsLoopRoomList = true;
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		LinkedList<Integer> delRoomList = new LinkedList<Integer>();
		for(java.util.Map.Entry<Integer, Room> room : mRoomMap.entrySet())
		{
			if(room.getValue().isOk == false && 
					room.getValue().createtime + WAIT_OTHER_WIN > now)
			{
				if(room.getValue().battlerole1.isOk == true)
				{
					room.getValue().battlerole1.isWin = SRealTimeEnd.WIN;
				}
				else if(room.getValue().battlerole2.isOk == true)
				{
					room.getValue().battlerole2.isWin = SRealTimeEnd.WIN;
				}
				room.getValue().SendEnd();
				delRoomList.addFirst(room.getKey());
			}
		}
		for(Integer key : delRoomList)
		{
			this.removeRoom(key);
		}
		this.IsLoopRoomList = false;
		logger.debug("WaitOtherWin end");
	}

	public class Room
	{
		public int roomkey;
		public BattleRole battlerole1;
		public BattleRole battlerole2;
		public boolean isOk = false;
		public long begintime = 0;	//开启战斗时间
		public long createtime = 0;	//创建房间时间
		public java.util.LinkedList<Integer> dieHeroList;
		
		public int insideid = 0;
		
		public int getinsideid()
		{
			return ++insideid;
		}
		
		public boolean isAllDie(BattleRole battlerole)
		{
			boolean isAllDie = true;
			for(chuhan.gsp.Hero hero : battlerole.heroList)
			{
				if(!dieHeroList.contains(hero.key))
				{
					isAllDie = false;
					break;
				}
			}
			if(isAllDie)
			{
				if(this.battlerole1.roleid == battlerole.roleid)
				{
					this.battlerole2.isWin = SRealTimeEnd.WIN;
				}
				else
				{
					this.battlerole1.isWin = SRealTimeEnd.WIN;
				}
			}
			return isAllDie;
		}
		
		public void SendEnd()
		{
			xdb.Procedure.psend(battlerole1.roleid, new SRealTimeEnd(battlerole1.isWin));
			xdb.Procedure.psend(battlerole2.roleid, new SRealTimeEnd(battlerole2.isWin));
		}
		
		public long getOtherRoleId(long roleid)
		{
			if(battlerole1.roleid == roleid)
				return battlerole2.roleid;
			else
				return battlerole1.roleid;
		}
		
		public boolean isRoleInRoom(long roleid)
		{
			if(battlerole1.roleid == roleid || battlerole2.roleid == roleid)
			{
				return true;
			}
			return false;
		}
		
		Room(int roomkey,BattleRole battlerole1,BattleRole battlerole2)
		{
			this.roomkey = roomkey;
			this.battlerole1 = battlerole1;
			this.battlerole2 = battlerole2;
			this.createtime = chuhan.gsp.main.GameTime.currentTimeMillis();
			processData();
		}
		
		private void processData()
		{
			//将insideid设置为最大key以上，保证key不重复
			for (chuhan.gsp.Hero hero : this.battlerole1.heroList) {
				if (hero.key >= insideid) {
					insideid = hero.key + 1;
				}
			}
			for (chuhan.gsp.Hero hero : this.battlerole2.heroList) {
				if (hero.key >= insideid) {
					insideid = hero.key + 1;
				}
			}
			//开始更新英雄key值，保证房间内唯一
			for(java.util.Map.Entry<Integer, Integer> v : this.battlerole1.useherokeylist.entrySet())	
			{
				for(chuhan.gsp.Hero hero : this.battlerole1.heroList)
				{
					if(hero.key == v.getValue())
					{
						int key = this.getinsideid();
						hero.key = key;
						v.setValue(key);
						continue;
					}
				}
				v.setValue(0);	//英雄列表里无此英雄（理论不会发生）
			}
			
			for(java.util.Map.Entry<Integer, Integer> v : this.battlerole2.useherokeylist.entrySet())	
			{
				for(chuhan.gsp.Hero hero : this.battlerole2.heroList)
				{
					if(hero.key == v.getValue())
					{
						int key = this.getinsideid();
						hero.key = key;
						v.setValue(key);
						continue;
					}
				}
				v.setValue(0);	//英雄列表里无此英雄（理论不会发生）
			}
		}
		
	}
	
}
