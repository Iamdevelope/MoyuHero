package chuhan.gsp.buff;

public class BuffConfig
{
	
	protected int buffTypeId;//buff type id
	
	protected String classname;
	
	protected String buffName;//buff name
	
	protected long sourceRoleId;//buff来源role id
	
	protected long sourceSkillId;//buff来源技能Id

	public int getBuffTypeId()
	{
		return buffTypeId;
	}

	public void setBuffTypeId(int buffTypeId)
	{
		this.buffTypeId = buffTypeId;
	}

	public String getClassname()
	{
		return classname;
	}

	public void setClassname(String classname)
	{
		this.classname = classname;
	}

	public String getBuffName()
	{
		return buffName;
	}

	public void setBuffName(String buffName)
	{
		this.buffName = buffName;
	}

	public long getSourceRoleId()
	{
		return sourceRoleId;
	}

	public void setSourceRoleId(long sourceRoleId)
	{
		this.sourceRoleId = sourceRoleId;
	}

	public long getSourceSkillId()
	{
		return sourceSkillId;
	}

	public void setSourceSkillId(long sourceSkillId)
	{
		this.sourceSkillId = sourceSkillId;
	}
	
	
	
}
