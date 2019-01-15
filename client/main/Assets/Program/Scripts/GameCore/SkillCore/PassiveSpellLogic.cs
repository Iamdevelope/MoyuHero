/********************************************************************
	created:	2015/11/03
	created:	3:11:2015   17:56
	filename: 	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\SkillCore\PassiveSpellLogic.cs
	file path:	D:\Dream Heros\trunk\Assets\Program\Scripts\GameCore\SkillCore
	file base:	PassiveSpellLogic
	file ext:	cs
	author:		Zhao Mingyang
	
	purpose:	战斗中英雄被动技能的主动释放逻辑
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveSpellLogic 
{
    private int m_NormalAttackCount;                    //普攻的次数 [11/3/2015 Zmy]

    bool OneSpellFreeState = false;
    bool TwoSpellFreeState = false;

    public PassiveSpellLogic()
    {
        ClearUp();
    }

    public void ClearUp()
    {
        m_NormalAttackCount = 1;
        OneSpellFreeState = false;
        TwoSpellFreeState = false;
    }

    public void UpdateNormalAttackCount()
    {
        m_NormalAttackCount++;

        if (m_NormalAttackCount > DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_X())
        {
            ClearUp();
        }
    }

    //校验被动技能之主动释放的概率[11/3/2015 Zmy]
    public bool CheckFreeLogic(ObjectHero objHero)
    {
        if (objHero.GetHeroData().QualityLev > 1 && OneSpellFreeState == false && m_NormalAttackCount >= DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_P())
        {
            int nProValueAddtion = (m_NormalAttackCount - DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_P()) * DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_B();
            int nProValue = DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_A() + nProValueAddtion;

            int iRnd = System.DateTime.Now.Millisecond;
            System.Random randomCoor = new System.Random(iRnd);
            int nRand = randomCoor.Next(1, 1000);

            if (nRand <= nProValue)
            {
                OneSpellFreeState = true;
                //Debug.Log("!满足第一个被动技能释放。在周期内第" + m_NormalAttackCount + "次普攻触发了！释放第一技能，普攻次数+1 =====s随机概率为" + nRand + "！！！释放概率：" + nProValue);
                m_NormalAttackCount++;
                if (m_NormalAttackCount > DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_X())
                {
                    ClearUp();
                }
                objHero.LaunchFreeSpellLogic(EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_FIRST);
                return true;
            }
            
        }

        if (objHero.GetHeroData().QualityLev > 3 && TwoSpellFreeState == false && m_NormalAttackCount >= DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_Q())
        {
            int nProValueAddtion = (m_NormalAttackCount - DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_Q()) * DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_D();
            int nProValue = DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_C() + nProValueAddtion;

            int iRnd = System.DateTime.Now.Millisecond;
            System.Random randomCoor = new System.Random(iRnd);
            int nRand = randomCoor.Next(1, 1000);

            if (nRand <= nProValue)
            {
                TwoSpellFreeState = true;
                //Debug.Log("!满足第二个被动技能释放。在周期内第" + m_NormalAttackCount + "次普攻触发了！释放第一技能，普攻次数+1 =====s随机概率为"+ nRand + "！！！释放概率："+nProValue);
                m_NormalAttackCount++;
                if (m_NormalAttackCount > DataTemplate.GetInstance().m_GameConfig.getPassive_Active_Skills_X())
                {
                    ClearUp();
                }
                objHero.LaunchFreeSpellLogic(EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_SECOND);
                return true;
            }
        }

        //Debug.Log("!本次普攻次数为："+m_NormalAttackCount + "没有触发任何被动技");
        return false;
    }
}
