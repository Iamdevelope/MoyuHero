using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameCore;

public class UI_MainHeroInfo : BaseUI 
{
    private Text HeroName;                                             //名字
    private Transform HeroLevel;                                       //等级
    private int HeroStar;                                              //星级
    private ObjectCard _card;
    public override void InitUIData()
    {
        base.InitUIData();
        HeroName =  selfTransform.FindChild("Name_txt").GetComponent<Text>();
        HeroLevel = selfTransform.FindChild("Level_txt");
    }


    public void InitHeroData(ObjectCard card)
    {
        _card = card;
        HeroTemplate _HeroData = card.GetHeroRow();
        gameObject.SetActive(true);
        ChsTextTemplate chs = new ChsTextTemplate();
        chs = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(_HeroData.getTitleID());
        HeroName.text = chs.languageMap["Chinese"];
        string level = card.GetHeroData().Level.ToString();
        InterfaceControler.AddLevelNum(level, HeroLevel);
        HeroStar = _HeroData.getQuality();
        int maxStar = _HeroData.getMaxQuality();
        for (int i = 5; i < 10; ++i)//星级
        {
            selfTransform.FindChild("Star_Image").GetChild(i - 5).GetComponent<Image>().enabled = i < 5 + maxStar;
            selfTransform.FindChild("Star_Image").GetChild(i).GetComponent<Image>().enabled = i < 5 + HeroStar;
            //if (i < 5 + HeroStar)
            //{
            //    Image temp = selfTransform.FindChild("Star_Image").GetChild(i).GetComponent<Image>();
            //    temp.enabled = true;
            //}
            //else
            //{
            //    Image temp = selfTransform.FindChild("Star_Image").GetChild(i).GetComponent<Image>();
            //    temp.enabled = false;
            //}
        }
    }

    public void OnClearObj()
    {
        _card = null;
        gameObject.SetActive(false);
    }

}
