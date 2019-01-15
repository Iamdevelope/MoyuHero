using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork.Data;
using DreamFaction.Utils;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.UI.Core;
using System;

public class UI_Advanced : UIAdvancedBase 
{
    private HeroaddstageTemplate m_CurTData;
    private HeroaddstageTemplate m_NextTData;
    private HeroData m_HeroData;
    private HeroTemplate m_HeroDataT;

    protected List<AttriItem> mPropAttributeList = null;
    protected List<ArticleItem> mPropArticleList = null;

    private HeroaddstageTemplate m_CurPopWinTData;//弹出所用的当前数据
    private HeroaddstageTemplate m_NextPopWinTData;//弹出所用的下一级数据

    public override void InitUIData()
    {
        base.InitUIData();
        mPropAttributeList = new List<AttriItem>();
        mPropArticleList = new List<ArticleItem>();
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_AdvancedSuccess, AdvancedSuccess);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_ButtonJinJieText.text = GameUtils.getString("ui_yingxiongqianghua_jinjie1");
        m_MaxLevelTipText.text = GameUtils.getString("ui_yingxiongqianghua_jinjie6");
    }

    public override void UpdateUIView()
    {
        int curStarMaxRank = GameUtils.GetCurStarMaxHalosPn(m_HeroDataT.getBorn(), m_HeroDataT.getQosition(), m_HeroData.StarLevel);//当前星级的最大阶数
        if (m_HeroData.CurStage >= curStarMaxRank)
        {
            NoAdvancedEffect.localPosition = new Vector3(m_GrayGO[0].GetComponent<RectTransform>().anchoredPosition.x, NoAdvancedEffect.localPosition.y, NoAdvancedEffect.localPosition.z);
            YesAdvancedEffect.localPosition = new Vector3(m_GrayGO[0].GetComponent<RectTransform>().anchoredPosition.x, YesAdvancedEffect.localPosition.y, YesAdvancedEffect.localPosition.z);
            NoAdvancedEffect.gameObject.SetActive(false);
        }
        else
        {
            NoAdvancedEffect.localPosition = new Vector3(m_GrayGO[m_HeroData.CurStage].GetComponent<RectTransform>().anchoredPosition.x, NoAdvancedEffect.localPosition.y, NoAdvancedEffect.localPosition.z);
            YesAdvancedEffect.localPosition = new Vector3(m_GrayGO[m_HeroData.CurStage].GetComponent<RectTransform>().anchoredPosition.x, YesAdvancedEffect.localPosition.y, YesAdvancedEffect.localPosition.z);
            NoAdvancedEffect.gameObject.SetActive(true);
        }  
    }

    /// <summary>
    /// 显示星级，阶数
    /// </summary>
    private void InitShowStarAndStage()
    {
        m_CurStartHalosPnText.text = GameUtils.getString("ui_yingxiongqianghua_jinjie2").Replace("{0}", m_HeroData.StarLevel.ToString()).Replace("{1}", m_HeroData.CurStage.ToString());
        m_NextStartHalosPnText.text = GameUtils.getString("ui_yingxiongqianghua_jinjie2").Replace("{0}", m_NextTData.getQuality().ToString()).Replace("{1}", m_NextTData.getHalosPn().ToString());
        //m_NextStartHalosPnInfoText.text = GameUtils.getString("ui_yingxiongqianghua_jinjie4") + GameUtils.getString("ui_yingxiongqianghua_jinjie2").Replace("{0}", m_NextTData.getQuality().ToString()).Replace("{1}", m_NextTData.getHalosPn().ToString());
        m_NextStartHalosPnInfoText.text = GameUtils.getString("ui_yingxiongqianghua_jinjie7");
        m_SpendText.text = m_NextTData.getGold().ToString();
        m_LevelOpenText.text = "";

        int curStarMaxRank = GameUtils.GetCurStarMaxHalosPn(m_HeroDataT.getBorn(), m_HeroDataT.getQosition(), m_HeroData.StarLevel);//当前星级的最大阶数
        for (int i = 0; i < m_GrayGO.Count;i++ )
        {
             m_GrayGO[i].SetActive(i < curStarMaxRank);          
        }
        for (int i = 0; i < m_LightGO.Count; i++)
        {
            m_LightGO[i].SetActive(i < m_HeroData.CurStage);
        }
        for (int i = 0; i < m_StarGO.Count; i++)
        {
            m_StarGO[i].SetActive(false);
            m_StarGO[i].SetActive(i < m_HeroData.StarLevel);
        }
     
    }

    /// <summary>
    /// 创建属性的Item
    /// </summary>
    private void GreatAttributeItem()
    {
        foreach (AttriItem attrItem in mPropAttributeList)
        {
            attrItem.Destroy();
        }
        mPropAttributeList.Clear();

        for (int i = 0; i < m_NextTData.getAttribute().Length + 1; i++)
        {
            mPropAttributeList.Add(CreateNullAttriUI());
        }

        AttriItem ui_item = null;

        //加战斗力属性
        ui_item = mPropAttributeList[0];
        if (ui_item != null)
        {
            bool isNoLevel = false;
            if (m_HeroData.StarLevel == 0 && m_HeroData.CurStage == 0)
                isNoLevel = true;
            float num = 0;
            GameConfig _cofig = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
            for (int i = 0;i<3;i++)
            {
                if (i == 0)
                {
                    if (isNoLevel)
                        num += _cofig.getCombat_attack_factor() * m_NextTData.getValue()[0];
                    else
                        num += _cofig.getCombat_defense_factor() * (m_NextTData.getValue()[0] - m_CurTData.getValue()[0]);
                }
                if (i == 1)
                {
                    if (isNoLevel)
                        num += _cofig.getCombat_defense_factor() * m_NextTData.getValue()[1];
                    else
                        num += _cofig.getCombat_defense_factor() * (m_NextTData.getValue()[1] - m_CurTData.getValue()[1]);
                }
                if (i == 2)
                {
                    if (isNoLevel)
                        num += _cofig.getCombat_blood_factor() * m_NextTData.getValue()[2];
                    else
                        num += _cofig.getCombat_blood_factor() * (m_NextTData.getValue()[2] - m_CurTData.getValue()[2]);
                }
            }
            //string type = GameUtils.GetAttriName();
            ui_item.SetInfo("战斗力", "+" + Math.Floor(num));
            ui_item.SetActive(true);
        }


        for (int i = 1; i < mPropAttributeList.Count; i++)
        {
            ui_item = mPropAttributeList[i];

            if (ui_item == null)
                continue;

            string type = GameUtils.GetAttriName(m_NextTData.getAttribute()[i - 1]);

            int num_Cur;
            if (m_HeroData.StarLevel == 0 && m_HeroData.CurStage == 0)
                num_Cur = 0;
            else
                num_Cur = m_CurTData.getValue()[i - 1];

            int num_Nex = m_NextTData.getValue()[i -1];
            if (num_Nex - num_Cur > 0)
            {
                ui_item.SetInfo(type, "+" + (num_Nex - num_Cur).ToString());
                ui_item.SetActive(true);
            }
        }
    }

    AttriItem CreateNullAttriUI()
    {
        Transform trans = (Transform)GameObject.Instantiate(m_Atrributesitem.transform);
        if (trans == null)
            return null;

        trans.parent = m_Atrributesitem.transform.parent;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);
        return new AttriItem(trans);
    }

    private void GreatArticleItem()
    {
        foreach (ArticleItem articleItem in mPropArticleList)
        {
            articleItem.Destroy();
        }
        mPropArticleList.Clear();

        for (int i = 0; i < m_NextTData.getStuff().Length; i++)
        {
            mPropArticleList.Add(CreateNullArticleUI());
        }

        ArticleItem ui_item = null;

        for (int i = 0; i < mPropArticleList.Count; i++)
        {
            ui_item = mPropArticleList[i];

            if (ui_item == null)
                continue;

            int id = m_NextTData.getStuff()[i];
            int haveNum = -1;

            if (id == 1400000003)//圣灵之泉 不是道具
                haveNum = ObjectSelf.GetInstance().HeroMoney;
            else
                haveNum = GetIdInBagNum(id);

            int needNum = m_NextTData.getNumbers()[i];
            if (haveNum >= 0)
            {
                ui_item.SetActive(true);
                ui_item.SetInfo(id, haveNum, needNum);
                
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

    public override void ShowHeroInfo(ObjectCard objectCard)
    {
        m_HeroData = objectCard.GetHeroData();
        m_HeroDataT = objectCard.GetHeroRow();
        
        m_CurTData = GameUtils.GetCurAdvancedData(m_HeroDataT.getBorn(), m_HeroDataT.getQosition(), m_HeroData.StarLevel, m_HeroData.CurStage);
        m_NextTData = GameUtils.GetHeroNextAdvancedData(objectCard);

        YesAdvancedEffect.gameObject.SetActive(false);

        if (m_HeroData.StarLevel == m_NextTData.getQuality() && m_HeroData.CurStage == m_NextTData.getHalosPn())
        {
            m_MaxLevelWindow.SetActive(true);
            m_NoMaxLevelWindow.SetActive(false);
        }
        else
        {
            m_MaxLevelWindow.SetActive(false);
            m_NoMaxLevelWindow.SetActive(true);

            InitShowStarAndStage();
            GreatAttributeItem();
            GreatArticleItem();
        }

        RefreshLevelNoEnoughtTip();
    }
    /// <summary>
    /// 刷新等级不足的提示
    /// </summary>
    private void RefreshLevelNoEnoughtTip()
    {
        int level = m_HeroData.Level;
        if (level < m_NextTData.getLevel())
        {
            m_LevelOpenText.text = GameUtils.getString("ui_yingxiongqianghua_jinjie3").Replace("{0}", m_NextTData.getLevel().ToString());
        }
    }

    protected override void OnClickAdvancedButton()
    {
        int level = m_HeroData.Level;
        if (level < m_NextTData.getLevel())
        {
            m_LevelOpenText.text = GameUtils.getString("ui_yingxiongqianghua_jinjie3").Replace("{0}", m_NextTData.getLevel().ToString());
            return;
        }

        if (ObjectSelf.GetInstance().Money < m_NextTData.getGold())
        {
            UI_HomeControler.Inst.AddUI(UI_QuikBuyGoldMgr.UI_ResPath);
            return;
        }

        if (AdvancedMaterialIsEnough())
        {
            m_CurPopWinTData = m_CurTData;
            m_NextPopWinTData = m_NextTData;

            YesAdvancedEffect.gameObject.SetActive(false);
            YesAdvancedEffect.gameObject.SetActive(true);
             
            CHeroJinjie _CHeroJinjie = new CHeroJinjie();
            _CHeroJinjie.herokey = (int)m_HeroData.GUID.GUID_value;
            IOControler.GetInstance().SendProtocol(_CHeroJinjie);
        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_jinjie5"), this.gameObject.transform);
            return;
        }

        
    }

    private void PopAdvancedSuccessWin()
    {
        GameObject go = UI_HomeControler.Inst.AddUI(AdvancedSuccessWin.UI_ResPath);
        AdvancedSuccessWin _UI_MysticPopWindow = go.GetComponent<AdvancedSuccessWin>();
        if (_UI_MysticPopWindow == null)
        {
            _UI_MysticPopWindow = go.gameObject.AddComponent<AdvancedSuccessWin>();
        }
        go.SetActive(true);
        _UI_MysticPopWindow.InitData(m_CurPopWinTData, m_NextPopWinTData);
    }

    /// <summary>
    /// 进阶成功回调
    /// </summary>
    private void AdvancedSuccess()
    {
        PopAdvancedSuccessWin();
        //InterfaceControler.GetInst().AddMsgBox("进阶成功", this.gameObject.transform);
    }

    /// <summary>
    /// 进阶材料是否足够
    /// </summary>
    /// <returns></returns>
    private bool AdvancedMaterialIsEnough()
    {
        int bagNum;
        for (int i = 0; i < m_NextTData.getStuff().Length;i++ )
        {
            if (m_NextTData.getStuff()[i] == 1400000003)//圣灵之泉 不是道具
                bagNum = ObjectSelf.GetInstance().HeroMoney;
            else
                bagNum = GetIdInBagNum(m_NextTData.getStuff()[i]);
          
            if (bagNum < m_NextTData.getNumbers()[i])
            {
                return false;
            }
        }
        return true;
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_AdvancedSuccess, AdvancedSuccess);
    }
}

/// <summary>
/// Iten物品类
/// </summary>
public class ArticleItem
{ 
    protected Image icon = null;
    protected Image bgImg = null;
    protected Text number = null;
    protected GameObject addIcon = null;
    protected Button m_SelfButton;
    protected int m_id;
    protected int m_HaveNum;

    private Transform mTrans = null;

    public ArticleItem(Transform trans)
    {
        if (trans == null)
            return;

        mTrans = trans;

        bgImg = mTrans.FindChild("Image").GetComponent<Image>();
        addIcon = mTrans.FindChild("addImage").gameObject;
        icon = mTrans.FindChild("Icon").GetComponent<Image>();
        number = mTrans.FindChild("Text").GetComponent<Text>();

        m_SelfButton = mTrans.GetComponent<Button>();
        m_SelfButton.onClick.AddListener(OnClickSelfButton);
    }

    public void SetInfo( int id, int havenum,int needNum)
    {
        Clean();
        m_id = id;
        m_HaveNum = havenum;
        if (havenum < 1)
        {
            //addIcon.SetActive(true);
            icon.gameObject.SetActive(true);
            number.text = "<color=#ff0000>" + havenum.ToString() + "</color>"  + "/" + needNum.ToString();
            if (id == 1400000003)//圣灵之泉 不是道具
            {
                icon.sprite = GameUtils.GetSpriteByResourceType(id);
            }
            else
            {
                if (DynamicItem.GetSprite(id) != null)
                    icon.sprite = DynamicItem.GetSprite(id);
                bgImg.sprite = GameUtils.GetItemQualitySprite(id);
            }
        }
        else
        {
            if (id == 1400000003)//圣灵之泉 不是道具
            {
                icon.sprite = GameUtils.GetSpriteByResourceType(id);
            }
            else
            {
                if (DynamicItem.GetSprite(id) != null)
                    icon.sprite = DynamicItem.GetSprite(id);
                bgImg.sprite = GameUtils.GetItemQualitySprite(id);
            }
            addIcon.SetActive(false);
            icon.gameObject.SetActive(true);
            number.text = havenum.ToString() + "/" + needNum.ToString();
        }
    }

    public void Destroy()
    {
        GameObject.DestroyImmediate(mTrans.gameObject);
    }

    void Clean()
    {
        icon.sprite = null;
        number.text = "";
    }

    public void SetActive(bool active)
    {
        mTrans.gameObject.SetActive(active); 
    }

    private void OnClickSelfButton()
    {
         if (m_HaveNum < 1)
             UICommonManager.Inst.ShowHeroObtain(m_id);
         else
             UICommonManager.Inst.ShowCommon(m_id);

    }
}

public class AttriItem
{
    protected Text title = null;
    protected Text number = null;

    private Transform mTrans = null;

    public AttriItem(Transform trans)
    {
        if (trans == null)
            return;

        mTrans = trans;

        title = mTrans.FindChild("Left_txt").GetComponent<Text>();
        number = mTrans.FindChild("Right_text").GetComponent<Text>();
    }

    public void SetInfo(string str1, string str2)
    {
        Clean();
        title.text = str1;
        number.text = str2;
    }

    public void Destroy()
    {
        GameObject.DestroyImmediate(mTrans.gameObject);
    }

    void Clean()
    {
        title.text = "";
        number.text = "";
    }

    public void SetActive(bool active)
    {
        mTrans.gameObject.SetActive(active);
    }
}
