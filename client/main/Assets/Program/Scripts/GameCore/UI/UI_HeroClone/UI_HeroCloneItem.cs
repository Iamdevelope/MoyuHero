using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using UnityEngine.UI;
using DreamFaction.UI;
using GNET;
using DreamFaction.GameCore;

public class UI_HeroCloneItem : UI_HeroCloneItemBase
{
    private List<int> m_CanHeroCloneIds;        //当前开启的克隆英雄
    private HerocloneTemplate m_HeroCloneData;  //当前的克隆英雄数据
    private HeroTemplate m_HeroData;            //当前的英雄的数据
    private ArtresourceTemplate m_AssetData;    //当前资源数据
    private bool isOpen;                        //是否开启
    private bool isInjectOk;                    //是否注入成功
    private bool isPress;                       //是否按下
    private int m_HeroId;                       //当前英雄ID
    private int m_SortId = 0;                   //排序Id
    private int m_CloneConAssetId = 0;          //克隆消耗资源ID
    private int m_CloneConAssetNum = 0;         //克隆消耗资源数量
    private string m_OpenCloneDes;              //克隆英雄的描述
    private int m_OpenWantAssetId;              //开启英雄所需要的资源ID
    private GameObject m_OpenStateObj;          //开启状态的UI显示
    private GameObject m_NotOpenStateObj;       //未开启状态的UI显示
    private Image m_HeroIcon;                   //克隆英雄的头像
    private Slider m_ProgressBar;               //注入进度
    private int m_TempNum = 0;                  //记数
    private GameObject m_OpenState;             //是否开启文本               

    private EventTriggerListener mEventTrigger = null;
    public int GetHeroId() { return m_HeroId; }



    public override void InitUIData()
    {
        base.InitUIData();
        m_OpenStateObj = selfTransform.FindChild("OpenStateObj").gameObject;
        m_NotOpenStateObj = selfTransform.FindChild("NotOpenStateObj").gameObject;
        m_OpenState = selfTransform.FindChild("SelectHeroInfo/OpenState").gameObject;
        m_HeroIcon = selfTransform.FindChild("SelectHeroInfo/Icon").GetComponent<Image>();
        m_ProgressBar = selfTransform.FindChild("OpenStateObj/CloneBar").GetComponent<Slider>();
        m_CanHeroCloneIds = ObjectSelf.GetInstance().GetHeroCloneList();
        
        mEventTrigger = EventTriggerListener.Get(m_InjectBtn.gameObject);
        mEventTrigger.InitPressInterval = 0.1f;
        mEventTrigger.needResetInterval = true;
        mEventTrigger.onPress = OnProgressForward;
        mEventTrigger.onDown = OnProgressDown;
        mEventTrigger.onUp = OnProgressBack;

        //EventTriggerListener.Get(m_InjectBtn.gameObject).pressInterval = 0.1f;
    }


