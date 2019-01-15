using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;

public class TextItem : BaseUI 
{
    private Text InfoText;//达成条件字体

    public override void InitUIData()
    {
        base.InitUIData();
        InfoText = selfTransform.FindChild("info").GetComponent<Text>();
    }
    public void SetTextData(string info)
    {
        InfoText.text = info;
    }
}
