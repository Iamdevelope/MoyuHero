using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork.Data;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using System.Collections.Generic;
using GNET;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;

public class UI_HeroBringUp : UI_HeroBringUpBase
{
    private HeroData m_HeroData;
    private HeroTemplate m_HeroDataT;
    private HerocultureTemplate m_CurTData;
    private HerocultureTemplate m_NextTData;
    private int m_ElementLevel;
    private int m_MaxElementLevel;//当前资质，定位，元素类型下 的最大等级
    private string m_ElementName;//当前元素的名字

    protected List<ArticleItem> mPropArticleList = null;

    public override void InitUIData()
    {
        base.InitUIData();
        mPropArticleList = new List<ArticleItem>();
        GameEventDispatcher.Inst.addEventListener(GameEventID.HE_PeiyangUp, BringUpSuccess);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_addPowText.text = m_ElementName + GameUtils.getString("ui_yingxiongqianghua_peiyang16");
        m_NextLevelText.text = GameUtils.getString("ui_yingxiongqianghua_peiyang6");
        m_ResetFont.text = GameUtils.getString("ui_yingxiongqianghua_peiyang7");
        m_BringUpFont.text = GameUtils.getString("ui_yingxiongqianghua_peiyang1");
    }


    public override void ShowHeroInfo(ObjectCard objectCard)
    {
        m_UpEffect.SetActive(false);

        m_HeroData = objectCard.GetHeroData();
        m_HeroDataT = objectCard.GetHeroRow();

        switch (ObjectSelf.GetInstance().CurBringType)
        {
            case Bring_Type.HUO:
                OnClickHuoButton();
                break;
            case Bring_Type.EARTH:
                OnClickEarthButton();
                break;
            case Bring_Type.WATER:
                OnClickWaterButton();
                break;
            case Bring_Type.WIND:
                OnClickWindButton();
                break;
        }
        SetCurElementName();
    }

    private void SetCurElementName()
    {
        switch (ObjectSelf.GetInstance().CurBringType)
        {
            case Bring_Type.HUO:
                m_ElementName = GameUtils.getString("ui_yingxiongqianghua_peiyang2");
                break;
            case Bring_Type.EARTH:
                m_ElementName = GameUtils.getString("ui_yingxiongqianghua_peiyang4");
                break;
            case Bring_Type.WATER:
                m_ElementName = GameUtils.getString("ui_yingxiongqianghua_peiyang3");
                break;
            case Bring_Type.WIND:
                m_ElementName = GameUtils.getString("ui_yingxiongqianghua_peiyang5");
                break;
        }
    }

    /// <summary>
    /// 获取对应的表数据
    /// </summary>
    /// <param name="id">1火，2土，3水，4风</param>
    /// /// <param>英雄数据中 是存在数组中的 所以相应的id + 1</param>
    private void GetTData(int id)
    {
        if (m_HeroData == null && m_HeroDataT == null)
            return;

        HeroTrainDB.ENUM_TRAIN_TYPE type = (HeroTrainDB.ENUM_TRAIN_TYPE)(id);
        m_ElementLevel = m_HeroData.HeroTrainDB.GetTrainLevForType(type);
        m_MaxElementLevel = GameUtils.GetCurCultureTData(m_HeroDataT.getBorn(), m_HeroDataT.getQosition(), id + 1);
        m_CurTData = GameUtils.GetCurCultureTData(m_HeroDataT.getBorn(), m_HeroDataT.getQosition(), id + 1, m_ElementLevel);
        m_NextTData = GameUtils.GetCurCultureTData(m_HeroDataT.getBorn(), m_HeroDataT.getQosition(), id + 1, m_ElementLevel + 1);

        ClearUp();
        ShowElementLevel();
        ShowElementAddValue();
        GreatArticleItem();
        BringButtonActive();
    }

    private void ShowElementLevel()
    {
        if (m_FourLvList.Count == 4)
        {
            for (int i = 0; i < m_HeroData.HeroTrainDB.TrainLevel.Length; i++)
            {
                m_FourLvList[i].text = "Lv." + m_HeroData.HeroTrainDB.TrainLevel[i].ToString();
            }
            
        }
    }

