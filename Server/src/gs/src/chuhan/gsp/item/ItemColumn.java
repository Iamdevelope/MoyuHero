package chuhan.gsp.item;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;






import xbean.ItemNumLimit;
import chuhan.gsp.attr.GoldAddType;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.AddItem;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.item.AddItemResult.AddResultEnum;
import chuhan.gsp.play.activity.ActivityGameManager;
import chuhan.gsp.state.State;
import chuhan.gsp.state.StateManager;
import chuhan.gsp.util.Conv;

public class ItemColumn implements Iterable<BasicItem> {
	protected final long roleid;
	protected final xbean.Bag bag;
	protected final boolean readonly;
	protected final SBagConfig conf;
	protected final static int DEPOTFULL_MSGID = 140397;
	protected final static int NOTENOUGHPOS_MSGID = 120059;
	protected final int bagId;
	
//	private static long timetest = 0;
	// private static final Logger logger = Logger.getLogger(ItemColumn.class);
	@SuppressWarnings("unchecked")
	ItemColumn(final long roleid, int bagId, final boolean readonly) {
		if(xtable.Properties.select(roleid) == null)
			throw new IllegalArgumentException("构造ItemColumn:"+bagId+"时，角色 "+roleid+" 不存在。");
		this.readonly = readonly;
		this.roleid = roleid;
		this.bagId = bagId;
		conf = Module.getInstance().getBagConfig(getBagid());
		final xdb.TTable<Long, xbean.Bag> table = (xdb.TTable<Long, xbean.Bag>) xdb.Xdb
				.getInstance().getTables().getTable(conf.tablename);
		if (table == null)
			throw new RuntimeException("未找到table=" + conf.tablename);
		final xbean.Bag mybag;
		if (readonly) {
			mybag = table.select(roleid,
					new xdb.TField<xbean.Bag, xbean.Bag>() {
						@Override
						public xbean.Bag get(xbean.Bag v) {
							return v.toData();
						}
					});
		} else {
			mybag = table.get(roleid);
		}
		if (mybag == null) {
			if (readonly)
				bag = xbean.Pod.newBagData();
			else
				bag = xbean.Pod.newBag();
			bag.setCapacity(conf.initcap);
			if (!readonly)
				table.insert(roleid, bag);
		} else {
			bag = mybag;
		}
	}

	protected int getBagid(){return bagId;};

	/**
	 * 添加物品
	 * 
	 * @param xi
	 * @param pos
	 * @return 物品的key
	 */
	protected int doAdd(xbean.Item xi, int countertype) {
		int nextid;
		while (true) {
			nextid = incNextId();
			// 把0故意跳过
			if (nextid == 0)
				nextid = incNextId();
			
			//临时修改bug
			if(bagId == 1)
				nextid = nextid * 2;
			else
				nextid = nextid * 2 + 1;
			
			if (null == bag.getItems().get(nextid)) {
				bag.getItems().put(nextid, xi);
				if(xi.getUniqueid() == 0)
				{//没有uid，生成一个
					final long uniqueid = Module.getInstance().getUniqueID();
					xi.setUniqueid(uniqueid);
					Module.logger.debug("添加物品uid=" + uniqueid);
				}
				break;
			}
		}
		/*int addnum = getNumber(xi);
		if (logtype != 0) {
			List<Number> itemStrs = new ArrayList<Number>();
	        itemStrs.add(xi.getId());
	        itemStrs.add(addnum);
	        itemStrs.add(xi.getUniqueid());
			//TODO LogUtil.doCreateItemLog(roleid, LogUtil.convertIntToString(itemStrs), logtype);
		}
		SYuanbaoItem yuanbaoitem = Module.getInstance().getItemManager().getYuanbaoItem(xi.getId());
		if (yuanbaoitem != null)
			xdb.Procedure.pexecuteWhileCommit(new PRecordNewItem(xi.getId(), addnum, countertype));*/
		return nextid;
	}
	
	public xbean.Bag getBag()
	{
		return bag;
	}
	
	protected xbean.Item doDelete(int key, boolean removeuid) {
		return bag.getItems().remove(key);
	}

	public static int getNumber(xbean.Item item) {
		int itemnum = item.getNumber();
		for (int num: item.getNumbermap().values()) {
			itemnum += num;
		}
		return itemnum;
	}
	
	public static void pileNumber(Map<Integer, Integer> pilenum, xbean.Item xitem) {
		for (Map.Entry<Integer, Integer> e: pilenum.entrySet()) {
			if (e.getKey() == 0) {
				xitem.setNumber(xitem.getNumber()+e.getValue());
			} else {
				Integer n = xitem.getNumbermap().get(e.getKey());
				if (n==null) {
					xitem.getNumbermap().put(e.getKey(), e.getValue());
				} else {
					xitem.getNumbermap().put(e.getKey(), n+e.getValue());
				}
			}
		}
	}
	/**
	 * 从包裹中移出物品,记得存下来,否则会造成数据冗余
	 * 
	 * @param key
	 * @param number
	 * @param reason
	 * @return
	 */
	public BasicItem moveOut(final int key, final int number, final String reason, boolean isnotify) {
		xbean.Item xi = bag.getItems().get(key);
		if (xi == null)
			return null;
		final xbean.Item removed;
		final int itemnum = getNumber(xi);
		if (number > itemnum) {
			return null;
		}
		if (number == -1 || number == itemnum) {
			removed = doDelete(key, false);
			if(isnotify)
			{
				SRemoveItem send = new SRemoveItem();
				send.bagid = Conv.toByte(getBagid());
				send.itemkeys.add(key);
				xdb.Procedure.psendWhileCommit(roleid, send);
			}
			else
				getBag().getRemovedkeys().add(key);
		} else {
			removed = xi.copy();
			Map<Integer, Integer> splitnum = doSplit(xi, number);
			if (getNumber(xi) <= 0) {
				throw new RuntimeException("物品数量有错误");
			}
			if(StateManager.isEntry(roleid))
			{
				SModItemNum send = new SModItemNum();
				send.bagid = Conv.toByte(getBagid());
				send.curnum = getNumber(xi);
				send.itemkey = key;
				xdb.Procedure.psendWhileCommit(roleid, send);
			}
			removed.setNumber(0);
			removed.getNumbermap().clear();
			pileNumber(splitnum, removed);
			removed.setUniqueid(0);
		}
		Module.logger.debug("拿出物品:" + reason);
		BasicItem item = Module.getInstance().toBasicItem(removed, roleid, getBagid(), key);
		return item;
	}
	/**
	 * 从角色其他包裹栏移动来的物品
	 * @param item
	 * @param pos
	 * @return
	 */
	public boolean moveIn(final BasicItem item, final int pos, final BasicItem dstitem) {
		if (pos == -1) {
			return addItem2AutoPos(item, true, -1);
		} else {
			if (dstitem != null && dstitem.getPosition() == pos) {
				pileItem(dstitem, item.dataItem.getNumber(), item.dataItem.getNumbermap(), 0);
				return true;
			} else
				return addItem2Pos(item, pos, -1);
		}
	}
	
