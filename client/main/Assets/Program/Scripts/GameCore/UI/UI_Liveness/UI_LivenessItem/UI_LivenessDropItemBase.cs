using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_LivenessDropItemBase : CellItem
{

    public Image icon;
    public Text desc;
    public Text numText;
    public UIHeroStar star;

    //符文
    public GameObject RuneIcon;//符文的父物体
    public Image mNorBg = null;
    public Image mSpecBg = null;
    public Image RuneImage = null;
    public GameObject[] mTypeObjs = null;

    public override void InitUIData()
    {
        base.InitUIData();
        icon = transform.FindChild("icon").GetComponent<Image>();
        desc = transform.FindChild("desc").GetComponent<Text>();
        numText = transform.FindChild("img/numText").GetComponent<Text>();
        star = transform.FindChild("star").GetComponent<UIHeroStar>();

        //符文
        RuneIcon = selfTransform.FindChild("RuneIconItem").gameObject;
        mNorBg = selfTransform.FindChild("RuneIconItem/RuneIconList/bg").GetComponent<Image>();
        mSpecBg = selfTransform.FindChild("RuneIconItem/RuneIconList/bg1").GetComponent<Image>();
        RuneImage = selfTransform.FindChild("RuneIconItem/RuneIconList/icon").GetComponent<Image>();
        mTypeObjs = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            mTypeObjs[i] = transform.FindChild("RuneIconItem/RuneIconList/bg/type" + (i + 1)).gameObject;
        }
    }
}
