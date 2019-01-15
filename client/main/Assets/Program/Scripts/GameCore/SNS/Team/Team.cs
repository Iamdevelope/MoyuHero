using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamFaction.GameSceneEditor;
using DreamFaction.GameNetWork;
public class Team
{
    public X_GUID[,] m_Matrix = new X_GUID[GlobalMembers.MAX_MATRIX_COUNT, GlobalMembers.MAX_TEAM_CELL_COUNT];			 // 每组队伍的具体成员
    public byte  m_DefaultGroup;
    public int m_TeamGroupType;//阵型类型,1为前2后3，2为前3后2
    public Dictionary<int, int> NumTypeDic=new Dictionary<int,int>();//根据编队选取阵型类型
    public int m_GodSoulID1 = -1;
    public int m_GodSoulID2 = -1;
    public int m_GodSoulID3 = -1;
    public int m_GodSoulID4 = -1;
    public Team()
    {
        ClearUp();
    }
    public void ClearUp()
    {
        for (int i = 0; i < GlobalMembers.MAX_MATRIX_COUNT; ++i)
        {
            for (int j = 0; j < GlobalMembers.MAX_TEAM_CELL_COUNT; ++j)
            {
                m_Matrix[i, j] = new X_GUID();
            }
        }
        m_DefaultGroup = 0;

        NumTypeDic.Clear();
    }
    /// <summary>
    /// 更新队伍信息！
    /// </summary>
    /// <param name="nID"></param>
    /// <param name="guid"></param>
    /// <param name="nIndex"></param>
    public void SetTeamMember(int nID, X_GUID guid, int nIndex)
    {
        if ((nID < 0) || (nID >= GlobalMembers.MAX_MATRIX_COUNT))
        {
            return;
        }
        if((nIndex < 0)||(nIndex >= GlobalMembers.MAX_TEAM_CELL_COUNT))
        {
            return;
        }
        if (guid.GUID_value == 0)
        {
            m_Matrix[nID, nIndex].CleanUp();
            return;
        }
        m_Matrix[nID, nIndex].Copy(guid);
    }
 
    public void SetFormationNum(int nID,int Num)
    {

    }
    public void RemoveTeamMember(int nID, X_GUID guid)
    {
        if ((nID < 0) || (nID >= GlobalMembers.MAX_MATRIX_COUNT))
        {
            return;
        }
        if(guid.IsValid())
        {
            for(int i=0; i<GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
            {
                if (m_Matrix[nID, i].GUID_value == guid.GUID_value)
                {
                    m_Matrix[nID, i].CleanUp();
                    break;
                }
            }
        }
    }

    public int FindTeamMemberIndex(int nID, X_GUID guid)
    {
        if ((nID < 0) || (nID >= GlobalMembers.MAX_MATRIX_COUNT))
        {
            return int.MaxValue;
        }
        for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
        {
            if (m_Matrix[nID, i].GUID_value == guid.GUID_value)
            {
                return i;
            }
        }
        return int.MaxValue;
    }

    public void FindTeamMember(int nID, int nIndex, ref X_GUID guid)
    {
        if ((nID < 0) || (nID >= GlobalMembers.MAX_MATRIX_COUNT))
        {
            return;
        }
        if ((nIndex < 0) || (nIndex >= GlobalMembers.MAX_TEAM_CELL_COUNT))
        {
            return;
        }
        guid = m_Matrix[nID, nIndex];
    }
   // public int GetTeamGroupType() { return m_TeamGroupType;}
    public void SetTeamGroupType(int type) { m_TeamGroupType = type; }
    public byte GetDefaultGroup() { return m_DefaultGroup; }
    public void SetDefaultGroup(byte type) { m_DefaultGroup = type; }
    public void SetNumTypeDic(int key,int type)
    {
        NumTypeDic[key] = type;
    }
    public int GetFormationType()
    {
        return NumTypeDic[GetDefaultGroup()];
    }
    public HeroFormationType GetFormation()
    {
        return (GetFormationType() == 1) ? HeroFormationType.Formation123 : HeroFormationType.Formation132;
    }

    //返回英雄是否在队伍中，并输出队伍编号 [5/6/2015 Zmy]
    public bool IsHeroInTeam(X_GUID guid,ref int nID)
    {
        for (int i = 0; i < GlobalMembers.MAX_MATRIX_COUNT; ++i)
        {
            for (int j = 0; j < GlobalMembers.MAX_TEAM_CELL_COUNT; ++j)
            {
               if (m_Matrix[i,j].Equals(guid))
               {
                   nID = i;
                   return true;
               }
            }
        }
        return false;
    }

    /// <summary>
    /// 返回英雄是否在各个队伍中,返回数组中[0]为队伍1，[1]为队伍2依次类推
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public bool[] GetHeroInTeamList(X_GUID guid)
    {
        bool[] _resultArray = new bool[GlobalMembers.MAX_MATRIX_COUNT];

        for (int i = 0; i < GlobalMembers.MAX_MATRIX_COUNT; ++i)
        {
            for (int j = 0; j < GlobalMembers.MAX_TEAM_CELL_COUNT; ++j)
            {
                if (m_Matrix[i, j].Equals(guid))
                {
                    _resultArray[i] = true;
                    break;
                }
            }
        }
        return _resultArray;
    }

    /// <summary>
    /// 获得指定位置的英雄的X_GUID
    /// </summary>
    /// <param name="groupId">第几个小队[0,2]</param>
    /// <param name="idx">小队中的位置[0,4]</param>
    /// <returns></returns>
    public X_GUID GetHeroGUID(int groupId, int idx)
    {
        if (groupId < 0 || groupId > GlobalMembers.MAX_MATRIX_COUNT - 1)
        {
            return null;
        }

        if (idx < 0 || idx > GlobalMembers.MAX_TEAM_CELL_COUNT - 1)
        {
            return null;
        }

        return m_Matrix[groupId, idx];
    }
    /// <summary>
    /// 获取英雄在珍惜中的位置 1 前排 2为后排
    /// </summary>
    /// <param name="guid">英雄的guid</param>
    /// <returns></returns>
    public int GetGroupPosByHeroGuid(X_GUID guid)
    {
            for (int j = 0; j < GlobalMembers.MAX_TEAM_CELL_COUNT; ++j)
            {
                if (m_Matrix[(int)m_DefaultGroup, j].Equals(guid))
                {
                    if (j == 0 || j == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            return -1;
    } 
    /// <summary>
    /// 获取英雄在队伍中的index
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public int GetTeamIndexByHeroGuid(X_GUID guid)
    {
        for (int j = 0; j < GlobalMembers.MAX_TEAM_CELL_COUNT; ++j)
        {
            if (m_Matrix[(int)m_DefaultGroup, j].Equals(guid))
            {
                return j;
            }
        }
        return -1;
    }
}
