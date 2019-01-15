using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 链接类特效
/// </summary>
public class EffectLink : EffectProperty
{
    private List<ObjectCreature>    m_Objlist;
    private float time = 0f;
    private int LinkCount = 0;
    private LineRenderer m_LineRenderer;
    public EffectLink()
    {
       
    }
    
    public override void OnUpdateState()
    {
        if (GetIsActivation() == false)
            return;
        time += Time.deltaTime;
        if (time > GetLiftTime())
        {
            time = 0f;
            m_LineRenderer.SetVertexCount(0);
            SetActivation(false);
        }
        else
        {
            LinkUpdate();
        }
    }
    public override void SetGameObject(GameObject obj)
    {
        base.SetGameObject(obj);

        SetActivation(true);
    }
    public void ResetTime()
    {
        time = 0f;
    }
    public void SetObjList(List<ObjectCreature> info)
    {
        m_Objlist = info;
        if(m_LineRenderer==null)
           m_LineRenderer = GetGameObject().GetComponent<LineRenderer>();
        m_LineRenderer.SetVertexCount(m_Objlist.Count);
    }
    private void LinkUpdate()
    {
        for(int i=0;i<m_Objlist.Count;++i)
        {
            if (m_Objlist[i].IsAlive() && m_Objlist[i].GetGameObject()!=null)
            {
                m_LineRenderer.SetVertexCount(LinkCount + 1);
                GetGameObject().GetComponent<LineRenderer>().SetPosition(LinkCount, m_Objlist[i].GetGameObject().transform.position);
                LinkCount++;
            }
        }
        LinkCount = 0;
    }
}
