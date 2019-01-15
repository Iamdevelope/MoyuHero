package chuhan.gsp.item;

import java.lang.reflect.Constructor;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

import org.apache.log4j.Logger;

import xbean.ItemNumLimit;
import xdb.Xdb;
import xdb.util.AutoKey;
import chuhan.gsp.attr.config10;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.ModuleManager;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

public class Module implements chuhan.gsp.main.ModuleInterface {
	static public final Logger logger = Logger.getLogger(Module.class);
	
	public Logger getLogger() {
		return logger;
	}

	public static Module getInstance() {
		return (Module)ModuleManager.getInstance().getModuleByName("item");
	}

	@Override
	public void exit() {
	}
		
	@Override
	public void init() throws Exception {
	}
	
	/*private static class RemoveFixedDateTimeoutItem implements WorldEventTab.CronTask {
		private final int itemid;
		RemoveFixedDateTimeoutItem(final int itemid) {this.itemid = itemid;}
		@Override
		public void run() {
			xbean.FixedDateTimeoutItemInfo iteminfo = xtable.Fixeddatetimeoutitem.get(itemid);
			if (iteminfo == null)
				return;
			for (long itemuid : iteminfo.getItemuid()) {
				BasicItem item = Module.getInstance().getItemByUid(itemuid, false);
				if (item != null)
					item.onTimeout();
			}
			xtable.Fixeddatetimeoutitem.remove(itemid);
		}
	}*/
		
	/**
	 * 获得物品栏容量
	 * @param roleid
	 * @param bagid
	 * @return
	 */
	public int getItemColumnCapacity( final long roleid, final int bagid ) {
		ItemColumn ic = getItemColumn( roleid, bagid, true);
		if ( ic == null )
			return -1;
		return ic.getCapacity();
	}
	
	public ArrayList<Integer> getItemColumnFreePos( final long roleid, final int bagid ) {
		ItemColumn ic = getItemColumn( roleid, bagid, true);
		if ( ic == null )
			return null;
		return ic.getFreepos();
	}
	
	public Set<Integer> getItemColumnKeySet( final long roleid, final int bagid ) {
		ItemColumn ic = getItemColumn( roleid, bagid, true);
		if ( ic == null )
			return null;
		return ic.bag.getItems().keySet();
	}
	
	public static ItemColumn getItemColumnByItemId( long roleid, int itemId, boolean readonly ) {
		item26 attr = chuhan.gsp.main.ConfigManager.getInstance().getConf(item26.class).get( itemId );
		if ( attr == null )
			return null;
			
		return getItemColumn(roleid, attr.getBag(), readonly);
	} 
	
	public static ItemColumn getItemColumn( long roleid, int bagid, boolean readonly ) {
		switch ( bagid ) {
		case BagTypes.BAG:
			return new Bag( roleid, readonly );
		case BagTypes.EQUIP:
			return new EquipColumn(roleid, readonly);
		case BagTypes.SKILL:
			return new SkillColumn(roleid, readonly);
		case BagTypes.SOUL:
			return new SoulColumn(roleid, readonly);
		case BagTypes.COLLECTION:
			return new ItemColumn(roleid, bagid, readonly);
		}
		return null;
	}
	
	public static Map<Integer,chuhan.gsp.Bag> getOnlineSendBags(long roleId)
	{
		Map<Integer,chuhan.gsp.Bag> bags = new HashMap<Integer, chuhan.gsp.Bag>();
		for(SBagConfig bagcfg : ConfigManager.getInstance().getConf(SBagConfig.class).values())
		{
			if(bagcfg.sendlogin == 0)
				continue;
			ItemColumn itemcol = getItemColumn(roleId, bagcfg.id, false);
			bags.put(bagcfg.id, itemcol.getBagInfo());
		}
		return bags;
	}
	
	/**
	 * 判断背包物品数是否大于最大值
	 * @param roleId
	 * @param bagIdList 共同计算的背包列表
	 * @param maxNum	最大值
	 * @return
	 */
	public static boolean isMorethanMax(long roleId,List<Integer> bagIdList,int maxNum,int addNum)
	{
		int nowNum = 0;
		for(Integer bagId : bagIdList){
			ItemColumn itemcol = getItemColumn(roleId, bagId, false);
			if(itemcol != null){
				nowNum += itemcol.size();
			}
		}
		return nowNum + addNum > maxNum;
	}

	/**
	 * 获得物品的最大数量
	 * @param roleid
	 * @param itemid
	 * @return
	 */
	public int getMaxOwn( final long roleid, final int itemid ) {
		item26 attr = chuhan.gsp.main.ConfigManager.getInstance().getConf(item26.class).get( itemid );
		if ( attr == null )
			return 0;
		return Integer.MAX_VALUE;
		//return attr.最大拥有数量;
	}
	