	public final boolean moveIn(final BasicItem item, final int pos) {
		return moveIn(item, pos, null);
	}

	/**
	 * 
	 * @param srcitem
	 * @param dstitem
	 * @param num -1则全部堆叠
	 */
	static void doPile(xbean.Item srcitem, xbean.Item dstitem, int num) {
		
		Map<Integer, Integer> heapnum;
		if (num != -1) {
			assert(getNumber(srcitem) >= num);
			heapnum = doSplit(srcitem, num);
		} else {
			heapnum = new HashMap<Integer, Integer>();
			heapnum.putAll(srcitem.getNumbermap());
			if (srcitem.getNumber() != 0) {
				heapnum.put(0, srcitem.getNumber());
				srcitem.setNumber(0);
			}
			srcitem.getNumbermap().clear();
		}
	//	doLog(roleid, getBagid(), true, srcitem.getId(), num);
		pileNumber(heapnum, dstitem);
		
	//	doLog(roleid, getBagid(), false, dstitem.getId(), num);
	}

	/**
	 * 获得物品在自己包裹里的拥有数量
	 * 
	 * @return
	 */
	public final int getItemOwnsNum(final long roleid, final int itemid) {
		int num = 0;
		for (final BasicItem bi : this) {
			if (bi.getItemid() == itemid)
				num+=bi.getNumber();
		}
		return num;
	}

	public final AddItemResult addItem(final int itemid, final int num, final String reason,
			final int countertype) {
		return addItem(itemid, num, 0, 0, reason, countertype);
	}
	

	
	public final AddItemResult addItem(final int itemid, final int num, final int numtype, final String reason,
			final int countertype) {
		return addItem(itemid, num, numtype, 0, reason, countertype);
	}
	/**
	 * 
	 * @param itemid
	 * @param num
	 * @param maxadd
	 * @return
	 */
	/*private int pileCanpile(final int itemid, final int num, final int numbertype, 
			final int initflag, final int maxadd, final int logtype) {
		int added = 0;
		Map<Integer,Integer> additems = pileCanpileReturn(itemid, num, numbertype, initflag, maxadd, logtype);
		for(int v : additems.values())
			added += v;
		return added;
	}*/
	
	/**
	 * 
	 * @param itemid
	 * @param num
	 * @param maxadd
	 * @return
	 */
	private Map<Integer,Integer> pileCanpile(final int itemid, final int num, final int numbertype, 
			final int initflag, final int maxadd, final int logtype) {
		Map<Integer,Integer> additems = new HashMap<Integer, Integer>();
		int added = 0;
		for (BasicItem bi : this) {
			if (added >= maxadd) {
				return additems;
			}
			int flag = initflag == -1 ? bi.getInitFlag() : initflag;
			if (bi.getItemid() == itemid && bi.getFlags() == flag) {
				int maxpile = bi.getAttr().stackNum - bi.getNumber();
				if (maxpile <= 0)
					continue;
				maxpile = Math.min(maxpile, num - added);
				int pilenum = Math.min(maxadd - added, maxpile);

				if (numbertype == 0) {
					pileItem(bi, pilenum, null, logtype);
				} else {
					Map<Integer, Integer> numbermap = new HashMap<Integer, Integer>();
					numbermap.put(numbertype, pilenum);
					pileItem(bi, 0, numbermap, logtype);
				}
				additems.put(bi.getKey(), pilenum);
				added+=pilenum;
			}
		}
		return additems;
	}
	
	/*public final List<AddItem> addItemAndReturn(final int itemid, final int num, final String reason,final int countertype, final int xiangguanid) {
		return addItem(itemid, num, 0,0,reason,countertype,xiangguanid);
	}
	
	public final List<AddItem> addItemAndReturn(final int itemid, final int num, final String reason) {
		return addItem(itemid, num, 0,0,reason,0,0);
	}*/
	
