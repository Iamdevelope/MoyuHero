package chuhan.gsp.item;

import java.util.List;

import xbean.ItemNumLimit;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.config10;
import chuhan.gsp.game.vip39;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.ParserString;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.OctetsStream;

public abstract class BasicItem {
	protected xbean.Item dataItem;
	// protected static final int NORMAL = 0;
	// protected static final int PRODUCT = 1;
	final protected item26 attr;
	protected long roleid = 0;
	protected int bagid = 0;
	protected int keyinbag = 0;
	protected abstract void afterInsert();

	protected abstract void afterDelete();

	public BasicItem(int itemid) {
		attr = ConfigManager.getInstance().getConf(item26.class).get(itemid);
		this.dataItem = xbean.Pod.newItem();
		this.dataItem.setId(itemid);
		setFlag(getInitFlag());
	}

	public BasicItem(xbean.Item item) {
		attr = ConfigManager.getInstance().getConf(item26.class).get(item.getId());
		this.dataItem = item;
	}

	public String getName() {
		return attr.name;
	}
	
	int getInitFlag() {
		int flag = 0;
		/*if (attr.是否拾取绑定)
			flag |= chuhan.gsp.Item.BIND;
		if (attr.bCanSale == 0)
			flag |= chuhan.gsp.Item.CANNOTONSTALL;*/
		return flag;
	}
	
	/**
	 * 如果是bind的,则无法更换主人
	 * 
	 * @param roleid
	 * @return
	 */
	protected void setOwnerid(final long roleid) {
		this.roleid = roleid;
	}

	/**
	 * 判断物品能否交易
	 * 
	 * @return
	 */
	public boolean canTrade() {
		if (isLocked() != -1)
			return false;
		else
			return (dataItem.getFlags() & (chuhan.gsp.Item.BIND | chuhan.gsp.Item.ONSTALL)) == 0;
	}

	public static int ITEM_LOCK_ERROR = 141078;

	/**
	 * 检测某物品是否被锁 如果readonly == false ，则同时清除过期时间锁
	 * 
	 * @param itemkey
	 * @return -1 ： 未锁，0 ： 普通锁 , >0 ： 时间锁到期的时间
	 */
	public long isLocked() {
		return -1;// 没有锁
	}

	/**
	 * 判断物品能否摆摊上架
	 * 
	 * @return
	 */
	public boolean canOnStall() {
		if (isLocked() != -1)
			return false;
		else
			return ((dataItem.getFlags() & (chuhan.gsp.Item.ONSTALL
					| chuhan.gsp.Item.BIND | chuhan.gsp.Item.CANNOTONSTALL)) == 0);
	}

	public void setFlag(final int flag) {
		dataItem.setFlags(dataItem.getFlags() | flag);
	}

	public long getOwnerid() {
		return roleid;
	}

	public int getBagid() {
		return bagid;
	}

	public int getFlags() {
		return dataItem.getFlags();
	}

	public boolean isBind() {
		return ((dataItem.getFlags() & chuhan.gsp.Item.BIND) == chuhan.gsp.Item.BIND)
				&& roleid > 0;
	}

	protected void setBagid(int bagid) {
		this.bagid = bagid;
	}

	public int getKey() {
		return keyinbag;
	}

	protected void setKey(int keyinbag) {
		this.keyinbag = keyinbag;
	}

	public item26 getAttr() {
		return attr;
	}

	public xbean.Item getDataItem() {
		return dataItem;
	}

	public int getPosition() {
		return dataItem.getPosition();
	}

	public int getColor()
	{
		return 0;
	}
	
	public int getFinalColor()
	{
		return getColor();
	}
	
	public int getItemid() {
		return dataItem.getId();
	}

	public long getUniqId() {
		if (roleid == 0)
			throw new RuntimeException("状态错误。该物品不在包裹中，所以没有唯一id");
		return dataItem.getUniqueid();
	}

	public int getNumber() {
		return ItemColumn.getNumber(dataItem);
	}
	
	public int getUseNumber(){
		ItemNumLimit limit = Module.getItemNumLimitInfo(roleid,false);
		Integer oldNum = limit.getItemnums().get(dataItem.getId());
		if(oldNum == null ){
			return 0;
		}
		return oldNum;
	}

