using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DG.Tweening;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameCore;
namespace DreamFaction.UI
{
    public class UI_HeroBeginnerManager : BaseUI
    {
        public static UI_HeroBeginnerManager _instance;
        //资源栏信息显示
        private Text mMoney;                    //金币数
        private Text mZiyuan1;                  //贤者之石（紫）
        private Text mZiyuan2;                  //贤者之石（黄）
        private Button backBtn;                 //返回按钮

        //进阶前信息显示
        private Text mLevelOld;                 //等级
        private Text mHpOld;                    //生命
        private Text mPhysicsAttacksOld;        //物理攻击
        private Text mMagicAttacksOld;          //法术攻击
        private Text mPhysicsDefenseOld;        //物理防御
        private Text mMagicDefenseOld;          //魔法防御
        private Text mAccuracyOld;              //命中
        private Text mEvaOld;                   //闪避
        private Text mCritOld;                  //暴击
        private Text mTenacityOld;              //韧性
        private Text mVelocityOld;              //速度
        private Text mSkillMaxOld;              //技能最高等级

        //进阶后信息
        private Text mLevelNew;                 //等级
        private Text mHpNew;                    //生命
        private Text mPhysicsAttacksNew;        //物理攻击
        private Text mMagicAttacksNew;          //法术攻击
        private Text mPhysicsDefenseNew;        //物理防御
        private Text mMagicDefenseNew;          //魔法防御
        private Text mAccuracyNew;              //命中
        private Text mEvaNew;                   //闪避
        private Text mCritNew;                  //暴击
        private Text mTenacityNew;              //韧性
        private Text mVelocityNew;              //速度
        private Text mSkillMaxNew;              //技能最高等级

        //进阶需求
        private Text mNeedMoney;                //需要消耗的金币
        private Text mNeedZiyuan;               //需要消耗的资源数量
        private Image mNeedZiyuanType;          //需要消耗资源的类型
        private Button beginnerButton;          //进阶按钮
        private Image mNeedMoneytype;

        //进阶后技能描述
        private Image mSkillBg;                 //技能图标
        private Text mSkillName;                //技能名称
        private Text mSkillType;                //技能类型
        private Text mSkillLevel;               //技能等级
        private GameObject mSkill;
        private GameObject mSkillDes;
        private Text mSkillDesText;
        private Button mSkillButton;