	/**
	 * 
	 * @param itemid
	 * @param num
	 * @return 添加了多少个物品
	 * @throws Readonly
	 * @throws RuntimeException
	 * @throws IllegalArgumentException
	 */
	public AddItemResult addItem(final int itemid, final int num, final int numtype, 
			final int initflag,
			final String reason,
			final int countertype) {
//		long nowbegin = chuhan.gsp.main.GameTime.currentTimeMillis();
		if (readonly || num <= 0)
			return new AddItemResult(AddResultEnum.FAIL);
		item26 attr = Module.getInstance().getAttr(itemid);
		if (attr == null)
			return new AddItemResult(AddResultEnum.FAIL);
		
		AddItemResult addresult = new AddItemResult(AddResultEnum.SUCC);
		int maxown = Module.getInstance().getMaxOwn(roleid, itemid);
		int maxcanadd = Integer.MAX_VALUE;
		if (maxown != 0) {
			maxcanadd = maxown - getItemOwnsNum(roleid, itemid);
		}
		int pileed = 0;
		Map<Integer,Integer> adds = pileCanpile(itemid, num, numtype, initflag, maxcanadd, countertype);
		for(Map.Entry<Integer,Integer> entry : adds.entrySet())
		{
			pileed += entry.getValue();
			addresult.getAddItems().add(new AddItem(entry.getKey(), itemid, entry.getValue()));
		}
		int leftnum = num - pileed;
		maxcanadd -= pileed;
		
		while (leftnum > 0) {
			if (maxcanadd <= 0) {
				List<String> parameters = new ArrayList<String>();
				parameters.add(attr.name);
				chuhan.gsp.msg.Message.psendMsgNotify(roleid, 142752, parameters);
				return addresult;
			}
			
			if(attr.stackNum <= 0)
				throw new RuntimeException("物品：  " + itemid + "的最大堆叠数错误： " + attr.stackNum);
		
			int gennum = Math.min(attr.stackNum, leftnum);
			gennum = Math.min(gennum, maxcanadd);
			BasicItem bi = Module.getInstance().genBasicItem(itemid, gennum, numtype);
			if (bi == null)
				break;
			if (initflag != 0) {
				bi.setFlag(bi.getFlags() | initflag);
			}
			if (!processAddItem(bi, -1, countertype)) {
				break;
			}
			leftnum -= gennum;
			maxcanadd -= gennum;
			addresult.getAddItems().add(new AddItem(bi.getKey(),itemid, gennum));
		}
		for(int i = 0;i<num;i++){
			//物品相关活动数据统计
			ActivityGameManager.getInstance().addItemActivity(this.roleid, attr, ActivityGameManager.ITEM_GET,0);
		}
//		long nowend = chuhan.gsp.main.GameTime.currentTimeMillis();
//		long nowtest = nowend - nowbegin;
//		timetest += nowtest;
//		DropManager.logger.debug(timetest + "itemaddtime" + nowtest);
		
		return addresult;
	}
	
	public static String getBagname(final int bagtype) {
		switch (bagtype) {
		case BagTypes.BAG: return "包裹";
		case BagTypes.EMPTY: return "Null包裹";
		}
		return "异常包裹";
	}
	
//	1.不包含元宝价值的道具生成时，字段标识为0.
//	2.元宝购买的道具，所花费的元宝全都是系统赠送元宝时，字段标识为1.
//	3.元宝购买的道具，所花费的元宝中包含人民币元宝时，字段标识为2.
//	4.可叠加物品记录三种物品的分别数量。
//	5.可叠加物品在消耗、使用、拆分、给予时，优先顺序为2-1-0
	
	public static Map<Integer, Integer> doSplit(xbean.Item xitem, int num) {
		Map<Integer, Integer> ret = new HashMap<Integer, Integer>();
		int leftnum = num;
		int curflag = 2;
		for (;curflag > 0 && leftnum > 0; curflag--) {
			Integer n = xitem.getNumbermap().get(curflag);
			if (n==null)
				continue;
			if (leftnum >= n) {
				xitem.getNumbermap().remove(curflag);
				ret.put(curflag, n);
				leftnum-=n;
			} else {
				ret.put(curflag, leftnum);
				xitem.getNumbermap().put(curflag, n-leftnum);
				leftnum=0;
			}
		}
		if (leftnum > 0) {
			xitem.setNumber(xitem.getNumber()-leftnum);
			ret.put(0, leftnum);
		}
		return ret;
	}
	
	private boolean addItem2AutoPos(final BasicItem bi, final boolean sendAddItemProtocol, final int logtype) {
//		int maxheap = bi.getAttr().最大堆叠数量;
		if (conf.initcap == 1) {
			pileCanpile(bi);
		}
		if (bi.getNumber() > 0) {
			List<Integer> freeposes = getFreepos();
			if (freeposes.isEmpty())
				return false;
			if (!addItem2Pos(bi, freeposes.get(0), sendAddItemProtocol, logtype))
				return false;
		}
		return true;
	}
	
	private boolean processAddItem(final BasicItem item, final int p, final int logtype) {
		if (p == -1) {
			return addItem2AutoPos(item, true, logtype);
		} else {
			BasicItem dstitem = getItemByPos(p);
			if (dstitem == null) {
				if (!addItem2Pos(item, p, logtype))
					return false;
				
			} else {
				if (!canPile(item.dataItem, dstitem.dataItem, 0))
					return false;
				int maxpilenum = dstitem.getAttr().stackNum - dstitem.getNumber();
				if (maxpilenum < item.getNumber())
					return false;
				pileItem(dstitem,  item.getDataItem().getNumber(), item.getDataItem().getNumbermap(), logtype);
			}
		}
		return true;
	}
	
	private boolean addItem2Pos(final BasicItem bi, final int pos, int logtype) {
		return addItem2Pos(bi, pos, true, logtype);
	}
	private boolean addItem2Pos(final BasicItem bi, final int pos, final boolean sendProcotol, int logtype) {
		if (pos < 0 || pos >= getCapacity())
			return false;
		bi.getDataItem().setPosition(pos);
		int key = this.doAdd(bi.getDataItem(), logtype);
		if (key == 0)
			return false;
		final long oldowner = bi.getOwnerid();
		
		bi.setKey(key);
		bi.setOwnerid(roleid);
		bi.setBagid(getBagid());
	
		if (oldowner == 0) {
			bi.onInsert();
		}
		if (sendProcotol && StateManager.getStateIdByRoleId(roleid) == State.ENTRY_STATE) {
			SAddItem send = new SAddItem();
			send.bagid = Conv.toByte(getBagid());
			send.itemdata.add(bi.getProtocolItem());
			xdb.Procedure.psendWhileCommit(roleid, send);
		}
		return true;
	}
	