	/**
	 * 解析物品的buff列表
	 * @param sItemBuff
	 * @param improve 默认为1即可
	 * @return 返回List<BuffArg>
	 * @throws Exception
	 */
	/*public static BuffArg[] parseItemBuff(SItemBuff sItemBuff) throws Exception
	{
		if(sItemBuff == null)
			return null;
		//List<BuffArg> buffArgs = new ArrayList<BuffArg>();
		BuffArg[] buffArgs = new BuffArg[3];
		if (sItemBuff.getBuff0_id() != -1)
		{
			buffArgs[0] = new BuffArg(sItemBuff.getBuff0_id());
			buffArgs[0].rateJs = new JavaScript(sItemBuff.getBuff0_rate());
			buffArgs[0].targetType = SkillConstant.BUFF_TARGET_TYPE_SINGLE_ORINGIN;
			buffArgs[0].roundJs = new JavaScript(sItemBuff.getBuff0_round());
			buffArgs[0].timeJs = new JavaScript(sItemBuff.getBuff0_time());
			if(!sItemBuff.getBuff0_values().equals("null"))
			{
				buffArgs[0].effectJsMap = knight.gsp.util.Parser.parseFightJsEffects(sItemBuff.getBuff0_values());
			}
		}
		if (sItemBuff.getBuff1_id() != -1)
		{
			buffArgs[1] = new BuffArg(sItemBuff.getBuff1_id());
			buffArgs[1].rateJs = new JavaScript(sItemBuff.getBuff1_rate());
			buffArgs[1].targetType = SkillConstant.BUFF_TARGET_TYPE_SAME_MAIN_BUFF;
			buffArgs[1].roundJs = new JavaScript(sItemBuff.getBuff1_round());
			buffArgs[1].timeJs = new JavaScript(sItemBuff.getBuff0_time());
			if(!sItemBuff.getBuff1_values().equals("null"))
			{
				buffArgs[1].effectJsMap = knight.gsp.util.Parser.parseFightJsEffects(sItemBuff.getBuff1_values());
			}
		}
		if (sItemBuff.getBuff2_id() != -1)
		{
			buffArgs[2] = new BuffArg(sItemBuff.getBuff2_id());
			buffArgs[2].rateJs = new JavaScript(sItemBuff.getBuff2_rate());
			buffArgs[2].targetType = SkillConstant.BUFF_TARGET_TYPE_SAME_MAIN_BUFF;
			buffArgs[2].roundJs = new JavaScript(sItemBuff.getBuff2_round());
			buffArgs[2].timeJs = new JavaScript(sItemBuff.getBuff0_time());
			if(!sItemBuff.getBuff2_values().equals("null"))
			{
				buffArgs[2].effectJsMap = knight.gsp.util.Parser.parseFightJsEffects(sItemBuff.getBuff2_values());
			}
		}
		return buffArgs;
	}*/
	
	public int getItemKeyByPos( final long roleid, final int bagid, final int pos ) {
		ItemColumn ic = getItemColumn( roleid, bagid, true );
		for ( Map.Entry<Integer, xbean.Item> itembean : ic.bag.getItems().entrySet() ) {
			if ( pos == itembean.getValue().getPosition() ) {
				return itembean.getKey();
			}
		}
		return -1;
	}
	
	public static int getItemIdByName(String name)
	{
		for(item26 sattr : ConfigManager.getInstance().getConf(item26.class).values())
		{
			if(sattr.name.equalsIgnoreCase(name))
				return sattr.id;
		}
		return 0;
	}
	
	/**
	 * marshal数据
	 * @param os
	 * @param tipsdata
	 * @return marshal成功与否
	 */
	public boolean marshalData( OctetsStream os, Object tipsdata ) {
		if ( tipsdata instanceof Octets ) {
			os.marshal( (Octets)tipsdata );
		} else if ( tipsdata instanceof xdb.Bean ) { 
			os.marshal( (xdb.Bean)tipsdata );
		} else if ( tipsdata instanceof String ) {
			os.marshal( (String)tipsdata );
		} else if ( tipsdata instanceof Integer ) {
			os.marshal( (Integer)tipsdata );
		} else if ( tipsdata instanceof Long ) {
			os.marshal( (Long)tipsdata );
		} else if ( tipsdata instanceof Double ) {
			os.marshal( (Double)tipsdata );
		} else if ( tipsdata instanceof Float ) {
			os.marshal( (Float)tipsdata );
		} else if ( tipsdata instanceof Boolean ) {
			os.marshal( (Boolean)tipsdata );
		} else {
			xdb.Trace.debug( "Can't marshal" );
			return false;
		}
		return true;
	}
	
	
	@Override
	public chuhan.gsp.main.ReloadResult reload() throws Exception
	{
		Module module = new Module();
		module.init();
		ModuleManager.getInstance().putModuleByName("item", module);
		return new chuhan.gsp.main.ReloadResult(true);
	}
	
