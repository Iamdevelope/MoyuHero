using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI;
using UnityEngine.Events;
using DreamFaction.Utils;

public class RuneItemCommonData
{
    public ItemTemplate ItemT = null;                               //符文模板数据;
    public int RuneLevel = 0;                                       //符文强化等级;
    public bool IsShowMaxEffect = false;                            //符文达到最大等级时是否显示最大等级特效;
    public RuneItemCommon.ClickEventHandler IconAction = null;      //符文图标点击事件;
    public RuneItemCommon.ClickEventHandler ItemAction = null;      //符文整个物体点击事件;
    public string EquipedHeroName = "";                             //XXXX已装备;
}

public class RuneItemCommon : CellItem
{
    public delegate void ClickEventHandler(object data);

    public enum RuneItemShowType
    {
        IconOnly,           //只显示符文--其余不显示;
        IconWithBg,         //正常显示--图标+背景;
        IconWithoutBg,      //仅显示符文图标--符文;
        Null,               //显示空白--空背景;
        Locked,             //显示锁定--锁定+背景;
        AddWithoutBg,       //显示加号不带背景--符文（符文处显示加号）;
        IconWithRightName,  //显示符文和符文名称--（IconWithoutBg + 右侧显示 符文名称、XX专属、XXX已装备）
    }

    public object data = null;                    //用于传递数据，点击事件回调也可以传出数据;

    private GameObject m_Obj = null;

    private RuneIconItem m_IconItem = null;
    private GameObject m_IconObj = null;

    private Button m_ItemBtn = null;
    private Image m_Bg = null;
    private Text m_Name = null;
    private GameObject m_levelObj = null;
    private Text m_LevelTxt = null;
    private GameObject m_StarsObj = null;
    private Image[] m_Stars = null;
    private RawImage m_EffectRawImg = null;
    private GameObject m_EffectSpecImgObj = null;
    private GameObject m_EquipObj = null;
    private GameObject m_LockImgObj = null;
    private GameObject m_SelectObj = null;

    //---------------nameObj-------------
    private GameObject m_NameObj = null;
    private Text m_RuneNameTxt = null;
    private Text m_EquipHeroNameTxt = null;
    private Text m_SpecTxt = null;

    private RuneItemCommonData m_RuneCommonData = null;

    private RuneItemCommon()
    {

    }

    public override void InitUIData()
    {
        base.InitUIData();

        Init(transform);
    }

    protected void Init(Transform trans)
    {
        m_Obj = trans.gameObject;

        m_IconObj = trans.FindChild("RuneIconItem").gameObject;
        m_IconItem = new RuneIconItem(m_IconObj.transform);

        m_ItemBtn = trans.GetComponent<Button>();
        m_Bg = trans.FindChild("Parent/BG").GetComponent<Image>();
        m_Name = trans.FindChild("Parent/Text").GetComponent<Text>();
        m_levelObj = trans.FindChild("Parent/Level").gameObject;
        m_LevelTxt = trans.FindChild("Parent/Level/Text").GetComponent<Text>();

        m_StarsObj = trans.FindChild("Parent/stars").gameObject;
        m_Stars = new Image[5];
        for (int i = 0; i < 5; i++)
        {
            m_Stars[i] = trans.FindChild("Parent/stars/Star_" + (i + 1)).GetComponent<Image>();
        }

        m_EffectRawImg = trans.FindChild("Parent/maxLevel").GetComponent<RawImage>();
        m_EffectSpecImgObj = trans.FindChild("Parent/maxLevel/Image").gameObject;

        m_EquipObj = trans.FindChild("Equip").gameObject;
        m_LockImgObj = trans.FindChild("Parent/FG").gameObject;
        m_SelectObj = trans.FindChild("Parent/Border").gameObject;

        m_NameObj = trans.FindChild("NameObj").gameObject;
        m_RuneNameTxt = trans.FindChild("NameObj/NameTxt").GetComponent<Text>();
        m_EquipHeroNameTxt = trans.FindChild("NameObj/Vertical/EquipTxt").GetComponent<Text>();
        m_SpecTxt = trans.FindChild("NameObj/Vertical/SpecTxt").GetComponent<Text>();

        m_EquipObj.SetActive(false);
        m_LockImgObj.SetActive(false);
        m_EffectRawImg.gameObject.SetActive(false);
        m_EffectSpecImgObj.SetActive(false);

        m_IconItem.AddIconClickListener(OnIconClick);
        m_ItemBtn.onClick.AddListener(OnItemClick);
    }

