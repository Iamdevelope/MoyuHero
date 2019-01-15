using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;

public class EquipmentItem : BaseUI
{
	protected Text m_Text;
    protected Image m_Bg;
    protected Image m_Icon;
    protected Image m_AddImage;
    Button m_SelfBtn;

    int m_TableId;

	public override void InitUIData()
	{
		base.InitUIData();
		m_Text = selfTransform.FindChild("Text").GetComponent<Text>();
        m_Bg = selfTransform.Find ( "Bg" ).GetComponent<Image> ();
        m_Icon = selfTransform.Find ( "Icon" ).GetComponent<Image> ();
        m_AddImage = selfTransform.Find ( "addImage" ).GetComponent<Image> ();
        m_SelfBtn = selfTransform.GetComponent<Button> ();
        m_SelfBtn.onClick.AddListener ( OnClickSelfBtn );
    }

	public override void InitUIView()
	{
		base.InitUIView();
	}

    public void ShowInfo (int table, int number)
    {
        m_TableId = table;

        int num = GetIdInBagNum ( table );
        m_Text.text = num + "/" + number.ToString ();
        m_Icon.sprite = DynamicItem.GetSprite ( table );

        m_AddImage.gameObject.SetActive (!( num > 0) );
    }

    private int GetIdInBagNum ( int id )
    {
        int haveNum = -1;
        ObjectSelf.GetInstance ().TryGetItemCountById ( EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id, ref haveNum );
        return haveNum;
    }

    void OnClickSelfBtn ()
    {
        UICommonManager.Inst.ShowHeroObtain ( m_TableId );
    }
}