        public List<GameObject> mStarListOld;
        public List<GameObject> mStarListNew;
        public List<GameObject> mStarBGListOld;
        public List<GameObject> mStarBGListNew;
        private List<BaseItem> item;
        private ObjectCard _Card;
        ObjectCard obj = new ObjectCard();
        //private RawImage mShow3DModel;
        private GameObject BeginnerSuccend;
        private GameObject Card3Dmodel;                                                      //当前实例化3D模型
        private Transform _Point;                                                            //3D模型实例化位置
        private int mZiyuan1Num;
        private int mZiyuan2Num;
        private Transform show3DModel;
        public GameObject text;
        private bool isSkill = false;
        private Transform MsgBoxGroup;
        float time;
        float closeTime;
        public bool isNidle1 = false;
        public bool isBack = false;
        public bool isSkillButton = false;
        public override void InitUIData()
        {
            base.InitUIData();
            _instance = this;
            MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
            //mShow3DModel = selfTransform.FindChild("BeginnerSuccend/Show3DModel").GetComponent<RawImage>();
            BeginnerSuccend = selfTransform.FindChild("BeginnerSuccend").gameObject;
            text = selfTransform.FindChild("BeginnerSuccend/Text").gameObject;
            //资源栏获取
            mMoney = selfTransform.FindChild("UI_Top/UI_Money/Text").GetComponent<Text>();
            mZiyuan1 = selfTransform.FindChild("UI_Top/UI_ziyuan1/Text").GetComponent<Text>();
            mZiyuan2 = selfTransform.FindChild("UI_Top/UI_ziyuan2/Text").GetComponent<Text>();
            backBtn = selfTransform.FindChild("UI_Top/Back_Btn").GetComponent<Button>();

            //进阶前信息获取
            mLevelOld = selfTransform.FindChild("UI_Center/Level/old").GetComponent<Text>();
            mHpOld = selfTransform.FindChild("UI_Center/HP/old").GetComponent<Text>();
            mPhysicsAttacksOld = selfTransform.FindChild("UI_Center/PhysicsAttacks/old").GetComponent<Text>();
            mMagicAttacksOld = selfTransform.FindChild("UI_Center/MagicAttacks/old").GetComponent<Text>();
            mPhysicsDefenseOld = selfTransform.FindChild("UI_Center/PhysicsDefense/old").GetComponent<Text>();
            mMagicDefenseOld = selfTransform.FindChild("UI_Center/MagicDefense/old").GetComponent<Text>();
            mAccuracyOld = selfTransform.FindChild("UI_Center/Accuracy/old").GetComponent<Text>();
            mEvaOld = selfTransform.FindChild("UI_Center/Eva/old").GetComponent<Text>();
            mCritOld = selfTransform.FindChild("UI_Center/Crit/old").GetComponent<Text>();
            mTenacityOld = selfTransform.FindChild("UI_Center/Tenacity/old").GetComponent<Text>();
            mVelocityOld = selfTransform.FindChild("UI_Center/Velocity/old").GetComponent<Text>();
            mSkillMaxOld = selfTransform.FindChild("UI_Center/SkillMax/old").GetComponent<Text>();

            //进阶后信息获取
            mLevelNew = selfTransform.FindChild("UI_Center/Level/new").GetComponent<Text>();
            mHpNew = selfTransform.FindChild("UI_Center/HP/new").GetComponent<Text>();
            mPhysicsAttacksNew = selfTransform.FindChild("UI_Center/PhysicsAttacks/new").GetComponent<Text>();
            mMagicAttacksNew = selfTransform.FindChild("UI_Center/MagicAttacks/new").GetComponent<Text>();
            mPhysicsDefenseNew = selfTransform.FindChild("UI_Center/PhysicsDefense/new").GetComponent<Text>();
            mMagicDefenseNew = selfTransform.FindChild("UI_Center/MagicDefense/new").GetComponent<Text>();
            mAccuracyNew = selfTransform.FindChild("UI_Center/Accuracy/new").GetComponent<Text>();
            mEvaNew = selfTransform.FindChild("UI_Center/Eva/new").GetComponent<Text>();
            mCritNew = selfTransform.FindChild("UI_Center/Crit/new").GetComponent<Text>();
            mTenacityNew = selfTransform.FindChild("UI_Center/Tenacity/new").GetComponent<Text>();
            mVelocityNew = selfTransform.FindChild("UI_Center/Velocity/new").GetComponent<Text>();
            mSkillMaxNew = selfTransform.FindChild("UI_Center/SkillMax/new").GetComponent<Text>();

            //进阶需求
            mNeedMoney = selfTransform.FindChild("UI_Center/UI_Money/Text").GetComponent<Text>();
            mNeedZiyuan = selfTransform.FindChild("UI_Center/UI_ziyuan/Text").GetComponent<Text>();
            mNeedZiyuanType = selfTransform.FindChild("UI_Center/UI_ziyuan").GetComponent<Image>();
            mNeedMoneytype = selfTransform.FindChild("UI_Center/UI_Money").GetComponent<Image>();
            beginnerButton = selfTransform.FindChild("UI_Center/UI_Btn_Beginner").GetComponent<Button>();

            //进阶后的技能展示
            mSkillBg = selfTransform.FindChild("Skill/SkillBg").GetComponent<Image>();
            mSkillLevel = selfTransform.FindChild("Skill/SkillNum/Text").GetComponent<Text>();
            mSkillType = selfTransform.FindChild("Skill/SkillBg/Text").GetComponent<Text>();
            mSkillName = selfTransform.FindChild("Skill/SkillName").GetComponent<Text>();
            mSkill = selfTransform.FindChild("Skill").gameObject;
            mSkillDes = selfTransform.FindChild("Skill/SkillDes").gameObject;
            mSkillDesText = selfTransform.FindChild("Skill/SkillDes/Des").GetComponent<Text>();
            mSkillButton = mSkillBg.transform.GetComponent<Button>();

            _Point = GameObject.Find("pos1").transform;
            show3DModel = selfTransform.FindChild("BeginnerSuccend/Show3DModel");
            isSkill = false;
            backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(HideUI));
            beginnerButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBeginnerButton));
            mSkillButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSkillButton));
            GameEventDispatcher.Inst.addEventListener(GameEventID.HE_BeginnerUp, SucceedBeginner);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Guide_Continue, ShowNewGuide);

            
        }

        void OnEnable()
        {
            HeroModelBack.Inst.ChangePanel("HeroAdv");
            UI_HeroInfo temp = UI_HeroInfo._instance;
            if (temp != null)
            {
                temp.uiMark = Core.UIMark.HeroUpgrade;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_InterfaceChange);
            }
        }
        void OnDisable()
        {
            HeroModelBack.Inst.ChangePanel("HeroBg");
            UI_HeroInfo temp = UI_HeroInfo._instance;
            if (temp != null)
            {
                temp.uiMark = Core.UIMark.DefaultMark;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_InterfaceChange);
            }
        }

        protected void OnDestroy()
        {
            _instance = null;
            GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_BeginnerUp, SucceedBeginner);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Guide_Continue, ShowNewGuide);
        }

        public void UpdateShow(ObjectCard Card)
        {
            _Card = Card;

            TopShow();
            OldShow();
            NewShow();
            NeedShow();
            //SucceedBeginner();

        }
        /// <summary>
        /// 资源栏显示
        /// </summary>
        public void TopShow()
        {
            mMoney.text = ObjectSelf.GetInstance().Money.ToString();
            item = ObjectSelf.GetInstance().CommonItemContainer.SortCommonItemByType(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL);
            mZiyuan1Num = 0;
            mZiyuan2Num = 0;
            for (int i = 0; i < item.Count; i++)
            {
                if (item[i].GetItemTableID() == 1402010005)
                {
                    mZiyuan1Num += item[i].GetItemCount();
                }
                if (item[i].GetItemTableID() == 1402010006)
                {
                    mZiyuan2Num += item[i].GetItemCount();
                }
            }
            mZiyuan1.text = mZiyuan1Num.ToString();
            mZiyuan2.text = mZiyuan2Num.ToString();


        }

        /// <summary>
        ///进阶前属性显示
        /// </summary>
        public void OldShow()
        {
            HeroTemplate _hero = _Card.GetHeroRow();
            HeroData _heroDate = _Card.GetHeroData();
            mLevelOld.text = _Card.GetHeroData().Level + "/" + _hero.getMaxLevel();
            mHpOld.text = (_Card.GetBaseMaxHP() - _heroDate.TrainingMaxHP).ToString();
            int nPhysicalTraniningValue = 0;
            if (_hero.getClientSignType()[1] == 0)
            {
                nPhysicalTraniningValue = _heroDate.TrainingPhysicalAttack;
            }
            mPhysicsAttacksOld.text = (_Card.GetPhysicalBaseAttack() - nPhysicalTraniningValue).ToString();
            int nMagicTraniningValue = 0;
            if (_hero.getClientSignType()[1] == 1)
            {
                nMagicTraniningValue = _heroDate.TrainingMagicAttack;
            }
            mMagicAttacksOld.text = (_Card.GetMagicBaseAttack() - nMagicTraniningValue).ToString();
            mPhysicsDefenseOld.text = (_Card.GetPhysicalBaseDefence() - _heroDate.TrainingPhysicalDefence).ToString();
            mMagicDefenseOld.text = (_Card.GetMagicBaseDefence() - _heroDate.TrainingMagicDefence).ToString();
            mAccuracyOld.text = _Card.GetBaseHit().ToString();
            mEvaOld.text = _Card.GetBaseDodge().ToString();
            mCritOld.text = _Card.GetBaseCritical().ToString();
            mTenacityOld.text = _Card.GetBaseTenacity().ToString();
            mVelocityOld.text = _Card.GetBaseSpeed().ToString();
            int m_HeroStar = _hero.getQuality();
            int maxStar = _hero.getMaxQuality();

            //星级品质界面控制
            for (int i = 0; i < 5; i++)
            {
                mStarListOld[i].SetActive(i + 1 <= m_HeroStar);
                mStarBGListOld[i].SetActive(i + 1 <= maxStar);
            }
            //for (int i = 0; i < mStarListOld.Count; i++)
            //{
            //    if (i < _hero.getQuality())
            //    {
            //        mStarListOld[i].SetActive(true);
            //    }
            //    else
            //    {
            //        mStarListOld[i].SetActive(false);
            //    }
            //}
            mSkillMaxOld.text = _hero.getSkillMaxLevel().ToString();


        }

        /// <summary>
        /// 进阶需求显示
        /// </summary>
        public void NeedShow()
        {
            HeroTemplate _hero = _Card.GetHeroRow();
            int stageUpCostType1 = _hero.getStageUpCostType1();
            if (stageUpCostType1 == 1400000002)//金币
            {
                mNeedMoneytype.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Home_03");
                mNeedMoney.text = _hero.getStageUpCost1().ToString();
                if (ObjectSelf.GetInstance().Money < _hero.getStageUpCost1())
                {
                    mNeedMoney.color = Color.red;
                    beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_25");
                }
                else
                {
                    mNeedMoney.color = Color.white;
                    beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_03");
                }
            }
            else if (stageUpCostType1 == 1400000001)//魔钻
            {
                mNeedMoneytype.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Home_02");
                mNeedMoney.text = _hero.getStageUpCost1().ToString();
                if (ObjectSelf.GetInstance().Gold < _hero.getStageUpCost1())
                {
                    mNeedMoney.color = Color.red;
                    beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_25");
                }
                else
                {
                    mNeedMoney.color = Color.white;
                    beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_03");
                }
            }
            int stageUpCostType2 = _hero.getStageUpCostType2();
            if (stageUpCostType2 == 1402010005)//贤者之石紫
            {
                mNeedZiyuanType.gameObject.SetActive(true);
                mNeedZiyuanType.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_jinjie_01");
                mNeedZiyuan.text = _hero.getStageUpCost2().ToString();
                if (mZiyuan1Num < _hero.getStageUpCost2())
                {
                    mNeedZiyuan.color = Color.red;
                    beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_25");
                }
                else
                {
                    mNeedZiyuan.color = Color.white;
                    beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_03");
                }
            }
            else if (stageUpCostType2 == 1402010006)//贤者之石黄
            {
                mNeedZiyuanType.gameObject.SetActive(true);
                mNeedZiyuanType.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_jinjie_02");
                mNeedZiyuan.text = _hero.getStageUpCost2().ToString();
                if (mZiyuan2Num < _hero.getStageUpCost2())
                {
                    mNeedZiyuan.color = Color.red;
                    beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_25");
                }
                else
                {
                    mNeedZiyuan.color = Color.white;
                    beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_03");
                }
            }
            else
            {
                mNeedZiyuanType.gameObject.SetActive(false);
            }
            if (_Card.GetHeroData().Level < _Card.GetHeroRow().getMaxLevel())
            {
                beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_25");
            }
            else
            {
                beginnerButton.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Tong_03");
            }

        }


        /// <summary>
        /// 进阶后的属性显示
        /// </summary>
        public void NewShow()
        {
            HeroTemplate _hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(_Card.GetHeroRow().getStageUpTargetID());
            int level = 0;
            switch (_hero.getQuality())
            {
                case 2:
                    mSkill.SetActive(false);
                    break;
                case 3:
                    mSkill.SetActive(true);
                    int skillID3 = _hero.getSkill2ID();
                    SkillTemplate skill3 = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(skillID3);
                    mSkillBg.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + skill3.getSkillIcon());
                    mSkillLevel.text = skill3.getSkillLevel().ToString();
                    mSkillName.text = GameUtils.getString(skill3.getSkillName());
                    mSkillType.text = GameUtils.getString("common_skilltypename_1");
                    UI_SkillTips _tips3 = new UI_SkillTips(_Card, skill3);
                    mSkillDesText.text = _tips3.SetShow();
                    level = DataTemplate.GetInstance().m_GameConfig.getHero_advanced_level_3();

                    break;
                case 4:
                    mSkill.SetActive(true);
                    int skillID4 = _hero.getSkill3ID();
                    if (skillID4 != -1)
                    {
                        SkillTemplate skill4 = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(skillID4);
                        mSkillBg.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + skill4.getSkillIcon());
                        mSkillLevel.text = skill4.getSkillLevel().ToString();
                        mSkillName.text = GameUtils.getString(skill4.getSkillName());
                        mSkillType.text = GameUtils.getString("common_skilltypename_3");
                        UI_SkillTips _tips4 = new UI_SkillTips(_Card, skill4);
                        mSkillDesText.text = _tips4.SetShow();
                    }
                    else
                    {
                        mSkill.SetActive(false); 
                    }
                    level = DataTemplate.GetInstance().m_GameConfig.getHero_advanced_level_4();
                    break;
                case 5:
                    level = DataTemplate.GetInstance().m_GameConfig.getHero_advanced_level_5();
                    mSkill.SetActive(false);
                    break;
                default:
                    break;
            }
            Hero hero = new Hero();
            hero.heroid = _Card.GetHeroRow().getStageUpTargetID();
            hero.herolevel = level;
            hero.heroviewid = _hero.getArtresources();
            // ObjectCard obj = new ObjectCard();
            obj.GetHeroData().Init(hero);


            HeroData _heroDate = obj.GetHeroData();
            mLevelNew.text = obj.GetHeroData().Level + "/" + _hero.getMaxLevel();
            mHpNew.text = (obj.GetBaseMaxHP() - _heroDate.TrainingMaxHP).ToString();
            int nPhysicalTraniningValue = 0;
            if (_hero.getClientSignType()[1] == 0)
            {
                nPhysicalTraniningValue = _heroDate.TrainingPhysicalAttack;
            }
            mPhysicsAttacksNew.text = (obj.GetPhysicalBaseAttack() - nPhysicalTraniningValue).ToString();
            int nMagicTraniningValue = 0;
            if (_hero.getClientSignType()[1] == 1)
            {
                nMagicTraniningValue = _heroDate.TrainingMagicAttack;
            }
            mMagicAttacksNew.text = (obj.GetMagicBaseAttack() - nMagicTraniningValue).ToString();
            mPhysicsDefenseNew.text = (obj.GetPhysicalBaseDefence() - _heroDate.TrainingPhysicalDefence).ToString();
            mMagicDefenseNew.text = (obj.GetMagicBaseDefence() - _heroDate.TrainingMagicDefence).ToString();
            mAccuracyNew.text = obj.GetBaseHit().ToString();
            mEvaNew.text = obj.GetBaseDodge().ToString();
            mCritNew.text = obj.GetBaseCritical().ToString();
            mTenacityNew.text = obj.GetBaseTenacity().ToString();
            mVelocityNew.text = obj.GetBaseSpeed().ToString();
            int m_HeroStar = _hero.getQuality();
            int maxStar = _hero.getMaxQuality();

            //星级品质界面控制
            for (int i = 0; i < 5; i++)
            {
                mStarListNew[i].SetActive(i + 1 <= m_HeroStar);
                mStarBGListNew[i].SetActive(i + 1 <= maxStar);
            }
            //for (int i = 0; i < mStarListNew.Count; i++)
            //{
            //    if (i < _hero.getQuality())
            //    {
            //        mStarListNew[i].SetActive(true);
            //    }
            //    else
            //    {
            //        mStarListNew[i].SetActive(false);
            //    }
            //}
            mSkillMaxNew.text = _hero.getSkillMaxLevel().ToString();
            Show3DModel(obj);

        }

        public void OnClickSkillButton()
        {
            mSkillDes.SetActive(true);
            isSkillButton = true;
        }

        public void OnClickBeginnerButton()
        {
            HeroTemplate _hero = _Card.GetHeroRow();
            if (_Card.GetHeroData().Level < _hero.getMaxLevel())
            {
                AddMsgBox(GameUtils.getString("hero_info_qualityupform_tip1"));

            }
            else if (ObjectSelf.GetInstance().Money < _hero.getStageUpCost1())
            {
                AddMsgBox(GameUtils.getString("hero_info_qualityupform_tip2"));
            }
            else if (_hero.getStageUpCostType2() == 1402010005)
            {
                if (mZiyuan1Num < _hero.getStageUpCost2())
                {
                    AddMsgBox(GameUtils.getString("hero_info_qualityupform_tip3"));
                }
                else
                {
                    CStarUpHero starUp = new CStarUpHero();
                    starUp.herokey = (int)_Card.GetHeroData().GUID.GUID_value;
                    IOControler.GetInstance().SendProtocol(starUp);
                }

            }
            else if (_hero.getStageUpCostType2() == 1402010006)
            {
                if (mZiyuan2Num < _hero.getStageUpCost2())
                {
                    AddMsgBox(GameUtils.getString("hero_info_qualityupform_tip3"));
                }
                else
                {
                    UI_HeroInfoManager._instance.GUI_ID = (int)_Card.GetHeroData().GUID.GUID_value;
                    CStarUpHero starUp = new CStarUpHero();
                    starUp.herokey = (int)_Card.GetHeroData().GUID.GUID_value;
                    IOControler.GetInstance().SendProtocol(starUp);
                }
            }
            else
            {
                UI_HeroInfoManager._instance.GUI_ID = (int)_Card.GetHeroData().GUID.GUID_value;
                CStarUpHero starUp = new CStarUpHero();
                starUp.herokey = (int)_Card.GetHeroData().GUID.GUID_value;
                IOControler.GetInstance().SendProtocol(starUp);
            }

            //新手引导相关 点击【进阶按钮】
            if (GuideManager.GetInstance().GetBackCount(200602))
            {
                GuideManager.GetInstance().ShowGuideWithIndex(200603);
            }

        }

        /// <summary>
        /// 监测新手引导 点击继续
        /// </summary>
        /// <param name="e"></param>
        private void ShowNewGuide(GameEvent e)
        {
            if ((int)e.data == -1)
            {
                GuideManager.GetInstance().StopGuide();
            }
            else
            {
                GuideManager.GetInstance().ShowNextGuide();
            }
        }
        private float skillTime;
        public void SucceedBeginner()
        {
            // AddMsgBox("进阶成功");
            BeginnerSuccend.SetActive(true);
            //Card3Dmodel.transform.position = new Vector3(-79.8f, 0f, -26.0f);
            //Animation ani = Card3Dmodel.transform.GetComponent<Animation>();
            //ani.AddClip(Resources.Load(common.EffectPath + "ModelPostion") as AnimationClip, "Move");
            //ani.Play("Move");
            skillTime = 0;
            isSkill = true;
        }
        public float speed = 0;
        //public bool isSucceed = false;
        public override void UpdateUIData()
        {
            base.UpdateUIData();
            //if (isSucceed)
            //{
            //    SucceedBeginner();
            //    isSucceed = false;
            //}
            // Debug.Log(show3DModel.transform.position);
            //Debug.Log(Card3Dmodel.transform.position);
            if (isSkill)
            {
                skillTime += Time.deltaTime;
                // Card3Dmodel.transform.position = new Vector3(-79.8f, Mathf.Lerp(0f, 3.0f, Time.time * speed), -26.0f);
                if (skillTime > 2f)
                {
                    Card3Dmodel.GetComponent<Animation>().Play("Attack1");
                    text.SetActive(true);
                    isNidle1 = true;
                    time = 0;
                    isSkill = false;
                }

            }
            if (isNidle1)
            {
                time += Time.deltaTime;
                if (time > 1.1f)
                {
                    Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
                    //Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
                    isNidle1 = false;
                    isBack = true;
                    
                }

            }

            if (isBack)
            {
                    if (Input.GetMouseButtonDown(0))
                    {
                        isBack = false;
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.HE_HeroBeginnerUpdateShow);
                        BeginnerSuccend.SetActive(false);
                        HideUI();
                    }
            }
          
            if (isSkillButton)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mSkillDes.SetActive(false);
                    isSkillButton = false;
                }
            }
        }

        public void Show3DModel(ObjectCard _card)
        {
            ModelCear();
            //通过表ID获取到英雄数据表
            HeroTemplate _HeroData = _card.GetHeroRow();
            //通过英雄数据表中的资源数据表ID得到资源表数据
            ArtresourceTemplate _Artresourcedata = new ArtresourceTemplate();
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_card.GetHeroRow().getArtresources());
            //通过资源表获取到角色默认美术资源（名称）     通过该名称获取到动态加载数据返回一个对象
            GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(_Artresourcedata.getArtresources());
            //实例化该对象
            Card3Dmodel = Instantiate(_AssetRes, _Point.position, _Point.rotation) as GameObject;
            float _zoom = _Artresourcedata.getArtresources_zoom();
            Card3Dmodel.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
            Card3Dmodel.transform.parent = _Point;
            //设置3D模型摩擦力
            Card3Dmodel.rigidbody.angularDrag = 1;
            Card3Dmodel.rigidbody.mass = 1;
            //_obj.transform.localScale = new Vector3(1.3f,1.3f,1.3f);
            Animation anim = Card3Dmodel.GetComponent<Animation>();
            if (anim == null)
                return;
            Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
            Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        }

        private void ModelCear()
        {
            if (Card3Dmodel != null)
                Destroy(Card3Dmodel);
        }

        public void AddMsgBox(string text)
        {
            DreamFaction.GameCore.InterfaceControler.GetInst().AddMsgBox(text);
        }

        public void ShowUI()
        {
            this.gameObject.SetActive(true);
        }

        public void HideUI()
        {
            ModelCear();
            this.gameObject.SetActive(false);
            UI_HeroListManager._instance.SetGridActive(true);
        }



    }
}
