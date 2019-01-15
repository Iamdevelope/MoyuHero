using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;

using UnityEngine.EventSystems;
using UnityEngine.Events;
public class UI_MysticItem : CellItem, IPointerDownHandler, IPointerUpHandler
{
    //public ItemTemplate item;
    protected Image bgImg = null;
    protected Image icon = null;
    protected Image addImg;
    protected Text number;
    protected Text expText;
    protected Button m_SelfButton;

    private Transform mTrans = null;
    private int m_InitHaveNum;//初始拥有的物品数量
    private int m_HaveNum; //拥有的物品数量
    protected int m_Exp;    //经验值
    protected int m_MysticId;//秘术的id
    private int m_AddItemNum = 0;//添加物品的数量  初始为0

    private float m_PressInterval = 0.15f;//按下的间隔时间
    private float mDelta;
    private float mClickTime;

    private ItemTemplate SelectItem;
    private Vector3 startPs;
    private Vector3 endPs;
    private float m_CallInterval = 0.5f;//调用函数的间隔

    public delegate void ExpItemClick(ItemTemplate itemtable);
    public event ExpItemClick onExpItemClick = null;

    bool isCallFunction = false;//是否开始调用函数
    bool isPress = false;//是否按下
    bool drag = false;//是否是滑动操作
    public void ArticleItem(Transform trans, int MysticId)
    {
        if (trans == null)
            return;

        mTrans = trans;
        m_MysticId = MysticId;

        bgImg = mTrans.FindChild("Image").GetComponent<Image>();
        expText = mTrans.FindChild("addExp").GetComponent<Text>();
        icon = mTrans.FindChild("Icon").GetComponent<Image>();
        addImg = mTrans.FindChild("addImage").GetComponent<Image>();
        number = mTrans.FindChild("Text").GetComponent<Text>();
    }

    /// <summary>
    /// 物品的基类
    /// </summary>
    /// <param name="id"></param>
    public void SetInfo(UI_MysticPopWindow.MysticItemData itemData)
    {
        SelectItem = itemData.GetItemTemplate;
        Clean();
        m_InitHaveNum = itemData.GetItemNum;
        m_HaveNum = m_InitHaveNum;
        number.text = "X" + m_HaveNum.ToString();

        bgImg.sprite = GameUtils.GetItemQualitySprite(SelectItem.getId());

        if (m_InitHaveNum < 1)
        {
            addImg.gameObject.SetActive(true);
            icon.gameObject.SetActive(true);
            icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + SelectItem.getIcon());
            number.text = "<color=#ff0000>" + m_HaveNum.ToString() + "</color>" ;
            m_Exp = SelectItem.getImprovexperience();
            expText.text = "Exp+" + m_Exp.ToString();
        }
        else
        {
            icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + SelectItem.getIcon());
            m_Exp = SelectItem.getImprovexperience();
            expText.text = "Exp+" + m_Exp.ToString();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPs = Input.mousePosition;
        if (!drag)
        {
            OnClickButton(null);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        endPs = Input.mousePosition;

        if (Mathf.Abs(Vector3.Distance(startPs, endPs)) < 0.5f)
        {
            if (mClickTime <= m_PressInterval)
            {
//                 if (m_InitHaveNum < 1)
                     UICommonManager.Inst.ShowHeroObtain(SelectItem.getId());
//                 else
//                     UICommonManager.Inst.ShowCommon(SelectItem.getId());
            }
        }
        mClickTime = 0;
        isPress = false;
        isCallFunction = false;
    }


    void Update()
    {
        if (isPress)
        {
            mClickTime += Time.deltaTime;
            if (mClickTime >= m_PressInterval)
            {
                isCallFunction = true;
            }
        }

        if (onExpItemClick != null && m_HaveNum > 0 && !ObjectSelf.GetInstance().GetIsMysticMaxLevel)
        {
            if (isCallFunction)
            {
                mDelta += Time.deltaTime;

                if (mDelta >= m_CallInterval)
                {
                    mDelta = 0f;
                    m_HaveNum--;
                    m_AddItemNum++;
                    number.text = "X" + m_HaveNum.ToString();
                    onExpItemClick(SelectItem);
                }
            }
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
        expText.text = "";
    }

    public void SetActive(bool active)
    {
        mTrans.gameObject.SetActive(active);
    }

    public void SetExpItemClick(ExpItemClick expItemClickHandle)
    {
        onExpItemClick = expItemClickHandle;
    }


    void OnClickButton(GameObject go)
    {
        isPress = true;
    }
}
