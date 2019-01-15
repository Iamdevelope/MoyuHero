using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using UnityEngine.UI;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork.Data;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.GameAudio;
using DG.Tweening;

namespace DreamFaction.UI
{
    public class UI_SkillIcon : BaseUI
    {
        public delegate  void ParticleCall();
        private ParticleCall m_CdEffectPlayFinish;
        public int skillId = 0;
        public int heroId = 0;

        public float cdTime = 0;
        public bool isEnabled = true;
        public bool isHeroDead = false;
        public bool isFighting = true;

        public GameObject skillIcon = null;
        public Image cdIcon = null;
        //private Transform mStarTrans;
        private Text mLevel;

        private Transform m_cd;
        private Image m_cd_N1; //十位数
        private Image m_cs_N2;  //个位数

        private Image m_HeroIcon;//英雄头像
        private Image m_FormPostion;  //阵型位置
        private Image m_SkillIcon;  //技能图标
        private Image m_IconBg;  //技能图标底框
        private Image m_LevelBg;  //等级背景
        private Text m_LevelText;  //等级文本
        private Image m_HeroIconBg;  //英雄头像背景框
        private Image m_HeroDeathEffect;  // 英雄死亡时红色外框
        private Image m_HightWaikuang;    //高亮外放光
        private Text m_cdTime;  //冷却倒计时


       // private Transform m_Effect_Waibiankuang; //当可以释放技能时 外边框闪光
        private Transform m_Effect_Shifangjineng; //成功释放技能
        private Transform m_Effect_CdComplte;//cd冷却完毕
        private bool isPlayComplte_Cd;  //cd冷却完毕特效是否播放完毕


        // 冷卻時間
        private float mMaxCoolDownTime = 0.0f;
        private CoolDownList mSkillCD;
        private SkillTemplate mSkillInfo = null;
        private ObjectHero mHero = null;

        private bool isClick = true; //是否可以点击(技能释放完成)  只使用与技能演示[Lyq]

        bool isTrigger = false;
        float curTime = 0.0f;
        float allTime = 2.5f;

        private bool isShowWarringEffect = false;//是否显示警告效果
        private float apleValue = 1;
        private bool isStartIn; //淡入
        private bool isStartOut; //淡出
        private int isCdFinish = 0; //冷却完成
        public enum SkillState
        { 
            CanUse,  //可以释放技能  高亮显示
            NoUse,    //角色移动时不可释放技能 ，置灰
            HeoDie,  //英雄已经死亡
            CdCool,   //cd冷却
            ReleaseSuccess,  //释放技能成功
        }
        public void SetIsClick(bool value) { isClick = value; }

        void Awake()
        {
            //mStarTrans = transform.FindChild("star").transform;
            mLevel = transform.FindChild("level/Text").GetComponent<Text>();
            cdIcon = transform.FindChild("Image(skillwaiting)").GetComponent<Image>();
            m_cd_N1 = transform.FindChild("cdTime/grid/1").GetComponent<Image>();
            m_cs_N2 = transform.FindChild("cdTime/grid/2").GetComponent<Image>();
            m_FormPostion = transform.FindChild("Image(zhengxing)").GetComponent<Image>();
            m_IconBg = transform.FindChild("Image").GetComponent<Image>();
            m_LevelBg = transform.FindChild("level/Image").GetComponent<Image>();
            m_LevelText = transform.FindChild("level/Text").GetComponent<Text>();
            m_HeroIconBg = transform.FindChild("touxiang").GetComponent<Image>();
            m_HeroDeathEffect = transform.FindChild("Image(herodeath)").GetComponent<Image>();
           // m_Effect_Waibiankuang = transform.FindChild("Ui_Effect_Wailunkuoguang01");
            m_Effect_Shifangjineng = transform.FindChild("Ui_Effect_Chenggongshifang01");
            m_Effect_CdComplte = transform.FindChild("Ui_Effect_Wanchenghonshanguag01");
            m_HightWaikuang = transform.FindChild("Image(waibiankuang)").GetComponent<Image>();
            m_HightWaikuang.gameObject.SetActive(false);
            m_Effect_CdComplte.gameObject.SetActive(false);
            m_Effect_Shifangjineng.gameObject.SetActive(false);
            m_FormPostion.gameObject.SetActive(false);
           // m_Effect_Waibiankuang.gameObject.SetActive(false);
            m_cd = transform.FindChild("cdTime");
            m_cdTime = transform.FindChild("cdTime/Text").GetComponent<Text>();
            m_cd.gameObject.SetActive(false);

        }

