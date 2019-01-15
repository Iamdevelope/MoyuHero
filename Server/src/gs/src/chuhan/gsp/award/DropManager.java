package chuhan.gsp.award;

import java.text.ParseException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;















import chuhan.gsp.Dictionary;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.hero.HeroColumn;
import chuhan.gsp.hero.HeroSkinColumn;
import chuhan.gsp.hero.PAddHero;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.innerdrop16;
import chuhan.gsp.item.normaldrop15;
import chuhan.gsp.log.LogBehavior;
import chuhan.gsp.log.Logger;
import chuhan.gsp.mail.MailColumn;
import chuhan.gsp.mail.PAddMail;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.msg.Message;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.play.activity.ActivityManager;
import chuhan.gsp.util.LogUtil;
import chuhan.gsp.util.ParserString;

/**
 * 掉落管理类
 * @author ylk
 *
 */
public class DropManager {	
	public static Logger logger = Logger.getLogger(DropManager.class);
	public static DropManager instance = new DropManager();
	
	private static final int PERCENT_MAX = 10000;
	
	public static final int ALLNOTFULL = 0;
	public static final int HEROFULL = -1;
	public static final int BAGFULL = -2;
	
	private DropManager() {

	}

	public static DropManager getInstance() {

		return instance;
	}
	
	public static void reload() throws Exception
	{
//		DropManager newInstance = new DropManager();
//		newInstance.init();
//		instance = newInstance;
	}
	
	public void init() throws ParseException
	{
		
	}
	/**
	 * 掉落并保存入口
	 * @param roleid
	 * @param dropStr
	 * @param reason
	 * @param loopNum
	 * @return
	 */
	public List<Integer> drop(long roleid, String dropStr, String reason){
		List<Integer> nmDropIdList = ParserString.parseString2Int(dropStr);
		return drop(nmDropIdList,roleid,reason,true,-1,false,1);
	}
	
	/**
	 * 掉落并保存入口(多次循环)
	 * @param roleid
	 * @param dropStr 大包掉落字符串
	 * @param reason
	 * @param loopNum	循环掉落次数
	 */
	public List<Integer> drop(long roleid, String dropStr, String reason,int loopNum){
		List<Integer> nmDropIdList = ParserString.parseString2Int(dropStr);
		return drop(nmDropIdList,roleid,reason,true,-1,false,loopNum);
	}
	
	/**
	 * 掉落并保存入口(新手引导必须掉落用)
	 * @param roleid
	 * @param dropStr
	 * @param reason
	 * @param notMail
	 * @return
	 */
	public List<Integer> drop(long roleid, String dropStr, String reason,boolean notMail){
		List<Integer> nmDropIdList = ParserString.parseString2Int(dropStr);
		return drop(nmDropIdList,roleid,reason,true,-1,notMail,1);
	}
	
	/**
	 * 掉落并是否保存入口
	 * @param roleid
	 * @param dropStr  大包掉落字符串
	 * @param reason
	 * @param isAdd
	 * @param battleId
	 * @return
	 */
	public List<Integer> drop(long roleid, String dropStr, String reason,boolean isAdd,int battleId){
		List<Integer> nmDropIdList = ParserString.parseString2Int(dropStr);
		return drop(nmDropIdList,roleid,reason,isAdd,battleId,false,1);
	}
	
	/**
	 * 掉落是否并判断是否保存
	 * @param nmDropIdList 大包列表
	 * @param roleId
	 * @param reason
	 * @param isAdd
	 * @param addType
	 * @return
	 */
	private List<Integer> drop(List<Integer> nmDropIdList ,long roleId,String reason,boolean isAdd,int battleId,
			boolean notMail,int loopNum){
		List<Integer> result = new ArrayList<Integer>();
		if(nmDropIdList == null)
			return null;
//		boolean isFull = false;
		MailColumn col = null;
		int num = ALLNOTFULL;
		for(int i = 0;i<loopNum;i++){
			for(Integer nmDropId : nmDropIdList){
				List<Integer> inDropGroupIdList = getInDpIdListByNmId(nmDropId);
				List<Integer> innerkeyList = getInkeyListByInDpIdList(inDropGroupIdList,battleId);
				if(innerkeyList != null){
					for(Integer innerkey : innerkeyList){
						if(innerkey.intValue() != -1){
							innerdrop16 innerInit = ConfigManager.getInstance().getConf(innerdrop16.class).get(innerkey);
							//掉落物品ID为-1时，未不掉落，不显示到客户端
							if(innerInit != null && innerInit.getObjectid() != -1){
								result.add(innerkey);
							}
						}
					}
				}
//				result.addAll(innerIdList);
				if(isAdd){
					if(num == ALLNOTFULL){
						num = innerIdListToDrop(innerkeyList,roleId,reason,notMail,false);
					}else{
						if(col == null){
							col = MailColumn.getMailColumn(roleId, false);
						}
						this.sendMail(roleId, innerkeyList, 1.0f, num, true,col);
					}
				}
			}
		}
		return result;
	}
	
