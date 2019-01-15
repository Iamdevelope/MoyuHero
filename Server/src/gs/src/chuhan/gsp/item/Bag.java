package chuhan.gsp.item;
import java.util.Iterator;

import chuhan.gsp.util.Conv;

/***
 * 
 * 
 */
//发送协议
public class Bag extends ItemColumn {
	public static final int MONEY_SYSLOCK_MSG = 145210;
	
	public Bag(long roleid, boolean readonly) {		
		super(roleid, BagTypes.BAG, readonly);
	}
	
	@Override
	public void clear() {
		final SRemoveItem send = new SRemoveItem();
		send.bagid = Conv.toByte(getBagid());
		for (BasicItem bi : this) {
			send.itemkeys.add(bi.getKey());
		}
		xdb.Procedure.psendWhileCommit(roleid, send);
		super.clear();
	}

/*	public int addItem(final int itemid, final int num, String reason ,boolean showMsg, 
			final List<xio.Protocol> toSend) {
		return addItem( itemid, num, reason, showMsg, toSend, false );
	}*/
	
	/*@Override
	public int addItem(final int itemid, final int num, final int numtype, final int initflag, final String reason,
			final int countertype, final int xiangguanid) {
		return addItem(itemid, num, numtype, initflag, reason, countertype, xiangguanid, true);
	}
	
	public int addItem(final int itemid, final int num, final String reason,
			final int countertype, final int xiangguanid, final boolean playeffect) {
		return addItem(itemid, num, 0, 0, reason, countertype, xiangguanid, playeffect);
	}*/
	/**
	 * addItem
	 * @param itemid 物品id
	 * @param num 物品数量
	 * @param reason 添加理由
	 * @param showMsg 是否发送公共提示
	 */
	/*public int addItem(final int itemid, final int num, final int numtype, 
			final int initflag, final String reason,
			final int countertype, final int xiangguanid, final boolean playeffect) {
		SItemAttr attr = Module.getInstance().getAttr(itemid);
		if ( attr == null )
			throw new RuntimeException("物品id="+itemid+"属性为null");
		final int added;
			
		added = super.addItem(itemid, num, numtype, initflag, reason, countertype, xiangguanid);
		
		// here
		if (added > 0 && playeffect) {
			SNotifyNewItemAdded send = new SNotifyNewItemAdded();
			send.additems.add(new AddItemInfo(itemid, added));
			xdb.Procedure.psendWhileCommit(roleid, send);
		}
		return added;
	}*/
	
	/*public long addSysMoney( final long money, final String reason, final int countertype, final int xiangguanid ) {
		if(money < 0)
		{
			xbean.Properties xprop = xtable.Properties.get(roleid);
			if (xprop.getMoneylocked())
			{
				Message.psendMsgNotify(roleid, MONEY_SYSLOCK_MSG, null);
				return 0;
			}
		}
		long ret = addMoney( money, reason, countertype, xiangguanid );
		if ( ret > 0 ) {
			if(!new PCheckRoleMoneyAndExpLimit(roleid,ret,PCheckRoleMoneyAndExpLimit.MONEY).call())
				throw new RuntimeException("money exceed day limit!");
			new knight.gsp.statistics.RoleQuitStatistics(roleid, false).addMoney(ret);
			knight.gsp.chengjiu.IAchievementSystem sys = knight.gsp.chengjiu.AchievementManager.getInstance().getAchievementSystem(roleid, false);
			knight.gsp.chengjiu.Helper.addAchievementParam(sys, 410301, (int)money);
			knight.gsp.chengjiu.Helper.addAchievementParam(sys, 410302, (int)money);
			knight.gsp.chengjiu.Helper.addAchievementParam(sys, 410303, (int)money);
		}
		return ret;
	}
	
	public long addTradeMoney( final long money, final String reason, final int countertype,final int xiangguanid ) {
		if(money < 0)
		{
			xbean.Properties xprop = xtable.Properties.get(roleid);
			if (xprop.getMoneylocked())
			{
				Message.psendMsgNotify(roleid, MONEY_SYSLOCK_MSG, null);
				return 0;
			}
		}
		long ret = addMoney(money, reason, countertype, xiangguanid);
		return ret;
	}*/