        public void setHero(ObjectHero hero)
        {
            mHero = hero;
            heroId = hero.GetHeroData().TableID;
        }
        public override void UpdateUIView()
        {
            if (!ObjectSelf.GetInstance().isSkillShow)
            {
                if (cdIcon.fillAmount > 0 && cdTime > 0 && !isHeroDead)
                {
                    cdTime -= Time.deltaTime;
                    cdIcon.fillAmount = cdTime / mMaxCoolDownTime;
                    ShowCdNumber();
                    SetHeroShowState(SkillState.CdCool);
                }

            }
            if (cdTime <= 0 && !isHeroDead && FightControler.Inst.GetFightState() == FightState.Fighting)
            {
                isCdFinish++;
                SetHeroShowState(SkillState.CanUse);
              
            }
            if (isTrigger)
            {
                curTime += Time.deltaTime;
                if (curTime > allTime)
                {
                    GuideManager.GetInstance().ShowGuideWithIndex(100312);
                    isTrigger = false;
                }
            }
        }
        public void ShowCdNumber()
        {
            m_cd.gameObject.SetActive(true);
            int cd = Mathf.CeilToInt(cdTime);
            int n1 = cd/10; //十位
            int n2 = cd%10;  //个位
            if (n1 > 0)
            {
                m_cdTime.text = n1+""+n2;
            }
            else if (n1 <= 0)
            {
                m_cdTime.text = "" + n2;
            }
            else
            {
                m_cd.gameObject.SetActive(false);
             }
            /*
            if (n1 >0)
            {
                m_cd_N1.sprite = GetImage(n1);
                m_cs_N2.sprite = GetImage(n2);
                m_cd_N1.gameObject.SetActive(true);
                m_cs_N2.gameObject.SetActive(true);
            }
            else if (n1<=0)
            {
                m_cs_N2.sprite = GetImage(n2);
                m_cd_N1.gameObject.SetActive(false);
                m_cs_N2.gameObject.SetActive(true);
            }
            else if(cdTime<=0)
            {
                m_cd_N1.gameObject.SetActive(false);
                m_cs_N2.gameObject.SetActive(false);
                m_cd.gameObject.SetActive(false);
            }
            */
        }
        Sprite GetImage(int value)
        {
            switch (value)
            {
                case 0:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7001");
                case 1:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7002");
                case 2:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7003");
                case 3:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7004");
                case 4:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7005");
                case 5:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7006");
                case 6:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7007");
                case 7:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7008");
                case 8:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7009");
                case 9:
                   return UIResourceMgr.LoadSprite("UI/Number/battle_number/img_7010");
                default:
                   return null;
            }
        }
        void OnDestroy()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_FightStateUpdate, SetSkillStatus);
        }

        public void InitIcon()
        {
            HeroTemplate ht = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroId);
            this.skillId = ht.getSkill1ID();
            mSkillInfo = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(skillId);

            ArtresourceTemplate at = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(ht.getArtresources());

            mMaxCoolDownTime = mSkillInfo.getCooldown() / 1000.0f;

            GameEventDispatcher.Inst.addEventListener(GameEventID.F_FightStateUpdate, SetSkillStatus);

           // GameObject button = transform.FindChild("Icon").gameObject;
            transform.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(OnSkillClick));
            //button.AddComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(OnSkillClick));

            m_HeroIcon = transform.FindChild("touxiang/Image(heroicon)").gameObject.GetComponent<Image>();
            m_HeroIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + at.getHeadiconresource());
            //hero.SetNativeSize();

            m_SkillIcon = transform.FindChild("Image(skillicon)").GetComponent<Image>();
            m_SkillIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + mSkillInfo.getSkillIcon());

             //int groupPostion= ObjectSelf.GetInstance().Teams.GetGroupPosByHeroGuid(mHero.GetGuid());
             //if (groupPostion != -1)
             //{
             //    if (groupPostion == 1)
             //    {
             //       m_FormPostion.sprite= UIResourceMgr.LoadSprite(common.defaultPath + "ui_zhenxing_02");
             //    }
             //    else if (groupPostion == 2)
             //    {
             //        m_FormPostion.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "ui_zhenxing_01");
             //    }
             //}
            //iconImg.SetNativeSize();

            //Transform cdObj = iconImg.transform.FindChild("CD");
            //cdIcon = cdObj.GetComponent<Image>();

            //// 设置星级
            //int istart = ht.getQuality();

            //for (int idx = 0; idx < GlobalMembers.HeroMaxStar; idx++)
            //{
            //    mStarTrans.GetChild(idx).gameObject.SetActive(idx<istart);
            //}
            // set skill level
            // 设置英雄等级
            //mLevel.text = (mSkillInfo.getSkillCostNum1()/30).ToString();
            mLevel.text = mHero.GetHeroData().Level.ToString();
        }
        public void InitCoolDown()
        {
            // init
            List<ObjectHero> heroList = SceneObjectManager.GetInstance().GetSceneHeroList();

            for (int i = 0; i < heroList.Count; i++)
            {
                if (heroList[i].GetHeroData().TableID == heroId)
                {
                    mSkillCD = heroList[i].GetCoolDownList();
                    break;
                }
            }
        }

        //点击技能图标
        public void OnSkillClick()
        {
            Debug.LogError("diuanji");
            // 新手引导相关
            if (GuideManager.GetInstance().isGuideUser)
            {
                FightControler.Inst.OnUpdatePowerValue(EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO, 50);
                // 点击【第一勇士技能】 100309
                //GuideManager.GetInstance().ShowNextGuide();
                

                if(GuideManager.GetInstance().GetBackCount(100309))
                {
                    GuideManager.GetInstance().ShowGuideWithIndex(100310);
                }

                if (GuideManager.GetInstance().GetBackCount(100311))
                {
                    GameTimeControler.Inst.SetState(UI_MenuPanel.Inst.mInitSpeed);
                    GuideManager.GetInstance().StopGuide();
                    isTrigger = true;
                }
                if (UI_MenuPanel.Inst != null)
                {
                    UI_MenuPanel.Inst.SetStageNameActive(true);
                }

            }

            if (!ObjectSelf.GetInstance().isSkillShow)
            {
                //队伍是否在移动中、英雄是否已死亡
                if (!OnCheck())
                {
                    //不能释放技能 （死亡或者、CD、消耗不足、无释放目标的等）
                    AudioControler.Inst.PlaySound("test_evil_death");
                    LogManager.Log("is hero cannot sound!!");
                    return;
                }
            }
            else
            {
                if (GetHero().OnUIPre_CheckUseSkillCondtion() == false)
                    return;
                if (!isClick)
                    return;
                isClick = false;
            }
            ReleaseSkillEffect();
            bool IsShowSelectTarget = SceneObjectManager.GetInstance().GetIsFireSignState();// 非集火状态才显示目标选择 [3/27/2015 Zmy]

            if (!IsShowSelectTarget && IsNeedSelectTarget(mSkillInfo) == EM_TARGET_TYPE.EM_TARGET_FRIEND)
            {
                // 对我方释放   
                EventRequestSkillPackage package = new EventRequestSkillPackage(mHero.GetGuid(), skillId, true);
                // 我方单体
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_ShowSkillTarget, package);
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips9");
                return;
            }
            else if (!IsShowSelectTarget && IsNeedSelectTarget(mSkillInfo) == EM_TARGET_TYPE.EM_TARGET_FRIEND_NO_SELF)
            {
                // 对我方释放   
                EventRequestSkillPackage package = new EventRequestSkillPackage(mHero.GetGuid(), skillId, true,false);
                // 我方单体
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_ShowSkillTarget, package);
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips9");
                return;
            }
            else if (!IsShowSelectTarget && IsNeedSelectTarget(mSkillInfo) == EM_TARGET_TYPE.EM_TARGET_ENEMY)
            {
                // 对敌方释放   
                ObjectCreature target;
                if (SceneObjectManager.GetInstance().IsOnlyOneMonsterLeft(out target))
                {
                    EventRequestSkillPackage package = new EventRequestSkillPackage(mHero.GetGuid(), skillId, target);
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_RequestReleaseSkill, package);
                }
                else
                {
                    EventRequestSkillPackage package = new EventRequestSkillPackage(mHero.GetGuid(), skillId, false);
                    // 敌方单体
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_ShowSkillTarget, package);
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips9");
                }
                return;
            }
            if (UI_FightControler.Inst != null)
            {
                UI_FightControler.Inst.onReleaseTargetLock();
            }
            // 请求释放技能
            EventRequestSkillPackage _package = new EventRequestSkillPackage(mHero.GetGuid(), skillId, null);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_RequestReleaseSkill, _package);

            //cdIcon.fillAmount = 1;
            //cdTime = mMaxCoolDownTime;

            
        }

        //判断技能释放条件，满足条件返回true,不满足条件弹提示信息并返回false
        public bool OnCheck()
        {
            if (isHeroDead)
            {
                // 英雄已死亡
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips7");
                return false;
            }

            if (!isFighting)
            {
                // 非战斗状态不能使用技能！
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips8");
                return false;
            }

            if (UI_FightControler.Inst != null && UI_FightControler.Inst.isWaitLock(mHero.GetGuid()))
            {
                return false;
            }

            return GetHero().OnUIPre_CheckUseSkillCondtion();
            //return SceneObjectManager.GetInstance().OnFree_PveHeroSkill(heroId, skillId);
        }

        //设技能状态可否点击，队伍行进时
        public void SetSkillStatus()
        {
            bool flag = true;
            if (FightControler.Inst.GetFightState() == FightState.Fighting)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            if (flag)
            {
                if (!isHeroDead)
                {
                    isFighting = true;
                    cdIcon.fillAmount = 0;
                    SetHeroShowState(SkillState.CanUse);
                }
            }
            else
            {
                if (!isHeroDead)
                {
                    isFighting = false;
                    cdIcon.fillAmount = 1;
                    cdTime = 0;
                    SetHeroShowState(SkillState.NoUse);
                }
            }
        }

        //英雄死亡后技能不可以使用
        public bool OnHeroDead(X_GUID uid)
        {
            if (mHero.GetGuid().Equals(uid))
            {
                isHeroDead = true;
                cdIcon.fillAmount = 1;
                SetHeroShowState(SkillState.HeoDie);
                return true;
            }
            return false;
        }

        // 重置cd
        public void onResetCD()
        {
            cdIcon.fillAmount = 1;
            cdTime = mMaxCoolDownTime;
        }

        private ObjectHero GetHero()
        {
            if (mHero == null)
            {
                List<ObjectHero> heroList = SceneObjectManager.GetInstance().GetSceneHeroList();

                for (int i = 0; i < heroList.Count; i++)
                {
                    if (heroList[i].GetHeroData().GUID == mHero.GetGuid())
                    {
                        mHero = heroList[i];
                    }
                }
            }
            return mHero;
        }

        /// <summary>
        /// 返回技能是否需要选择目标 //封装一个模板接口，通过表数据返回想要的信息[3/27/2015 Zmy]
        /// </summary>
        /// <returns>EM_TARGET_INVALID：技能无需选择目标，也就是群体技能
        ///          EM_TARGET_FRIEND: 技能需要在我方选择一个目标
        ///          EM_TARGET_ENEMY: 技能需要在敌方选择一个目标</returns>
        public EM_TARGET_TYPE IsNeedSelectTarget(SkillTemplate _row)
        {
            if (_row == null)
            {
                return EM_TARGET_TYPE.EM_TARGET_INVALID;//表示无需选择目标
            }

            if ((_row.getTarget() == 1 && _row.getCoverage() == 0) || _row.getTarget() == 22 || _row.getTarget() == 23)
            {
                return EM_TARGET_TYPE.EM_TARGET_FRIEND; //表示需要在我方选择一个目标
            }
            else if (_row.getTarget() == 8)
            {
                return EM_TARGET_TYPE.EM_TARGET_FRIEND_NO_SELF; //表示需要在我方选择一个目标(非自己)
            }
            else if ((_row.getTarget() == 2 && _row.getCoverage() == 0) || _row.getTarget() == 21)
            {
                return EM_TARGET_TYPE.EM_TARGET_ENEMY;//表示需要在敌方选择一个目标
            }
            return EM_TARGET_TYPE.EM_TARGET_INVALID;//表示无需选择目标
        }
        /// <summary>
        /// 根据英雄状态 显示技能状态 Wyf
        /// </summary>
        void SetHeroShowState(SkillState skillState)
        {
            switch (skillState)
            {
                case SkillState.CanUse:
                    StopAllCoroutines();
                   // m_SkillHighLigth.gameObject.SetActive(true);
                    //m_Effect_Waibiankuang.gameObject.SetActive(true);
                    if (isCdFinish <= 1)
                    {
                        StartCoroutine(CDFinish());
                    }
                    else
                    {
                       // m_Effect_Waibiankuang.gameObject.SetActive(true);
                    }
                    m_HightWaikuang.gameObject.SetActive(true);
                    m_cd.gameObject.SetActive(false);
                    m_HeroDeathEffect.gameObject.SetActive(false);
                    m_HeroIcon.color = new Color(1,1,1);
                    m_FormPostion.color = new Color(1, 1, 1);
                    m_SkillIcon.color = new Color(1, 1, 1);
                    m_IconBg.color = new Color(1, 1, 1);
                    m_LevelBg.color = new Color(1, 1, 1);
                    m_LevelText.color = new Color(1, 1, 1);
                    m_HeroIconBg.color = new Color(1, 1, 1);
                    break;
                case SkillState.NoUse:
                    StopAllCoroutines();
                    isCdFinish = 0;
                    m_HightWaikuang.gameObject.SetActive(false);
                  //  m_Effect_Waibiankuang.gameObject.SetActive(false);
                    m_HeroDeathEffect.gameObject.SetActive(false);
                    m_HeroIcon.color = new Color(131/255f,126/255f,126/255f);
                    m_FormPostion.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_SkillIcon.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_IconBg.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_LevelBg.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_LevelText.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_HeroIconBg.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_cd.gameObject.SetActive(false);
                    break;
                case SkillState.HeoDie:
                    StopAllCoroutines();
                    isCdFinish = 0;
                    m_cd.gameObject.SetActive(false);
                    m_HightWaikuang.gameObject.SetActive(false);
                    //m_Effect_Waibiankuang.gameObject.SetActive(false);
                    m_HeroDeathEffect.gameObject.SetActive(true);
                    //m_SkillHighLigth.gameObject.SetActive(false);
                    m_HeroIcon.color = new Color(1,1,1);
                    m_FormPostion.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_SkillIcon.color = new Color(1, 1, 1);
                    m_IconBg.color = new Color(1, 1, 1);
                    m_LevelBg.color = new Color(1, 1, 1);
                    m_LevelText.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_HeroIconBg.color = new Color(1, 1, 1);
                    GameUtils.SetImageGrayState(m_HeroIcon, true);
                    GameUtils.SetImageGrayState(m_SkillIcon, true);
                    GameUtils.SetImageGrayState(m_LevelBg, true);
                    GameUtils.SetImageGrayState(m_HeroIconBg, true);
                    break;
                case SkillState.CdCool:
                    StopAllCoroutines();
                    isCdFinish = 0;
                    m_cd.gameObject.SetActive(true);
                    m_HightWaikuang.gameObject.SetActive(false);
                    //m_Effect_Waibiankuang.gameObject.SetActive(false);
                    m_HeroDeathEffect.gameObject.SetActive(false);
                    m_HeroIcon.color = new Color(131/255f,126/255f,126/255f);
                    m_FormPostion.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_SkillIcon.color = new Color(1, 1, 1);
                    m_IconBg.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_LevelBg.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_LevelText.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    m_HeroIconBg.color = new Color(131 / 255f, 126 / 255f, 126 / 255f);
                    break;
                default:
                    break;
            }
        }
        void ReleaseSkillEffect()
        {
            //m_Effect_Waibiankuang.gameObject.SetActive(false);
            m_Effect_CdComplte.gameObject.SetActive(false);
            m_Effect_Shifangjineng.gameObject.SetActive(true);
            if (m_Effect_Shifangjineng.GetComponentInChildren<ParticleSystem>() != null)
            {
                m_Effect_Shifangjineng.GetComponentInChildren<ParticleSystem>().Play(); 
            }
        }
        IEnumerator CDFinish()
        {
            //m_Effect_Waibiankuang.gameObject.SetActive(false);
            m_Effect_Shifangjineng.gameObject.SetActive(false);
            if (m_Effect_CdComplte.gameObject.activeSelf)
            {
                m_Effect_CdComplte.gameObject.SetActive(false);
                m_Effect_CdComplte.gameObject.SetActive(true);
            }
            else
            {
                m_Effect_CdComplte.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1.0f);
            //m_Effect_Waibiankuang.gameObject.SetActive(true);
        }
        /// <summary>
        /// 点击警告
        /// </summary>
        void OnClickWarring()
        {
            //isShowWarringEffect = true;
            //m_WarringAni.gameObject.SetActive(true);
            //m_WarringAni.Play();
           
            
        }
        //void OnGUI()
        //{
        //    if (GUILayout.Button("ss"))
        //    {
        //        OnClickWarring();
        //    }
        //}
    } 
}