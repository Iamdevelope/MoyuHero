using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamFaction.GameNetWork;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;

public class HeroContainer
{
    private byte m_ContainerType;
    private int m_ContaianerSize;
    private List<ObjectCard> m_HeroList = new List<ObjectCard>();

    private List<X_GUID> newHeroList = new List<X_GUID>();

    public void ClearUp()
    {
        m_HeroList.Clear();
        newHeroList.Clear();
    }
    public void SetContainerSize(int size)
    {
        if (size > 0)
            m_ContaianerSize = size;
    }
    public int GetContainerSize()
    {
        return m_ContaianerSize;
    }
    public void SetContainerType(byte type)
    {
        m_ContainerType = type;
    }

    public void AddIHero(ObjectCard var)
    {
        m_HeroList.Add(var);
        var.UpdateSpellEffectValue();
    }

    public ObjectCard FindHero(X_GUID guid)
    {
        foreach (ObjectCard var in m_HeroList)
        {
            if (var.GetGuid().GUID_value == guid.GUID_value)
            {
                return var;
            }
        }
        return null;
    }

    /// <summary>
    /// （因为不存在重复的英雄，所以这个接口可用）;
    /// </summary>
    /// <param name="tableId">Hero表id</param>
    /// <returns></returns>
    public ObjectCard FindHero(int tableId)
    {
        foreach (ObjectCard var in m_HeroList)
        {
            if (var.GetHeroData().TableID == tableId)
            {
                return var;
            }
        }

        return null;
    }

    public List<ObjectCard> GetHeroList()
    {
        return m_HeroList;
    }

    public ObjectCard GetHeroDataByIndex(int nIndex)
    {
        if (nIndex < 0 || nIndex >= m_HeroList.Count)
        {
            return null;
        }

        return m_HeroList[nIndex];
    }

    public void EreaseHero(X_GUID guid)
    {
        foreach (ObjectCard var in m_HeroList)
        {
            if (var.GetGuid().GUID_value == guid.GUID_value)
            {
                var.ClearEquipState();//删除英雄，符文不删除。还原装配状态 [6/2/2015 Zmy]
                m_HeroList.Remove(var);

                GameEventDispatcher.Inst.dispatchEvent(GameEventID.Net_RemoveHero, guid);
                break;
            }
        }
    }

    public void RefreshHero(Hero _hero)
    {
        X_GUID _guid = new X_GUID();
        _guid.GUID_value = _hero.key;
        ObjectCard obj = FindHero(_guid);
        if (obj != null)
        {
            obj.GetHeroData().Init(_hero,true);
            obj.UpdateAttributeValue();
        }
        else
        {
            ObjectCard pHero = new ObjectCard();
            pHero.GetHeroData().Init(_hero);
            pHero.UpdateItemEffectValue();
            pHero.UpdateTeamEffectValue();
            pHero.UpdateTrainEffectValue();

            AddIHero(pHero);

            newHeroList.Add(_guid);

            AssetLoader.Inst.DynamicLoadHeroCardRes(_hero.heroid);
        }
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.Net_RefreshHero, _hero.key);
        
    }

    public bool IsNewHero(X_GUID id)
    {
        for (int i = 0; i < newHeroList.Count; i++)
        {
            if (newHeroList[i].GUID_value==id.GUID_value)
            {
                newHeroList.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    //返回英雄背包最大上线
    public int GetHeroBagSizeMax()
    {
        int nInitialSize = DataTemplate.GetInstance().m_GameConfig.getInitial_hero_packset();//初始上限
        int nCurSizeSum = DataTemplate.GetInstance().m_GameConfig.getHero_packset_per_expand() * ObjectSelf.GetInstance().HeroBuyCount;//已购买增加的上限

        int nCurLevel = ObjectSelf.GetInstance().Level;
        PlayerTemplate _row = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(nCurLevel);
        int nLeveladdition = _row.getExtraHeroPackset();//等级加成
        return nInitialSize + nCurSizeSum + nLeveladdition;
    }


    /// <summary>
    /// 判断某物品是否装备于英雄身上;
    /// 如果heroGUID为null，则查询物品是否在所有英雄上装备着;
    /// </summary>
    /// <param name="itemEquip"></param>
    /// <param name="heroGUID"></param>
    /// <returns></returns>
    public bool IsItemEquiped(ItemEquip itemEquip, X_GUID heroGUID)
    {
        ObjectCard card = FindHero(heroGUID);

        return IsItemEquiped(itemEquip, card);
    }

    public bool IsItemEquiped(ItemEquip itemEquip, ObjectCard objectCard = null)
    {
        if(itemEquip == null)
            return false;

        ///指定的英雄，则去指定的英雄身上查找;
        if (objectCard != null)
            return objectCard.GetHeroData().IsItemEquiped(itemEquip);

        ///没有指定英雄，在所有英雄身上查找;
        for (int i = 0, j = m_HeroList.Count; i < j; i++ )
        {
            if (m_HeroList[i] == null)
                continue;

            if (m_HeroList[i].GetHeroData().IsItemEquiped(itemEquip))
                return true;
        }

        return false;
    }

    /// <summary>
    /// 获得某个物品装备在哪个ObjectCard身上;
    /// </summary>
    /// <param name="itemEquip"></param>
    /// <returns></returns>
    public ObjectCard GetItemUser(ItemEquip itemEquip)
    {
        for (int i = 0, j = m_HeroList.Count; i < j; i++)
        {
            if (m_HeroList[i] == null)
                continue;

            if (m_HeroList[i].GetHeroData().IsItemEquiped(itemEquip))
                return m_HeroList[i];
        }

        return null;
    }

    /// <summary>
    /// 返回已上阵的英雄列表 and 未上阵的英雄列表
    /// </summary>
    /// <param name="_list">未上阵的英雄列表</param>
    /// <returns></returns>
    public List<ObjectCard> GetYetFormList(ref List<ObjectCard> _list)
    {
        _list.Clear();
        int idx = 0;
        List<ObjectCard> tempList = new List<ObjectCard>();
        for (int i = 0; i < m_HeroList.Count; i++)
        {
            ObjectCard card = m_HeroList[i];
            if (ObjectSelf.GetInstance().Teams.IsHeroInTeam(card.GetGuid(), ref idx))
            {
                tempList.Add(card);
            }
            else
            {
                _list.Add(card);
            }
        }

        return tempList;
    }
}

public class HeroComparer : IComparer<ObjectCard>
{
    public int Compare(ObjectCard left, ObjectCard right)
    {
        if (left.GetHeroData().FightVigor < right.GetHeroData().FightVigor)
            return 1;
        else if ( left.GetHeroData().FightVigor == right.GetHeroData().FightVigor)
        {
            if (left.GetHeroData().CurStage < right.GetHeroData().CurStage)
                return 1;
            else if (left.GetHeroData().CurStage == right.GetHeroData().CurStage)
            {
                if (left.GetHeroData().StarLevel < right.GetHeroData().StarLevel)
                    return 1;
                else if (left.GetHeroData().StarLevel == right.GetHeroData().StarLevel)
                {
                    if (left.GetHeroData().Level < right.GetHeroData().Level)
                        return 1;
                    else if (left.GetHeroData().Level == right.GetHeroData().Level)
                    {
                        if (left.GetHeroRow().getBorn() < right.GetHeroRow().getBorn())
                            return 1;
                        else if (left.GetHeroRow().getBorn() == right.GetHeroRow().getBorn())
                            return 0;
                        else
                            return -1;
                    }
                    else
                        return -1;
                }
                else
                    return -1;
            }
            else
                return -1;
        }
        else
            return -1;
    }
}