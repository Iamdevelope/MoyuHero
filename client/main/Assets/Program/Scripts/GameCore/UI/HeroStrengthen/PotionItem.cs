using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;

public class PotionItem : BaseUI
{
    Image m_Icon;
    Text m_Number;
    Image m_AddImage;
    Button m_SelfBtn;
    int m_Count = 0;
    int m_TableID = 0;

    public override void InitUIData ()
    {
        base.InitUIData ();

        m_Icon = selfTransform.Find ( "Icon" ).GetComponent<Image> ();
        m_Number = selfTransform.Find ( "Text" ).GetComponent<Text> ();
        m_AddImage = selfTransform.Find ( "addImage" ).GetComponent<Image> ();
        m_SelfBtn = selfTransform.GetComponent<Button> ();
        m_SelfBtn.onClick.AddListener ( OnClickSelfBtn );
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    public void ShowInfo ( int count, ItemTemplate item )
    {
        m_Count = count;
        m_TableID = item.GetID ();
        m_Number.text = count.ToString ();
        m_Icon.sprite = UIResourceMgr.LoadSprite ( common.defaultPath + item.getIcon_s () );

        //m_AddImage.gameObject.SetActive ( !( count > 0 ) );
    }

    void OnClickSelfBtn ()
    {
        if ( m_Count > 0 )
        {
            // 显示资源信息
            UICommonManager.Inst.ShowHeroObtain ( m_TableID );
        }
        else
        {
            // 购买道具
            //UI_HomeControler.Inst.AddUI ( HeroItemBuy.UI_ResPath );
            //HeroItemBuy.Inst.ShowInfo ( m_TableID );
            UICommonManager.Inst.ShowHeroObtain(m_TableID);
        }
    }

}
