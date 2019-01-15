using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using GNET;
using DreamFaction.Utils;

public class UI_MedardGotoObj : BaseUI
{
    public static string UI_ResPath = "UI_Home/UI_MedalReardGotoObj_1_1";
    private Text m_DesTxt;                                     //描述文本
    private Button m_GotoBtn;                                  //前往按钮
    private Button m_LaterTalkBtn;                             //稍后再说按钮
    private Text m_GotoBtnTxt;                                 //转到按钮文本
    private Text m_LaterTalkBtnTxt;                            //稍后再说按钮文本
    public override void InitUIData()
    {
        base.InitUIData();
        m_DesTxt = selfTransform.FindChild("DesTxt").GetComponent<Text>();
        m_GotoBtn = selfTransform.FindChild("GotoBtn").GetComponent<Button>();
        m_LaterTalkBtn = selfTransform.FindChild("LaterTalkBtn").GetComponent<Button>();
        m_GotoBtnTxt = selfTransform.FindChild("GotoBtn/Text").GetComponent<Text>();
        m_LaterTalkBtnTxt = selfTransform.FindChild("LaterTalkBtn/Text").GetComponent<Text>();


        m_GotoBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGotoBtn));
        m_LaterTalkBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLaterTalkBtn));
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_DesTxt.text = GameUtils.getString("pokedex_content13");
    }

    /// <summary>
    /// 立即前往按钮
    /// </summary>
    private void OnClickGotoBtn()
    {
        CTuJianHeros _ctj = new CTuJianHeros();
        IOControler.GetInstance().SendProtocol(_ctj);

        UI_HomeControler.Inst.AddUI(UI_HandBookManager.UI_ResPath);
        UI_HomeControler.Inst.AddUI(UI_MedalReard.UI_ResPath);

        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    /// <summary>
    /// 稍后再说按钮
    /// </summary>
    private void OnClickLaterTalkBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
}
