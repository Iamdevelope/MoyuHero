using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;

public class UniversalItemCell : CellItem 
{
    public enum UniversalItemSize
    {
        Normal_135,     //默认尺寸135*135；
        Type_114,       //尺寸114*114;
    }

    public Color DisableImageColor;
    public Color EnableCountTextColor;
    public Color DisableCountTextColor;
    public Sprite m_Star;
    public Sprite m_BlackStar;

    private Button m_SelfButton;
    private Image m_HightLightBG;
    private Image m_ItemBG;
    private Image m_ItemImage;
    private Image m_HeroImage;
    private RuneItemCommon m_RuneItemCommon;

    private Image m_ItemCountImage;
    private Text m_ItemCountText;

    private Text m_TittleText;
    private Text m_HeadText;
    private Text m_CenterText;

    private GameObject m_StarPanel;
    private Image[] m_StarArray;
    private GameObject m_CheckClaim;
    private Text m_CheckClaimText;
    private Action<int> m_ButtonAction;

    private bool isLoaded = false;
    public EM_OBJECT_CLASS m_Type;
    public int m_ItemID = -1;

    public override void InitUIData()
    {
        base.InitUIData();
        LoadAllComponent();//该函数在多个地方调用，防止Awake在某些情况下不执行的蛋疼问题。
    }
    public override void InitUIState()
    {
        base.InitUIState();
        LoadAllComponent();
    }
    private void LoadAllComponent()
    {
        if (isLoaded)
            return;

        m_SelfButton = selfTransform.GetComponent<Button>();
        m_SelfButton.onClick.AddListener(OnSelfButtonClick);
        m_HightLightBG = selfTransform.FindChild("HightLightBG").GetComponent<Image>();
        m_ItemBG = selfTransform.FindChild("ItemBG").GetComponent<Image>();
        m_ItemImage = selfTransform.FindChild("ItemBG/ItemImage").GetComponent<Image>();
        m_HeroImage = selfTransform.FindChild("ItemBG/HeroImage").GetComponent<Image>();

        m_ItemCountImage = selfTransform.FindChild("ItemCountImage").GetComponent<Image>();
        m_ItemCountText = selfTransform.FindChild("ItemCountImage/CountText").GetComponent<Text>();

        m_TittleText = selfTransform.FindChild("TittleText").GetComponent<Text>();
        m_HeadText = selfTransform.FindChild("HeadText").GetComponent<Text>();
        m_CenterText = selfTransform.FindChild("CenterText").GetComponent<Text>();

        m_StarPanel = selfTransform.FindChild("StarPanel").gameObject;
        m_StarArray = new Image[5];
        m_StarArray[0] = selfTransform.FindChild("StarPanel/Star1Image").GetComponent<Image>();
        m_StarArray[1] = selfTransform.FindChild("StarPanel/Star2Image").GetComponent<Image>();
        m_StarArray[2] = selfTransform.FindChild("StarPanel/Star3Image").GetComponent<Image>();
        m_StarArray[3] = selfTransform.FindChild("StarPanel/Star4Image").GetComponent<Image>();
        m_StarArray[4] = selfTransform.FindChild("StarPanel/Star5Image").GetComponent<Image>();

        m_CheckClaim = selfTransform.FindChild("CheckClaim").gameObject;
        m_CheckClaimText = selfTransform.FindChild("CheckClaim/CheckClaimText").GetComponent<Text>();
        InitComponentState();
        isLoaded = true;
    }
    private void InitComponentState()
    {
        m_Type = EM_OBJECT_CLASS.EM_OBJECT_CLASS_INVALID;
        m_ItemID = -1;
        m_ButtonAction = null;
        m_SelfButton.enabled = false;
        m_HightLightBG.gameObject.SetActive(false);
        m_ItemBG.gameObject.SetActive(true);
        m_ItemImage.gameObject.SetActive(false);
        m_ItemImage.preserveAspect = true;
        m_HeroImage.gameObject.SetActive(false);
        m_HeroImage.preserveAspect = true;
        m_ItemCountImage.gameObject.SetActive(false);
        m_TittleText.gameObject.SetActive(false);
        m_HeadText.gameObject.SetActive(false);
        m_CenterText.gameObject.SetActive(false);
        m_StarPanel.SetActive(false);
        m_CheckClaim.SetActive(false);
        m_CheckClaimText.text = GameUtils.getString("sign_content4");
    }

