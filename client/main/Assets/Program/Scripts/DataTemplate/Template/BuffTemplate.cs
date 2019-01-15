//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class BuffTemplate : IExcelBean
{
	#region attribute
	//buffID
	private	int m_id;
	//name
	private	string m_name;
	//DES
	private	string m_DES;
	//效果逻辑ID
	private	int m_buffType;
	//逻辑参数
	private	int[] m_param;
	//概率（百分比）
	private	int m_probability;
	//是否为有益buff
	private	int m_conduce;
	//是否可被驱散
	private	int m_dispel;
	//制造者死亡消失
	private	int m_makerDeath;
	//互斥ID
	private	int m_exclusionID;
	//互斥优先级
	private	int m_exclusionPriority;
	//持续时间（毫秒）
	private	int m_durationTime;
	//叠加上限
	private	int m_maxOverlayCount;
	//buff小图标
	private	string m_buffIconSmall;
	//buff大图标
	private	string m_buffIconBig;
	//buff特效播放位置
	private	string m_buffEffectPosition;
	//buff起始特效
	private	string m_buffEffectOp;
	//buff循环特效
	private	string m_buffEffect;
	//buff结束特效
	private	string m_buffEffectEd;
	//buff音效
	private	string m_buffSound;
	//buff生效音效
	private	string m_buffEffectSound;
	//buff生效音效条件
	private	int m_buffEffectSoundCondition;
	//buff时间结束时音效
	private	string m_buffEndSound;
	//hot/dot生效间隔
	private	int m_buffEffectInterval;
	//buff触发类型
	private	int m_buffTriggerType;
	//hot/dot生效类型
	private	int m_buffEffectType;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_name = ReadToString(data);
		m_DES = ReadToString(data);
		m_buffType = data.ReadInt32();
		m_param = parserXMLIntArray(ReadToString(data));
		m_probability = data.ReadInt32();
		m_conduce = data.ReadInt32();
		m_dispel = data.ReadInt32();
		m_makerDeath = data.ReadInt32();
		m_exclusionID = data.ReadInt32();
		m_exclusionPriority = data.ReadInt32();
		m_durationTime = data.ReadInt32();
		m_maxOverlayCount = data.ReadInt32();
		m_buffIconSmall = ReadToString(data);
		m_buffIconBig = ReadToString(data);
		m_buffEffectPosition = ReadToString(data);
		m_buffEffectOp = ReadToString(data);
		m_buffEffect = ReadToString(data);
		m_buffEffectEd = ReadToString(data);
		m_buffSound = ReadToString(data);
		m_buffEffectSound = ReadToString(data);
		m_buffEffectSoundCondition = data.ReadInt32();
		m_buffEndSound = ReadToString(data);
		m_buffEffectInterval = data.ReadInt32();
		m_buffTriggerType = data.ReadInt32();
		m_buffEffectType = data.ReadInt32();
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getName()
	{
		return this.m_name;
	}

	public	string	getDES()
	{
		return this.m_DES;
	}

	public	int	getBuffType()
	{
		return this.m_buffType;
	}

	public	int[]	getParam()
	{
		return this.m_param;
	}

	public	int	getProbability()
	{
		return this.m_probability;
	}

	public	int	getConduce()
	{
		return this.m_conduce;
	}

	public	int	getDispel()
	{
		return this.m_dispel;
	}

	public	int	getMakerDeath()
	{
		return this.m_makerDeath;
	}

	public	int	getExclusionID()
	{
		return this.m_exclusionID;
	}

	public	int	getExclusionPriority()
	{
		return this.m_exclusionPriority;
	}

	public	int	getDurationTime()
	{
		return this.m_durationTime;
	}

	public	int	getMaxOverlayCount()
	{
		return this.m_maxOverlayCount;
	}

	public	string	getBuffIconSmall()
	{
		return this.m_buffIconSmall;
	}

	public	string	getBuffIconBig()
	{
		return this.m_buffIconBig;
	}

	public	string	getBuffEffectPosition()
	{
		return this.m_buffEffectPosition;
	}

	public	string	getBuffEffectOp()
	{
		return this.m_buffEffectOp;
	}

	public	string	getBuffEffect()
	{
		return this.m_buffEffect;
	}

	public	string	getBuffEffectEd()
	{
		return this.m_buffEffectEd;
	}

	public	string	getBuffSound()
	{
		return this.m_buffSound;
	}

	public	string	getBuffEffectSound()
	{
		return this.m_buffEffectSound;
	}

	public	int	getBuffEffectSoundCondition()
	{
		return this.m_buffEffectSoundCondition;
	}

	public	string	getBuffEndSound()
	{
		return this.m_buffEndSound;
	}

	public	int	getBuffEffectInterval()
	{
		return this.m_buffEffectInterval;
	}

	public	int	getBuffTriggerType()
	{
		return this.m_buffTriggerType;
	}

	public	int	getBuffEffectType()
	{
		return this.m_buffEffectType;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}