	/**
	 * 判断是否超过背包容量
	 * @param roleId
	 * @param heroNum
	 * @param itemNum
	 * @return
	 */
	public int getIsFull(int heroNum,int itemNum,long roleId,boolean notSendFullMsg){
		if(heroNum == 0 && itemNum == 0){
			return ALLNOTFULL;
		}
		chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
		if( heroNum != 0 ){
			HeroColumn herocol = HeroColumn.getHeroColumn(roleId, false);
			if( prole.getMaxHeroSize() < herocol.getxcolumn().getHeroes().size() + heroNum ){
				if( !notSendFullMsg ){
					Message.psendMsgNotify(roleId, MsgIdManager.HEROBAG_IS_FULL.getType());
				}
				return HEROFULL;
			}
		}
		if( itemNum != 0 ){	
			List<Integer> bagIdList = new ArrayList<>();
			bagIdList.add(BagTypes.BAG);
			bagIdList.add(BagTypes.EQUIP);
			if(Module.isMorethanMax(roleId, bagIdList, prole.getMaxItemSize(),itemNum)){
				if( !notSendFullMsg ){
					Message.psendMsgNotify(roleId, MsgIdManager.ITEMBAG_IS_FULL.getType());
				}
				return BAGFULL;
			}			
		}
		return 0;
	}
	
	/**
	 * 掉落保存
	 * @param innerIdList
	 * @param roleId
	 * @param reason
	 * @param notMail
	 * @return  背包是否已经满了
	 */
	public int innerIdListToDrop(List<Integer> innerIdList,long roleId,String reason,boolean notMail,
			boolean notSendFullMsg){
		int num = sendMailOrDropAdd(roleId,innerIdList,1.0f,notSendFullMsg);
		if( notMail || num == ALLNOTFULL){
			List<innerdrop16> innerList = getInnerByInnerIdList(innerIdList);
			innerTodrop(innerList,roleId,reason,1.0f);
		}
		return num;
	}
	/**
	 * 掉落保存(关卡用)
	 * @param innerIdList
	 * @param roleId
	 * @param reason
	 * @param notMail
	 * @param addNum
	 */
	public void innerIdListToDrop(List<Integer> innerIdList,long roleId,String reason,boolean notMail,float addNum){
		int num = sendMailOrDropAdd(roleId,innerIdList,1.0f,false);
		if( notMail || num == ALLNOTFULL){
			List<innerdrop16> innerList = getInnerByInnerIdList(innerIdList);
			innerTodrop(innerList,roleId,reason,addNum);
		}
	}
	/**
	 * 判断是否包满发邮件
	 * @param roleId
	 * @param dropList
	 * @return
	 */
	private int sendMailOrDropAdd(long roleId,List<Integer> dropList,float addNum,boolean notSendFullMsg){
		int[] array = this.getNumByinnerIdList(dropList);
		int num = this.getIsFull(array[0], array[1], roleId,notSendFullMsg);
		if(num == ALLNOTFULL){
			return num;
		}else{
			MailColumn col = MailColumn.getMailColumn(roleId, false);
			this.sendMail(roleId, dropList, addNum, num, notSendFullMsg,col);
//			Message.psendMsgNotify(roleId, 135);
//			return true;
		}
		return num;
	}
	
