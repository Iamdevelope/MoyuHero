using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;

public class HeroItemBuy : BaseUI
{
    public static HeroItemBuy Inst = null;
    public static string UI_ResPath = "HeroStrengthen/UI_HeroItemBuy_1_20";

    protected Text m_Text;
    protected Text m_UI_Text_Name;
    protected Text m_UI_Text_Total;
    protected Text m_Text_1;
    protected Text m_UI_Text_CountTitle;
    protected Text m_UI_Text_Count;
    protected Button m_UI_Btn_Add;
    protected Button m_UI_Btn_Minus;
    protected Text m_UI_Text_Num;
    protected Button m_UI_Btn_Sell;
    protected Text m_Text_2;
    protected Button m_UI_Btn_Close;
    protected Button m_UI_Btn_Max;
    protected Image m_Icon;

    int m_Tableid = 0;
    int m_Number = 1;
    ItemTemplate m_Item;
    ShopTemplate m_ShopTemp;

    public override void InitUIData ()
    {
        Inst = this;
        base.InitUIData ();
        m_Text = selfTransform.FindChild ( "UI_BG_Main/title/Text" ).GetComponent<Text> ();
        m_UI_Text_Name = selfTransform.FindChild ( "UI_BG_Main/UI_Text_Name" ).GetComponent<Text> ();
        m_UI_Text_Total = selfTransform.FindChild ( "UI_BG_Main/UI_BG_Cost/UI_BG_Total/UI_Text_Total" ).GetComponent<Text> ();
        m_Text_1 = selfTransform.FindChild ( "UI_BG_Main/UI_BG_Cost/UI_BG_Total/Text" ).GetComponent<Text> ();
        m_UI_Text_CountTitle = selfTransform.FindChild ( "UI_BG_Main/UI_Text_CountTitle" ).GetComponent<Text> ();
        m_UI_Text_Count = selfTransform.FindChild ( "UI_BG_Main/UI_Text_Count" ).GetComponent<Text> ();
        m_UI_Btn_Add = selfTransform.FindChild ( "UI_BG_Main/UI_Btn_Add" ).GetComponent<Button> ();
        m_UI_Btn_Add.onClick.AddListener ( OnClickUI_Btn_Add );
        m_UI_Btn_Minus = selfTransform.FindChild ( "UI_BG_Main/UI_Btn_Minus" ).GetComponent<Button> ();
        m_UI_Btn_Minus.onClick.AddListener ( OnClickUI_Btn_Minus );
        m_UI_Text_Num = selfTransform.FindChild ( "UI_BG_Main/UI_Bg_Num/UI_Text_Num" ).GetComponent<Text> ();
        m_UI_Btn_Sell = selfTransform.FindChild ( "UI_BG_Main/UI_Btn_Sell" ).GetComponent<Button> ();
        m_UI_Btn_Sell.onClick.AddListener ( OnClickUI_Btn_Sell );
        m_Text_2 = selfTransform.FindChild ( "UI_BG_Main/UI_Btn_Sell/Text" ).GetComponent<Text> ();
        m_UI_Btn_Close = selfTransform.FindChild ( "UI_BG_Main/UI_Btn_Close" ).GetComponent<Button> ();
        m_UI_Btn_Close.onClick.AddListener ( OnClickUI_Btn_Close );
        m_UI_Btn_Max = selfTransform.FindChild ( "UI_BG_Main/UI_Btn_Max" ).GetComponent<Button> ();
        m_UI_Btn_Max.onClick.AddListener ( OnClickUI_Btn_Max );

        m_Icon = selfTransform.Find ( "UI_BG_Main/UI_Image_Icon" ).GetComponent<Image> ();
        GameEventDispatcher.Inst.addEventListener ( GameEventID.KE_ModItemNum, OnBuyItemSuccess );
        GameEventDispatcher.Inst.addEventListener ( GameEventID.KE_KnapsackAdd, OnBuyItemSuccess );

    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    public void OnBuyItemSuccess ()
    {
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
    }

    public void ShowInfo ( int tableid )
    {
        m_Tableid = tableid;

        m_ShopTemp = DataTemplate.GetInstance ().GetShopTemplateByParaID ( m_Tableid );


        // icon
        m_Item = ( ItemTemplate ) DataTemplate.GetInstance ().m_ItemTable.getTableData ( tableid );
        m_Icon.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + m_Item.getIcon_s () );

        int number = 0;
        ObjectSelf.GetInstance ().TryGetItemCountById ( EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, tableid, ref number );
        m_UI_Text_Count.text = number.ToString ();

        m_UI_Text_Name.text = GameUtils.getString ( m_Item.getName () );

        m_UI_Text_Num.text = m_Number.ToString ();

        m_UI_Text_Total.text = ( m_Number * m_ShopTemp.getCost () [ 0 ] ).ToString ();
    }

