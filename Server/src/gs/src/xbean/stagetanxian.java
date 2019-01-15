
package xbean;

public interface stagetanxian extends xdb.Bean {
	public stagetanxian copy(); // deep clone
	public stagetanxian toData(); // a Data instance
	public stagetanxian toBean(); // a Bean instance
	public stagetanxian toDataIf(); // a Data instance If need. else return this
	public stagetanxian toBeanIf(); // a Bean instance If need. else return this

	public java.util.List<xbean.tanxian> getStagetanxian(); // 每章节探险列表
	public java.util.List<xbean.tanxian> getStagetanxianAsData(); // 每章节探险列表

}