    /// <summary>
    /// 初始化数据显示
    /// </summary>
    /// <param name="heroCloneData"></param>
    public void InitHeroCloneItemData(HerocloneTemplate  heroCloneData)
    {
        m_HeroCloneData = heroCloneData;
        m_HeroId = heroCloneData.getId();
        m_HeroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(m_HeroId);
        m_AssetData = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(m_HeroData.getArtresources());
       
        
        InitHeroCloneUIData();
        InitHeroCloneStateUI();
            
    }
    /// <summary>
    /// 初始化状态显示
    /// </summary>
    private void InitHeroCloneStateUI()
    {
        if (m_CanHeroCloneIds.Contains(m_HeroId))
        {
            m_OpenStateObj.SetActive(true);
            m_OpenState.SetActive(false);
            m_NotOpenStateObj.SetActive(false);
            //GameUtils.SetImageGrayState(m_HeroIcon, false);
            m_HeroIcon.color = Color.white;
        }
        else
        {
            m_OpenStateObj.SetActive(false);
            m_OpenState.SetActive(true);
            m_NotOpenStateObj.SetActive(true);
            //GameUtils.SetImageGrayState(m_HeroIcon, true);
            m_HeroIcon.color = Color.gray;
        }
    }
    /// <summary>
    /// 初始化数据显示
    /// </summary>
    private void InitHeroCloneUIData()
    {
        m_CloneConAssetId = m_HeroCloneData.getCloneCostId();
        m_CloneConAssetNum = m_HeroCloneData.getCloneCostValue();
        m_OpenCloneDes = GameUtils.getString(m_HeroCloneData.getOpenConditionDes());
        m_HeroName.text = GameUtils.getString(m_HeroData.getTitleID());
        m_EliteNum.text = m_CloneConAssetNum.ToString();
        m_OpenTjTxt.text = m_OpenCloneDes;
        m_HeroIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_AssetData.getHeadiconresource());
        
    }

    public override void UpdateUIView()
    {
        JudgeCosState();
    }
    /// <summary>
    /// 判断是否注入成功
    /// </summary>
    private void JudgeCosState()
    {
        if (isPress)
        {
            if (m_CloneConAssetNum <= UI_HeroCloneManager.Inst.GetHeroCloneConAssetNum(m_CloneConAssetId))
            {
                if (m_TempNum == m_CloneConAssetNum)
                {
                    //Debug.Log("is inject ok !");
                    isInjectOk = true;
                    OnInjectOKMsg();
                }
            }
            if (!isInjectOk)
            {
                if (m_TempNum < m_CloneConAssetNum || UI_HeroCloneManager.Inst.isNotCos)
                {
                    //Debug.Log("is inject error !");
                    isInjectOk = false;
                }
            }
        }
    }
    /// <summary>
    /// 注入成功
    /// </summary>
    private void OnInjectOKMsg()
    {
        CHeroClone _chc = new CHeroClone();
        _chc.heroid = m_HeroId;
        IOControler.GetInstance().SendProtocol(_chc);
        isPress = false;
    }
    /// <summary>
    /// 按下注入
    /// </summary>
    /// <param name="go"></param>
    private void OnProgressForward(GameObject go)
    {
        isInjectOk = false;
        //isPress = false;
        if (m_TempNum <= m_CloneConAssetNum)
        {
            if (m_TempNum == m_CloneConAssetNum)
                return;
            UI_HeroCloneManager.Inst.AnalogCos(m_TempNum);
            if (UI_HeroCloneManager.Inst.isNotCos)
                return;

            isPress = true;
            m_TempNum++;
            float _tempNum = (float)m_TempNum;
            float _value = _tempNum / m_CloneConAssetNum;
            m_ProgressBar.value = _value;
            m_EliteNum.text = (m_CloneConAssetNum - m_TempNum).ToString();

            float _interval = mEventTrigger.pressInterval;
            mEventTrigger.pressInterval = Mathf.Max(0.01f, _interval - 0.02f);
        }
    }


    /// <summary>
    /// 弹起注入
    /// </summary>
    /// <param name="go"></param>
    private void OnProgressBack(GameObject go)
    {
        isPress = false;
        if (!isInjectOk)
        {
            m_ProgressBar.value = 0f;
            UI_HeroCloneManager.Inst.isNotCos = false;
        }
        m_TempNum = 0;
        UpdateCosData();
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    public void UpdateCosData()
    {
        m_ProgressBar.value = 0f;
        InitHeroCloneUIData();
        UI_HeroCloneManager.Inst.InitHeroCloneConAsset();
    }


    /// <summary>
    /// 英雄头像按钮
    /// </summary>
    protected override void OnClickHeroIconBtn()
    {
        UI_HomeControler.Inst.AddUI(HeroInfoPop.UI_ResPath);
        HeroInfoPop.inst.SetShowData(m_HeroData);
    }

    /// <summary>
    /// 点下注入按钮  如消耗品不足弹出提示
    /// </summary>
    private void OnProgressDown(GameObject go)
    {
        if (UI_HeroCloneManager.Inst.GetHeroCloneConAssetNum(m_CloneConAssetId) <= 0)
        {
            string _text = GameUtils.getString("heroclone_content6");
            InterfaceControler.GetInst().AddMsgBox(_text, transform.parent.parent.parent, 1);
        }
    }

}
