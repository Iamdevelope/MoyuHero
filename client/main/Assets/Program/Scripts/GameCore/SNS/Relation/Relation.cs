using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;

public class Relation
{
    private List<RelationMemberData> m_RelationMember = new List<RelationMemberData>();
    private List<ApplyMemberData> m_ApplyMember = new List<ApplyMemberData>();

    public void AddRelationMember(RelationMemberData var)
    {
        m_RelationMember.Add(var);
    }

    public void AddApplyMember(ApplyMemberData var)
    {
        m_ApplyMember.Add(var);
    }

    public RelationMemberData FindRelationMember(X_GUID guid)
    {
        foreach (RelationMemberData var in m_RelationMember)
        {
            if (var.MemberGUID.GUID_value == guid.GUID_value)
            {
                return var;
            }
        }
        return null;
    }

    public void EreaseRelationMember(X_GUID guid)
    {
        foreach (RelationMemberData var in m_RelationMember)
        {
            if (var.MemberGUID.GUID_value == guid.GUID_value)
            {
                m_RelationMember.Remove(var);
                break;
            }
        }
    }

    public void EreaseApplyMember(X_GUID guid)
    {
        foreach (ApplyMemberData var in m_ApplyMember)
        {
            if (var.MemberData.MemberGUID.GUID_value == guid.GUID_value)
            {
                m_ApplyMember.Remove(var);
                break;
            }
        }
    }

    public void RemoveAllRelationMember()
    {
        m_RelationMember.Clear();
    }

    public void RemoveAllApplyMember()
    {
        m_ApplyMember.Clear();
    }
}