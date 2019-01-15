package chuhan.gsp.item.types;

import com.goldhuman.Common.Octets;

import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.UseResult;
/**
 * 普通物品
 * @author liuchen
 *
 */
public class NormalItem extends BasicItem{

	NormalItem(int itemid) {
		super(itemid);
	}
	NormalItem(xbean.Item item) {
		super(item);
	}

	@Override
	protected void afterInsert() {
		// TODO Auto-generated method stub
		
	}

	@Override
	protected void afterDelete() {
		// TODO Auto-generated method stub
		
	}
	@Override
	public Octets getExtdataOctets() {
		return new Octets();
	}
	@Override
	public UseResult use(long roleId, int num, int dstkey) {
		return UseResult.FAIL;
	}
}