    private void LoadStar(int star, int maxStar, bool needBlackBG = true)
    {
        if (maxStar < star)
            maxStar = star;
        m_StarPanel.SetActive(true);
        for (int i = 0; i < m_StarArray.Length; i++)
        {
            if (i < star)
            {
                m_StarArray[i].sprite = m_Star;
            }
            else
            {
                m_StarArray[i].sprite = m_BlackStar;
            }

            if (needBlackBG)
                m_StarArray[i].gameObject.SetActive(i < maxStar);
            else
                m_StarArray[i].gameObject.SetActive(i < star);

        }
    }
    private Sprite GetRuneSprite(ItemTemplate itemData)
    {
        Sprite _image = null;
        string _spriteName = itemData.getIcon_s();
        if (_spriteName != null)
        {
             _image = UIResourceMgr.LoadSprite(common.defaultPath + _spriteName);
        }
        return _image;
    }
    private void ItemTypeProcess(ItemTemplate itemData)
    {
        if (m_RuneItemCommon == null)
            m_RuneItemCommon = RuneFactory.CreateRuneItemCommom(m_ItemBG.transform);
        RuneItemCommonData _data = new RuneItemCommonData();
        _data.ItemT = itemData;
        m_RuneItemCommon.SetRuneItemData(_data,RuneItemCommon.RuneItemShowType.IconOnly);
        m_RuneItemCommon.transform.localScale = Vector3.one * 0.9f;
    }

    private void OnSelfButtonClick()
    {
        if (m_ButtonAction != null)
            m_ButtonAction(m_ItemID);
    }

    /************************公共方法*******************************/
    /// <summary>
    /// 生成UniversalItemCell的静态方法，参数：UniversalItemCell的父物体
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static UniversalItemCell GenerateItem(Transform parent)
    {
        GameObject go = UIResourceMgr.LoadPrefab("UI/Prefabs/UI_SignIn/UniversalItemCell") as GameObject;
        if (go == null)
            return null;
        go = GameObject.Instantiate(go, parent.position, parent.transform.rotation) as GameObject;
        if (go == null)
            return null;

        go.transform.SetParent(parent);
        go.transform.localScale = Vector3.one;
        UniversalItemCell result = go.GetComponent<UniversalItemCell>();
        if (result != null)
        {
            result.LoadAllComponent();
        }

        return result;
    }

    
    public void Destroy()
    {
        base.OnReadyForClose();
        m_SelfButton = null;
        m_HightLightBG = null;
        m_ItemBG = null;
        m_ItemImage = null;
        m_HeroImage = null;
        m_RuneItemCommon = null;
        m_ItemCountImage = null;
        m_ItemCountText = null;
        m_TittleText = null;
        m_HeadText = null;
        m_CenterText = null;
        m_StarPanel = null;
        m_StarArray[0] = null;
        m_StarArray[1] = null;
        m_StarArray[2] = null;
        m_StarArray[3] = null;
        m_StarArray[4] = null;
        m_StarArray = null;
        m_CheckClaim = null;
        m_CheckClaimText = null;
        m_ButtonAction = null;

        Destroy(this);
    }

    /// <summary>
    /// 使用物品ID初始化，参数2为物品数量。当ID为英雄、符文，或者数量不大于0时会隐藏数量面板
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="count"></param>
    public void InitByID(int itemID,int count = 0)
    {
        m_ItemID = itemID;
        m_Type = GameUtils.GetObjectClassById(itemID);
        switch (m_Type)
        { 
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                ItemTemplate _itemTable = DataTemplate.GetInstance().m_ItemTable.getTableData(itemID) as ItemTemplate;
                if (_itemTable != null)
                {
                    ItemTypeProcess(_itemTable);
                    m_ItemImage.gameObject.SetActive(false);
                    m_HeroImage.gameObject.SetActive(false);
                    LoadStar(_itemTable.getRune_quality(), _itemTable.getRune_quality(),false);
                }
                m_ItemCountImage.gameObject.SetActive(false);
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                HeroTemplate _heroTable = DataTemplate.GetInstance().m_HeroTable.getTableData(itemID) as HeroTemplate;
                if (_heroTable != null)
                {
                    m_HeroImage.sprite = DynamicItem.GetSprite(itemID);
                    m_HeroImage.gameObject.SetActive(true);
                    //m_HeroImage.SetNativeSize();
                    m_ItemImage.gameObject.SetActive(false);
                    LoadStar(_heroTable.getQuality(),_heroTable.getMaxQuality());
                }
                m_ItemCountImage.gameObject.SetActive(false);
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER:
                MonsterTemplate _monsterTable = DataTemplate.GetInstance().m_MonsterTable.getTableData(itemID) as MonsterTemplate;
                if (_monsterTable != null)
                {
                    m_HeroImage.sprite = DynamicItem.GetSprite(itemID);
                    m_HeroImage.gameObject.SetActive(true);
                    //m_HeroImage.SetNativeSize();
                    m_ItemImage.gameObject.SetActive(false);
                    m_StarPanel.SetActive(false);
                }
                break;
            default: 
                m_ItemImage.sprite = DynamicItem.GetSprite(itemID);
                m_ItemImage.gameObject.SetActive(true);
                m_HeroImage.gameObject.SetActive(false);
                m_ItemCountImage.gameObject.SetActive(count > 0);
                m_ItemCountText.text = string.Format("X{0}", count);
                break;
        }
    }
    public void InitBySprite(Sprite icon,int count = 0)
    {
        m_ItemImage.sprite = icon;
        m_ItemImage.gameObject.SetActive(true);
        m_HeroImage.gameObject.SetActive(false);
        m_ItemCountImage.gameObject.SetActive(count > 0);
        m_ItemCountText.text = string.Format("×{0}", count);
    }