	/**
	 * 从非绑定转为绑定状态 谁来notify ? 如果这件事情发生在加入包裹之前，那么不需要notify。否则这里要发包
	 */
	public void bind() {
		final int f = this.dataItem.getFlags() | chuhan.gsp.Item.BIND;
		this.dataItem.setFlags(f);
	}

	/**
	 * 如果是将包裹里的物品绑定需要notify
	 */
	public void bindAndNotify() {
		bind();
		SRefreshItemFlag ref = new SRefreshItemFlag(getKey(),
				dataItem.getFlags(), Conv.toByte(bagid));
		//TODO xdb.Procedure.psendWhileCommit(roleid, ref);
	}

	/** 物品如果关联的有其它表。在这个函数删除 */
	public final void onDelete() {
		afterDelete();
	}
	/** 物品如果关联的有其它表。在这个函数插入 */
	public final void onInsert() {
		afterInsert();
	}

	public xdb.Bean getExtInfo() {
		return null;
	}

	protected OctetsStream os = null;

	public chuhan.gsp.Item getProtocolItem() {
		chuhan.gsp.Item pitem = new chuhan.gsp.Item();
		pitem.id = getItemid();
		pitem.key = getKey();
		pitem.number = Conv.toShort(getNumber());
		pitem.timer = Conv.toShort(getUseNumber());
		pitem.extdata = getExtdataOctets();
		return pitem;

	}
	
	public abstract Octets getExtdataOctets();

	public boolean canUse(long roleid, int idtype, long objid) {
		return true;
	}

	protected UseItemHandler getUseItemHandler() {
		return null;
	}
	public final UseResult onUse(int num) {
		return _onUse(num, getUseItemHandler());
	}

	public final UseResult onUse(int num, UseItemHandler handler) {
		if (handler == null)
			handler = getUseItemHandler();
		return _onUse(num, handler);
	}

	protected UseResult _onUse(int num, UseItemHandler handler) {
		if (handler == null)
			return UseResult.FAIL;
		return handler.onUse(getOwnerid(), this, num);
	}

	/**
	 * 生成物品的日志信息，为远程日志服务
	 * 
	 * 不同类型的物品需要覆盖
	 * 
	 * @return
	 */
	public String getItemLog() {
		return new StringBuilder().append(getItemid()).append(",")
				.append(getNumber()).append(",").append(getUniqId())
				.append(";").toString();
	}

	public boolean canLock() {
		return true;
	}// 判断该物品是否能够加锁
	
	public int getSellPrice()
	{
		return attr.sellPrice;
	}
	
	public UseResult use(long roleId, int num, int dstkey)
	{
		return UseResult.SUCC;
	}
	
	public boolean canSell()
	{
		if(attr.getIfSell() <= 0 ){
			return false;
		}
		return true;
	}
	
	public boolean dailyLimit(long roleId){
		
		Integer oldNum = Module.getItemNumLimitInfo(roleId,false).getItemnums().get(dataItem.getId());
		if(null == oldNum) {
			oldNum = 0;
		}
		
		//特殊物品处理（使用次数在vip表里）
		List<Integer> specialId = ParserString.parseString2Int(ConfigManager.getInstance().
				getConf(config10.class).get(1250).configvalue);
		if( specialId.contains(this.attr.getId()) ){
			PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
			vip39 vipInit = prop.getVipInit();
			if(oldNum < vipInit.getMaxUseApPotion() + getAttr().getUsenumber() ){
				return false;
			}else{
				return true;
			}
		}
		specialId = ParserString.parseString2Int(ConfigManager.getInstance().
				getConf(config10.class).get(1324).configvalue);
		if( specialId.contains(this.attr.getId()) ){
			PropRole prop = chuhan.gsp.attr.PropRole.getPropRole(roleId, false);
			vip39 vipInit = prop.getVipInit();
			if(oldNum < vipInit.getMaxUseEpPotion() + getAttr().getUsenumber() ){
				return false;
			}else{
				return true;
			}
		}
		
		if( getAttr().getUsenumber() == 0 || getAttr().getUsenumber() == -1 ){
			return false; //无限制使用
		}
		
		if(oldNum < getAttr().getUsenumber() ){
			return false; //每天使用次数
		}
		
		return true;
	}
	
}
