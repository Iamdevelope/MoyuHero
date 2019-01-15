using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;

public class UpgradePop : BaseUI
{
    public static UpgradePop Inst;
    public static string UI_ResPath = "HeroStrengthen/UI_UpgradePop_1_2";

    GameObject m_AttrList;
    GameObject m_AttrItem;

    protected float m_CurTime = 0.0f;
    protected float m_AllTime = 2.0f;

    public override void InitUIData ()
    {
        Inst = this;
        base.InitUIData ();
        m_AttrList = selfTransform.Find ( "win/AttributesList" ).gameObject;
        m_AttrItem = selfTransform.Find ( "win/AttributesList/StartLayout" ).gameObject;
    }
    
    public void ShowInfo (ObjectCard card)
    {
        Debug.Log ( "ShowInfo" );
    }

    void Update ()
    {
        m_CurTime += Time.deltaTime;

        if ( m_CurTime > m_AllTime )
        {
            UI_HomeControler.Inst.ReMoveUI ( gameObject );
        }
    }

}