    /// <summary>
    /// 设置预制件中上、中、下三段文字。写入参数null会关闭对应组件
    /// </summary>
    /// <param name="tittle"></param>
    /// <param name="head"></param>
    /// <param name="center"></param>
    public void SetText(string tittle,string head,string center)
    {
        if (string.IsNullOrEmpty(tittle))
            m_TittleText.gameObject.SetActive(false);
        else
        {
            m_TittleText.gameObject.SetActive(true);
            m_TittleText.text = tittle;
        }

        if (string.IsNullOrEmpty(head))
            m_HeadText.gameObject.SetActive(false);
        else
        {
            m_HeadText.gameObject.SetActive(true);
            m_HeadText.text = head;
        }

        if (string.IsNullOrEmpty(center))
            m_CenterText.gameObject.SetActive(false);
        else
        {
            m_CenterText.gameObject.SetActive(true);
            m_CenterText.text = center;
        }
    }
    
    /// <summary>
    /// 强制设置显示物品数量处显示指定文字;
    /// </summary>
    /// <param name="str"></param>
    public void SetCount(string str)
    {
        m_ItemCountImage.gameObject.SetActive(!string.IsNullOrEmpty(str));
        m_ItemCountText.text = str;
    }

    /// <summary>
    /// 设置图标尺寸大小;
    /// </summary>
    /// <param name="size"></param>
    public void SetSize(UniversalItemSize size)
    {
        float defaultSize = 135f;
        switch (size)
        {
            case UniversalItemSize.Normal_135:
                break;
            case UniversalItemSize.Type_114:
                float scale = 114f / defaultSize;
                selfTransform.localScale = new Vector3(scale, scale, 1f);
                break;
            default:
                break;
        }
    }
    
    /// <summary>
    /// 显示/关闭带对号的已领取字样，参数2可以将“已领取”设置成其他文字，默认为“已领取字样”
    /// </summary>
    /// <param name="value"></param>
    /// <param name="text"></param>
    public void SetCheckClaim(bool value, string text = null)
    {
        if (string.IsNullOrEmpty(text))
            m_TittleText.text = GameUtils.getString("sign_content4");
        else
            m_TittleText.text = text;

        m_CheckClaim.SetActive(value);
    }
    /// <summary>
    /// 显示/关闭高亮背景图
    /// </summary>
    /// <param name="value"></param>
    public void SetHightLight(bool value)
    {
        m_ItemBG.enabled = !value;
        m_HightLightBG.gameObject.SetActive(value);
        if (value)
            SetItemGray(false);
    }
    /// <summary>
    /// 将背景图以及图标的颜色混入灰色
    /// </summary>
    /// <param name="value"></param>
    public void SetItemGray(bool value)
    {
        if (value)
        {
            if (m_RuneItemCommon != null)
                m_RuneItemCommon.SetColor(DisableImageColor);
            else
            {
                m_ItemImage.color = DisableImageColor;
                m_HeroImage.color = DisableImageColor;
                m_ItemCountImage.color = DisableImageColor;
                m_ItemCountText.color = DisableCountTextColor;
            }
            m_ItemBG.color = DisableImageColor;
        }
        else
        {
            if (m_RuneItemCommon != null)
                m_RuneItemCommon.SetColor(Color.white);
            else
            {
                m_ItemImage.color = Color.white;
                m_HeroImage.color = Color.white;
                m_ItemCountImage.color = Color.white;
                m_ItemCountText.color = EnableCountTextColor;
            }
            m_ItemBG.color = Color.white;
        }
    }
    /// <summary>
    /// 添加点击回调
    /// </summary>
    /// <param name="callback"></param>
    public void AddClickListener(Action<int> callback)
    {
        if (m_ButtonAction == null)
            m_ButtonAction = callback;
        else
            m_ButtonAction = Delegate.Combine(m_ButtonAction, callback) as Action<int>;

        m_SelfButton.enabled = m_ButtonAction != null;
    }
    /// <summary>
    /// 删除点击回调
    /// </summary>
    /// <param name="callback"></param>
    public void RemoveClickListener(Action<int> callback)
    {
        if (m_ButtonAction != null)
        {
            m_ButtonAction = Delegate.Remove(m_ButtonAction, callback) as Action<int>;
        }
        m_SelfButton.enabled = m_ButtonAction != null;
    }
}
