
package xbean;

public interface RebateChargeActivity extends xdb.Bean {
	public RebateChargeActivity copy(); // deep clone
	public RebateChargeActivity toData(); // a Data instance
	public RebateChargeActivity toBean(); // a Bean instance
	public RebateChargeActivity toDataIf(); // a Data instance If need. else return this
	public RebateChargeActivity toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, Integer> getAwardinfo(); // key=rmb valeu=num
	public java.util.Map<Integer, Integer> getAwardinfoAsData(); // key=rmb valeu=num

}
