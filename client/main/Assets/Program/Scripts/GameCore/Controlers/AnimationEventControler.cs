using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
namespace DreamFaction.GameCore
{
    /// <summary>
    /// 动作回调控制器
    /// </summary>
    public class AnimationEventControler : BaseControler
    {
        //===========================找位置用====================================================
        public Transform Pre_Head_C_EffectPoint;   // 头部中心      
        public Transform Pre_Head_T_EffectPoint;   // 头部顶部      
        public Transform Pre_Hand_L_EffectPoint;   // 左手          
        public Transform Pre_Hand_R_EffectPoint;   // 右手          
        public Transform Pre_Foot_L_EffectPoint;   // 左脚          
        public Transform Pre_Foot_R_EffectPoint;   // 右脚          
        public Transform Pre_Chest_EffectPoint;  // 胸部          
        public Transform Pre_Bottom_EffectPoint;     // 底部          
        public Transform Pre_Weapon01_EffectPoint;   // 武器01   
        public Transform Pre_Weapon02_EffectPoint;   // 武器02   
        public Transform Pre_Normal_HurtPoint;       //普通受击点
        public Transform Pre_Special_HurtPoint;      //特殊受击点
        public Transform Pre_Footsteps_EffectPoint;  //双脚之间更随移动点
        public Transform MainBody;//主身体对象
        //===========================实例化物体===============================================
        GameObject Pref_Head_C_EffectPoint;   // 头部中心      
        GameObject Pref_Head_T_EffectPoint;   // 头部顶部      
        GameObject Pref_Hand_L_EffectPoint;   // 左手          
        GameObject Pref_Hand_R_EffectPoint;   // 右手          
        GameObject Pref_Foot_L_EffectPoint;   // 左脚          
        GameObject Pref_Foot_R_EffectPoint;   // 右脚          
        GameObject Pref_Chest_EffectPoint;  // 胸部          
        GameObject Pref_Bottom_EffectPoint;   // 底部          
        GameObject Pref_Weapon01_EffectPoint;   // 武器01
        GameObject Pref_Weapon02_EffectPoint;   // 武器02   
        GameObject Pref_Normal_HurtPoint;       //普通受击点
        GameObject Pref_Special_HurtPoint;      //特殊受击点
        GameObject Pref_Footsteps_EffectPoint;//双脚之间更随移动点
        private int m_OwnerType;       //标记拥有者类型。 英雄对象设置1，敌对设置0 [1/29/2015 Zmy]
        protected override void InitData()
        {
            Pre_Head_C_EffectPoint = GetTransform(this.transform, "Head_C_EffectPoint");
            Pre_Head_T_EffectPoint = GetTransform(this.transform, "Head_T_EffectPoint");
            Pre_Hand_L_EffectPoint = GetTransform(this.transform, "Hand_L_EffectPoint");
            Pre_Hand_R_EffectPoint = GetTransform(this.transform, "Hand_R_EffectPoint");
            Pre_Foot_L_EffectPoint = GetTransform(this.transform, "Foot_L_EffectPoint");
            Pre_Foot_R_EffectPoint = GetTransform(this.transform, "Foot_R_EffectPoint");
            Pre_Chest_EffectPoint = GetTransform(this.transform, "Chest_EffectPoint");
            Pre_Bottom_EffectPoint = GetTransform(this.transform, "Bottom_EffectPoint");
            Pre_Weapon01_EffectPoint = GetTransform(this.transform, "Weapon01_EffectPoint");
            Pre_Weapon02_EffectPoint = GetTransform(this.transform, "Weapon02_EffectPoint");
            Pre_Normal_HurtPoint = GetTransform(this.transform, "Normal_HurtPoint");
            Pre_Special_HurtPoint = GetTransform(this.transform, "Special_HurtPoint");
            Pre_Footsteps_EffectPoint = GetTransform(this.transform, "Footsteps_EffectPoint");
            MainBody = GetTransform(this.transform, "Body");
        }
        public void SetOwnerType(int ntype)
        {
            m_OwnerType = ntype;
        }
        public int GetOwnerType()
        {
            return m_OwnerType;
        }
        public Transform GetTransform(Transform check, string name)
        {
            foreach (Transform t in check.GetComponentsInChildren<Transform>())
            {
                if (t.name == name)
                {
                    return t;
                }
            }
            return null;
        }
        /// 动作回调事件，请求播放特效 头部中心
        public void Head_C_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Head_C_EffectPoint, Pre_Head_C_EffectPoint);
        }
        public void Head_C_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Head_C_EffectPoint, Pre_Head_C_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 头部顶部
        public void Head_T_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Head_T_EffectPoint, Pre_Head_T_EffectPoint);
        }
        public void Head_T_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Head_T_EffectPoint, Pre_Head_T_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 左手
        public void Hand_L_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Hand_L_EffectPoint, Pre_Hand_L_EffectPoint);
        }
        public void Hand_L_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Hand_L_EffectPoint, Pre_Hand_L_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 右手
        public void Hand_R_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Hand_R_EffectPoint, Pre_Hand_R_EffectPoint);
        }
        public void Hand_R_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Hand_R_EffectPoint, Pre_Hand_R_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 左脚
        public void Foot_L_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Foot_L_EffectPoint, Pre_Foot_L_EffectPoint);
        }
        public void Foot_L_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Foot_L_EffectPoint, Pre_Foot_L_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 右脚
        public void Foot_R_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Foot_R_EffectPoint, Pre_Foot_R_EffectPoint);
        }
        public void Foot_R_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Foot_R_EffectPoint, Pre_Foot_R_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 胸部
        public void Chest_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Chest_EffectPoint, Pre_Chest_EffectPoint);
        }
        public void Chest_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Chest_EffectPoint, Pre_Chest_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 底部
        public void Bottom_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Bottom_EffectPoint, Pre_Bottom_EffectPoint);
        }
        public void Bottom_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Bottom_EffectPoint, Pre_Bottom_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 武器01 
        public void Weapon01_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Weapon01_EffectPoint, Pre_Weapon01_EffectPoint);
        }
        public void Weapon01_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Weapon01_EffectPoint, Pre_Weapon01_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 武器02
        public void Weapon02_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Weapon02_EffectPoint, Pre_Weapon02_EffectPoint);
        }
        public void Weapon02_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Weapon02_EffectPoint, Pre_Weapon02_EffectPoint);
        }
        /// 动作回调事件，请求播放特效 普通受击点 
        public void Normal_HurtPoint(string effectName)
        {
            addEff(effectName, Pref_Normal_HurtPoint, Pre_Normal_HurtPoint);
        }
        public void Normal_HurtPoint2(string effectName)
        {
            addEff2(effectName, Pref_Normal_HurtPoint, Pre_Normal_HurtPoint);
        }
        /// 动作回调事件，请求播放特效 特殊受击点 
        public void Special_HurtPoint(string effectName)
        {
            addEff(effectName, Pref_Special_HurtPoint, Pre_Special_HurtPoint);
        }
        public void Special_HurtPoint2(string effectName)
        {
            addEff2(effectName, Pref_Special_HurtPoint, Pre_Special_HurtPoint);
        }
        /// 动作回调事件，请求播放特效 双脚之间更随移动点 
        public void Footsteps_EffectPoint(string effectName)
        {
            addEff(effectName, Pref_Footsteps_EffectPoint, Pre_Footsteps_EffectPoint);
        }
        public void Footsteps_EffectPoint2(string effectName)
        {
            addEff2(effectName, Pref_Footsteps_EffectPoint, Pre_Footsteps_EffectPoint);
        }
        //=======================================================================================================
        //添加特效为子物体
        private void addEff(string effectName, GameObject Clone, Transform parent)
        {
            //Clone = Instantiate(Resources.Load(effectName), parent.position, parent.rotation) as GameObject;
            //Clone.transform.parent = parent;
            ObjectCreature pCaster = SceneObjectManager.GetInstance().GetSceneObjectByGameObject(this.gameObject);
            EffectManager.GetInstance().InstanceEffect_Static(effectName, pCaster,parent, 0f,true);
        }
        //添加特效非子物体
        private void addEff2(string effectName, GameObject Clone, Transform parent)
        {
            ObjectCreature pCaster = SceneObjectManager.GetInstance().GetSceneObjectByGameObject(this.gameObject);
            EffectManager.GetInstance().InstanceEffect_Static(effectName, pCaster,parent,0f,false);
        }
        //######################################################普通攻击动作接口###############################################
        //模型类近战普通攻击弹道回调
        private void NormalAttack_ModelNear()
        {
            SceneObjectManager.GetInstance().NormalAttack_CallBack(gameObject, m_OwnerType);
        }
        //模型类远程普通攻击弹道回调
        private void NormalAttack_ModelFar()
        {
            SceneObjectManager.GetInstance().NormalAttack_CallBack(gameObject, m_OwnerType);
        }
        //粒子类近战普通攻击弹道回调
        private void NormalAttack_ParticleNear()
        {

        }
        //粒子类远程普通攻击弹道回调
        private void NormalAttack_ParticleFar()
        {
            SceneObjectManager.GetInstance().NormalAttack_CallBack(gameObject, m_OwnerType);
        }
        //普通攻击结束回调
        private void NormalAttackEnd()
        {
            //切换到普攻待机动作 [1/29/2015 Zmy]
            SceneObjectManager.GetInstance().OnChangeActionState(gameObject, m_OwnerType, ObjectCreature.ObjectActionState.AttackIdle);
        }
        //######################################################技能攻击动作接口###############################################
        //飞行技能攻击回调
        // 创建技能弹道特效 [3/4/2015 Zmy]
        private void SkillAttack_Fly(string nCurHit)
        {
            if (nCurHit.Contains(".0"))
            {
                string _tmp = nCurHit.Substring(0, nCurHit.LastIndexOf(".0"));
                if (_tmp.Equals("-1") || string.IsNullOrEmpty(_tmp))
                {
                    SceneObjectManager.GetInstance().SkillAttack_CallBack(gameObject, m_OwnerType, 1, 1);
                }
                else
                {
                    SceneObjectManager.GetInstance().SkillAttack_CallBack(gameObject, m_OwnerType, 1, int.Parse(_tmp));
                }
            }
            else
            {
                if (nCurHit.Equals("-1") || string.IsNullOrEmpty(nCurHit))
                {
                    SceneObjectManager.GetInstance().SkillAttack_CallBack(gameObject, m_OwnerType, 1, 1);
                }
                else
                {
                    try
                    {
                        SceneObjectManager.GetInstance().SkillAttack_CallBack(gameObject, m_OwnerType, 1, int.Parse(nCurHit));
                    }
                    catch (System.Exception ex)
                    {
                        LogManager.LogError("!!!Error: SkillAttack_Fly() param Parse is error!! the GameObject is:" + gameObject + "====param is :" + nCurHit);
                    }
                    
                }
            }
        }
        //瞬间技能攻击回调
        private void SkillAttack_Moment()
        {
            SceneObjectManager.GetInstance().SkillAttack_CallBack(gameObject, m_OwnerType,2);
        }
        //引导技能攻击回调
        private void SkillAttack_Guidance()
        {
            SceneObjectManager.GetInstance().SkillAttack_CallBack(gameObject, m_OwnerType, 3);
        }
        //技能攻击结束回调
        private void SkillAttackEnd()
        {
            if (ObjectSelf.GetInstance().isSkillShow)
            {
                SceneObjectManager.GetInstance().InitSkillShowState();
                UI_SkillShow.Inst.GetSkillIconScript().SetIsClick(true);
                return;
            }
            SceneObjectManager.GetInstance().SkillAttack_CallBack(gameObject, m_OwnerType,4);
        }
        //######################################################战斗待机动作接口################################################
        //战斗待机结束回调
        private void FightIdleEnd()
        {
            SceneObjectManager.GetInstance().OnChangeActionState(gameObject, m_OwnerType, ObjectCreature.ObjectActionState.normalAttack);
        }
        //######################################################被击动作接口####################################################
        //被击结束回调
        private void HurtEnd()
        {
           
            
            ObjectCreature pCaster = SceneObjectManager.GetInstance().GetSceneObjectByGameObject(this.gameObject);
            if (pCaster == null)
                return;

            switch (pCaster.GetActionState())
            { 
                case ObjectCreature.ObjectActionState.Hurting:
                    {
                        if (pCaster.GetCacheLastActionState() == ObjectCreature.ObjectActionState.dizzy && pCaster.IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO))
                        {
                            //重置回眩晕中 [10/19/2015 Zmy]
                            pCaster.SetObjectActionState(ObjectCreature.ObjectActionState.dizzy);
                        }
                        else
                        {
                            pCaster.SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
                        }
                    }
                    break;
                case ObjectCreature.ObjectActionState.checkHurting:
                    {
                        //受击结束播放一次不带事件回调的待机动画，用于可以在checkHurting状态下可以再次播放受击动作 [9/28/2015 Zmy]
                        if (pCaster is ObjectHero)
                        {
                            ObjectHero obj = pCaster as ObjectHero;
                            obj.GetAnimation().Anim_Fidle(false);
                        }
                        else
                        {
                            ObjectMonster obj = pCaster as ObjectMonster;
                            obj.GetAnimation().Anim_Fidle(false);
                        }
                    }
                    break;
                default:
                    break;
            }
            
            
        }
        //######################################################死亡动作接口####################################################
        //死亡结束回调
        private void DieEnd()
        {
            SceneObjectManager.GetInstance().OnChangeActionState(gameObject, m_OwnerType, ObjectCreature.ObjectActionState.destory);
        }

        private void NullFunction()
        {

        }


        //通过委托，根据函数名称回调事件函数 [2/12/2015 Zmy]
        public void CallBack_EventFunction(string methodName,string[] param1)
        {
            switch (methodName)
            {
                case "NormalAttack_ModelNear":
                    {
                        AnimEventFunction function = this.NormalAttack_ModelNear;
                        function();
                    }
                    break;
                case "NormalAttack_ModelFar":
                    {
                        AnimEventFunction function = this.NormalAttack_ModelFar;
                        function();
                    }
                    break;
                case "NormalAttack_ParticleNear":
                    {
                        AnimEventFunction function = this.NormalAttack_ParticleNear;
                        function();
                    }
                    break;
                case "NormalAttack_ParticleFar":
                    {
                        AnimEventFunction function = this.NormalAttack_ParticleFar;
                        function();
                    }
                    break;
                case "NormalAttackEnd":
                    {
                        AnimEventFunction function = this.NormalAttackEnd;
                        function();
                    }
                    break;
                case "SkillAttack_Fly":
                    {
                        AnimEventFunctionParam function = this.SkillAttack_Fly;
                        function(param1[0]);
                    }
                    break;
                case "SkillAttack_Moment":
                    {
                        AnimEventFunction function = this.SkillAttack_Moment;
                        function();
                    }
                    break;
                case "SkillAttack_Guidance":
                    {
                        AnimEventFunction function = this.SkillAttack_Guidance;
                        function();
                    }
                    break; 
                case "SkillAttackEnd":
                    {
                        AnimEventFunction function = this.SkillAttackEnd;
                        function();
                    }
                    break;
                //case "FightIdleEnd":
                //    {
                //        AnimEventFunction function = this.FightIdleEnd;
                //        function();
                //    }
                //    break;
                case "HurtEnd":
                    {
                        AnimEventFunction function = this.HurtEnd;
                        function();
                    }
                    break;
                case "DieEnd":
                    {
                        AnimEventFunction function = this.DieEnd;
                        function();
                    }
                    break;
                case "Head_C_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Head_C_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Head_C_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Head_C_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Head_T_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Head_T_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Head_T_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Head_T_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Hand_L_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Hand_L_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Hand_L_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Hand_L_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Hand_R_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Hand_R_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Hand_R_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Hand_R_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Foot_L_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Foot_L_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Foot_L_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Foot_L_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Foot_R_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Foot_R_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Foot_R_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Foot_R_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Chest_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Chest_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Chest_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Chest_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Bottom_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Bottom_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Bottom_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Bottom_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Weapon01_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Weapon01_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Weapon01_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Weapon01_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Weapon02_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Weapon02_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Weapon02_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Weapon02_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Normal_HurtPoint":
                    {
                        AnimEventFunctionParam function = this.Normal_HurtPoint;
                        function(param1[0]);
                    }
                    break;
                case "Normal_HurtPoint2":
                    {
                        AnimEventFunctionParam function = this.Normal_HurtPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Special_HurtPoint":
                    {
                        AnimEventFunctionParam function = this.Special_HurtPoint;
                        function(param1[0]);
                    }
                    break;
                case "Special_HurtPoint2":
                    {
                        AnimEventFunctionParam function = this.Special_HurtPoint2;
                        function(param1[0]);
                    }
                    break;
                case "Footsteps_EffectPoint":
                    {
                        AnimEventFunctionParam function = this.Footsteps_EffectPoint;
                        function(param1[0]);
                    }
                    break;
                case "Footsteps_EffectPoint2":
                    {
                        AnimEventFunctionParam function = this.Footsteps_EffectPoint2;
                        function(param1[0]);
                    }
                    break;
            }
        }
    }
    //委托回调事件函数 [2/12/2015 Zmy]
    delegate void AnimEventFunction();
    // 带参数的委托执行回调函数 [2/12/2015 Zmy]
    delegate void AnimEventFunctionParam(string param1);
}

