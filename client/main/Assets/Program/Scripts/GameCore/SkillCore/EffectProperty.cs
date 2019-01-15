using UnityEngine;
using System.Collections;

public class EffectProperty 
{
    /// <summary>
    /// 实例化对象
    /// </summary>
    private GameObject      m_EffectObject;
    /// <summary>
    /// 目标位置
    /// </summary>
    private Vector3         m_TargetLocation;
    /// <summary>
    /// 原始位置
    /// </summary>
    private Vector3         m_InitialLocation;
    /// <summary>
    /// 生命时间
    /// </summary>
    private float           m_LiftTime;
    /// <summary>
    /// 是否激活
    /// </summary>
    private bool            m_bActivation;
    /// <summary>
    /// 发起人              [3/12/2015 Zmy]
    /// </summary>
    private ObjectCreature  m_pCasterObj;

    private string          m_EffectName;

    public GameObject       GetGameObject()
    {
        return m_EffectObject;
    }
    public virtual void             SetGameObject(GameObject obj)
    {
        m_EffectObject = obj;
        Light _info = obj.GetComponent<Light>();
        if (_info != null)
        {
            m_LiftTime = _info.range;
        }
    }
    public bool             GetIsActivation()
    {
        return m_bActivation;
    }
    public void             SetActivation(bool bIs)
    {
        m_bActivation = bIs;
        m_EffectObject.SetActive(bIs);
    }

    public float            GetLiftTime()
    {
        return m_LiftTime;
    }
    public void             SetLiftTime(float time)
    {
        m_LiftTime = time;
    }

    public string           GetEffectName()
    {
        return m_EffectName;
    }
    public void             SetEffectName(string name)
    {
        m_EffectName = name;
    }

    public Vector3          GetInitialLocation()
    {
        return m_InitialLocation;
    }
    public void             SetInitialLocation(Vector3 pos)
    {
        m_InitialLocation = new Vector3(pos.x,pos.y,pos.z);

        if (m_EffectObject != null)
        {
            m_EffectObject.transform.position = m_InitialLocation;
        }
    }

    public Vector3          GetTargetLocation()
    {
        return m_TargetLocation;
    }
    public void             SetTargetLocation(Vector3 pos)
    {
        m_TargetLocation = new Vector3(pos.x, pos.y, pos.z);
    }
    public void             SetCasterObj(ObjectCreature obj)
    {
        m_pCasterObj = obj;
    }
    public ObjectCreature   GetCasterObj()
    {
        return m_pCasterObj;
    }
    public virtual void OnUpdateState()
    {

    }
}
