using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;


public class LogoAlphaShow : BaseUI 
{
    public Image BaiDi;
    public Image Logo;
    private float AlphaValue = 0;
    public override void UpdateUIView()
    {
        base.UpdateUIView();
        if (AlphaValue <= 1f)
        { 
         AlphaValue += Time.deltaTime *0.7f;
         Logo.color = new Color(1f, 1f, 1f, AlphaValue);
        }
    }

}
