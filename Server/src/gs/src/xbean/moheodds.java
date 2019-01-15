
package xbean;

public interface moheodds extends xdb.Bean {
	public moheodds copy(); // deep clone
	public moheodds toData(); // a Data instance
	public moheodds toBean(); // a Bean instance
	public moheodds toDataIf(); // a Data instance If need. else return this
	public moheodds toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, Integer> getMoheoddsmap(); // 魔盒几率列表
	public java.util.Map<Integer, Integer> getMoheoddsmapAsData(); // 魔盒几率列表

}