	/**
	 * 发送邮件
	 * @param roleId
	 * @param dropList
	 * @param addNum
	 * @param num
	 * @param notSendFullMsg
	 */
	private void sendMail(long roleId,List<Integer> dropList,float addNum,int num,boolean notSendFullMsg,
			MailColumn col){
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		String sender = "mail_tips1";
		String title = "mail_tips3";
		String msg = "mail_tips5";
		if(num == BAGFULL){
			title = "mail_tips2";
			msg = "mail_tips4";
		}
//		MailColumn col = MailColumn.getMailColumn(roleId, false);
		col.addMail(col.createMail(sender,title,msg, null, this.getMailItemListByInnerkey(dropList,addNum), 
				now+MailColumn.DEFAULT_TIME,null),notSendFullMsg);
	}
	/**
	 * 发送邮件或者掉落
	 * @param roleId
	 * @param obj
	 * @param num
	 * @param par
	 * @param reason
	 */
	public void sendMailOrDropAdd(long roleId,int obj,int num,int par,String reason){
		List<Integer> objectId = new ArrayList<Integer>();
		List<Integer> objectNum = new ArrayList<Integer>();
		List<Integer> parList = new ArrayList<Integer>();
		objectId.add(obj);
		objectNum.add(num);
		parList.add(par);
		this.sendMailOrDropAdd(roleId, objectId, objectNum, parList, reason);
	}
	/**
	 * 发送邮件或者掉落
	 * @param roleId
	 * @param objList
	 * @param numList
	 * @param parList
	 * @param reason
	 */
	public void sendMailOrDropAdd(long roleId,List<Integer> objList,List<Integer> numList,List<Integer> parList,
			String reason){
		int[] array = this.getNumByList(objList, numList);
		int num = this.getIsFull(array[0], array[1], roleId,false);
		if(num == ALLNOTFULL){
			for(int i = 0;i<objList.size();i++){
				int numadd = 1;
				if(numList != null){
					numadd = numList.get(i);
				}
				int par1 = 0;
				if(parList != null){
					par1 = parList.get(i);
				}
				this.dropAdd(objList.get(i), numadd, par1, 0, roleId, reason);
			}
		}else{
			long now = chuhan.gsp.main.GameTime.currentTimeMillis();
			String sender = "mail_tips1";
			String title = "mail_tips3";
			String msg = "mail_tips5";
			if(num == BAGFULL){
				title = "mail_tips2";
				msg = "mail_tips4";
			}
			MailColumn col = MailColumn.getMailColumn(roleId, false);
			col.addMail(col.createMail(sender,title,msg, null, this.getMailItemListByList(objList,numList,parList), 
					now+MailColumn.DEFAULT_TIME,null),false);
		}
	}
	/**
	 * 通过掉落包转换成邮件物品列表
	 * @param innerKeyList
	 * @return
	 */
	public List<xbean.MailItem> getMailItemListByInnerkey(List<Integer> innerKeyList,float addNum){
		List<innerdrop16> innerList = this.getInnerByInnerIdList(innerKeyList);
		List<xbean.MailItem> items = new LinkedList<xbean.MailItem>();
		for( innerdrop16 inner : innerList ){
			xbean.MailItem test = xbean.Pod.newMailItem();
			test.setObjectid(inner.getObjectid());
			test.setDropnum((int) ((float)inner.getDropnum() * addNum));
			test.setDropparameter1(inner.getDropparameter1());
			test.setDropparameter2(inner.getDropparameter2());
			items.add(test);
		}
		return items;
	}
	/**
	 * 通过掉落物品list转换成邮件物品列表
	 * @param objList
	 * @param numList
	 * @param parList
	 * @return
	 */
	public List<xbean.MailItem> getMailItemListByList(List<Integer> objList,List<Integer> numList,List<Integer> parList){
		List<xbean.MailItem> items = new LinkedList<xbean.MailItem>();
		for(int i = 0;i< objList.size();i++){
			int numadd = 1;
			if(numList != null){
				numadd = numList.get(i);
			}
			int par1 = 0;
			if(parList != null){
				par1 = parList.get(i);
			}
			xbean.MailItem test = xbean.Pod.newMailItem();
			test.setObjectid(objList.get(i));
			test.setDropnum(numadd);
			test.setDropparameter1(par1);
			test.setDropparameter2(0);
			items.add(test);
		}
		return items;
	}
	
	/**
	 * 获取掉落map
	 * @param innerIdList
	 * @return
	 */
	public Map<Integer,Integer> getInnerDropMap(List<Integer> innerIdList){
		Map<Integer,Integer> result = new HashMap<Integer,Integer>();
		List<innerdrop16> innerList = getInnerByInnerIdList(innerIdList);
		for(innerdrop16 inner : innerList){
			Integer num = result.get(inner.getObjectid());
			if(num == null){
				result.put(inner.getObjectid(), inner.getDropnum());
			}else{
				result.put(inner.getObjectid(), num + inner.getDropnum());
			}
		}
		return result;
	}
	
	/**
	 * 获取抽奖的掉落物对象
	 * @param innerIdList
	 * @return
	 */
	public chuhan.gsp.play.lottery.Items getLottery(List<Integer> innerIdList){
		chuhan.gsp.play.lottery.Items result = new chuhan.gsp.play.lottery.Items();
		List<innerdrop16> innerList = getInnerByInnerIdList(innerIdList);
		for(innerdrop16 inner : innerList){
			result.itemid = inner.getObjectid();
			result.num = inner.getDropnum();
			break; // 因为抽奖保证只掉一种，所以直接跳出循环即可
		}
		return result;
	}
	
	/**
	 * 小包列表进行逐个掉落
	 * @param innerList
	 * @param roleId
	 * @param reason
	 */
	private void innerTodrop(List<innerdrop16> innerList,long roleId,String reason,float addNum){
		if(innerList == null)
			return;		
		for(innerdrop16 innerDrop : innerList){
			dropAdd(innerDrop.getObjectid(),(int)((float)innerDrop.getDropnum()*addNum),innerDrop.getDropparameter1(),
					innerDrop.getDropparameter2(),roleId,reason);
		}
	}
	
	/**
	 * 根据分类处理掉落(供其他类调用)
	 * @param objectid
	 * @param dropnum
	 * @param dropparameter1
	 * @param dropparameter2
	 * @param roleId
	 * @param reason
	 */
	public void dropAddByOther(int objectid,int dropnum,int dropparameter1,int dropparameter2,
			long roleId,String reason){
		this.sendMailOrDropAdd(roleId, objectid, dropnum, dropparameter1, reason);
//		dropAdd(objectid,dropnum,dropparameter1,dropparameter2,roleId,reason);
	}
	
