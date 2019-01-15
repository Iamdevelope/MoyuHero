using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.UI;

public class UI_TitleBtnItem : BaseUI
{
    public int _id = 0;
    private Button _SelectBtn;
    private GameObject _Effect;
    public override void InitUIData()
    {
        base.InitUIData();
        _SelectBtn = this.GetComponent<Button>();
        _Effect = selfTransform.FindChild("Button").gameObject;
        _SelectBtn.onClick.AddListener(new UnityAction(OnsSelecked));
    }

    void OnsSelecked()
    {
        UI_HeroInfo._instance.SelectedShow(this._id);
    }

    public void HighlightShow()
    {
        _Effect.SetActive(true);
    }

    public void GeneralShow()
    {
        _Effect.SetActive(false);
    }
}
