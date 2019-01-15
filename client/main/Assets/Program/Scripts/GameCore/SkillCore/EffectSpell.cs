using UnityEngine;
using System.Collections;
using DreamFaction.LogSystem;
/// <summary>
/// [2/4/2015 Zmy]
/// </summary>

public delegate void EffectHit_Callback(ObjectCreature pTarget);
public class EffectSpell : EffectProperty 
{
    private SpellInfo               m_SpellInfo;
    // 目标obj [2/5/2015 Zmy]
    private GameObject              TargetObj;
    private ObjectCreature          TargetCreature;

    bool                            m_IsHitCallBack;    //是否触发命中后的回调
    private EffectHit_Callback      EffectHitHandle;    //命中回調函數
    Vector3 targetPos = new Vector3();

    float time = 0f;
    float moveSpeed = 0;
    public EffectSpell(SpellInfo info)
    {
        m_SpellInfo = info;
    }
 
    public override void OnUpdateState()
    {
        if (GetIsActivation() == false)
            return;

         time += Time.deltaTime;
        if (time < GetLiftTime() && TargetObj != null)
        {
            moveSpeed = (GetLiftTime() - time) / GetLiftTime();
            //实时目标点，跟随位置变动轨迹线实时变动 [11/5/2015 Zmy]
//             GetGameObject().transform.position = new Vector3(Mathf.Lerp(TargetObj.transform.position.x, GetInitialLocation().x, moveSpeed),
//                                                              Mathf.Lerp(TargetObj.transform.position.y, GetInitialLocation().y, moveSpeed),
//                                                              Mathf.Lerp(TargetObj.transform.position.z, GetInitialLocation().z, moveSpeed));

            //固定目标点，不会变动的轨迹线 [11/5/2015 Zmy]
            GetGameObject().transform.position = new Vector3(Mathf.Lerp(targetPos.x, GetInitialLocation().x, moveSpeed),
                                                             Mathf.Lerp(targetPos.y, GetInitialLocation().y, moveSpeed),
                                                             Mathf.Lerp(targetPos.z, GetInitialLocation().z, moveSpeed));


            GetGameObject().transform.LookAt(TargetObj.transform);
            //Quaternion ratation = Quaternion.LookRotation(GetTargetLocation() - GetInitialLocation());
            //GetGameObject().transform.rotation = Quaternion.Slerp(GetGameObject().transform.rotation, ratation, 20 * Time.deltaTime);
        }
        else
        {
            CallBack_StartSpell();
        }
    }

    private void CallBack_StartSpell()
    {
        time = 0f;
        SetActivation(false);
        if (m_SpellInfo == null)
            return;
        
        if (m_IsHitCallBack && EffectHitHandle != null)
        {
            EffectHitHandle(TargetCreature);
            m_IsHitCallBack = false;
            EffectHitHandle = null;
            return;
        }
        SkillTemplate pRow = m_SpellInfo.GetSpellRow();
        // 编号为0 回调到普攻的技能逻辑 [3/5/2015 Zmy]
        if (pRow.getSkillNo() == 0)
        {
            GetCasterObj().OnCommonSkillActiveOnce();
        }
        else
        {
            //子弹命中后回调技能逻辑
            GetCasterObj().OnSpecialSkillActiveOnce();
        }
        
    }
    public void InitBornPoint(ObjectCreature obj)
    {
        Transform _tran = GetTransform(obj.GetGameObject().transform, m_SpellInfo.GetSpellRow().getBullEffectPoint()); 
        if (_tran == null)
        {
            LogManager.LogError("!!!Error: [InitBornPoint]弹道特效" + obj.GetGameObject() + "目标发射点位置获取错误:" + m_SpellInfo.GetSpellRow().getBullEffectPoint() + "  技能ID：" + m_SpellInfo.GetSpellRow().getId());
            return;
        }
        SetCasterObj(obj);
        SetInitialLocation(_tran.position);
    }
    public void InitTargetPoint(ObjectCreature obj)
    {
        if (obj==null||obj.GetGameObject() == null)
            return;
        Transform _tran = GetTransform(obj.GetGameObject().transform, m_SpellInfo.GetSpellRow().getEffectHitPoint());
        if (_tran == null)
        {
            LogManager.LogError("!!!Error: [InitTargetPoint]弹道特效" + obj.GetGameObject() + "目标命中点位置获取错误:" + m_SpellInfo.GetSpellRow().getEffectHitPoint() + "  技能ID：" + m_SpellInfo.GetSpellRow().getId());
            return;
        }

        SetTargetLocation(_tran.position);
         
        TargetObj = _tran.gameObject;

        targetPos = new Vector3(TargetObj.transform.position.x, TargetObj.transform.position.y, TargetObj.transform.position.z);
        TargetCreature = obj;
    }

    public override void SetGameObject(GameObject obj)
    {
        base.SetGameObject(obj);

        float distance = Vector3.Distance(GetInitialLocation(),GetTargetLocation());
        float time = distance / m_SpellInfo.GetSpellRow().getBallIsticSpeed();

        SetLiftTime(time);
        SetActivation(true);
       
    }

    public void ResetTime()
    {
        time = 0f;
    }
    private Transform GetTransform(Transform check, string naeme)
    {
        foreach (Transform t in check.GetComponentsInChildren<Transform>())
        {
            if (t.name == naeme)
            {
                return t;
            }
        }
        return null;
    }

    public void SetHitEffectHandle(EffectHit_Callback handle)
    {
        EffectHitHandle = handle;
        m_IsHitCallBack = handle == null ? false : true;
    }
}