	/**
	 * 根据分类处理掉落
	 * @param objectid
	 * @param dropnum
	 * @param dropparameter1
	 * @param dropparameter2
	 * @param roleId
	 * @param reason
	 */
	private void dropAdd(int objectid,int dropnum,int dropparameter1,int dropparameter2,
			long roleId,String reason){
		chuhan.gsp.attr.PropRole prole = chuhan.gsp.attr.PropRole.getPropRole(roleId, true);
		if(prole == null){
			return;
		}
		LogUtil.logInfoWhileCommit(LogBehavior.DROPMADD,logger, prole,
				String.valueOf(objectid),String.valueOf(dropnum),String.valueOf(dropparameter1)
				,String.valueOf(dropparameter2),reason);
//		logger.infoWhileCommit("dropAdd:"+prole.getProperties().getRolename() + ":"+roleId + ":" + objectid +":" + 
//				dropnum +"--"+dropparameter1+"--"+dropparameter2 + ":"+reason);
		int idBegin = IDManager.getInstance().getIdBegin(objectid);
/*		if((idBegin == IDManager.BEGIN_FUWEN || idBegin == IDManager.BEGIN_ITEM) && !isAddItem){
			logger.info("用户："+roleId+"物品背包已满，无法添加"+objectid);
			return;
		}
		if(idBegin == IDManager.BEGIN_HERO && !isAddHero){
			logger.info("用户："+roleId+"英雄背包已满，无法添加"+objectid);
			return;
		}*/
		switch(idBegin){
		case IDManager.BEGIN_ZIYUAN:
			addZiYuan(objectid,dropnum,roleId,reason);
			break;
		case IDManager.BEGIN_FUWEN:
			addItem(objectid,dropnum,roleId,reason);
			break;
		case IDManager.BEGIN_ITEM:
			addItem(objectid,dropnum,roleId,reason);
			break;
		case IDManager.BEGIN_HERO:
			addHero(objectid,dropnum,dropparameter1,roleId,reason);
			break;	
		case IDManager.BEGIN_SKIN:
			addSkin(objectid,roleId);
			break;
		}
	}
	/**
	 * 根据小包判断有多少物品和英雄
	 * @param innerList
	 * @return
	 */
	public int[] getNumByinnerList(List<innerdrop16> innerList){
		int result[] = {0,0};
		if(innerList == null){
			return result;
		}
		for(innerdrop16 innerDrop : innerList){
			int type = isHeroOrItem(innerDrop.getObjectid());
			if(type == 2){
				result[0] += innerDrop.getDropnum();
			}else if(type == 1){
				result[1] += innerDrop.getDropnum();
			}else if(type == 0){
				result[1]++;
			}
		}
		return result;
	}
	/**
	 * 根据小包ID判断有多少物品和英雄
	 * @param innerIdList
	 * @return
	 */
	public int[] getNumByinnerIdList(List<Integer> innerIdList){
		int result[] = {0,0};
		if(innerIdList == null){
			return result;
		}
		for(Integer innerDropId : innerIdList){
			innerdrop16 innerDrop = ConfigManager.getInstance().getConf(innerdrop16.class).get(innerDropId);
			if(innerDrop == null){
				continue;
			}
			int type = isHeroOrItem(innerDrop.getObjectid());
			if(type == 2){
				result[0] += innerDrop.getDropnum();
			}else if(type == 1){
				result[1] += innerDrop.getDropnum();
			}else if(type == 0){
				result[1]++;
			}
		}
		return result;
	}
	/**
	 * 根据类别判断有多少物品和英雄
	 * @param objectList
	 * @param nums
	 * @return
	 */
	public int[] getNumByList(List<Integer> objectList,List<Integer> nums){
		int result[] = {0,0};
		if(objectList == null){
			return result;
		}
		if(nums == null){
			for (Integer objectid : objectList) {
				int type = isHeroOrItem(objectid);
				if(type == 2){
					result[0]++;
				}else if(type == 1){
					result[1]++;
				}else if(type == 0){
					result[1]++;
				}
			}
		}else if( objectList.size() == nums.size() ){
			for(int i = 0;i< objectList.size();i++){
				int type = isHeroOrItem(objectList.get(i));
				if(type == 2){
					result[0] += nums.get(i);
				}else if(type == 1){
					result[1] += nums.get(i);
				}else if(type == 0){
					result[1]++;
				}
			}
		}
		return result;
	}
	/**
	 * 根据邮件附件物品列表判断有多少物品和英雄
	 * @param objectList
	 * @return
	 */
	public int[] getNumByMailItemList(List<xbean.MailItem> mailItemList){
		int result[] = {0,0};
		if(mailItemList == null){
			return result;
		}
		for(xbean.MailItem mailItem : mailItemList){
			int type = isHeroOrItem(mailItem.getObjectid());
			if(type == 2){
				result[0] += mailItem.getDropnum();
			}else if(type == 1){
				result[1] += mailItem.getDropnum();
			}else if(type == 0){
				result[1]++;
			}
		}
		return result;
	}
	/**
	 * 判断objectid是英雄还是物品
	 * @param objectid
	 * @return 英雄1，物品0，其他-1
	 */
	public int isHeroOrItem(int objectid){
		int idBegin = IDManager.getInstance().getIdBegin(objectid);
		if( IDManager.BEGIN_HERO == idBegin ){
			return 2;
		}else if( idBegin == IDManager.BEGIN_FUWEN ){
			return 1;
		}else if( idBegin == IDManager.BEGIN_ITEM ){
			return 0;
		}
		return -1;
	}
	
