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
using DreamFaction.UI;
public class UI_HeroLevelupmanager : BaseUI
{
    public static UI_HeroLevelupmanager _instance;

    private Button mLevelup1;
    private Button mLevelup10;
    private Text mTitle;                            //称号
    private Text mName;                             //名字
    private Text mLevel;                            //等级
    private Text mNeedExp;                          //需要经验
    private Text mNeedDes;                          //经验描述
    private Slider mExpProgress;                    //经验条
    private Image mExpProOver;                      //经验条前背景
    private Image mAlignment;                       //阵营
    private Image mType1;                           //标示1
    private Image mType2;                           //标示2
    private Text allExpCrystal;                     //总的经验结晶
    private Text oneExpCrustal;                     //升一级所需的结晶
    private Text tenExpCrustal;                     //升十级所需的结晶
    private int m_HeroStar;
    public List<GameObject> starList = new List<GameObject>();
    public List<GameObject> starBgList = new List<GameObject>();
    private int mLevelNum;
    private ObjectCard objc;
    private Button LevelBack_btn;//等级提升面板返回按钮
    private int expSurplus;
    private int levelSurplus;
    private int expCrustalSurplus;
    private Transform MsgBoxGroup;
    private GameObject expEffects;
    private GameObject expEffectsMax;
    public bool isPlay = false;
    public bool isSlider = true;
    private bool isStar;
    private float time = 0;
    private float mNewExp;

