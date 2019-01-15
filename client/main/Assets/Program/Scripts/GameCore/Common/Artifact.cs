using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.LogSystem;
using DreamFaction.GameEventSystem;
using GNET;
/// <summary>
///  单独一个神器对象的数据结构。负责属性运算 [5/26/2015 Zmy]
/// </summary>
public class Artifact
{
    private bool                    m_ActivateState;    //激活状态。满足激活条件。属性运行才会返回有效值
    private ArtifactDB              m_pArtifactData = new ArtifactDB();
    private int                     m_nArtifactType;

    private Flag32                  m_DirtyMask = new Flag32();
    private int                     m_MaxHP;                    //血上限
    private int                     m_PhysicalAttack;           //物理攻击
    private int                     m_PhysicalDefence;		    //物理防御
    private int                     m_MagicAttack;				//法术攻击
    private int                     m_MagicDefence;				//法术防御
    private int                     m_Hit;					    //命中
    private int                     m_Dodge;				    //闪避
    private int                     m_Critical;				    //暴击
    private int                     m_Tenacity;				    //韧性

    ArtifactTemplate                m_pRowData;
    public ArtifactDB               GetArtifactDB() { return m_pArtifactData; }
    public ArtifactTemplate         GetArtifactRow(){ return m_pRowData; }
    public Artifact()
    {
        m_ActivateState = true;           // 测试数据
        m_nArtifactType = -1;
        m_pRowData = null;
        m_MaxHP = 0;
        m_PhysicalAttack = 0;
        m_PhysicalDefence = 0;
        m_MagicAttack = 0;
        m_MagicDefence = 0;
        m_Hit = 0;
        m_Dodge = 0;
        m_Critical = 0;
        m_Tenacity = 0;
    }

    public void InitArtifactData(GNET.Artifact pInit)
    {
        m_nArtifactType = pInit.artifacttype;
        m_pArtifactData.Copy(pInit);

        m_pRowData = (ArtifactTemplate)DataTemplate.GetInstance().m_ArtifactTable.getTableData(pInit.artifactid);

        UpdateActivateState();
    }

    public bool UpdateActivateState()
    {
        if (ObjectSelf.GetInstance().Level >= m_pRowData.getPlayerLevel() && m_ActivateState == false)
        {
            //满足开启条件 [5/27/2015 Zmy]
            m_ActivateState = true;

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Aritfact_Enable);
            return true;
        }