    protected virtual void OnClickUI_Btn_Add ()
    {
        if ( m_Number < 100 )
        {
            m_Number++;
            m_UI_Text_Num.text = m_Number.ToString ();
            m_UI_Text_Total.text = ( m_Number * m_ShopTemp.getCost () [ 0 ] ).ToString ();

            if ( ObjectSelf.GetInstance ().Gold < m_Number * m_ShopTemp.getCost () [ 0 ] )
            {
                m_UI_Text_Total.color = Color.red;
            }
            else
            {
                m_UI_Text_Total.color = Color.white;
            }
        }
        else
        {
            InterfaceControler.GetInst ().AddMsgBox ( GameUtils.getString ( "ui_yingxiongqianghua_shengji8" ) );
        }

    }

    protected virtual void OnClickUI_Btn_Minus ()
    {
        if ( m_Number > 1 )
        {
            m_Number--;
            m_UI_Text_Num.text = m_Number.ToString ();
            m_UI_Text_Total.text = ( m_Number * m_ShopTemp.getCost () [ 0 ] ).ToString ();

            if ( ObjectSelf.GetInstance ().Gold < m_Number * m_ShopTemp.getCost () [ 0 ] )
            {
                m_UI_Text_Total.color = Color.red;
            }
            else
            {
                m_UI_Text_Total.color = Color.white;
            }
        }
        else
        {
            InterfaceControler.GetInst ().AddMsgBox ( GameUtils.getString ( "ui_yingxiongqianghua_shengji7" ) );
        }

    }

    protected virtual void OnClickUI_Btn_Sell ()
    {
        if ( ObjectSelf.GetInstance ().Gold < m_Number * m_ShopTemp.getCost () [ 0 ] )
        {
            InterfaceControler.GetInst ().AddMsgBox ( "元宝不足" );
            return;
        }

        if ( m_Number > 0 )
        {
            CShopBuy proto = new CShopBuy ();
            proto.shopid = m_ShopTemp.GetID ();
            proto.num = m_Number;
            proto.isdiscount = 0;

            IOControler.GetInstance ().SendProtocol ( proto );
        }

    }

    protected virtual void OnClickUI_Btn_Max ()
    {
        //m_Number = ObjectSelf.GetInstance ().Gold / m_ShopTemp.getCost () [ 0 ];
        //m_Number = Mathf.Min(m_Number, 99);
        if ( m_Number < 99 )
        {
            m_Number = 99;
            m_UI_Text_Num.text = m_Number.ToString ();
            m_UI_Text_Total.text = ( m_Number * m_ShopTemp.getCost () [ 0 ] ).ToString ();

            if ( ObjectSelf.GetInstance ().Gold < m_Number * m_ShopTemp.getCost () [ 0 ] )
            {
                m_UI_Text_Total.color = Color.red;
            }
            else
            {
                m_UI_Text_Total.color = Color.white;
            }

        }
        else
        {
            InterfaceControler.GetInst ().AddMsgBox ( GameUtils.getString ( "ui_yingxiongqianghua_shengji8" ) );
        }

    }

    protected virtual void OnClickUI_Btn_Close ()
    {
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
    }

    public virtual void OnDestroy ()
    {
        Destroy ( m_Text );
        Destroy ( m_UI_Text_Name );
        Destroy ( m_UI_Text_Total );
        Destroy ( m_Text_1 );
        Destroy ( m_UI_Text_CountTitle );
        Destroy ( m_UI_Text_Count );
        Destroy ( m_UI_Btn_Add );
        Destroy ( m_UI_Btn_Minus );
        Destroy ( m_UI_Text_Num );
        Destroy ( m_UI_Btn_Sell );
        Destroy ( m_Text_2 );
        Destroy ( m_UI_Btn_Close );
        Destroy ( m_UI_Btn_Max );

        GameEventDispatcher.Inst.removeEventListener ( GameEventID.KE_ModItemNum, OnBuyItemSuccess );
        GameEventDispatcher.Inst.removeEventListener ( GameEventID.KE_KnapsackAdd, OnBuyItemSuccess );
    }
}
