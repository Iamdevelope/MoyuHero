using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;
/// <summary>
/// 动作控制器，每个object身上动态增加此脚本，通过Animation组件配置所有用到的动作名称，并封装所有关于动作播放的接口逻辑  [1/28/2015 Zmy]
/// </summary>
public class AnimationControl : BaseControler 
{
    private Animation _anim;
    private AnimationEventControler _event;

    private string lastAnimatimName;

    private string walk_anim;                            // 走 [1/30/2015 Zmy]
    private string run_anim;                             // 跑 [1/30/2015 Zmy]
    private string jump_anim;                            // 跳 [1/30/2015 Zmy]
    private string pose_anim;                            // 休闲动作 [1/30/2015 Zmy]
    private string alarm_anim;                           // 惊恐 [1/30/2015 Zmy]
    private string dizzy_anim;                           // 眩晕 [1/30/2015 Zmy]
    private string pick_anim;                            // 拾取 [1/30/2015 Zmy]
    private string Nidle1_anim;                          // 普通待机 [1/30/2015 Zmy]
    private string Nidle2_anim;                          // 休闲待机 [1/30/2015 Zmy]
    private string summon_anim;                          // 召唤动作 [1/30/2015 Zmy]
    private string Fidle1_anim;                          // 战斗待机 [1/30/2015 Zmy]
    private string Fidle2_anim;                          // 切状态用战斗待机 [1/30/2015 Zmy]
    private string die_anim;                             // 死亡 [1/30/2015 Zmy]
    private string hurt1_anim;                           // 受击 [1/30/2015 Zmy]
    private string hurt2_anim;                           // 位移受击 [1/30/2015 Zmy]
    private string[] attack_anim = new string[3];        // 普通攻击1 2 3 [1/30/2015 Zmy]
    private string[] skill_anim = new string[2];         // 技能1 2 [1/30/2015 Zmy]
    private float fadeTime = 0.3f;                       // 融合时间  [1/30/2015 Zmy]
    private float Attack2 = 0.8f;                        //普通攻击2随机最大值
    private float Attack3 = 1.2f;                        //普通攻击3随机最大值
    private float random = 0;                            //动作组随机数
    private string Attack_anim;                          //普通攻击实际播放动作
    private float FidLengh;                              //普通攻击时战斗待机的实际播放时间

    private float SurplusAttackLength;                   //剩余普通攻击时间 [9/28/2015 Zmy]
    // ============================= 继承接口 =================================

    private AnimEventData m_EventData;
    private ArtresourceTemplate m_ModelActionRow;
    float m_CurAnimSpeed = 1.0f;
    // 第一步初始化数据 [1/19/2015 Zmy]
    protected override void InitData()
    {
        _anim = gameObject.GetComponent<Animation>();
        _event = gameObject.AddComponent<AnimationEventControler>();

        walk_anim   = "Walk1";                      
        run_anim    = "Run1";
        jump_anim   = "Jump1";
        pose_anim   = "Pose1";
        alarm_anim  = "Alarm1";
        dizzy_anim  = "Dizzy1";
        pick_anim   = "Pick1";
        Nidle1_anim = "Nidle1";
        Nidle2_anim = "Nidle2";
        summon_anim = "Summon1";
        Fidle1_anim = "Fidle1";
        Fidle2_anim = "Fidle0";
        die_anim    = "Die1";
        hurt1_anim  = "Hurt1";
        hurt2_anim  = "Hurt2";
        for (int i = 1; i <= 3; i++)
        {
            attack_anim[i - 1] = "Attack" + i;
        }
        for (int i = 1; i <= 2; i++)
        {
            skill_anim[i - 1] = "Skill" + i;
        }
        lastAnimatimName = walk_anim;
    }
    protected override void UpdateState()
    {
        if (GetOwnerType() == 1)
        {
           ObjectHero obj = (ObjectHero)SceneObjectManager.GetInstance().GetSceneObjectByGameObject(this.gameObject);
            if (obj != null)
            {
                CurState = obj.GetActionState();
                if (obj.GetCurLockTarget() != null)
                {
                    CurLockTarget = obj.GetCurLockTarget().GetGameObject();
                }
                if (obj.GetSkillLockTarget() != null)
                {
                    CurLockSkillTarget = obj.GetSkillLockTarget().GetGameObject();
                }
                
            }
           
        }
        else
        {
           ObjectMonster obj = (ObjectMonster)SceneObjectManager.GetInstance().GetSceneObjectByGameObject(this.gameObject);
            if (obj != null)
            {
                CurState = obj.GetActionState();
            }
           
        }

        if (m_EventData != null && _anim[lastAnimatimName] != null)
            m_EventData.OnUpdateEvent(_event,_anim[lastAnimatimName]);
    }