    private void ShowElementAddValue()
    {
        m_ElementMaxLevel.text = GameUtils.getString("ui_yingxiongqianghua_peiyang14").Replace("{0}", m_ElementName);
        m_NextLevelText.transform.parent.gameObject.SetActive(true);
        m_WuPingitem.transform.parent.gameObject.SetActive(true);
        m_ElementMaxLevel.gameObject.SetActive(false);

        if (m_ElementLevel == 0)
        {
            if (m_NextTData == null)
                return;

            for (int i = 0; i < m_NextTData.getAttribute().Length; i++)
            {
                if (i > 1)
                    continue;
                string type = GameUtils.GetAttriName(m_NextTData.getAttribute()[i]);
                if (i == 0)
                {
                    m_CurAtrributesitemText_0.text = type + " +" + "0";
                    m_NextAtrributesitemText_0.text = type + " +" + m_NextTData.getValue()[i].ToString();
                }
                if (i == 1)
                {
                    m_CurAtrributesitemText_1.text = type + " +" + "0";
                    m_NextAtrributesitemText_1.text = type + " +" + m_NextTData.getValue()[i].ToString();
                }
            }
        }
        else if (m_ElementLevel + 1 > m_MaxElementLevel)
        {
            if (m_CurTData == null)
                return;

            for (int i = 0; i < m_CurTData.getAttribute().Length; i++)
            {
                if (i > 1)
                    continue;
                string type = GameUtils.GetAttriName(m_CurTData.getAttribute()[i]);
                if (i == 0)
                    m_CurAtrributesitemText_0.text = type + " +" + m_CurTData.getValue()[i].ToString();
                if (i == 1)
                    m_CurAtrributesitemText_1.text = type + " +" + m_CurTData.getValue()[i].ToString();
            }

            m_NextLevelText.transform.parent.gameObject.SetActive(false);
            m_WuPingitem.transform.parent.gameObject.SetActive(false);
            m_ElementMaxLevel.gameObject.SetActive(true);
        }
        else
        {
            if (m_CurTData == null || m_NextTData == null)
                return;

            for (int i = 0; i < m_CurTData.getAttribute().Length;i++ )
            {
                if (i > 1)
                    continue;
                string type = GameUtils.GetAttriName(m_CurTData.getAttribute()[i]);
                if(i == 0)
                    m_CurAtrributesitemText_0.text = type + " +" + m_CurTData.getValue()[i].ToString();
                if (i == 1)
                    m_CurAtrributesitemText_1.text = type + " +" + m_CurTData.getValue()[i].ToString();
            }

            for (int i = 0; i < m_NextTData.getAttribute().Length; i++)
            {
                if (i > 1)
                    continue;
                string type = GameUtils.GetAttriName(m_NextTData.getAttribute()[i]);
                if (i == 0)
                    m_NextAtrributesitemText_0.text = type + " +" + (m_NextTData.getValue()[i] - m_CurTData.getValue()[i]).ToString();
                if (i == 1)
                    m_NextAtrributesitemText_1.text = type + " +" + (m_NextTData.getValue()[i] - m_CurTData.getValue()[i]).ToString();
            }
        }
    }

