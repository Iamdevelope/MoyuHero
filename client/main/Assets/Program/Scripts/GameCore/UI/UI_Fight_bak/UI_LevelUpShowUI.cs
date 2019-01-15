using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DreamFaction.Utils;

public class UI_LevelUpShowUI : BaseUI 
{
    public delegate void CallBack();
    private CallBack m_PlayCall;
    public static UI_LevelUpShowUI _inst;
    public static string UI_ResPath = "UI_General/UI_LevelUpWindow_1_1";
    private bool isClick = true;


    private Button _OkBtn;
    private Text _FrontLevel;
    private Text _FrontPower;
    private Text _FrontHero;
    private Text _FrontBag;
    private Text _CurLevel;
    private Text _CurPower;
    private Text _CurHero;
    private Text _CurBag;
    private Text m_PromptObjTxt;

    private Transform[] m_EffectRow = new Transform[3]; //每一行的闪光特效
    private Transform m_Level;
    private Transform m_Power;
    private Transform m_PowerMax;
   
    public override void InitUIData()
    {
        base.InitUIData();
        _inst = this;
        _OkBtn = selfTransform.FindChild("OKBtn").GetComponent<Button>();
        _FrontLevel = selfTransform.FindChild("Level/frontLevelTxt").GetComponent<Text>();
        _CurLevel = selfTransform.FindChild("Level/curLevelTxt").GetComponent<Text>();
        _FrontPower = selfTransform.FindChild("Power/frontPowerTxt").GetComponent<Text>();
        _CurPower = selfTransform.FindChild("Power/curPowerTxt").GetComponent<Text>();
        _FrontHero = selfTransform.FindChild("Hero/frontHeroTxt").GetComponent<Text>();
        _CurHero = selfTransform.FindChild("Hero/curHeroTxt").GetComponent<Text>();
        _FrontBag = selfTransform.FindChild("Bag/frontBagTxt").GetComponent<Text>();
        _CurBag = selfTransform.FindChild("Bag/curBagTxt").GetComponent<Text>();
        m_PromptObjTxt = selfTransform.FindChild("HintObj/Bottom/Text").GetComponent<Text>();
        m_Level = selfTransform.FindChild("Level");
        m_Power = selfTransform.FindChild("Power");
        m_PowerMax = selfTransform.FindChild("Hero");
        m_Level.gameObject.SetActive(false);
        m_Power.gameObject.SetActive(false);
        m_PowerMax.gameObject.SetActive(false);
        m_EffectRow[0] = selfTransform.FindChild("effects/1");
        m_EffectRow[1] = selfTransform.FindChild("effects/2");
        m_EffectRow[2] = selfTransform.FindChild("effects/3");
        for (int i = 0; i < m_EffectRow.Length; i++)
        {
            m_EffectRow[i].gameObject.SetActive(false);
        }


        _OkBtn.onClick.AddListener(new UnityAction(OnClose));
    }

    public override void InitUIView()
    {
        base.InitUIView();
        InitUI();
        //Invoke("OnClose",3);
    }

    //初始化UI
    void InitUI()
    {
        int _frontLevrel = ObjectSelf.GetInstance().GetPlayOldLevel();
        int _curLevel = (int)ObjectSelf.GetInstance().Level;
        _FrontLevel.text = _frontLevrel.ToString();
        _CurLevel.text = _curLevel.ToString();
        int _power = 0;
        int _hero = 0;
        int _bag = 0;
        //for (int i = _frontLevrel; i < _curLevel; i++)
        //{
        //    PlayerTemplate pRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(i);
        //    _power = pRow.getExtraAp() + _cofig.getInitial_ap_upper_limit(); 
        //    _hero += pRow.getExtraHeroPackset();
        //    _bag += pRow.getExtraCommonItemPackset();
        //}
        VipTemplate _vipData = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(ObjectSelf.GetInstance().VipLevel);
        GameConfig _cofig = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        PlayerTemplate _pRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(_frontLevrel);
        _power = _pRow.getExtraAp() + _cofig.getInitial_ap_upper_limit() + _vipData.getExtraAp();
        _FrontPower.text = _power.ToString();
        _CurPower.text = ObjectSelf.GetInstance().ActionPointMax.ToString();
        _hero = _cofig.getInitial_hero_packset() + _pRow.getExtraHeroPackset();
        _FrontHero.text = _hero.ToString();
        _CurHero.text = ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax().ToString();
        _bag = _cofig.getInitial_common_item_packset() + _pRow.getExtraCommonItemPackset();
        _FrontBag.text = _bag.ToString();
        _CurBag.text = ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax().ToString();
        
        int _apRecover = 0;
        if ((_curLevel - _frontLevrel) > 1)
        {
            for (int i = 0; i < (_curLevel - _frontLevrel); i++)
            {
                PlayerTemplate _nRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(_curLevel - i);
                _apRecover += _nRow.getApRecover();
            }
        }
        else
        {
            PlayerTemplate _nRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(_curLevel);
            _apRecover = _nRow.getApRecover();
        }
        string _text = string.Format(GameUtils.getString("fight_playerlevelup_content1"), _apRecover);
        m_PromptObjTxt.text = _text;

        //播放特效
        StartCoroutine(PlayEffectRow(0, 1.5f, () => { m_Level.gameObject.SetActive(true); }));
        StartCoroutine(PlayEffectRow(1, 2.0f, () => { m_Power.gameObject.SetActive(true); }));
        StartCoroutine(PlayEffectRow(2, 2.5f, () => { m_PowerMax.gameObject.SetActive(true); }));
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();
        if (isClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //if (EventSystem.current.IsPointerOverGameObject())
                //    return;
                OnClose();
                isClick = false;
            }
        }

    }

    void OnClose()
    {
        if (UI_FightControler.Inst != null)
        {
            if (UI_FightControler.Inst.GetIsSpecialStage())
            {
                UI_FightControler.Inst.SpecialStageSpecialTips();
            }
            if (UI_FightControler.Inst.GetIsMysteriousShop())
            {
                UI_FightControler.Inst.MysteriousShopSpecialTips();
            }
        }
        if (gameObject != null)
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }
    }
    IEnumerator PlayEffectRow(int index, float time, CallBack callback)
    {
        yield return new WaitForSeconds(time);
        m_EffectRow[index].gameObject.SetActive(true);
        if (m_EffectRow[index].GetComponentInChildren<ParticleSystem>() != null)
        {
            m_EffectRow[index].GetComponentInChildren<ParticleSystem>().Play();
        }
        if (callback != null)
        {
            callback();
        }
    }
}