    public void InitEventData(int nID)
    {
        m_EventData = new AnimEventData(nID);
        if (DataTemplate.GetInstance().m_ArtresourceTable.tableContainsKey(nID))
        {
            m_ModelActionRow = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(nID);
        }
        
    }

    public ObjectCreature.ObjectActionState CurState;
    public GameObject CurLockTarget;
    public GameObject CurLockSkillTarget;

    public void SetOwnerType(int ntype)
    {
        _event.SetOwnerType(ntype);
    }
    public int  GetOwnerType()
    {
        return _event.GetOwnerType();
    }

    public string LastAnim
    {
        get
        {
            return lastAnimatimName;
        }
    }
    public bool Loop
    {
        get
        {
            if (_anim.wrapMode == WrapMode.Loop)
                return true;
            return false;
        }
        set
        {
            if (value)
            {
                _anim.wrapMode = WrapMode.Loop;
            }
            else
            {
                _anim.wrapMode = WrapMode.Default;
            }
        }
    }

    public bool IsEndFrame
    {
        get
        {
            return !_anim.isPlaying;
        }
    }

    public float AnimTime
    {
        get
        {
            if (_anim[lastAnimatimName] == null)
            {
                return 99f;
            }

            return _anim[lastAnimatimName].time;
        }
    }

    // ============================= 公共接口 =================================