    private void GreatArticleItem()
    {
        if (m_ElementLevel + 1 > m_MaxElementLevel || m_NextTData == null)
            return;

        foreach (ArticleItem articleItem in mPropArticleList)
        {
            articleItem.Destroy();
        }
        mPropArticleList.Clear();

        for (int i = 0; i < 1; i++)
        {
            mPropArticleList.Add(CreateNullArticleUI());
        }

        ArticleItem ui_item = null;

        for (int i = 0; i < mPropArticleList.Count; i++)
        {
            ui_item = mPropArticleList[i];

            if (ui_item == null)
                continue;

            int id = m_NextTData.getConsumption();
            int haveNum = -1;

            if (id == 1400000003)//圣灵之泉 不是道具
                haveNum = ObjectSelf.GetInstance().HeroMoney;
            else
                haveNum = GetIdInBagNum(id);

            int needNum = m_NextTData.getNumber();
            if (haveNum >= 0)
            {
                ui_item.SetInfo(id, haveNum, needNum);
                ui_item.SetActive(true);
            }
        }
    }
    /// <summary>
    /// 获取指定id的物品在背包中的数量
    /// </summary>
    /// <param name="id"></param>
    private int GetIdInBagNum(int id)
    {
        int haveNum = -1;
        ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id, ref haveNum);
        return haveNum;
    }

    /// <summary>
    /// 培养按钮是否置灰
    /// </summary>
    private void BringButtonActive()
    {
        if (m_ElementLevel + 1 > m_MaxElementLevel)
        {
            GameUtils.SetBtnSpriteGrayState(m_BringButton, true);
            m_BringButton.enabled = false;
        }
        else
        {
            GameUtils.SetBtnSpriteGrayState(m_BringButton, false);
            m_BringButton.enabled = true;
        }
    }

    ArticleItem CreateNullArticleUI()
    {
        Transform trans = (Transform)GameObject.Instantiate(m_WuPingitem.transform);
        if (trans == null)
            return null;

        trans.parent = m_WuPingitem.transform.parent;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);
        return new ArticleItem(trans);
    }

    protected override void OnClickHuoButton()
    {
        ObjectSelf.GetInstance().CurBringType = Bring_Type.HUO;
        GetTData((int)ObjectSelf.GetInstance().CurBringType);
        SetCurElementName();
        m_LightPoint.transform.localPosition = new UnityEngine.Vector3(m_HuoButton.transform.GetComponent<RectTransform>().anchoredPosition.x, m_LightPoint.transform.localPosition.y,0);
        m_UpEffect.transform.localPosition = new UnityEngine.Vector3(m_HuoButton.transform.GetComponent<RectTransform>().anchoredPosition.x, m_UpEffect.transform.localPosition.y, 0);
    }

    protected override void OnClickEarthButton() 
    {
        ObjectSelf.GetInstance().CurBringType = Bring_Type.EARTH;
        GetTData((int)ObjectSelf.GetInstance().CurBringType);
        SetCurElementName();
        m_LightPoint.transform.localPosition = new UnityEngine.Vector3(m_EarthButton.transform.GetComponent<RectTransform>().anchoredPosition.x, m_LightPoint.transform.localPosition.y, 0);
        m_UpEffect.transform.localPosition = new UnityEngine.Vector3(m_EarthButton.transform.GetComponent<RectTransform>().anchoredPosition.x, m_UpEffect.transform.localPosition.y, 0);
    }

    protected override void OnClickWaterButton()
    {
        ObjectSelf.GetInstance().CurBringType = Bring_Type.WATER;
        GetTData((int)ObjectSelf.GetInstance().CurBringType);
        SetCurElementName();
        m_LightPoint.transform.localPosition = new UnityEngine.Vector3(m_WaterButton.transform.GetComponent<RectTransform>().anchoredPosition.x, m_LightPoint.transform.localPosition.y, 0);
        m_UpEffect.transform.localPosition = new UnityEngine.Vector3(m_WaterButton.transform.GetComponent<RectTransform>().anchoredPosition.x, m_UpEffect.transform.localPosition.y, 0);
    }

    protected override void OnClickWindButton()
    {
        ObjectSelf.GetInstance().CurBringType = Bring_Type.WIND;
        GetTData((int)ObjectSelf.GetInstance().CurBringType);
        SetCurElementName();
        m_LightPoint.transform.localPosition = new UnityEngine.Vector3(m_WindButton.transform.GetComponent<RectTransform>().anchoredPosition.x, m_LightPoint.transform.localPosition.y, 0);
        m_UpEffect.transform.localPosition = new UnityEngine.Vector3(m_WindButton.transform.GetComponent<RectTransform>().anchoredPosition.x, m_UpEffect.transform.localPosition.y, 0);
    }

    protected override void OnClickResetButton()
    {
        if (m_ElementLevel == 0)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_peiyang13"), this.gameObject.transform);//至少送一名5;<image res ='Sprites/Ui_jiesuan_02' height='50' width='50'/>;英雄;"重置<color=#ff0000>" + m_ElementName + "</color>" + "培养，需花费;" + "<image res ='Sprites/zuanshi' height='50' width='50'/>;50钻石"
            return;
        }
        UICommonManager.Inst.ShowMsgBox(GameUtils.getString("ui_yingxiongqianghua_peiyang10"), GameUtils.getString("ui_yingxiongqianghua_peiyang11").Replace("{0}", m_ElementName), GameUtils.getString("ui_yingxiongqianghua_peiyang12").Replace("{0}", m_ElementName), GameUtils.getString("ui_yingxiongqianghua_peiyang7"), ResetHandle, null, null);
        //UICommon_MsgBox msg = UICommonManager.Inst.ShowMsgBox("重置培养","aa","bb","重置",ResetHandle,null,null);
        //msg.SetData();
    }

    private void ResetHandle(object data)
    {
        CPeiyangHero chero = new CPeiyangHero();
        chero.herokey = (int)m_HeroData.GUID.GUID_value;
        chero.slotnum = (byte)((int)ObjectSelf.GetInstance().CurBringType + 1);
        chero.isreset = (byte)1;
        IOControler.GetInstance().SendProtocol(chero);
    }

    protected override void OnClickBringButton()
    {
        if (m_ElementLevel + 1 > m_MaxElementLevel)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_peiyang9"), this.gameObject.transform);
            return;
        }

        int id = m_NextTData.getConsumption();
        int haveNum = -1;

        if (m_NextTData.getConsumption() == 1400000003)//圣灵之泉 不是道具
            haveNum = ObjectSelf.GetInstance().HeroMoney;
        else
            haveNum = GetIdInBagNum(id);

        int needNum = m_NextTData.getNumber();
        if (haveNum < needNum)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_peiyang8"), this.gameObject.transform);
            return;
        }

        CPeiyangHero chero = new CPeiyangHero();
        chero.herokey = (int)m_HeroData.GUID.GUID_value;
        chero.slotnum = (byte)((int)ObjectSelf.GetInstance().CurBringType + 1);
        chero.isreset = (byte)0;
        IOControler.GetInstance().SendProtocol(chero);
    }

    public void BringUpSuccess()
    {
        m_UpEffect.SetActive(false);
        m_UpEffect.SetActive(true);
        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_peiyang15"), this.gameObject.transform);
    }

    protected void ClearUp()
    { 
        m_CurAtrributesitemText_0.text = "";
        m_CurAtrributesitemText_1.text ="";
        m_NextAtrributesitemText_0.text = "";
        m_NextAtrributesitemText_1.text = "";
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_PeiyangUp, BringUpSuccess);
    }
}
