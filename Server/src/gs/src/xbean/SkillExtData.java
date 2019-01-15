
package xbean;

public interface SkillExtData extends xdb.Bean {
	public SkillExtData copy(); // deep clone
	public SkillExtData toData(); // a Data instance
	public SkillExtData toBean(); // a Bean instance
	public SkillExtData toDataIf(); // a Data instance If need. else return this
	public SkillExtData toBeanIf(); // a Bean instance If need. else return this

	public int getLevel(); // 
	public int getGrade(); // 
	public int getExp(); // 

	public void setLevel(int _v_); // 
	public void setGrade(int _v_); // 
	public void setExp(int _v_); // 
}