	public long addMoney(
			final long money, 
			final String reason,
			final int countertype,
			final int xiangguanid) {
		if (readonly)
			return 0;
		// 防止溢出
		long res = (Long.MAX_VALUE - getMoney() <= money) ? Long.MAX_VALUE : money + getMoney();// 防止溢出;
		if (res < 0 ) {
			Module.logger.error( "金钱不足" );
			return 0;
		} else if (res > conf.maxmoney ) {
			chuhan.gsp.msg.Message.psendMsgNotify(roleid, 0);
			return 0;
		}
		long ret = addMoney(money, reason);
		reason.replace( ' ', '_' );
		return ret;
	}
	
	@Override
	public long addMoney(final long money, final String reason) {
		long ret = super.addMoney(money, reason);
		return ret;
	}
	@Override
	public BasicItem moveOut(final int key, final int number, final String reason, boolean isnotify) {
		BasicItem item = super.moveOut( key, number, reason ,isnotify);
		return item;
	}
	@Override
	public AddItemResult addItem(final BasicItem item, final int pos, String reason, final int countertype, final int xiangguanid) {
		return addItem(item, pos, reason, countertype, xiangguanid, true);
	}
	public AddItemResult addItem(final BasicItem item, final int pos, String reason, final int countertype, final int xiangguanid, final boolean playeffect) {
		// 添加了item.getNumber()个物品 ;
		final long oldownerid = item.getOwnerid();
		final int oldbagid = item.getBagid();
		final int oldnumber = item.getNumber();
		{
			AddItemResult ret = super.addItem(item, pos, reason, countertype, xiangguanid);
			if (ret.result != AddItemResult.AddResultEnum.SUCC) {
				 return ret;
			}
		}
		// 添加了多少个物品?
		/*if (roleid != oldownerid && playeffect) {
			SNotifyNewItemAdded send = new SNotifyNewItemAdded();
			send.additems.add(new AddItemInfo(item.getItemid(), oldnumber));
			xdb.Procedure.psendWhileCommit(roleid, send);
		}*/
		return new AddItemResult(AddItemResult.AddResultEnum.SUCC);
	}
	
	@Override
	public boolean moveIn(BasicItem item, int pos, BasicItem dstitem) {
		if (super.moveIn(item, pos, dstitem)) {
			return true;
		}
		return false;
	}
	@Override
	public BasicItem getItem(int itemkey) {
		BasicItem item = super.getItem(itemkey);
		return item;
	}


	@Override
	public int getBagid() {
		return BagTypes.BAG;
	}
	
	@Override
	public int countItemNumber(int itemid, int flag) {
		return super.countItemNumber(itemid, flag);
	}
	@Override
	public int countItemNumberExceptTradeStall(int  itemid){
		return super.countItemNumberExceptTradeStall(itemid);
	}
	
	private class BagIterator implements Iterator<BasicItem> {
		private Iterator<Integer> bagiter;
		public BagIterator(){		
			bagiter = bag.getItems().keySet().iterator();
		}

		@Override
		public boolean hasNext() {		
			return bagiter.hasNext();
		}

		@Override
		public BasicItem next() {
			xbean.Item item;
			if ( bagiter.hasNext() ) {
				int key = bagiter.next();
				item = bag.getItems().get( key );
				if ( item == null )
					return null;
				return Module.getInstance().toBasicItem( 
						item, roleid, BagTypes.BAG, key );
			} else 
				return null;
		}

		@Override
		public void remove() {
			//这里会抛出NoSuchElementException
			throw new RuntimeException( "暂时不支持删除" );	
		}
	}
	@Override
	public Iterator<BasicItem> iterator() {
		return new BagIterator();
	}
}