        return false;
    }
    public void CleanUp()
    {
        m_pArtifactData.CleanUp();
    }


    public bool IsBitSet(EM_ATTRIBUTE nType)
    {
        return m_DirtyMask.isSetBit((int)nType);
    }

    public void SetBitFlag(EM_ATTRIBUTE nType)
    {
        m_DirtyMask.UpdateBits((int)nType, true);
    }

    public void ClearBitFlag(EM_ATTRIBUTE nType)
    {
        m_DirtyMask.UpdateBits((int)nType, false);
    }
  
    //所需英雄的属性权重和
    public float GetEffectAttriSUM()
    {
        float nSUM = 0;
        for (int i = 0; i < GlobalMembers.MAX_ARTIFACT_HERO_COUNT; i++ )
        {
            if (m_pRowData.getHeroID()[i] != -1)
            {
                nSUM += m_pRowData.getHeroNum()[i] * m_pRowData.getWeight()[i];
            }
        }
        return nSUM;
    }
    //已注入英雄的属性权重之和 [5/26/2015 Zmy]
    public float GetIntoRecodeSUM()
    {
        float nSUM = 0;
        for (int i = 0; i < GlobalMembers.MAX_ARTIFACT_HERO_COUNT; i++)
        {
            if (m_pRowData.getHeroID()[i] != -1)
            {
                nSUM += m_pArtifactData.m_IntoRecord[i] * m_pRowData.getWeight()[i];
            }
        }
        return nSUM;
    }

    // 属性加成值 [5/26/2015 Zmy]
    public float GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE nType)
    {
        for (int i = 0; i < GlobalMembers.MAX_ARTIFACT_HERO_COUNT; i++ )
        {
            if (m_pRowData.getAttriType()[i] == (int)nType)
            {
                return m_pRowData.getAttriValue()[i];
            }
        }
        return 0;
    }
    // 等级提升的加成，即:累加每一级的属性值 [5/26/2015 Zmy]
    public float GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE nType)
    {
        if (m_pRowData.getLevel() <= 1)
        {
            return 0;
        }
        float nSum = 0;

        for (int nLevel = m_pRowData.getLevel() - 1; nLevel >= 1; nLevel-- )
        {
            int nTableID = m_pRowData.GetID() - (m_pRowData.getLevel() - nLevel);//上一级的ID = 当前ID  - （当前等级与上一级的差值）
            ArtifactTemplate _tmpRow = (ArtifactTemplate)DataTemplate.GetInstance().m_ArtifactTable.getTableData(nTableID);
            if (_tmpRow != null && _tmpRow.getLevel() == nLevel)
            {
                for (int i = 0; i < GlobalMembers.MAX_ARTIFACT_HERO_COUNT; i++)
                {
                    if (_tmpRow.getAttriType()[i] == (int)nType)
                    {
                        nSum += _tmpRow.getAttriValue()[i];
                        break;
                    }
                }
            }
        }
        return nSum;
    }
    public int GetMaxHP()						//获得最大hp
    {
        if (m_ActivateState == false)
            return 0;
        
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
            m_MaxHP = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP));

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP);
        }
        return m_MaxHP;
    }

	public int GetCurHP()						//获得最大hp
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
            m_MaxHP = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP);
		}
		return m_MaxHP;
	}

    public int GetPhysicalAttack()		        //攻击
    {
        if (m_ActivateState == false)
            return 0;

        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
            m_PhysicalAttack = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK)); 

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK);
        }
        return m_PhysicalAttack;
    }

	public int GetCurPhysicalAttack()		        //攻击
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
            m_PhysicalAttack = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK);
		}
		return m_PhysicalAttack;
	}

    public int GetPhysicalDefence() 		    //防御
    {
        if (m_ActivateState == false)
            return 0;

        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
            m_PhysicalDefence = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE)); 

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE);
        }
        return m_PhysicalDefence;
    }
	public int GetCurPhysicalDefence() 		    //防御
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
            m_PhysicalDefence = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE);
		}
		return m_PhysicalDefence;
	}


    public int GetMagicAttack() 		        //攻击
    {
        if (m_ActivateState == false)
            return 0;

        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
            m_MagicAttack = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK)); 

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK);
        }
        return m_MagicAttack;
    }

	public int GetCurMagicAttack() 		        //攻击
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
            m_MagicAttack = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK);
		}
		return m_MagicAttack;
	}

    public int GetMagicDefence()		        //防御
    {
        if (m_ActivateState == false)
            return 0;

        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
            m_MagicDefence = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE));

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE);
        }
        return m_MagicDefence;
    }
	public int GetCurMagicDefence()		        //防御
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
            m_MagicDefence = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE);
		}
		return m_MagicDefence;
	}

    public int GetDodge()		                //闪避
    {
        if (m_ActivateState == false)
            return 0;

        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
			//m_Dodge = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_HIT) + GetAppendLevelAttributeValue(EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_HIT));

            m_Dodge = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE)); 

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE);
        }
        return m_Dodge;
    }
	public int GetCurDodge()		                //闪避
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
			//m_Dodge = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_HIT) + GetAppendLevelAttributeValue(EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_HIT));

            m_Dodge = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE);
		}
		return m_Dodge;
	}

    public int GetCritical()		            //暴击
    {
        if (m_ActivateState == false)
            return 0;

        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
            m_Critical = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL)); 

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL);
        }
        return m_Critical;
    }

	public int GetCurCritical()		            //暴击
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
            m_Critical = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL);
		}
		return m_Critical;
	}

    public int GetHit() 			            //总命中
    {
        if (m_ActivateState == false)
            return 0;

        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
            m_Hit = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT)); 

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT);
        }
        return m_Hit;
    }

	public int GetCurHit() 			            //总命中
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
            m_Hit = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT);
		}
		return m_Hit;
	}

    public int GetTenacity()			        //总韧性
    {
        if (m_ActivateState == false)
            return 0;

        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY))
        {
            float nSum = GetEffectAttriSUM();
            float nCurSum = GetIntoRecodeSUM();
            //最终
            m_Tenacity = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY) + GetAppendLevelAttributeValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY)); 

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY);
        }
        return m_Tenacity;
    }

	public int GetCurTenacity()			        //总韧性
	{
		if (m_ActivateState == false)
			return 0;

		if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY))
		{
			float nSum = GetEffectAttriSUM();
			float nCurSum = GetIntoRecodeSUM();
			//最终
            m_Tenacity = (int)(nCurSum / nSum * GetAppendArrtibuteValue(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY));

			ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY);
		}
		return m_Tenacity;
	}
}