	/**
	 * 加资源数量
	 * @param objectid
	 * @param dropnum
	 */
	private void addZiYuan(int objectid,int dropnum,long roleId,String reason){
		//英雄克隆用的英雄之血
		if(IDManager.getInstance().isHeroClone(objectid)){
			ActivityManager.getInstance().addHeroClone(roleId, objectid);
			return;
		}
		
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(prole == null)
			return;
		switch(objectid){
		case IDManager.YUANBAO:
			prole.addYuanBao(dropnum,0);
			break;
		case IDManager.GOLD:
			prole.addGold(dropnum, 0);
			break;
		case IDManager.TILI:
			prole.addTili(dropnum);
			break;
		case IDManager.JINENGDIAN:
			prole.addJinengdian(dropnum);
			break;
		case IDManager.PVPTILI:
			break;
		case IDManager.TANXIANTILI:
			prole.addTXTili(dropnum);
			break;
		case IDManager.CHUANSHUOZS:
			xbean.bossrole bossrole = chuhan.gsp.play.wordboss.Module.getxbossrole(roleId, false);
			bossrole.setChuanshuozs(bossrole.getChuanshuozs() + dropnum);
			break;
		default:
			prole.addZiYuan(dropnum, 0,objectid);
		}
	}
	
	/**
	 * 加物品
	 * @param objectid
	 * @param dropnum
	 */
	private void addItem(int objectid,int dropnum,long roleId,String reason){
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				ItemColumn itemcol = Module.getItemColumnByItemId(roleId, objectid, false);
				if(itemcol != null)
					itemcol.addItem(objectid, dropnum, reason, 1);
				return true;
			};
		}.call();
	}
	
	/**
	 * 加英雄
	 * @param objectid
	 * @param dropnum
	 * @param dropparameter1
	 */
	private void addHero(int objectid,int dropnum,int dropparameter1,long roleId,String reason){	
		for(int i = 0;i<dropnum;i++){	
			PAddHero pAddHero = new PAddHero(roleId, objectid,dropparameter1,reason);
			pAddHero.call();
		}
	}
	
	/**
	 * 加皮肤
	 * @param skinId
	 * @param roleId
	 */
	private void addSkin(int skinId,long roleId){
		HeroSkinColumn skincol = HeroSkinColumn.getHeroSkinColumn(roleId, false);
		skincol.addSkin(skinId,true);
	}
	
	/**
	 * 根据小包ID列表获取小包数据（最终掉落）
	 * @param innerIdList
	 * @return
	 */
	public List<innerdrop16> getInnerByInnerIdList(List<Integer> innerIdList){
		List<innerdrop16> result = new ArrayList<innerdrop16>();
		if(innerIdList == null)
			return result;
		for(Integer innerId : innerIdList){
			innerdrop16 innerDrop = ConfigManager.getInstance().getConf(innerdrop16.class).get(innerId);
			if(innerDrop != null)
				result.add(innerDrop);
		}
		return result;		
	}
	
	/**
	 * 根据掉落小包list返回掉落物品map
	 * @param innerIdList
	 * @return
	 */
	public Map<Integer,Integer> getDropObjectMapByInnerList(List<Integer> innerKeyList){
		Map<Integer,Integer> result = new HashMap<Integer,Integer>();
		for(Integer innerKey : innerKeyList){
			innerdrop16 innerDrop = ConfigManager.getInstance().getConf(innerdrop16.class).get(innerKey);
			if(innerDrop != null){
				Integer num = result.get(innerDrop.getObjectid());
				if(num != null){
					num += innerDrop.getDropnum();
					result.put(innerDrop.getObjectid(), num);
				}else{
					result.put(innerDrop.getObjectid(), innerDrop.getDropnum());
				}
			}
		}
		return result;
	}
	
	/**
	 * 根据小包掉落组(多个掉落组id)计算掉落小包
	 * @param inDropIdList
	 * @return
	 */
	private List<Integer> getInkeyListByInDpIdList(List<Integer> inDropIdList,int battleId){
		List<Integer> result = new ArrayList<Integer>();
		if(inDropIdList == null)
			return result;
		
		//特殊活动增加值
		float heroAdd = 1.0f;
		float itemAdd = 1.0f;
		if(battleId != -1){
			float[] add = ActivityGameManager.getInstance().dropAddArray(battleId);
			heroAdd = add[0];
			itemAdd = add[1];
		}
		
		for(Integer inDropId : inDropIdList){
			if(inDropId == -1){
				continue;
			}
			List<innerdrop16> allInnerList = getAllInnerListByInDpId(inDropId);
			if(allInnerList == null || allInnerList.size() == 0)
				continue;
			//根据小包掉落类型计算掉落数据
			if(allInnerList.get(0).getInnerdroptype() == 1){
				for(int i = 0;i < allInnerList.get(0).getInnerdroptime();i++){
					for(innerdrop16 inDrop : allInnerList){
						result.add(inDrop.getId());
					}
				}
			}else if(allInnerList.get(0).getInnerdroptype() == 2){
				for(innerdrop16 innerdrop : allInnerList){
					List<innerdrop16> tempList = new ArrayList<innerdrop16>();
					tempList.add(innerdrop);
					for(int i = 0;i<innerdrop.getInnerdroptime();i++){
						HashMap<Integer,DropInit> dropMap = getDropMap(tempList,PERCENT_MAX,heroAdd,itemAdd);
						List<Integer> dropList = getDropIdList(dropMap,1);
						result.addAll(dropList);
					}
				}
			}else if(allInnerList.get(0).getInnerdroptype() == 3){
				HashMap<Integer,DropInit> dropMap = getDropMap(allInnerList,0,heroAdd,itemAdd);
				List<Integer> dropList = getDropIdList(dropMap,allInnerList.get(0).getInnerdroptime());
				result.addAll(dropList);
			}
		}
		return result;
	}
	
	/**
	 * 根据大包掉落获取小包掉落组
	 * @param normalDropId
	 * @return
	 */
	private List<Integer> getInDpIdListByNmId(int normalDropId){
		normaldrop15 nmdrop = ConfigManager.getInstance().getConf(normaldrop15.class).get(normalDropId);
		if(nmdrop == null){
			return null;
		}
		String indrop = nmdrop.getInnerdrop();
		String inProb = nmdrop.getInnerdropprob();
		
		/*//特殊活动判断(临时test写法，活动表还没有)
		int actNum = 100;
		if( actNum == 0 ){
			List<Integer> actindex = ParserString.parseString2Int(nmdrop.getActivitydropindex());
			List<String> actdrop = ParserString.parseString(nmdrop.getActivitydrop(),"@");
			List<String> actprob = ParserString.parseString(nmdrop.getActivitydropprob(),"@");
			if(actindex == null || actdrop == null || actprob == null){
				return null;
			}
			for(int i = 0; i < actindex.size() ; i++){
				if( actindex.get(i) == actNum ){
					if( actdrop.size() <= i || actprob.size() <= i ){
						return null;
					}
					indrop = actdrop.get(i);
					inProb = actprob.get(i);
				}
			}
		}
		*/
		
		//全掉落
		if(nmdrop.getNormaldroptype() == 1){
			List<Integer> result = new ArrayList<Integer>();
			List<Integer> parserList = ParserString.parseString2Int(indrop);
			for(int i = 0;i < nmdrop.getNormaldroptime(); i++){
				result.addAll(parserList);
			}
			return result;
		}
		
		List<Integer> allDrop = ParserString.parseString2Int(indrop);
		List<Integer> allProb = ParserString.parseString2Int(inProb);
		if(allDrop == null || allProb == null || allDrop.size() != allProb.size())
			return null;
		
		//根据几率掉落
		if(nmdrop.getNormaldroptype() == 2){
			List<Integer> result = new ArrayList<Integer>();
			for(int i = 0; i < allDrop.size(); i++){
				for(int j = 0 ; j<nmdrop.getNormaldroptime(); j++){
					HashMap<Integer,DropInit> dropMap = getDropMap(allDrop.get(i),allProb.get(i),PERCENT_MAX);
					result.addAll(getDropIdList(dropMap,1));
				}
			}
			return result;
		}
		if(nmdrop.getNormaldroptype() == 3){
			HashMap<Integer,DropInit> dropMap = getDropMap(allDrop,allProb,0,nmdrop.getNormaldroptime());
			return getDropIdList(dropMap,nmdrop.getNormaldroptime());
		}
		return null;
	}
	/**
	 * 通过掉落数组构建掉落map
	 * @param allDrop
	 * @param allProb
	 * @param maxnum
	 * @return
	 */
	public HashMap<Integer,DropInit> getDropMap(List<Integer> allDrop,List<Integer> allProb, int maxnum){
		return getDropMap(allDrop,allProb,maxnum,1);
	}
	/**
	 * 通过单个掉落数组构建掉落map
	 * @param inDrop
	 * @param inProb
	 * @param maxnum
	 * @return
	 */
	public HashMap<Integer,DropInit> getDropMap(int inDrop,int inProb, int maxnum){
		List<Integer> allDrop = new ArrayList<Integer>();
		List<Integer> allProb = new ArrayList<Integer>();
		allDrop.add(inDrop);
		allProb.add(inProb);
		return getDropMap(allDrop,allProb,maxnum,1);
	}
	/**
	 * 通过掉落数组构建掉落map
	 * @param allDrop
	 * @param allProb
	 * @param maxnum
	 * @param dropnum  可掉落次数
	 */
	public HashMap<Integer,DropInit> getDropMap(List<Integer> allDrop,List<Integer> allProb, int maxnum,int dropnum){
		HashMap<Integer,DropInit> dropMap = new HashMap<Integer,DropInit>();
		if(allDrop == null || allProb == null || allDrop.size() != allProb.size())
			return dropMap;

		int percentCount = 0;
		for(int i = 0;i< allDrop.size();i++){
			int id = allDrop.get(i);
			int percent = allProb.get(i);
			percentCount += percent;
			DropInit di = new DropInit(percent, id,dropnum);
			dropMap.put(dropMap.size(), di);
		}
		if(maxnum != 0){
			int percent = maxnum - percentCount;
			if(percent > 0){
				DropInit di = new DropInit(percent, -1, Integer.MAX_VALUE);
				dropMap.put(dropMap.size(), di);
			}
		}
		return dropMap;
	}
	
	/**
	 * 通过掉落数组构建掉落map
	 * @param allDrop
	 * @param maxnum
	 * @param dropnum
	 * @return
	 */
	public HashMap<Integer,DropInit> getDropMap(List<Integer> allDrop, int maxnum,int dropnum){
		HashMap<Integer,DropInit> dropMap = new HashMap<Integer,DropInit>();
		if(allDrop == null)
			return dropMap;

		int percentCount = 0;
		for(int i = 0;i< allDrop.size();i++){
			int id = allDrop.get(i);
			int percent = 1;
			percentCount += percent;
			DropInit di = new DropInit(percent, id,dropnum);
			dropMap.put(dropMap.size(), di);
		}
		if(maxnum != 0){
			int percent = maxnum - percentCount;
			if(percent > 0){
				DropInit di = new DropInit(percent, -1, Integer.MAX_VALUE);
				dropMap.put(dropMap.size(), di);
			}
		}
		return dropMap;
	}
	
	/**
	 * 从小包列表获取掉落map
	 * @param inDropList
	 * @param maxnum
	 * @return
	 */
	private HashMap<Integer,DropInit> getDropMap(List<innerdrop16> inDropList, int maxnum,
			float heroAdd,float itemAdd){
		HashMap<Integer,DropInit> dropMap = new HashMap<Integer,DropInit>();
		if(inDropList == null)
			return dropMap;
		int percentCount = 0;
		for(innerdrop16 inDrop : inDropList){
			int id = inDrop.getId();
			int percent = inDrop.getDropwight();
			int idBegin = IDManager.getInstance().getIdBegin(id);
			if( idBegin == IDManager.BEGIN_FUWEN || idBegin == IDManager.BEGIN_ITEM ){
				percent = (int)((float)percent * itemAdd);
			}else if( idBegin == IDManager.BEGIN_HERO ){
				percent = (int)((float)percent * heroAdd);
			}
			percentCount += percent;
			DropInit di = new DropInit(percent, id,inDrop.getInnerdroptime());
			dropMap.put(dropMap.size(), di);
		}
		if(maxnum != 0){
			int percent = maxnum - percentCount;
			if(percent > 0){
				DropInit di = new DropInit(percent, -1, Integer.MAX_VALUE);
				dropMap.put(dropMap.size(), di);
			}
		}
		return dropMap;
	}
	
	/**
	 * 根据掉落组id获取所有小包唯一ID
	 * @param innerdropId
	 * @return
	 */
	private List<innerdrop16> getAllInnerListByInDpId(int innerdropId){
		List<innerdrop16> result = new ArrayList<innerdrop16>();
		TreeMap<Integer,innerdrop16> treeMap = ConfigManager.getInstance().getConf(innerdrop16.class);
		for(Map.Entry<Integer, innerdrop16> entry : treeMap.entrySet()){
			if(entry.getValue().getInnerdropid() == innerdropId){
				result.add(entry.getValue());
			}
		}
		return result;
	}
	/**
	 * 根据小包ID获得小包key
	 * @param innerdropId
	 * @return
	 */
	public List<Integer> getAllInnerKeyListByInDpId(int innerdropId){
		List<Integer> result = new ArrayList<Integer>();
		TreeMap<Integer,innerdrop16> treeMap = ConfigManager.getInstance().getConf(innerdrop16.class);
		for(Map.Entry<Integer, innerdrop16> entry : treeMap.entrySet()){
			if(entry.getValue().getInnerdropid() == innerdropId){
				result.add(entry.getValue().getId());
			}
		}
		return result;
	}
	
	/**
	 * 根据掉落比例和次数随机掉落组
	 * @param dropMap
	 * @param numMax
	 * @return
	 */
	public List<Integer> getDropIdList(Map<Integer,DropInit> dropMap, int numMax){
		List<Integer> result = new ArrayList<Integer>();
		if(dropMap == null)
			return result;
//		int resultSize = result.size();
		for(int num = 0;result.size()<numMax; num++){
			int[] percentlist = new int[dropMap.size()];
			int i = 0;
			for (java.util.Map.Entry<Integer, DropInit> drop : dropMap.entrySet()) {
				percentlist[i++] = drop.getValue().percent;
			}
			int dropnum = chuhan.gsp.util.Misc.getProbability(percentlist);
			if(dropnum == -1){
				result.add(-1);
				continue;
			}
			DropInit drop = dropMap.get(dropnum);
			if(drop == null){
				result.add(-1);
				continue;
			}
			if(drop.num > 0){
				drop.num--;
				result.add(drop.id);
				//当对象掉落次数为0以后，则从队列里删除此物品（防止随机到此物品浪费次数）
				if(drop.num <= 0){
					dropMap = getNewDropMap(dropMap,drop.sameType,dropnum);
				}
				continue;
			}
			//防止掉落死循环
			if( num - numMax > (numMax > 20 ? numMax : 20) ){
				break;
			}
		}
		return result;
	}
	
	public Map<Integer,DropInit> getNewDropMap(Map<Integer,DropInit> dropMap,int sameType,int dropnum){
		dropMap.remove(dropnum);
		Map<Integer,DropInit> result = new HashMap<Integer,DropInit>();
		int i = 0;
		for( Map.Entry<Integer,DropInit> entry : dropMap.entrySet() ){
			if(sameType != -1 && entry.getValue().sameType == sameType ){
				continue;
			}
			result.put(result.size(), entry.getValue());
		}
		return result;
	}
	
	/**
	 * 消耗物品
	 * @param objectidStr
	 * @param usenumStr
	 * @param roleId
	 * @param reason
	 * @return
	 */
	public boolean useDel(String objectidStr,String usenumStr,long roleId,String reason){
		if( (objectidStr == null && usenumStr == null) ||
				objectidStr.equals("") && usenumStr.equals(""))
			return true;
		List<Integer> objectIdList = ParserString.parseString2Int(objectidStr);
		List<Integer> usenumList = ParserString.parseString2Int(usenumStr);
		if(objectIdList == null || usenumList == null || objectIdList.size() != usenumList.size())
			return false;
		for(int i = 0;i<objectIdList.size();i++){
			if(objectIdList.get(i) == -1){
				continue;
			}
			if( !useDel(objectIdList.get(i),usenumList.get(i),roleId,reason) ){
				return false;
			}
		}
		return true;
	}
	
	/**
	 * 根据分类处理
	 * @param objectid
	 * @param usenum
	 * @param roleId
	 * @param reason
	 */
	public boolean useDel(int objectid,int usenum,long roleId,String reason){
		int idBegin = IDManager.getInstance().getIdBegin(objectid);
		LogUtil.logInfoWhileCommit(LogBehavior.USEMDEL,logger, roleId,
				String.valueOf(objectid),String.valueOf(usenum),reason);
		switch(idBegin){
		case IDManager.BEGIN_ZIYUAN:
			return delZiYuan(objectid,usenum,roleId,reason);
		case IDManager.BEGIN_FUWEN:
			return false;
		case IDManager.BEGIN_ITEM:
			return delItem(objectid,usenum,roleId,reason);
		case IDManager.BEGIN_HERO:
			return false;
		default:
			return false;
		}
	}
	
	/**
	 * 消耗资源数量
	 * @param objectid
	 * @param usenum
	 * @param roleId
	 * @param reason
	 */
	private boolean delZiYuan(int objectid,int usenum,long roleId,String reason){
		PropRole prole = PropRole.getPropRole(roleId, false);
		if(prole == null)
			return false;
		switch(objectid){
		case IDManager.YUANBAO:
			return usenum*-1 == prole.delYuanBao(usenum*-1, 0);
		case IDManager.GOLD:
			return usenum*-1 == prole.delGold(usenum*-1, 0);
		case IDManager.TILI:
			return prole.useTili(usenum);
		case IDManager.JINENGDIAN:
			return prole.useJinengdian(usenum);
		case IDManager.PVPTILI:
			return false;
		case IDManager.TANXIANTILI:
			return prole.useTXTili(usenum);
		case IDManager.CHUANSHUOZS:
			xbean.bossrole bossrole = chuhan.gsp.play.wordboss.Module.getxbossrole(roleId, false);
			if(bossrole.getChuanshuozs() > usenum){
				bossrole.setChuanshuozs(bossrole.getChuanshuozs() - usenum);
				return true;
			}
			return false;
		default:
			return usenum*-1 == prole.delZiYuan(usenum*-1, 0,objectid);
		}
	}
	
	/**
	 * 消耗物品
	 * @param objectid
	 * @param usenum
	 */
	private boolean delItem(int objectid,int usenum,long roleId,String reason){
		ItemColumn itemcol = Module.getItemColumn(roleId, 1, false);
		return usenum == itemcol.removeItemById(objectid, usenum, 1, reason);
	}
	
}
