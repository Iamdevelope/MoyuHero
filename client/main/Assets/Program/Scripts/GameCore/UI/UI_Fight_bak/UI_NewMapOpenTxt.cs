using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.UI;

public class UI_NewMapOpenTxt : BaseUI 
{
    public static UI_NewMapOpenTxt _inst;
    public static string UI_ResPath = "UI_Fight/UI_NewMapTxt_2_2";

    private bool isClick = true;
    private Text m_Tilite;
    private Text m_NewMapName;
    private Button m_CloseBtn;
    private Text m_Text;
    private Image m_NewMapBackImg;

    public override void InitUIData()
    {
        base.InitUIData();
        _inst = this;
        m_Tilite = selfTransform.FindChild("TitlePanel/TitleObj/Text").GetComponent<Text>();
        m_NewMapName = selfTransform.FindChild("NewMapName").GetComponent<Text>();
        m_CloseBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
        m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
        m_Text = selfTransform.FindChild("CloseBtn/Text").GetComponent<Text>();
        m_NewMapBackImg = selfTransform.FindChild("NewMapBackImg").GetComponent<Image>();
    }
    public override void InitUIView()
    {
        base.InitUIView();
        Invoke("OnClickCloseBtn",3);
        InitUI();
    }

    void InitUI()
    {
        ChapterinfoTemplate levelInfo = (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(ObjectSelf.GetInstance().GetCurChapterID());
        m_NewMapName.text = GameUtils.getString( levelInfo.getChapterName());
        m_NewMapBackImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + levelInfo.getBackgroundPicture());
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();
        if (isClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClickCloseBtn();
                isClick = false;
            }
        }

    }

    private void OnClickCloseBtn()
    {
        if (gameObject != null)
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
            ObjectSelf.GetInstance().SetIsNewMap(false);
        }
    }
}
