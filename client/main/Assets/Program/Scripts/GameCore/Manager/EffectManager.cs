/********************************************************************
	created:	2015/03/12
	created:	12:3:2015   11:27
	filename: 	d:\dream heros\assets\program\scripts\gamecore\manager\effectmanager.cs
	file path:	d:\dream heros\assets\program\scripts\gamecore\manager
	file base:	effectmanager
	file ext:	cs
	author:		Zmy
	
	purpose:	特效管理器，缓存当前场景内实例化的所有特效，通过激活对象启动逻辑
 *              当前使用到的特效分两大类：
 *              一类静态特效，主要负责显示在人物绑定点或指定位置，通过特效身上的Light组件约定生存周期，或通过接口指定生存周期
 *              二类弹道特效，主要用于子弹类的技能特效，通过对起点到目标点距离和配置表的速度定义，做插值计算位移，到达目标点后消失并回调函数
 *              
 *              可根据需要扩展其他类型的特效表现
*********************************************************************/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameSceneEditor;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using DreamFaction.GameAudio;
public class EffectManager : BaseControler
{
    private static EffectManager _inst;
    public static EffectManager GetInstance()
    {
        return _inst;
    }


    private List<EffectStatic>      m_EffectStaticList  = new List<EffectStatic>();
    private List<EffectSpell>       m_EffectSpellList   = new List<EffectSpell>();
    private List<EffectLink>        m_EffectLinkList    = new List<EffectLink>();
    // ============================= 继承接口 =================================
    protected override void InitData()
    {
        if (_inst == null)
        {
            _inst = this;
        }
        else
        {
            GameObject.Destroy(this);
        }
    }

    protected override void UpdateState()
    {
        for (int i = 0; i < m_EffectStaticList.Count; ++i )
        {
            if (m_EffectStaticList[i].GetGameObject() == null)
            {
                m_EffectStaticList.RemoveAt(i);
                continue;
            }
            if (m_EffectStaticList[i].GetIsActivation())
            {
                m_EffectStaticList[i].OnUpdateState();
            }
        }

        for (int i = 0; i < m_EffectSpellList.Count; ++i)
        {
            if (m_EffectSpellList[i].GetGameObject() == null)
            {
                m_EffectSpellList.RemoveAt(i);
                continue;
            }
            if (m_EffectSpellList[i].GetIsActivation())
            {
                m_EffectSpellList[i].OnUpdateState();
            }
        }
        for(int i=0;i<m_EffectLinkList.Count;++i)
        {
            if (m_EffectLinkList[i].GetIsActivation())
            {
                m_EffectLinkList[i].OnUpdateState();
            }
        }
    }
    // ============================= 公共接口 =================================

