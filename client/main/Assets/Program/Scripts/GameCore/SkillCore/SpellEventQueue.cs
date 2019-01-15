using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.SkillCore;
using DreamFaction.LogSystem;


public class SPELL_EVENT
{
    public Impact m_pImpact = new Impact();
    public SPELL_EVENT_ID m_nEventID = SPELL_EVENT_ID.SPELL_EVENT_ID_INVALID;

    public void CleanUp()
    {
        m_pImpact = null;
        m_nEventID = SPELL_EVENT_ID.SPELL_EVENT_ID_INVALID;
    }
}

public class SpellEventQueue
{
    private Dictionary<SPELL_EVENT_ID, List<SPELL_EVENT>> m_UseList = new Dictionary<SPELL_EVENT_ID, List<SPELL_EVENT>>();
    private List<SPELL_EVENT>   m_FreeCache = new List<SPELL_EVENT>();

    public bool HaveEvent(SPELL_EVENT_ID id)
    {
        if ((id >= SPELL_EVENT_ID.SPELL_EVENT_ID_NUMBER) || (id <= SPELL_EVENT_ID.SPELL_EVENT_ID_INVALID))
        {
            return false;
        }
        if (m_UseList.ContainsKey(id))
        {
            return m_UseList[id].Count > 0 ? true : false; 
        }
		return false;
    }

    public List<SPELL_EVENT> GetEventList(SPELL_EVENT_ID id)
    {
        if ((id >= SPELL_EVENT_ID.SPELL_EVENT_ID_NUMBER) || (id <= SPELL_EVENT_ID.SPELL_EVENT_ID_INVALID))
        {
            LogManager.LogAssert(null);
        }
        if (!m_UseList.ContainsKey(id))
        {
              m_UseList.Add(id, new List<SPELL_EVENT>());
        }

        return m_UseList[id];
    }

    public bool Push(Impact pImpact, SPELL_EVENT_ID nID)
    {
        if ((pImpact == null) || (nID >= SPELL_EVENT_ID.SPELL_EVENT_ID_NUMBER) || (nID <= SPELL_EVENT_ID.SPELL_EVENT_ID_INVALID))
        {
            LogManager.LogAssert(null);
        }

        if (m_FreeCache.Count > 0)
        {
            SPELL_EVENT pRet = m_FreeCache[0];
            m_FreeCache.RemoveAt(0);

            pRet.m_pImpact = pImpact;
            pRet.m_nEventID = nID;
            m_UseList[nID].Add(pRet);
        }
        else
        {
            if (!m_UseList.ContainsKey(nID))
            {
                m_UseList.Add(nID, new List<SPELL_EVENT>());
            }
            SPELL_EVENT pEvent = new SPELL_EVENT();
            pEvent.m_pImpact = pImpact;
            pEvent.m_nEventID = nID;
            m_UseList[nID].Add(pEvent);
        }

        return true;
    }

    public	bool Remove(Impact pImpact, SPELL_EVENT_ID nID)
	{
        if ((pImpact == null) || (nID >= SPELL_EVENT_ID.SPELL_EVENT_ID_NUMBER) || (nID <= SPELL_EVENT_ID.SPELL_EVENT_ID_INVALID))
        {
            return false;
        }
        foreach(var pRet in m_UseList[nID])
        {
            if (pRet.m_pImpact == pImpact)
	        {
		        m_UseList[nID].Remove(pRet);
                break;
	        }
        }
        return true;
	}

    public bool Recycle(SPELL_EVENT pCache)
    {
        if (pCache == null)
            return false;

        pCache.CleanUp();
        m_FreeCache.Insert(0,pCache);
        return true;
    }

    public void CleanUp()
	{
		for (int index=0; index < (int)SPELL_EVENT_ID.SPELL_EVENT_ID_NUMBER; ++index)
		{
            SPELL_EVENT_ID i = (SPELL_EVENT_ID)index;
			while (m_UseList[i].Count > 0)
			{
                List<SPELL_EVENT> it = new List<SPELL_EVENT>();
                it = m_UseList[i];
                SPELL_EVENT p = new SPELL_EVENT();
                p = it[0];
                m_UseList[i].RemoveAt(0);
				Recycle(p);
			}
		}
	}
}