	/*private static class PRecordNewItem extends xdb.Procedure {
		private final int itemid;
		private final int itemnum;
		private final int logid;
		PRecordNewItem(final int itemid, final int itemnum, final int logid) {
			this.itemid = itemid; this.itemnum = itemnum; this.logid = logid;
		}
		@Override protected boolean process() {
			 xbean.ItemCreateInfo ici = xtable.Yuanbaoitemcreated.get(itemid);
			 if (ici == null) {
				 ici = xbean.Pod.newItemCreateInfo();
				 xtable.Yuanbaoitemcreated.insert(itemid, ici);
			 }
			 Integer oldlognum = ici.getNuminfo().get(logid);
			 ici.getNuminfo().put(logid, oldlognum == null ? itemnum : oldlognum + itemnum);
			 return true;
		}
	}*/
	
	
	protected void pileItem(final BasicItem dstitem, final int num, 
			final Map<Integer, Integer> nummap, final int countertype) {
		assert(dstitem.getBagid() == getBagid());
		assert(dstitem.getOwnerid() == roleid);

		dstitem.dataItem.setNumber(dstitem.dataItem.getNumber() + num);
		int addnum = num;
		if (nummap != null) {
			for (Map.Entry<Integer, Integer> e : nummap.entrySet()) {
				Integer oldnum = dstitem.dataItem.getNumbermap().get(e.getKey());
				if (oldnum == null) {
					dstitem.dataItem.getNumbermap().put(e.getKey(), e.getValue());
				} else {
					dstitem.dataItem.getNumbermap().put(e.getKey(), e.getValue() + oldnum);
				}
				addnum += e.getValue();
//				pilednum += e.getValue();
			}
		}
		if (addnum != 0) {
			/*final int logtype = getLogtype(countertype);
			if (logtype != 0) {
				xbean.Item xi = dstitem.dataItem;
				List<Number> itemStrs = new ArrayList<Number>();
		        itemStrs.add(xi.getId());
		        itemStrs.add(addnum);
		        itemStrs.add(xi.getUniqueid());
				LogUtil.doCreateItemLog(roleid, LogUtil.convertIntToString(itemStrs), logtype);
			}
			SYuanbaoItem yuanbaoitem = Module.getInstance().getItemManager().getYuanbaoItem(dstitem.getItemid());
			if (yuanbaoitem != null)
				xdb.Procedure.pexecuteWhileCommit(new PRecordNewItem(dstitem.getItemid(), addnum, countertype));*/
			if(StateManager.isEntry(roleid))
			{
				SModItemNum modnum = new SModItemNum();
				modnum.bagid = Conv.toByte(getBagid());
				modnum.curnum = dstitem.getNumber();
				modnum.itemkey = dstitem.getKey();
				xdb.Procedure.psendWhileCommit(roleid, modnum);
			}
		}

	}

	private void pileCanpile(final BasicItem bi) {
		pileCanpile(bi, 0, getCapacity() - 1);
	}

	private void pileCanpile(final BasicItem bi, final int minpos, final int maxpos) {
		int maxheap = bi.getAttr().stackNum;
		if (maxheap <= 1)
			return;
		for (Map.Entry<Integer, xbean.Item> xitem : bag.getItems().entrySet()) {
			if (xitem.getValue().getPosition() < minpos
					|| xitem.getValue().getPosition() > maxpos)
				continue;
			if (canPile(bi.getDataItem(), xitem.getValue(), 0)) {
				int heapnum = Math.min(bi.getNumber(), maxheap - getNumber(xitem.getValue()));
				if (heapnum <= 0)
					continue;
				doPile(bi.getDataItem(), xitem.getValue(), heapnum);
				if(StateManager.isEntry(roleid))
				{
					final SModItemNum send = new SModItemNum();
					send.bagid = Conv.toByte(getBagid());
					send.curnum = getNumber(xitem.getValue());
					send.itemkey = xitem.getKey();
					xdb.Procedure.psendWhileCommit(roleid, send);
				}
				if (bi.getNumber() <= 0)
					break;
			}
		}
		if (bi.getNumber() <= 0 && bi.getBagid() == getBagid() && bi.getOwnerid() == roleid) {
			doDelete(bi.getKey(), true);
			SRemoveItem send = new SRemoveItem();
			send.bagid = Conv.toByte(bi.getBagid());
			send.itemkeys.add(bi.getKey());
			xdb.Procedure.psendWhileCommit(roleid, send);
		}
	}

	public boolean addItems(final List<BasicItem> items, final String reason, final int countertype) {
		SAddItem addprotocol = new SAddItem();
		addprotocol.bagid = Conv.toByte(getBagid());
		for (BasicItem item : items) {
			int addnum = item.getNumber();
			if (!this.addItem2AutoPos(item, false, countertype)) {
				return false;
			}
			if (item.getNumber() > 0) {
				addprotocol.itemdata.add(item.getProtocolItem());
			}
		}
		if(StateManager.getStateIdByRoleId(roleid) == State.ENTRY_STATE)
			xdb.Procedure.psendWhileCommit(roleid, addprotocol);
		return true;
	}
	
	/**
	 * 
	 * @param item
	 * @param p -1则自动寻找位置
	 * @param reason
	 * @return
	 */
	public AddItemResult addItem(final BasicItem item, final int p, final String reason, final int countertype, final int xiangguanid) {
		int hasnum = getItemOwnsNum(roleid, item.getItemid());
		int maxown = Module.getInstance().getMaxOwn(roleid, item.getItemid());
		if (maxown != 0 && hasnum + item.getNumber() > maxown) {
			List<String> parameters = new ArrayList<String>();
			parameters.add(item.getAttr().name);
			chuhan.gsp.msg.Message.psendMsgNotify(roleid, 142752, parameters);
			return new AddItemResult(AddResultEnum.MAX_OWN_NUM);
		}
		if (item.getOwnerid() != 0 && item.getOwnerid() != roleid) {
			if (item.isBind())
				return new AddItemResult(AddItemResult.AddResultEnum.BIND_ITEM);
		}
		final int addnum = item.getNumber();
		if (!processAddItem(item, p, countertype))
			return new AddItemResult(AddItemResult.AddResultEnum.FULL);
		
		StringBuilder sb = new StringBuilder();
		sb.append("添加物品,理由:").append(reason);
		Module.logger.debug(sb.toString());
		

		return new AddItemResult(AddItemResult.AddResultEnum.SUCC);
	}
	
	public int removeItemById(int itemid, int num, int countertype, String reason) {
		return removeItemById(itemid, num, countertype, reason,
				false, 0);
	}
	
	public int removeItemById(int itemid, int num, int countertype,String reason, boolean isLock){
		return removeItemById(itemid, num, countertype, reason, isLock, 0);
	}