    public bool action(string actionName,bool loop = false, bool isTag = true)
    {
        if (_anim == null || _anim[actionName] == null )
        {
          //  LogManager.Log("！！！！！！！！Error：【"+ gameObject +"】对象播放一个不存在的动作名称:" + actionName);
            return false;
        }
        if (m_ModelActionRow != null && isTag == false)
        {
            _anim[actionName].speed = m_CurAnimSpeed;
        }
        

        
        if (lastAnimatimName.Equals(actionName) == false)
        {
            lastAnimatimName = actionName;
            
            //m_EventData.HandleRemindEvents(_event);
            m_EventData.InitCurAnimNameForEventList(actionName);
            
            if (!Mathf.Approximately(_anim[actionName].time, 0f))
            {
                _anim[actionName].time = 0f;
                //_anim.Sample();
            }
        }

        Loop = loop;
        //_anim.Play(actionName);
        _anim.CrossFade(actionName, 0.3f);


        return true;
    }
    public void Anim_Walk()
    {
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getWalk1();
        }
        action(walk_anim,true,false);
    }

    public void Anim_Run()
    {
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getRun1();
        }
        action(run_anim, true,false);
    }

    public void Anim_Jump()
    {
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getJump1();
        }
        action(jump_anim, false, false);
    }

    public void Anim_Pose()
    {
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getPose1();
        }
        action(pose_anim, false, false);
    }

    public void Anim_Alarm()
    {
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getAlarm1();
        }
        action(alarm_anim,true,false);
    }

    public void Anim_Dizzy()
    {
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getDizzy1();
        }
        action(dizzy_anim,true,false);
    }

    public void Anim_Pick()
    {
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getPick1();
        }
        action(pick_anim, false, false);
    }

    public void Anim_Summon()
    {
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getSummon1();
        }
        action(summon_anim, false, false);
    }

    public void Anim_Hurt(bool  bDefault = true)
    {
        if (bDefault)
        {
            if (m_ModelActionRow != null)
            {
                m_CurAnimSpeed = m_ModelActionRow.getHurt1();
            }
            action(hurt1_anim, false, false);
        }
        else
        {
            if (m_ModelActionRow != null)
            {
                m_CurAnimSpeed = m_ModelActionRow.getHurt2();
            }
            action(hurt2_anim, false, false);
        }
    }

    public void Anim_Die(bool removeCollision = true)
    {
        if (IsEndFrame == false)
        {
            _anim.Stop();
        }
        if (m_ModelActionRow != null)
        {
            m_CurAnimSpeed = m_ModelActionRow.getDie1();
        }
        action(die_anim,false,false);

        if (removeCollision)
        {
            DisCollision();
        }
    }

    public void DisCollision()
    {
        //死去的物体使碰撞失效;
        Collider c = gameObject.GetComponent<Collider>();
        if (c != null)
        {
            c.enabled = false;
        }

        NavMeshAgent nma = gameObject.GetComponent<NavMeshAgent>();
        if (nma != null)
        {
            nma.enabled = false;
        }
    }

    public void Anim_Fidle(bool bDefault = true)
    {
        if (bDefault)
        {
            action(Fidle1_anim,true,false);//战斗待机不记录上一次动作，方便普通攻击做随机攻击 [1/30/2015 Zmy]
        } 
        else
        {
            action(Fidle2_anim);
        }
    }

    public void Anim_Nidle(bool bDefault = true)
    {
        if (bDefault)
        {
            if (m_ModelActionRow != null)
            {
                m_CurAnimSpeed = m_ModelActionRow.getNidle1();
            }
            action(Nidle1_anim,true,false);
        } 
        else
        {
            if (m_ModelActionRow != null)
            {
                m_CurAnimSpeed = m_ModelActionRow.getNidle2();
            }
            action(Nidle2_anim,false,false);
        }
    }
    private void RandomAttack(string[] actions)
    {
        Attack_anim = string.Empty;
        random = Random.Range(0.1f, 1);
        switch (actions.Length)
        {
            case 1:
                Attack_anim=attack_anim[0];
                break;
            case 2:
                if (random > Attack2)
                {
                    Attack_anim = attack_anim[1];
                    Attack2 = 0.8f;
                }
                else
                {
                    Attack_anim = attack_anim[0];
                    Attack2 -= 0.1f;
                }
                break;
            case 3:
                if (random >= Attack3)
                {
                    Attack_anim = attack_anim[2];
                    Attack3 = 1.2f;
                }
                else if (random >= Attack2 && random < Attack3)
                {
                    Attack_anim = attack_anim[1];
                    Attack2 = 0.8f;
                }
                else if (random < Attack2)
                {
                    Attack_anim = attack_anim[0];
                    Attack2 -= 0.1f;
                    Attack3 -= 0.1f;
                }
                break;
        }
        if(_anim == null || _anim[Attack_anim] == null)
        {
            LogManager.Log("！！！！！！！！Error：【" + gameObject + "】对象播放一个不存在的动作名称:" + Attack_anim);
            Attack_anim = attack_anim[0];
        }
    }
    public void SetNormalAttackSpeed(float speed)
    {
        GameConfig _config = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        float _time = 1 / Mathf.Max(_config.getAttackFrequencyT(), Mathf.Min(speed / _config.getAttackFrequencyS(), _config.getAttackFrequencyU()));//计算实际攻击间隔
        float _ActLengh = m_EventData.GetNormalAttackEndTime(Attack_anim) <= 0 ? _anim[Attack_anim].length : m_EventData.GetNormalAttackEndTime(Attack_anim);//普攻动作时间长度取值修正 [7/30/2015 Zmy]
        float _FidLengh = _anim["Fidle1"].length;
        if (_time > _ActLengh)
        {
            FidLengh = 0.1f;//_time - _ActLengh;
        }
        else
        {
            FidLengh = 0;
            float _actspeed = _ActLengh / _time;
            _anim[Attack_anim].speed = _actspeed;
        }

        SurplusAttackLength = m_EventData.GetSurplusNormalAttackLength(Attack_anim);
    }
    public void UpdateFidTime()
    {
        if(FidLengh<=0)
        {
            FidLengh = 0;
            ObjectCreature obj = SceneObjectManager.GetInstance().GetSceneObjectByGameObject(this.gameObject);
            obj.SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
        }
        else
        {
            FidLengh -= Time.deltaTime;
        }
    }

    public void UpdateSurplusNormalAttackTime()
    {
        if (SurplusAttackLength <= 0)
        {
            SurplusAttackLength = 0;
            ObjectCreature obj = SceneObjectManager.GetInstance().GetSceneObjectByGameObject(this.gameObject);
            obj.SetObjectActionState(ObjectCreature.ObjectActionState.AttackIdle);
        }
        else
        {
            SurplusAttackLength -= Time.deltaTime;
        }
    }
    public float GetFidLengh() { return FidLengh; }
    public void Anim_Attack()
    {
        ObjectCreature obj = SceneObjectManager.GetInstance().GetSceneObjectByGameObject(this.gameObject);
        if(obj is ObjectHero)
        {
            ObjectHero hero = obj as ObjectHero;
            string[] Actions = hero.GetSpellNormal().GetSpellRow().getAction();
            RandomAttack(Actions);
        }
        else
        {
            ObjectMonster monster = obj as ObjectMonster;
            string[] Actions = monster.GetSpellNormal().GetSpellRow().getAction();
            RandomAttack(Actions);
        }
        
    }
    public void Anim_StartAttack()
    {
        action(Attack_anim);
    }
    //一般技能(不包括引导技能)
    public void Anim_Skill(string[] AnimNames)
    {
        _anim.Stop();
        action(AnimNames[0]);
    }
    //引导技能专用
    public void Anim_GuidanceSkill(string AnimNames,bool loop=false)
    {
        action(AnimNames,loop);
    }

    public AnimationEventControler EventControl
    {
        get { return _event; }
    }
}