    //public void SetRuneItemTableID(int id)
    //{
    //    if (id == -1)
    //    {
    //        SetRuneItemTemplate(null);
    //    }
    //    else
    //    {
    //        ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(id);

    //        SetRuneItemTemplate(itemT);
    //    }
    //}

    public void SetRuneItemData(RuneItemCommonData data ,RuneItemShowType type = RuneItemShowType.IconWithBg)
    {
        m_RuneCommonData = data;

        //icon点击没有回调事件,那么不阻止事件传递;
        m_IconItem.SetIsBlock(data.IconAction != null);
        //item点击没有回调事件,那么不阻止事件传递;
        SetIsItemBlock(data.ItemAction != null);

        ItemTemplate itemT = data.ItemT;

        switch (type)
        {
            case RuneItemShowType.IconOnly:
                m_LockImgObj.SetActive(false);
                m_Bg.gameObject.SetActive(false);
                m_IconItem.SetActive(true);
                m_IconItem.SetLevelInfoActive(false);
                m_IconItem.SetIcon(common.defaultPath + itemT.getIcon());
                m_IconItem.SetRuneType(itemT.getRune_type());
                m_IconItem.SetIsSpecial(RuneModule.IsSpecialRune(itemT));
                m_levelObj.SetActive(false);
                m_Name.gameObject.SetActive(false);
                SetStarActive(false);
                m_NameObj.SetActive(false);

                SetPosition(Vector3.zero);
                break;
            case RuneItemShowType.IconWithBg:
                m_NameObj.SetActive(false);
                m_LockImgObj.SetActive(false);
                m_Bg.gameObject.SetActive(true);

                if (itemT != null)
                {
                    m_IconItem.SetActive(true);
                    m_IconItem.SetIcon(common.defaultPath + itemT.getIcon());
                    m_IconItem.SetRuneType(itemT.getRune_type());
                    bool isSpec = RuneModule.IsSpecialRune(itemT);
                    m_IconItem.SetIsSpecial(isSpec);
                    m_EffectSpecImgObj.SetActive(isSpec);
                    m_IconItem.SetStarsNum(itemT.getRune_quality());
                    m_IconItem.SetLevel(data.RuneLevel);
                    m_IconItem.SetLevelInfoActive(false);
                    m_EffectRawImg.color = RuneManager.Inst.GetEffColor((EM_RUNE_TYPE)(itemT.getRune_type()));

                    m_Name.text = GameUtils.getString(itemT.getName());
                    m_LevelTxt.text = "+" + data.RuneLevel;
                    m_levelObj.SetActive(true);
                    SetItemStarsNum(itemT.getRune_quality());

                    if (data.IsShowMaxEffect)
                    {
                        bool isMax = DataTemplate.GetInstance().IsRuneStrenthFullLevel(itemT, data.RuneLevel);
                        SetMaxLevelEffectActive(isMax);
                    }
                    else
                    {
                        SetMaxLevelEffectActive(false);
                    }
                }
                break;
            case RuneItemShowType.IconWithoutBg:
                m_NameObj.SetActive(false);
                m_LockImgObj.SetActive(false);
                m_Bg.gameObject.SetActive(false);

                if (itemT != null)
                {
                    m_IconItem.SetActive(true);
                    m_IconItem.SetIcon(common.defaultPath + itemT.getIcon());
                    m_IconItem.SetRuneType(itemT.getRune_type());
                    m_IconItem.SetIsSpecial(RuneModule.IsSpecialRune(itemT));
                    m_IconItem.SetStarsNum(itemT.getRune_quality());
                    m_IconItem.SetLevel(data.RuneLevel);
                    m_IconItem.SetLevelInfoActive(true);
                    
                    m_Name.text = "";
                    m_LevelTxt.text = "+" + data.RuneLevel;
                    m_levelObj.SetActive(false);
                    SetStarActive(false);

                }

                SetPosition(Vector3.zero);
                break;
            case RuneItemShowType.Null:
                m_NameObj.SetActive(false);
                m_LockImgObj.SetActive(false);
                m_Bg.gameObject.SetActive(true);
                m_Name.text = "";
                m_levelObj.SetActive(false);
                m_IconItem.SetActive(false);
                SetStarActive(false);

                break;
            case RuneItemShowType.Locked:
                m_NameObj.SetActive(false);
                m_LockImgObj.SetActive(true);
                m_Bg.gameObject.SetActive(true);
                m_Name.text = "";
                m_levelObj.SetActive(false);
                m_IconItem.SetActive(false);
                SetStarActive(false);

                break;
            case RuneItemShowType.AddWithoutBg:
                m_NameObj.SetActive(false);
                m_LockImgObj.SetActive(false);
                m_Bg.gameObject.SetActive(true);
                m_Name.text = "";
                m_levelObj.SetActive(false);
                SetStarActive(false);

                m_IconItem.SetActive(true);
                m_IconItem.SetLevelInfoActive(false);
                m_IconItem.SetRuneType(EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID);
                m_IconItem.ShowAddIcon();

                SetPosition(Vector3.zero);
                break;
            case RuneItemCommon.RuneItemShowType.IconWithRightName:
                m_NameObj.SetActive(true);
                m_LockImgObj.SetActive(false);
                m_Bg.gameObject.SetActive(false);

                if (itemT != null)
                {
                    m_IconItem.SetActive(true);
                    m_IconItem.SetIcon(common.defaultPath + itemT.getIcon());
                    m_IconItem.SetRuneType(itemT.getRune_type());
                    m_IconItem.SetIsSpecial(RuneModule.IsSpecialRune(itemT));
                    m_IconItem.SetStarsNum(itemT.getRune_quality());
                    m_IconItem.SetLevel(data.RuneLevel);
                    m_IconItem.SetLevelInfoActive(true);
                    m_IconItem.SetSize(RuneIconItemSize.Big);
                    
                    m_Name.text = "";
                    m_LevelTxt.text = "+" + data.RuneLevel;
                    m_levelObj.SetActive(false);
                    SetStarActive(false);
                    m_RuneNameTxt.text = GameUtils.getString(itemT.getName());
                    if (string.IsNullOrEmpty(data.EquipedHeroName))
                    {
                        m_EquipHeroNameTxt.gameObject.SetActive(false);
                    }
                    else
                    {
                        m_EquipHeroNameTxt.gameObject.SetActive(true);
                        m_EquipHeroNameTxt.text = data.EquipedHeroName;
                    }
                    m_SpecTxt.text = GameUtils.getString(itemT.getSpecialHeroDes());
                }

                SetPosition(Vector3.zero);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置item星级显示（非icon星级）；
    /// </summary>
    /// <param name="isActive"></param>
    private void SetStarActive(bool isActive)
    {
        m_StarsObj.SetActive(isActive);
    }

    private void SetItemStarsNum(int starNum)
    {
        SetStarActive(starNum > 0);

        for (int i = 0; i < 5; i++ )
        {
            m_Stars[i].gameObject.SetActive(i < starNum);
        }
    }

    private void OnItemClick()
    {
        if (m_RuneCommonData != null && m_RuneCommonData.ItemAction != null)
        {
            m_RuneCommonData.ItemAction(data);
        }
    }

    private void OnIconClick()
    {
        if (m_RuneCommonData != null && m_RuneCommonData.IconAction != null)
        {
            m_RuneCommonData.IconAction(data);
        }
    }

    private void SetIsItemBlock(bool isBlock)
    {
        CanvasGroup cg = selfTransform.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = selfTransform.gameObject.AddComponent<CanvasGroup>();
        }

        cg.blocksRaycasts = isBlock;
    }

    /// <summary>
    /// 如果调用设置位置，那么pivot和anchor都会设置成居中;
    /// </summary>
    /// <param name="pos"></param>
    public void SetPosition(Vector3 pos)
    {
        RectTransform rt = selfTransform.GetComponent<RectTransform>();
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchorMin = new Vector2(0.5f, 0.5f);

        rt.pivot = new Vector2(0.5f, 0.5f);

        rt.localPosition = pos;
    }

    /// <summary>
    /// 目前只支持设置符文Icon变色;
    /// </summary>
    public void SetColor(Color color)
    {
        m_IconItem.SetColor(color);
    }

    public void SetSelected(bool isSelected)
    {
        m_SelectObj.SetActive(isSelected);
    }

    public void SetEquiped(bool isEquiped)
    {
        m_EquipObj.SetActive(isEquiped);
    }

    public void SetMaxLevelEffectActive(bool isActive)
    {
        m_EffectRawImg.gameObject.SetActive(isActive);
    }
    

    protected void OnDestroy()
    {
        Destroy();
    }

    private void Destroy()
    {
        base.OnReadyForClose();

        m_IconItem.Destroy();

        data = null;
        m_RuneCommonData = null;

        m_Obj = null;

        m_IconItem = null;
        m_IconObj = null;

        m_ItemBtn = null;
        m_Bg = null;
        m_Name = null;
        m_levelObj = null;
        m_LevelTxt = null;
        m_Stars = null;
        m_EffectRawImg = null;
        m_EquipObj = null;
        m_LockImgObj = null;
    }
}