	public int removeItemById(int itemid, int num, int countertype, String reason, boolean isLock, int isBind) {
		if (readonly)
			return 0;
		if (num <= 0)
			return 0;

		int leftnum = num;
		Map<Integer, Integer> removekeys = new HashMap<Integer, Integer>();
		boolean b = false;
		if(isBind == 1){
			b = true;
		}
		for (BasicItem item : this) {
			if (leftnum <= 0)
				break;
			if (item == null) {
				continue;
			}
			if (item.getItemid() != itemid) {
				continue;
			}

			if (isLock && isLocking(item))
				continue;
			
			if(isBind != 0){
				if(b && isBind(item.getDataItem()))
					continue;
			}

			int movenum = Math.min(leftnum, item.getNumber());
			leftnum -= movenum;
			removekeys.put(item.getKey(), movenum);
		}
		for (Map.Entry<Integer, Integer> removekey : removekeys.entrySet()) {
			removeItemByKey(removekey.getKey(), removekey.getValue(),
					countertype, reason,true);
		}
		Module.logger.debug("删除物品id=" + itemid + ", 数量为" + (num - leftnum)
				+ ": " + reason);
		return num - leftnum;
	}
	
	
	//删除绑定的指定物品ID
	public int removeBindingItemById(int itemid, int num, int countertype,String reason) {
		if (readonly)
			return 0;
		if (num <= 0)
			return 0;

		int leftnum = num;
		Map<Integer, Integer> removekeys = new HashMap<Integer, Integer>();
		for (BasicItem item : this) {
			if (leftnum <= 0)
				break;
			if (item == null) {
				continue;
			}
			if (item.getItemid() != itemid) {
				continue;
			}

			if (isLocking(item))
				continue;
			
			if(!isBind(item.getDataItem())){
				continue;
			}

			int movenum = Math.min(leftnum, item.getNumber());
			leftnum -= movenum;
			removekeys.put(item.getKey(), movenum);
		}
		for (Map.Entry<Integer, Integer> removekey : removekeys.entrySet()) {
			removeItemByKey(removekey.getKey(), removekey.getValue(),
					countertype, reason,true);
		}
		Module.logger.debug("删除物品id=" + itemid + ", 数量为" + (num - leftnum)
				+ ": " + reason);
		return num - leftnum;
	}

	private boolean isLocking(BasicItem item) {
		if (item.isLocked() == -1) {
			return false;
		}

		if (item.isLocked() == 0) {
			return true;
		}

		if (chuhan.gsp.main.GameTime.currentTimeMillis() < item.isLocked())
			return true;

		return false;
	}

	/**
	 * 
	 * @param pos
	 * @param num
	 * @return 删除了多少个
	 */
	public int removeItemByPos(int pos, int num, int countertype, String reason) {
		if (readonly)
			return 0;
		Module.logger.debug("删除物品pos=" + pos + ", 数量为" + num + ": " + reason);
		for (BasicItem item : this) {
			if (item.dataItem.getPosition() != pos) {
				continue;
			}
			return removeItemByKey(item.getKey(), num, countertype, reason,true);
		}
		return 0;
	}

	public int removeItemByKey(int key, int num, int countertype, String reason) {
		return removeItemByKey(key, num, countertype, reason, true);
	}
	
	/**
	 * 
	 * @param pos
	 * @param num
	 * @return 删除了多少个
	 */
	public int removeItemByKey(int key, int num, int countertype,
			 String reason, boolean isnotify) {
		BasicItem bi = this.moveOut(key, num, reason, isnotify);
		if (bi == null)
			return 0;
		List<Number> itemStrs = new ArrayList<Number>();
        itemStrs.add(bi.getItemid());
        itemStrs.add(bi.getNumber());
        itemStrs.add(bi.dataItem.getUniqueid());
		bi.onDelete();
		//LogUtil.doCosumeItemLog(roleid, LogUtil.convertIntToString(itemStrs), getRemoveLogtype(countertype));
		Module.logger.debug("删除物品key=" + key + ", 数量为" + num + ": " + reason);
		return bi.getNumber();
	}
	
	protected boolean isBind(xbean.Item xi) {
		return true;//(xi.getFlags() & chuhan.gsp.Item.BIND) == chuhan.gsp.Item.BIND;
	}

	/*private int getFirstFreePos(int page) {
		if (isFull())
			return -1;
		final int startpos = (page - 1) * Constant.BAG_PAGE_SIZE;
		final int endpos = page * Constant.BAG_PAGE_SIZE - 1;
		int fpos = Integer.MAX_VALUE;
		for (int pos : getFreepos()) {
			if (pos >= startpos && pos <= endpos && fpos > pos)
				fpos = pos;
		}
		if (fpos == Integer.MAX_VALUE)
			return -1;
		return fpos;
	}*/

	/*protected boolean moveItemToPage(BasicItem item, int page) {
		if (conf.可否堆叠 == 1) {
			final int startpos = (page - 1) * Constant.BAG_PAGE_SIZE;
			final int endpos = page * Constant.BAG_PAGE_SIZE - 1;
			this.pileCanpile(item, startpos, endpos);
		}
		if (item.getNumber() > 0) {
			final int xpos = getFirstFreePos(page);
			if (xpos == -1) {
				return false;
			}
			if (!addItem2Pos(item, xpos, 0))
				return false;
		}
		return true;
	}*/

	private class BagIterator implements Iterator<BasicItem> {
		final private Iterator<Integer> iter;

		public BagIterator() {
			iter = bag.getItems().keySet().iterator();
		}

		@Override
		public boolean hasNext() {
			return iter.hasNext();
		}

		@Override
		public BasicItem next() {
			final int key = iter.next();
			xbean.Item item = bag.getItems().get(key);
			if (item == null)
				return null;
			return Module.getInstance().toBasicItem(item, roleid, getBagid(), key);
		}

		@Override
		public void remove() {
			// 这里会抛出NoSuchElementException
			throw new RuntimeException("暂时不支持删除");
		}
	}
	@Override
	public Iterator<BasicItem> iterator() {
		return new BagIterator();
	}

