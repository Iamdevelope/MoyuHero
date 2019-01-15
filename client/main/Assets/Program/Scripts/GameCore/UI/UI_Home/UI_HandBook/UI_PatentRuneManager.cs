using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.UI;
using DreamFaction.Utils;
using UnityEngine.UI;

public class UI_PatentRuneManager : CustomUI 
{
    public static UI_PatentRuneManager Inst;
    public static string UI_ResPath = "UI_Home/UI_PanentRaue_2_3";

    private Transform m_Grid = null;                                                            //Grid
    private LoopLayout m_HeroLayout = null;
    public GameObject ItemMsgUI = null;                                                         //符文弹窗
    private Button m_CloseBtn = null;                                                           //关闭按钮
    private Text m_TiliteText = null;                                                           //标题文本
    
    public override void InitUIData()
    {
        base.InitUIData();
        Inst = this;
        m_Grid = selfTransform.FindChild("ItemList/Grid");
        ItemMsgUI = selfTransform.FindChild("ItemMsgUI").gameObject;
        m_HeroLayout = m_Grid.GetComponent<LoopLayout>();

        m_CloseBtn = selfTransform.FindChild("PlayerInfoItem/CloseBtn").GetComponent<Button>();
        m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
        m_TiliteText = selfTransform.FindChild("PlayerInfoItem/Image/TiliteText").GetComponent<Text>();

        captionPath = "caption";
    }

    public override void InitUIView()
    {
        base.InitUIView();

        m_HeroLayout.cellCount = UI_HandBookManager.Inst.m_RaueHandBookList.Count;
        m_HeroLayout.updateCellEvent = LoadPrefabUI;
        m_HeroLayout.Reload();
    }


    /// <summary>
    /// 初始化加载Prefab
    /// </summary>
    private void LoadPrefabUI(int index, RectTransform cell)
    {
            UI_PatentItem _item = cell.GetComponent<UI_PatentItem>();
            if (_item == null)
            {
                _item = cell.gameObject.AddComponent<UI_PatentItem>();
            }

            _item.index = index;
            _item.InitItemData(UI_HandBookManager.Inst.m_RaueHandBookList[index]);
 
    }

    /// <summary>
    /// 关闭按钮
    /// </summary>
    private void OnClickCloseBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
}
