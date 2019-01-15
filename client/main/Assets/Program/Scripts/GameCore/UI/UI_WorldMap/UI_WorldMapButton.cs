using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
public class UI_WorldMapButton : BaseUI
{
    private Button mapButton;
    public override void InitUIData()
    {
        base.InitUIData();
        mapButton = this.transform.GetComponent<Button>();
        mapButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnChapterClick));
    }

    public void OnChapterClick()
    {
        ObjectSelf.GetInstance().SetCurChapterID(int.Parse(this.name));
        UI_WordMap._instance.ChapterShow();
        
    }

}