	public long getUniqueID() {
		AutoKey<Long> autokey = Xdb.getInstance().getTables().getTableSys().getAutoKeys().getAutoKeyLong("ITEM_UID");
		if (autokey == null)
			return 0;
		return autokey.next();
	}

	public BasicItem toBasicItem( xbean.Item i, long roleid, int bagid, int key )  {
		int itemid = i.getId();
		try {
		String itemclassname = getItemClass( itemid );
			java.lang.reflect.Constructor<?>  constructor = 
				Class.forName(itemclassname ).getDeclaredConstructor( 
						xbean.Item.class );
			constructor.setAccessible( true );
			BasicItem item = (BasicItem)constructor.newInstance(i);
			item.bagid = bagid;
			item.roleid = roleid;
			item.keyinbag = key;
			return item;
		} catch ( Exception e ) {
			xdb.Trace.debug( "生成物品" + itemid+"出错:" + e.toString() );
			return null;
		}
	}
	public BasicItem genBasicItem(int itemid, int num,  xdb.Bean extinfo) throws RuntimeException, IllegalArgumentException {
		return genBasicItem(itemid, num, 0, extinfo, true);
	}
	
	public BasicItem genBasicItem(int itemid, int num, int numtype) throws RuntimeException, IllegalArgumentException {
		return genBasicItem(itemid, num, numtype, null, true);
	}
	
	public BasicItem genBasicItem(final int itemid, final int num, int numtype, xdb.Bean extinfo, boolean calcScore) 
		throws RuntimeException, IllegalArgumentException {	
		final String itemclassname = getItemClass(itemid);
		Module.logger.debug("准备生成" + itemclassname + "物品对象");
		BasicItem item;
		try {
			Constructor<?> constructor;
			if (extinfo != null) {
				constructor = Class.forName(itemclassname).getDeclaredConstructor(int.class, 
						xdb.Bean.class);
				constructor.setAccessible(true);
				item = (BasicItem)constructor.newInstance(itemid, extinfo);
			} else {
				constructor = Class.forName(itemclassname).getDeclaredConstructor(int.class);
				constructor.setAccessible(true);
				item = (BasicItem)constructor.newInstance(itemid);
			}
		} catch ( Exception e ) {
			e.printStackTrace();
			xdb.Trace.debug( "生成物品出错:" + e.toString() );
			return null;
		}
		
		item.setFlag(item.getInitFlag());
		if (numtype == 0) {
			item.getDataItem().setNumber(num);
		} else if (numtype == 1) {
			item.getDataItem().getNumbermap().put(1, num);
		} else if (numtype == 2) {
			item.getDataItem().getNumbermap().put(2, num);
		} else {
			throw new IllegalArgumentException("物品log日志类型异常");
		}
		
		return item;
	}
	
	public String getItemClass( final int itemid ) {
		item26 attr = ConfigManager.getInstance().getConf(item26.class).get(itemid);
		return attr.classname;
	}	
	public item26 getAttr(int itemId)
	{
		return ConfigManager.getInstance().getConf(item26.class).get(itemId);
	}
	
	public SBagConfig getBagConfig(int bagId)
	{
		return ConfigManager.getInstance().getConf(SBagConfig.class).get(bagId);
	}
	
	
	public static ItemNumLimit getItemNumLimitInfo(long roleId,boolean readonly) {
		long now = chuhan.gsp.main.GameTime.currentTimeMillis();
		xbean.ItemNumLimit itemnumlimitinfo;
		if(readonly) {
			itemnumlimitinfo = xtable.Itemlimits.select(roleId);
		} else {
			itemnumlimitinfo = xtable.Itemlimits.get(roleId);
		}
		if(null == itemnumlimitinfo) {
			if(readonly) {
				itemnumlimitinfo = xbean.Pod.newItemNumLimitData();
				itemnumlimitinfo.setTime(now);
			} else {
				itemnumlimitinfo = xbean.Pod.newItemNumLimit();
				itemnumlimitinfo.setTime(now);
				xtable.Itemlimits.insert(roleId, itemnumlimitinfo);
			}
		}
		if( !DateUtil.inTheSameDay(now, itemnumlimitinfo.getTime()) ){
			itemnumlimitinfo.getItemnums().clear();
			itemnumlimitinfo.setTime(now);
		}
		return itemnumlimitinfo;
	}
	
	public static void addUseNum( long roleId,final int itemid ){
		
		ItemNumLimit limit = getItemNumLimitInfo(roleId,false);
		Integer oldNum = limit.getItemnums().get(itemid);
		if(null == oldNum) {
			oldNum = 0;
		}
		limit.getItemnums().put(itemid, oldNum + 1);//增加使用次数
	}
	
	
	
}