	public boolean addFlag(int key, int flag, int bagid) {
		if (readonly)
			return false;
		xbean.Item xi = bag.getItems().get(key);
		if (xi == null)
			return true;
		else {
			xi.setFlags(xi.getFlags() | flag);
			SRefreshItemFlag ref = new SRefreshItemFlag(key, xi.getFlags(),Conv.toByte(bagid));
			//XXX xdb.Procedure.psendWhileCommit(roleid, ref);
		}
		return false;
	}

	public boolean removeFlag(int key, int flag, int bagid) {
		if (readonly)
			return false;
		xbean.Item xi = bag.getItems().get(key);
		if (xi == null)
			return true;
		else {
			xi.setFlags(xi.getFlags() & ~flag);
			SRefreshItemFlag ref = new SRefreshItemFlag(key, xi.getFlags(),Conv.toByte(bagid));
			//XXX xdb.Procedure.psendWhileCommit(roleid, ref);
		}
		return true;
	}

	/**
	 * 服务器统一处理，并且不发刷新道具flag的消息给客户端
	 * 
	 * @param flag
	 * @return
	 */
	public boolean removeAllItemFlag(int flag) {
		if (readonly)
			return false;
		java.util.Iterator<Integer> iter = bag.getItems().keySet().iterator();
		while (iter.hasNext()) {
			xbean.Item xi = bag.getItems().get(iter.next());
			if (xi == null)
				return false;
			xi.setFlags(xi.getFlags() & ~flag);
		}
		return true;
	}

	/**
	 * 对物品位置，挨个重新赋值
	 * 
	 * @return 有多少个物品的位置发生改变
	 */
	protected int sort(boolean fightType) {
		if (readonly)
			return 0;
		/*int count = 0;
		final java.util.SortedSet<BasicItem> myitems = new java.util.TreeSet<BasicItem>(
				new ItemComparator(fightType));
		for (BasicItem item : this) {
			if (item.getBagid() != BagTypes.QUEST)
				myitems.add(item);
		}
		int pos = 0;
		for (final BasicItem i : myitems) {
			if (i.getPosition() != pos) {
				i.getDataItem().setPosition(pos);
				count++;
			}
			pos++;
		}
		return count;*/
		return 0;
	}

	public int getCapacity() {
		return this.bag.getCapacity();
	}
	
	public int addCapacity(int size) {
		if (!readonly)
			bag.setCapacity(bag.getCapacity() + size);
		return bag.getCapacity();
	}

	public boolean isFull() {
		return size() >= getCapacity();
	}

	public int size() {
		return this.bag.getItems().size();
	}

	public boolean isEmpty() {
		return bag.getItems().size() == 0;
	}

	public int getRemainSize() {
		return getCapacity() - size();
	}

	public static boolean canPile(xbean.Item srcitem, xbean.Item dstitem, int ignore) {
		if (dstitem == null || srcitem == null)
			return false;
		if (srcitem.getId() != dstitem.getId())
			return false;
		if (ignore == 0)
			return srcitem.getFlags() == dstitem.getFlags();
		return (srcitem.getFlags() | ignore) == (dstitem.getFlags() | ignore); 
	}

	protected int incNextId() {
		final int id = bag.getNextid() + 1;
		// TODO:给key设置一个上限，超过上限则遍历现有的物品，找到一个可用的key
		if (id < 0)
			throw new RuntimeException("生成key出错");
		bag.setNextid(id);
		return id;
	}

	private BasicItem toBasicItem(xbean.Item item, int key) {
		return Module.getInstance().toBasicItem(item, roleid, getBagid(), key);
	}

	public BasicItem getItem(int key) {
		xbean.Item item = bag.getItems().get(key);
		if (item == null)
			return null;

		return toBasicItem(item, key);
	}
	
	/**
	 * 通过物品ID取到key（新手引导特殊处理用）
	 * @param itemId
	 * @return
	 */
	public int getKeyByItemId(int itemId){
		for( Map.Entry<Integer, xbean.Item> entry : bag.getItems().entrySet() ){
			if(entry.getValue().getId() == itemId){
				return entry.getKey();
			}
		}
		return -1;
	}
	
	
	/**
	 * 根据物品的唯一Id取得 BasicItem
	 * @param uniqueid
	 * @return
	 */
	public BasicItem getItemByUnKey(long uniqueid){
		for(int key : bag.getItems().keySet()){
			xbean.Item item = bag.getItems().get(key);
			if(item != null && item.getUniqueid() == uniqueid){
				return toBasicItem(item, key);
			}
		}
		return null;
	}

	public BasicItem getItemByPos(int pos) {
		Map.Entry<Integer, xbean.Item> item = getXitemByPos(pos);
		if (item == null)
			return null;
		return toBasicItem(item.getValue(), item.getKey());
	}

	private Map.Entry<Integer, xbean.Item> getXitemByPos(int pos) {
		if (pos < 0)
			return null;
		for (Map.Entry<Integer, xbean.Item> item : bag.getItems().entrySet()) {
			if (item.getValue().getPosition() == pos)
				return item;
		}
		return null;
	}

	public java.util.ArrayList<Integer> getFreepos() {
		java.util.ArrayList<Integer> frees = new java.util.ArrayList<Integer>();
		for (int i = 0; i < this.getCapacity(); i++) {
			frees.add(i);
		}
		for (xbean.Item xi : bag.getItems().values()) {
			frees.remove((Integer) xi.getPosition());
		}
		Collections.sort(frees);
		return frees;
	}

	public boolean isPosFree(int pos) {
		if (pos < 0 || pos > getCapacity())
			return false;
		for (xbean.Item item : bag.getItems().values()) {
			if (item.getPosition() == pos)
				return false;
		}
		return true;
	}

	/**
	 * 根据 itemid 统计这个物品在包裹里的数量
	 * 
	 * @param itemid
	 * @param flag
	 *            忽略什么样的物品
	 * @return
	 */
	public int countItemNumber(int itemid, int flag) {
		int count = 0;
		final Map<Integer, xbean.Item> items = bag.getItems();
		if (null == items)
			return -1;
		for (final xbean.Item e : items.values()) {
			if (itemid == e.getId() && (e.getFlags() & flag) == 0) {
				count += getNumber(e);
			}
		}
		return count;
	}