//  
/// <summary>
/// 封装角色动作事件数据类。负责根据不同条件获取指定人物下不同动作所包含的回调事件数据[2/12/2015 Zmy]
/// </summary>
public class AnimEventData
{
    //角色对象下的所有动作事件数据 [2/12/2015 Zmy]
    public Dictionary<int, AnimEventTemplate> data = new Dictionary<int, AnimEventTemplate>();

    //当前动作名称下的事件数据列表 [2/12/2015 Zmy]
    List<AnimEventTemplate> _CurAnimNameForEventList = new List<AnimEventTemplate>();
    public AnimEventData(int id)
    {
        for (int i = 0,index = 0; i < DataTemplate.GetInstance().m_AnimEventTable.getDataCount(); ++i)
        {
            AnimEventTemplate pData = (AnimEventTemplate)DataTemplate.GetInstance().m_AnimEventTable.getTableData(i);
            if (pData.m_playerID == id)
            {
                data.Add(index, pData);

                index++;
            }
        }
    }

    public float GetNormalAttackEndTime(string attackName)
    {
        for (int i = 0; i < data.Count; ++i)
        {
            AnimEventTemplate pData = data[i];
            if (pData.m_AnimName.Equals(attackName) && pData.m_FunctionName.Equals("NormalAttackEnd"))
            {
                return pData.m_HitTime;
            }
        }

        return 0f;
    }

    public float GetSurplusNormalAttackLength(string attackName)
    {
        float fEnd = 0f;
        float fStar = 0f;
        for (int i = 0; i < data.Count; ++i)
        {
            AnimEventTemplate pData = data[i];
            if (pData.m_AnimName.Equals(attackName) && (pData.m_FunctionName.Equals("NormalAttack_ModelNear") || pData.m_FunctionName.Equals("NormalAttack_ModelFar") || pData.m_FunctionName.Equals("NormalAttack_ParticleFar")))
            {
                fStar = pData.m_HitTime;
            }

            if (pData.m_AnimName.Equals(attackName) && pData.m_FunctionName.Equals("NormalAttackEnd"))
            {
                fEnd = pData.m_HitTime;
            }
        }
        return fEnd - fStar < 0f ? 0f : fEnd - fStar;
    }

    public void HandleRemindEvents(AnimationEventControler eventControler)
    {
        if (_CurAnimNameForEventList != null && _CurAnimNameForEventList.Count > 0)
        {
            _CurAnimNameForEventList.Sort(SortAsTimeLength);

            for (int i = 0; i < _CurAnimNameForEventList.Count; i++ )
            {
                eventControler.CallBack_EventFunction(_CurAnimNameForEventList[i].m_FunctionName, _CurAnimNameForEventList[i].m_Param);
            }
        }
    }

    public static int SortAsTimeLength(AnimEventTemplate aet1, AnimEventTemplate aet2)
    {
        return aet2.m_HitTime > aet1.m_HitTime ? 1 : -1;
    }

    public void InitCurAnimNameForEventList(string _anim)
    {

        _CurAnimNameForEventList.Clear();

        for (int i = 0; i < data.Count; ++i)
        {
            AnimEventTemplate pData = data[i];
            if (pData.m_AnimName.Equals(_anim))
            {
                //if (curAnim[_anim].time < pData.m_HitTime)
                {
                    _CurAnimNameForEventList.Add(pData);
                }
            }
        }
    }

    public void OnUpdateEvent(AnimationEventControler _eventControler, AnimationState _animState)
    {
        if (_CurAnimNameForEventList.Count <= 0)
            return;

        for (int i = 0; i < _CurAnimNameForEventList.Count; ++i )
        {
            if (!_animState.name.Equals(_CurAnimNameForEventList[i].m_AnimName))
                continue;
            
            if (_animState.time >= _CurAnimNameForEventList[i].m_HitTime || _animState.enabled == false)
            {
                _eventControler.CallBack_EventFunction(_CurAnimNameForEventList[i].m_FunctionName, _CurAnimNameForEventList[i].m_Param);

                if (_CurAnimNameForEventList.Count <= 0)
                    return;
               
                _CurAnimNameForEventList.RemoveAt(i);
                break;
            }
        }
    }
}