    /// <summary>
    /// 根据名称索引一个空闲的静态effect对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public EffectStatic GetFreeEffectStatic(string name)
    {
        for (int i = 0; i < m_EffectStaticList.Count; ++i )
        {
            if (m_EffectStaticList[i].GetGameObject() != null && m_EffectStaticList[i].GetEffectName().Equals(name) && m_EffectStaticList[i].GetIsActivation()== false)
            {
                return m_EffectStaticList[i];
            }
        }
        return null;
    }
    public EffectLink GetFreeEffectLink(string name)
    {
        for (int i = 0; i < m_EffectLinkList.Count; ++i)
        {
            if (m_EffectLinkList[i].GetGameObject() != null && m_EffectLinkList[i].GetEffectName().Equals(name) && m_EffectLinkList[i].GetIsActivation() == false)
            {
                return m_EffectLinkList[i];
            }
        }
        return null;
    }
    public EffectSpell GetFreeEffectSpell(string name)
    {
        for (int i = 0; i < m_EffectSpellList.Count; ++i)
        {
            if (m_EffectSpellList[i].GetGameObject() != null && m_EffectSpellList[i].GetEffectName().Equals(name) && m_EffectSpellList[i].GetIsActivation() == false)
            {
                return m_EffectSpellList[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 实例化链接特效
    /// </summary>
    /// <param name="effectName">要实例化的特效名称</param>
    public void InstanceEffect_Link(string effectName, ObjectCreature pCaster, Vector3 pos,List<ObjectCreature> info,float lifetime = 0f)
    {
        EffectLink _effect = GetFreeEffectLink(effectName);
        if (_effect != null && _effect.GetGameObject() != null)
        {
            GameObject obj = _effect.GetGameObject();
            obj.transform.position = pos;
            _effect.SetCasterObj(pCaster);
            _effect.SetObjList(info);
            _effect.ResetTime();
            _effect.SetActivation(true);
        }
        else
        {
            GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(effectName);
            if (_AssetRes == null)
                return;
            EffectLink _newEffect = new EffectLink();

            GameObject _obj = Instantiate(_AssetRes, pos, Quaternion.identity) as GameObject;
            if (pCaster is ObjectMonster)
            {
                float _scale = ((ObjectMonster)pCaster).GetMonsterRow().getMonsterEnlarge();
                _obj.transform.localScale = new UnityEngine.Vector3(_scale, _scale, _scale);
            }
            GameUtils.AttachParticleCS(_obj.transform);
            _newEffect.SetGameObject(_obj);
            _newEffect.SetCasterObj(pCaster);
            _newEffect.SetEffectName(effectName);
            _newEffect.SetObjList(info);
            if (lifetime > 0f)
            {
                _newEffect.SetLiftTime(lifetime);
            }
            m_EffectLinkList.Add(_newEffect);
        }
    }
    /// <summary>
    /// 实例化一个静态effect
    /// </summary>
    /// <param name="effectName">要实例化的特效名称</param>
    /// <param name="parent">实例化特效依附的父类挂点</param>
    /// <param name="lifetime">是否重置指定的生存时间，默认为特效对象上的Light组件获得</param>
    /// <param name="isAttach">是否需要依附，默认不依附任何挂点</param>
    public void InstanceEffect_Static(string effectName, ObjectCreature pCaster,Transform parent, float lifetime = 0f, bool isAttach = false)
    {
        EffectStatic _effect = GetFreeEffectStatic(effectName);
        if (_effect != null && _effect.GetGameObject() != null)
        {
            GameObject obj = _effect.GetGameObject();
            obj.transform.position = parent.position;
            obj.transform.rotation = parent.transform.rotation;
            _effect.SetCasterObj(pCaster);
            if (isAttach)
                obj.transform.parent = parent;
            _effect.ResetTime();
            _effect.SetActivation(true);
        }
        else
        {
            GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(effectName);
            if (_AssetRes == null)
            {
                LogManager.LogError("!!!!Error:InstanceEffect_Static() effectRes is null！The effectName:" +effectName + "GameObject is:" + pCaster.GetGameObject() + "effect location:" + parent.name);
                return;
            }

            EffectStatic _newEffect = new EffectStatic();

            GameObject _obj = Instantiate(_AssetRes, parent.position, parent.transform.rotation) as GameObject;
            if (pCaster is ObjectMonster)
            {
                float _scale = ((ObjectMonster)pCaster).GetMonsterRow().getMonsterEnlarge();
                _obj.transform.localScale = new UnityEngine.Vector3(_scale, _scale, _scale);
            }
            GameUtils.AttachParticleCS(_obj.transform);
            if (isAttach)
            {
                _obj.transform.parent = parent;
            }
            _newEffect.SetGameObject(_obj);
            _newEffect.SetCasterObj(pCaster);
            _newEffect.SetEffectName(effectName);
            //if (lifetime > 0f)
            //{
            //    _newEffect.SetLiftTime(lifetime);
            //}
            
            m_EffectStaticList.Add(_newEffect);
        }
    }

    /// <summary>
    /// 在指定坐标实例化一个存在指定生存时间的静态特效
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="parent">坐标</param>
    /// <param name="lifetime">生存时间</param>
    /// <param name="_SpellInfo"></param>
    public void InstanceEffect_Static(string effectName, ObjectCreature pCaster, Vector3 parent, float lifetime, SpellInfo _SpellInfo)
    {
        EffectStatic _effect = GetFreeEffectStatic(effectName);
        if (_effect != null && _effect.GetGameObject() != null)
        {
            GameObject obj = _effect.GetGameObject();
            obj.transform.position = parent;
            _effect.SetCasterObj(pCaster);
            _effect.ResetTime();
            _effect.SetActivation(true);
        }
        else
        {
            GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(effectName);
            if (_AssetRes == null)
                return;

            EffectStatic _newEffect = new EffectStatic();

            GameObject _obj = Instantiate(_AssetRes, parent,Quaternion.identity) as GameObject;
            if (pCaster is ObjectMonster)
            {
                float _scale = ((ObjectMonster)pCaster).GetMonsterRow().getMonsterEnlarge();
                _obj.transform.localScale = new UnityEngine.Vector3(_scale, _scale, _scale);
            }
            GameUtils.AttachParticleCS(_obj.transform);
            _newEffect.SetGameObject(_obj);
            _newEffect.SetLiftTime(lifetime);
            _newEffect.SetCasterObj(pCaster);
            _newEffect.SetEffectName(effectName);
            m_EffectStaticList.Add(_newEffect);
        }
        //技能震动
        FightEditorContrler.GetInstantiate().SkillShake(_SpellInfo.GetSpellRow().getVibrationScreen(), EM_SPELL_SHAKE_TYPE.EM_SPELL_SHAKE_TYPE_RELEASE);
    }
    /// <summary>
    /// 实例化弹道特效
    /// </summary>
    /// <param name="effectName">特效名称</param>
    /// <param name="MasterObj">发起者对象</param>
    /// <param name="TargetObj">目标对象</param>
    /// <param name="_SpellInfo">子弹的技能信息</param>
    public void InstanceEffect_Bullet(string effectName, ObjectCreature MasterObj,  ObjectCreature TargetObj, SpellInfo _SpellInfo,EffectHit_Callback HitHandle = null)
    {
        //子弹释放中音效
        AudioControler.Inst.PlaySound(_SpellInfo.GetSpellRow().getBulletsFiredSound());
        //技能弹道的音效
        AudioControler.Inst.PlaySound(_SpellInfo.GetSpellRow().getBallIsticSound());

        EffectSpell _effect = GetFreeEffectSpell(effectName);
        if (_effect != null && _effect.GetGameObject() != null)
        {
            GameObject obj = _effect.GetGameObject();
            _effect.ResetTime();
            _effect.InitBornPoint(MasterObj);
            _effect.InitTargetPoint(TargetObj);
            _effect.SetHitEffectHandle(HitHandle);
            _effect.SetGameObject(obj);
        }
        else
        {
            GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(effectName);
            if (_AssetRes == null)
                return;

            EffectSpell _newEffect = new EffectSpell(_SpellInfo);
            _newEffect.InitBornPoint(MasterObj);
            _newEffect.InitTargetPoint(TargetObj);

            GameObject _obj = Instantiate(_AssetRes, _newEffect.GetInitialLocation(), Quaternion.identity) as GameObject;
            if (MasterObj is ObjectMonster)
            {
                float _scale = ((ObjectMonster)MasterObj).GetMonsterRow().getMonsterEnlarge();
                _obj.transform.localScale = new UnityEngine.Vector3(_scale, _scale, _scale);
            }
            GameUtils.AttachParticleCS(_obj.transform);
            _newEffect.SetGameObject(_obj);
            _newEffect.SetEffectName(effectName);
            _newEffect.SetHitEffectHandle(HitHandle);

            m_EffectSpellList.Add(_newEffect);
        }
        //技能震动
        if(FightEditorContrler.GetInstantiate() != null)
            FightEditorContrler.GetInstantiate().SkillShake(_SpellInfo.GetSpellRow().getVibrationScreen(), EM_SPELL_SHAKE_TYPE.EM_SPELL_SHAKE_TYPE_RELEASE);
    }

    /// <summary>
    /// 指定一个静态特效立即失效。PS：必须通过持有人与特效名称查找
    /// </summary>
    /// <param name="pCaster"></param>
    /// <param name="_effectName"></param>
    public void DisableStaticEffect(ObjectCreature pCaster,string _effectName)
    {
        int nCount = m_EffectStaticList.Count;
        for (int i = 0; i < nCount; i++ )
        {
            if (m_EffectStaticList[i].GetIsActivation() && m_EffectStaticList[i].GetEffectName().Equals(_effectName))
            {
                if (m_EffectStaticList[i].GetCasterObj().GetGuid().Equals(pCaster.GetGuid()))
                {
                    m_EffectStaticList[i].SetActivation(false);
                }
            }
        }
    }

    /// <summary>
    /// 死亡对象的特效从管理器中删除
    /// </summary>
    /// <param name="pDieObj"></param>
    public void OnRemoveDeadObjOfEffect(ObjectCreature pDieObj)
    {
        int nStaticCount = m_EffectStaticList.Count;
        for (int i = nStaticCount - 1; i >= 0; i--)
        {
            if (m_EffectStaticList[i].GetCasterObj() != null && m_EffectStaticList[i].GetCasterObj().GetGuid().Equals(pDieObj.GetGuid()))
            {
                GameObject obj = m_EffectStaticList[i].GetGameObject();
                Destroy(obj);
                obj = null;
                m_EffectStaticList.RemoveAt(i);
            }
        }

        int nSpellCount = m_EffectSpellList.Count;
        for (int i = nSpellCount - 1; i >= 0; i--)
        {
            if (m_EffectSpellList[i].GetCasterObj() != null && m_EffectSpellList[i].GetCasterObj().GetGuid().Equals(pDieObj.GetGuid()))
            {
                GameObject obj = m_EffectSpellList[i].GetGameObject();
                Destroy(obj);
                obj = null;
                m_EffectSpellList.RemoveAt(i);
            }
        }
    }




}