	/**
	 * 获取包裹中物品的个数：除 摆摊,交易上架,财产保护 锁定的除外。
	 * 
	 * @param itemid
	 * @return
	 */
	public int countItemNumberExceptTradeStall(int itemid) {
		int count = 0;
		if (getBagid() != BagTypes.BAG)
			count = countItemNumber(itemid, 0);
		else {
			final Map<Integer, xbean.Item> items = bag.getItems();
			if (null == items)
				count = -1;
			else {
				Iterator<Integer> iter = items.keySet().iterator();
				while (iter.hasNext()) {
					int itemkey = iter.next();
					BasicItem basicitem = getItem(itemkey);
					xbean.Item e = items.get(itemkey);
					if (itemid == basicitem.getItemid()
//							&& (basicitem.getFlags() & chuhan.gsp.Item.ONSTALL) == 0
							&& basicitem.isLocked() == -1)// edit by lc
						count += getNumber(e);
				}
			}
		}
		return count;
	}
	/**目前只给装备冲星用,bind参数控制返回的是绑定物品的key,还是非绑定物品的key
	 * 获取包裹中物品的个数：除 摆摊,交易上架,财产保护 锁定的除外。
	 * 
	 * @param itemid
	 * @return
	 */
	/*
	public List<Integer> countItemNumberExceptTradeStall(int itemid,int bind,List<Integer> keys) {
		if (getBagid() != BagTypes.BAG){
			final Map<Integer, xbean.Item> items = bag.getItems();
			if (null == items)
				return keys;
			for (final Entry<Integer, xbean.Item> e : items.entrySet()) {
				if (itemid == e.getValue().getId()) {
					if ((e.getValue().getFlags() & Item.BIND) == bind) 
						keys.add(e.getKey());
				}
			}
		}
		else {
			final Map<Integer, xbean.Item> items = bag.getItems();
			if (null == items)
				return keys;
			else {
				Iterator<Entry<Integer, xbean.Item>> iter = items.entrySet().iterator();
				while (iter.hasNext()) {
					Entry<Integer, xbean.Item> entry = iter.next();
					int itemkey = entry.getKey();
					BasicItem basicitem = getItem(itemkey);
					if (itemid == basicitem.getItemid()
							&& (basicitem.getFlags() & chuhan.gsp.Item.ONSTALL) == 0
							&& basicitem.isLocked() == -1)// edit by lc
						if ((entry.getValue().getFlags() & Item.BIND) == bind) 
							keys.add(entry.getKey());
				}
			}
		}
		return keys;
	}
	*/

	/**
	 * 添加或减少金钱。扣钱的时候，如果钱不够，则会异常。
	 * 
	 * @param money
	 *            正数代表增加，负数代表减少
	 * @param reason
	 *            记录日志用
	 * @return 0代表未变化.否则为金钱的变动数值
	 */
	public long addMoney(final long money, final String reason) {
		if (readonly)
			return 0;
		// 防止溢出
		long res = (Long.MAX_VALUE - getMoney() <= money) ? Long.MAX_VALUE
				: money + getMoney();// 防止溢出;
		if (res < 0) {
			Module.logger.error("金钱不足");
			return 0;
		} else if (res > conf.maxmoney) {
			return 0;
		}
		long oldvalue = bag.getMoney();
		bag.setMoney(res);
		//this.notifyMoney(money);
		long realadd = res - oldvalue;

		Module.logger.debug("添加金钱" + money + ": " + reason);
		return realadd;
	}

	public long getMoney() {
		return bag.getMoney();
	}

	public long getMaxMoney() {
		return conf.maxmoney;
	}
	


	public void clear() {
		List<Integer> keys = new ArrayList<Integer>();
		for (BasicItem bi : this) {
			keys.add(bi.getKey());
			bi.onDelete();
		}
		for (int key : keys) {
			doDelete(key, true);
		}
	}

	private Map<Integer, Integer> collectItemInfo() {
		Map<Integer, Integer> iteminfo = new HashMap<Integer, Integer>();
		for (xbean.Item item : bag.getItems().values()) {
			Integer num = iteminfo.get(item.getId());
			if (num == null) {
				iteminfo.put(item.getId(), getNumber(item));
			} else {
				iteminfo.put(item.getId(), getNumber(item) + num);
			}
		}
		return iteminfo;
	}

	protected boolean arrange(boolean fightType) {
		if (readonly)
			return false;
		Map<Integer, Integer> backup = collectItemInfo();
		final java.util.Set<Integer> toRemove = new java.util.TreeSet<Integer>();
		final java.util.Set<Integer> toIngore = new java.util.TreeSet<Integer>();
		for (final Map.Entry<Integer, xbean.Item> item : bag.getItems()
				.entrySet()) {
			if (toIngore.contains(item.getKey())
					|| toRemove.contains(item.getKey()))
				continue;

			final chuhan.gsp.item.item26 attr = Module.getInstance().getAttr(item.getValue().getId());
			// 此处把不可堆叠的物品已经全部忽略。
			if (getNumber(item.getValue()) >= attr.stackNum) {
				toIngore.add(item.getKey());
				continue;
			}

			for (final Map.Entry<Integer, xbean.Item> item2 : bag.getItems()
					.entrySet()) {
				if (toIngore.contains(item2.getKey())
						|| toRemove.contains(item2.getKey()))
					continue;
				if (item2.getKey() == item.getKey()
						|| !canPile(item2.getValue(), item.getValue(), 0))
					continue;
				final int pilednumber = Math.min(attr.stackNum
						- getNumber(item.getValue()), getNumber(item2.getValue()));
				if (pilednumber > 0) {
					doPile(item2.getValue(), item.getValue(), pilednumber);
				}
				if (getNumber(item2.getValue()) == 0)
					toRemove.add(item2.getKey());
				if (getNumber(item.getValue()) == attr.stackNum) {
					toIngore.add(item.getKey());
					break;
				}
			}
		}
		for (final Integer key : toRemove) {
			xbean.Item xitem = bag.getItems().get(key);
			if (xitem != null) {
				doDelete(key, true);
			}
			
		}
		sort(fightType);
		Map<Integer, Integer> after = collectItemInfo();
		if (after.size() != backup.size()) {
			Module.logger.error("整理包裹发生错误");
			return false;
		}
		for (Map.Entry<Integer, Integer> item : after.entrySet()) {
			Integer num = backup.get(item.getKey());
			if (num == null || num.intValue() != item.getValue().intValue()) {
				Module.logger.error("整理包裹发生错误");
				return false;
			}
		}
		return true;
	}
	
	

/*	public static chuhan.gsp.Item xitem2ProtocolItem(xbean.Item dstItem,
			int key, int isnew) {
		final chuhan.gsp.Item ki = new chuhan.gsp.Item();
		//ki.flags = dstItem.getFlags();
		assert(dstItem.getId() > 0 && dstItem.getId() <= Short.MIN_VALUE);
		ki.id = (short)dstItem.getId();
		ki.number = Conv.toShort(getNumber(dstItem));
		//ki.position = dstItem.getPosition();
		ki.key = key;
		return ki;
	}*/