    private bool isBanInput;  //是否禁用输入
    public override void InitUIData()
    {

        _instance = this;
        LevelBack_btn = selfTransform.FindChild("BackGround/Btn_back").GetComponent<Button>();
        mLevelup1 = selfTransform.FindChild("Menu/UP1").GetComponent<Button>();
        mLevelup10 = selfTransform.FindChild("Menu/UP10").GetComponent<Button>();
        mTitle = selfTransform.FindChild("HeroInfo/TitleName_txt").GetComponent<Text>();
        mName = selfTransform.FindChild("HeroInfo/PlayerName_Img/PlayerName_txt").GetComponent<Text>();
        mLevel = selfTransform.FindChild("HeroInfo/Level_txt").GetComponent<Text>();
        mNeedExp = selfTransform.FindChild("HeroInfo/Need/number").GetComponent<Text>();
        mNeedDes = selfTransform.FindChild("HeroInfo/Need/Text").GetComponent<Text>();
        mExpProgress = selfTransform.FindChild("HeroInfo/ExpSlider").GetComponent<Slider>();
        mExpProOver = selfTransform.FindChild("HeroInfo/ExpSlider/over").GetComponent<Image>();
        mAlignment = selfTransform.FindChild("HeroInfo/Level_Img").GetComponent<Image>();
        mType1 = selfTransform.FindChild("HeroInfo/PlayerType0_Img").GetComponent<Image>();
        mType2 = selfTransform.FindChild("HeroInfo/PlayerType1_Img").GetComponent<Image>();
        allExpCrystal = selfTransform.FindChild("Gem/Text").GetComponent<Text>();
        oneExpCrustal = selfTransform.FindChild("Menu/UP1/Num").GetComponent<Text>();
        tenExpCrustal = selfTransform.FindChild("Menu/UP10/Num").GetComponent<Text>();
        MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
        expEffects = selfTransform.FindChild("HeroInfo/ExpSlider/ExpIncrease01/OutLight").gameObject;
        expEffectsMax = selfTransform.FindChild("HeroInfo/ExpSlider/Max01").gameObject;

        mLevelup1.onClick.AddListener(new UnityEngine.Events.UnityAction(onLevelup1Call));
        mLevelup10.onClick.AddListener(new UnityEngine.Events.UnityAction(onLevelup10Call));
        LevelBack_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClose));
        GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroLevelUpSucceed, isStarSucceedShow);
        expEffects.SetActive(false);
        expEffectsMax.SetActive(false);
        isStar = true;
    }

    public override void InitUIView()
    {
        HeroModelBack.Inst.ChangePanel("HeroLvUP");
    }

    protected void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroLevelUpSucceed, isStarSucceedShow);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroLevelUpDefeat, isStarDefeat);

        HeroModelBack.Inst.ChangePanel("HeroBg");
    }
    public override void UpdateUIData()
    {
        base.UpdateUIData();
        if (isPlay)
        {

            if (mExpProgress.value == 100)
            {
                UI_EffectManager._instance.DisableEffect("LevelUp");
                //UI_EffectManager._instance.InstanceEffect_Link("LevelUp", UI_HeroListManager._instance.GetPoint());
                isSlider = true;
            }

        }
        if (mExpProgress.value == 0)
        {
            mExpProOver.gameObject.SetActive(false);
        }
        else
        {
            mExpProOver.gameObject.SetActive(true);
        }
    }

    public void UpdateShow(ObjectCard obj)
    {
        this.objc = obj;
        mNewExp = objc.GetHeroData().GetExpPercentage() * 100;
        HeroTemplate _HeroItem = objc.GetHeroRow();
        //称号显示
        ChsTextTemplate _title = new ChsTextTemplate();
        _title = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(_HeroItem.getTitleID());
        mTitle.text = _title.languageMap["Chinese"]; ;
        //名称显示
        ChsTextTemplate _name = new ChsTextTemplate();
        _name = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(_HeroItem.getNameID());
        mName.text = _name.languageMap["Chinese"];
        mLevel.text = obj.GetHeroData().Level.ToString();
        //星级
        m_HeroStar = _HeroItem.getQuality();
        int m_maxStar = _HeroItem.getMaxQuality();
        for (int i = 0; i < 5; i++)
        {
            starList[i].SetActive(i + 1 <= m_HeroStar);
            starBgList[i].SetActive(i + 1 <= m_maxStar);
        }
        InitHeroTypes(_HeroItem);
        //for (int i = 5; i < 10; ++i)
        //{
        //    if (i < 5 + m_HeroStar)
        //    {
        //        Image temp = selfTransform.FindChild("HeroInfo/Stars").GetChild(i).GetComponent<Image>();
        //        temp.enabled = true;
        //    }
        //    else
        //    {

        //        Image temp = selfTransform.FindChild("HeroInfo/Stars").GetChild(i).GetComponent<Image>();
        //        temp.enabled = false;
        //    }
        //}
        NeedExp();
        ExpCrustal();
    }
    //英雄种族等信息
    private void InitHeroTypes(HeroTemplate _HeroItem)
    {
        if (_HeroItem.getClientSignType()[0] == 0 && _HeroItem.getClientSignType()[1] == 0)//近战物理
        {
            mType1.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_06");
        }
        if (_HeroItem.getClientSignType()[0] == 0 && _HeroItem.getClientSignType()[1] == 1)//近战法术
        {
            mType1.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_05");
        }
        if (_HeroItem.getClientSignType()[0] == 1 && _HeroItem.getClientSignType()[1] == 0)//远程物理
        {
            mType1.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_04");
        }
        if (_HeroItem.getClientSignType()[0] == 1 && _HeroItem.getClientSignType()[1] == 1)//远程法术
        {
            mType1.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_07");
        }
        if (_HeroItem.getClientSignType()[2] == 0)//肉盾
        {
            mType2.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_02");
        }
        if (_HeroItem.getClientSignType()[2] == 1)//输出
        {
            mType2.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_03");
        }
        if (_HeroItem.getClientSignType()[2] == 2)//辅助
        {
            mType2.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_clientSignType_01");
        }
        if (_HeroItem.getCamp() == 1)//生灵
        {
            mAlignment.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_01");
        }
        if (_HeroItem.getCamp() == 2)//神抵
        {
            mAlignment.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_03");
        }
        if (_HeroItem.getCamp() == 3)//恶魔
        {
            mAlignment.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Zhongzu_02");
        }
    }

    public void NeedExp()
    {
        HeroTemplate _HeroItem = objc.GetHeroRow();
        int expid = _HeroItem.getExpNum() - 1;
        int level = objc.GetHeroData().Level;
        if (level != null)
        {
            if (level < _HeroItem.getMaxLevel())
            {
                HeroexpTemplate _HeroExp = (HeroexpTemplate)DataTemplate.GetInstance().m_HeroExpTable.getTableData(level);
                int needxp = _HeroExp.getExp()[expid]-objc.GetHeroData().Exp;
                mNeedExp.text = needxp.ToString();
            }
        }


    }
    //升级失败
    public void isStarDefeat()
    {
        ExpCrustal();
        NeedExp();
    }
    //升级成功
    public void isStarSucceedShow()
    {

        int distance = objc.GetHeroData().Level - int.Parse(mLevel.text);
        if (distance == 1)
        {
            if (objc.GetHeroData().Level <= (objc.GetHeroRow().getMaxLevel() + 1))
            {
                NeedExp();
                ExpCrustal();
                if (isSlider)
                {
                    StartCoroutine(SliderRun());
                } 
            }
        }
        else
        {
            if (objc.GetHeroData().Level <= (objc.GetHeroRow().getMaxLevel() + 10))
            {
                NeedExp();
                ExpCrustal();
                if (isSlider)
                {
                    StartCoroutine(SliderRun());
                } 
            }
        }

        isBanInput = false;//解禁输入
    }

    public void ExpCrustal()
    {
        int allExpNum = ObjectSelf.GetInstance().ExpFruit;
        allExpCrystal.text = allExpNum.ToString();
        int proportion = DataTemplate.GetInstance().m_GameConfig.getJingyanjiejing_to_jingyan();
        HeroTemplate _HeroItem = objc.GetHeroRow();
        mLevel.text = objc.GetHeroData().Level.ToString();
        int expid = _HeroItem.getExpNum() - 1;
        int level = int.Parse(mLevel.text);

        if (level < _HeroItem.getMaxLevel())
        {
            HeroexpTemplate _HeroExp = (HeroexpTemplate)DataTemplate.GetInstance().m_HeroExpTable.getTableData(level);
            int needxp = _HeroExp.getExp()[expid] - objc.GetHeroData().Exp;
            oneExpCrustal.text = needxp.ToString();
            int tenneedxp = 0;
            levelSurplus = 0;
            expSurplus = 0;
            expCrustalSurplus = 0;
            //距离当阶最大等级是否为10级
            if (level >= (_HeroItem.getMaxLevel() - 10))
            {
                for (int i = level; i < _HeroItem.getMaxLevel(); i++)
                {
                    HeroexpTemplate ten_HeroExp = (HeroexpTemplate)DataTemplate.GetInstance().m_HeroExpTable.getTableData(i);
                    tenneedxp += ten_HeroExp.getExp()[expid];
                }
            }
            else
            {
                for (int i = level; i < level + 10; i++)
                {
                    HeroexpTemplate ten_HeroExp = (HeroexpTemplate)DataTemplate.GetInstance().m_HeroExpTable.getTableData(i);
                    if (allExpNum < _HeroExp.getExp()[expid])
                    {
                        expSurplus = _HeroExp.getExp()[expid];
                        expCrustalSurplus = allExpNum;
                        levelSurplus = i;
                    }
                    allExpNum -= _HeroExp.getExp()[expid];
                    tenneedxp += ten_HeroExp.getExp()[expid];
                }
            }
            tenExpCrustal.text = (tenneedxp - objc.GetHeroData().Exp).ToString();
        }
        mExpProgress.value = (int)(objc.GetHeroData().GetExpPercentage() * 100);

        //等级到达当阶最大等级时 
        if (int.Parse(mLevel.text) >= _HeroItem.getMaxLevel())
        {

            mExpProgress.value = 100f;
            mLevelup1.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_07");
            mLevelup10.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_07");
            oneExpCrustal.text = "------";
            tenExpCrustal.text = "------";
            //mExpProOver.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_08");
            mExpProgress.value = 99.9f;
            mNeedDes.text = GameUtils.getString("hero_levelup_content1"); //"已达到当阶最高级"
            expEffectsMax.SetActive(true);
            mNeedExp.gameObject.SetActive(false);
            //sPlay = false;

        }
        //升一级经验结晶不足
        else if( int.Parse(allExpCrystal.text) == 0 || int.Parse(oneExpCrustal.text) > int.Parse(allExpCrystal.text))
        {
            mLevelup1.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_07");
            mLevelup10.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_07");
        }
         //升10级经验结晶不足
        else if (int.Parse(allExpCrystal.text) == 0 || int.Parse(tenExpCrustal.text) > int.Parse(allExpCrystal.text))
        {
            mLevelup10.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_07");
        }
        else
        {
            isPlay = true;
            //mExpProOver.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_HeroInfo_01");
            mNeedDes.text = GameUtils.getString("hero_levelup_content2"); //"升级所需经验"
            mNeedExp.gameObject.SetActive(true);
            oneExpCrustal.gameObject.SetActive(true);
            tenExpCrustal.gameObject.SetActive(true);
            expEffectsMax.SetActive(false);
            //结晶数为0时
            if (int.Parse(allExpCrystal.text) == 0)
            {
                mLevelup1.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_07");
                mLevelup10.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_07");
            }
            else
            {
                mLevelup1.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_04");
                mLevelup10.gameObject.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Up_04");
            }
        }
    }


    private void onLevelup1Call()
    {
        HeroTemplate _HeroItem = objc.GetHeroRow();
        if (_HeroItem.getMaxLevel() == int.Parse(mLevel.text))
        {
            AddMsgBox(GameUtils.getString("hero_levelup_content4"));//"已达到当阶满级"
        }
        else if (int.Parse(allExpCrystal.text) == 0 || int.Parse(oneExpCrustal.text) > int.Parse(allExpCrystal.text))
        {
            AddMsgBox(GameUtils.getString("hero_levelup_content3")); //"经验结晶不足，无法升级"
        }
        else if (objc.GetHeroData().Level <= _HeroItem.getMaxLevel())
        {
            if(!isBanInput)
            LevelUP(1);
        }
    }
    private void onLevelup10Call()
    {

        //新手引导相关 点击【升10级】
        if (GuideManager.GetInstance().GetBackCount(200503))
        {
            GuideManager.GetInstance().ShowGuideWithIndex(200504);
        }
        //DisabledBtn();
        HeroTemplate _HeroItem = objc.GetHeroRow();
        if (int.Parse(mLevel.text) == _HeroItem.getMaxLevel())
        {
            AddMsgBox(GameUtils.getString("hero_levelup_content4"));//"已达到当阶满级"
        }
        else if (int.Parse(allExpCrystal.text) == 0 || int.Parse(tenExpCrustal.text) > int.Parse(allExpCrystal.text))
        {
            AddMsgBox(GameUtils.getString("hero_levelup_content3")); //"经验结晶不足，无法升级"
        }
        else if (objc.GetHeroData().Level <= _HeroItem.getMaxLevel())
        {
            //DisabledBtn()
            if (!isBanInput) 
            LevelUP(10);
        }
    }
    private void LevelUP(byte distance)
    {
        CHeroLevelUpSpeed clus = new CHeroLevelUpSpeed();
        clus.levelnum = distance;
        clus.herokey = (int)objc.GetGuid().GUID_value;
        IOControler.GetInstance().SendProtocol(clus);
        isBanInput = true;//禁止输入
    }


    // public void SliderRun()
    IEnumerator SliderRun()
    {
        isSlider = false;
       // mLevel.text = objc.GetHeroData().Level.ToString();
        expEffects.SetActive(true);
        float max = mExpProgress.maxValue + 1;
        float min = 0;
        if (isStar)
        {
            min = mNewExp;
            isStar = false;
        }
        else
        {
            min = mExpProgress.minValue + 1;
        }
        for (; ; min += 1f)
        {
            if (min > max)
            {
                mExpProgress.value = 0;
                mLevelNum = 0;
                break;
            }
            else
            {
                //min = objc.GetHeroData().GetExpPercentage();
                mExpProgress.value = min;
            }

            yield return new WaitForSeconds(0.001f);

        }
        NeedExp();
        ExpCrustal();
        StopCoroutine(SliderRun());
        expEffects.SetActive(false);
        //if (int.Parse(mLevel.text) >= objc.GetHeroRow().getMaxLevel())
        //{
        //    if (mExpProgress.value==100)
        //    {
        //        UI_EffectManager._instance.DisableEffect("LevelUp");
        //        UI_EffectManager._instance.InstanceEffect_Link("LevelUp", UI_HeroListManager._instance.GetPoint());
        //    }            
        //}
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
        Destroy(this.gameObject);
    }


    void OnClose()
    {
        isSlider = true;
        expEffects.SetActive(false);
        expEffectsMax.SetActive(false);
        UI_EffectManager._instance.DisableEffect("LevelUp");
        UI_HeroListManager._instance.SetGridActive(true);
        UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = false;
        HideUI();
        //新手引导相关 引导英雄进阶（强制）
        if (GuideManager.GetInstance().GetBackCount(200504))
        {
            GuideManager.GetInstance().ShowGuideWithIndex(200601);
        }
    }

}