	// protected void notifyModItemNum( int key, int num) {
	// if (key <= 0 || num <= 0)
	// return;
	// final SModItemNum send = new SModItemNum();
	// send.bagid = getBagid();
	// send.curnum = num;
	// send.itemkey = key;
	// xdb.Procedure.psendWhileCommit(roleid, send);
	// }
	//
	// protected void notifyRemoveItem( int key) {
	// if (key <= 0)
	// return;
	// final SRemoveItem send = new SRemoveItem();
	// send.itemkey = key;
	// send.bagid = getBagid();
	// xdb.Procedure.psendWhileCommit(roleid, send);
	// }
	//
	// protected void notifyAddItem( java.util.ArrayList<chuhan.gsp.Item> data )
	// {
	// if (data.isEmpty())
	// return;
	// final SAddItem send = new SAddItem();
	// send.data = data;
	// send.bagid = getBagid();
	// xdb.Procedure.psendWhileCommit(roleid, send);
	// }
	// protected void notifyAddItem( xbean.Item xi, int key, final int isnew ) {
	// if (xi==null)
	// return;
	// chuhan.gsp.Item item = xitem2ProtocolItem(xi, key, isnew );
	// final SAddItem send = new SAddItem();
	// send.data.add(item);
	// send.bagid = getBagid();
	// xdb.Procedure.psendWhileCommit(roleid, send);
	// }
	// protected void notifyModItemPos( int key, int pos ) {
	// final SModItemPos send = new SModItemPos();
	// send.itemkey = key;
	// send.pos = pos;
	// send.bagid = getBagid();
	// xdb.Procedure.psendWhileCommit(roleid, send);
	// }
	protected void notifyMoney(long money) {
		final SRefreshMoney refmoney = new SRefreshMoney();
		refmoney.bagid = Conv.toByte(getBagid());
		refmoney.money = bag.getMoney();
		xdb.Procedure.psendWhileCommit(roleid, refmoney);
	}

	protected void psendMsgNotify(int msgId, List<String> parameters) {
		chuhan.gsp.msg.Message.psendMsgNotify(roleid, msgId, parameters);
	}

	/**
	 * 获取发给客户端的包裹信息
	 * 
	 * @return
	 */

	protected chuhan.gsp.Bag getBagInfo() {
		final chuhan.gsp.Bag ret = new chuhan.gsp.Bag();
		for (BasicItem bi : this) {
			if (bi.getBagid() != getBagid())
				continue;
			ret.items.add(bi.getProtocolItem());
		}
		//ret.capacity = getCapacity();
		//ret.money = bag.getMoney();
		return ret;
	}
	

	public String getItemLogsByKeys(List<Integer> itemkeys) {
		StringBuilder sb = new StringBuilder();
		for (int itemkey : itemkeys) {
			BasicItem item = getItem(itemkey);
			if (item == null)
				continue;
			sb.append(item.getItemLog());
		}
		return sb.toString();
	}

	public static String getItemLogs(List<BasicItem> items) {
		StringBuilder sb = new StringBuilder();
		for (BasicItem item : items) {
			if (item == null)
				continue;
			sb.append(item.getItemLog());
		}
		return sb.toString();
	}
	
	public int sellItem(int itemkey, int num, boolean isnotify)
	{
		BasicItem item = getItem(itemkey);
		if(item == null)
			return 0;
		if(item.getAttr().getQuality() <= 0)
			return 0;
		if(item.getNumber() < num)
			return 0;
		if(!item.canSell())
			return 0;
		removeItemByKey(itemkey, num, 1, "sell_item", isnotify);		
		
		Bag bag = new Bag(roleid, false);
		bag.addMoney(item.getSellPrice(), "sell_item");// 售卖价格
		
		// 加金钱
		PropRole prole = PropRole.getPropRole(roleid, false);
		prole.addGold(item.getSellPrice()*num, GoldAddType.SELL_EQUIP);
		
		return item.getSellPrice()*num;
	}
	
	public List<AddItem> getAddItems(Map<Integer,Integer> result)
	{
		List<AddItem> adds = new LinkedList<AddItem>();
		for(Map.Entry<Integer, Integer> entry : result.entrySet())
		{
			BasicItem item = getItem(entry.getKey());
			if(item == null)
				continue;
			adds.add(new AddItem(entry.getKey(), item.getItemid(), entry.getValue()));
		}
		return adds;
	}
	
	public static void sendAddNewItem(long roleId, List<AddItem> additems)
	{
		SShowAddItem show = new SShowAddItem();
		for(AddItem additem : additems)
			show.data.add(new ShowItemData(additem.getKey(),Conv.toShort(additem.getId()),Conv.toShort(additem.getNum())));
		xdb.Procedure.psendWhileCommit(roleId, show);
	}
	
	public void sendRemovedItems()
	{
		if(getBag().getRemovedkeys() == null || getBag().getRemovedkeys().size() == 0){
			return;
		}
		SRemoveItem snd = new SRemoveItem();
		snd.bagid = Conv.toByte(getBagid());
		snd.itemkeys.addAll(getBag().getRemovedkeys());
		xdb.Procedure.psendWhileCommit(roleid, snd);
		clearRemovedItems();
	}
	
	public void clearRemovedItems()
	{
		getBag().getRemovedkeys().clear();
	}
	
}
