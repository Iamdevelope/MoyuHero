using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.UI;

public class UI_RuneMassage : BaseUI
{
    public static UI_RuneMassage _instance;
    public List<GameObject> mStarList = new List<GameObject>();
    private Text runeName;
    //private Image[] mTypes = null;
    //private Image runeIcon;
    //private Text runeLevel;
    private RuneIconItem mIconItem = null;
    protected GameObject mAttriList = null;
    protected GameObject mAttriTitleTxt = null;
    protected GameObject mRuneAttriObj = null;
    protected GameObject mAddRuneAttriObj = null;
    protected GameObject mAttriDetailTxt = null;
    private Button intensifyBtn;        //强化
    private Button authenticateBtn;     //鉴定
    private Text userName;  //装备人的名字

    public X_GUID mRuneGUID = null;

    //创建属性需要的集合
    List<GameObject> goCache = new List<GameObject>();
    List<GameObject> showAtt = new List<GameObject>();
    List<GameObject> temp = new List<GameObject>();
    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        runeName = selfTransform.FindChild("UI_BG_Title/name").GetComponent<Text>();

        //mTypes = new Image[4];

        //for (int i = 0; i < 4; i++ )
        //{
        //    mTypes[i] = selfTransform.FindChild("Image/type" + (i + 1)).GetComponent<Image>();
        //}

        //runeIcon = selfTransform.FindChild("UI_Icon").GetComponent<Image>();
        //runeLevel = selfTransform.FindChild("UI_Icon/levle(bg)/UI_Text_Level").GetComponent<Text>();
        mIconItem = new RuneIconItem(selfTransform.FindChild("RuneIconItem"));
        mAttriList = transform.FindChild("Attris/AttriList").gameObject;
        mAttriTitleTxt = transform.FindChild("Attris/Items/AttriTitle").gameObject;
        mRuneAttriObj = transform.FindChild("Attris/Items/AttriPair").gameObject;
        mAddRuneAttriObj = transform.FindChild("Attris/Items/AddAttriPair").gameObject;
        mAttriDetailTxt = transform.FindChild("Attris/Items/LineTxt").gameObject;
        intensifyBtn = selfTransform.FindChild("UI_Btn_Intensify").GetComponent<Button>();
        authenticateBtn = selfTransform.FindChild("UI_Btn_Authenticate").GetComponent<Button>();
        userName = selfTransform.FindChild("UI_BG_Title/state").GetComponent<Text>();
        intensifyBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickIntensify));
        authenticateBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAuthenticate));

       // UpdateShow(0, UI_RuneMange._instance.runeList[0].transform.GetComponent<UI_RuneButtonMassage>().rune);
    }
    public void OnClickIntensify()
    {
        UI_RuneMange._instance.mGuid = mRuneGUID;
        if (mRuneGUID == null)
            return;
        ItemEquip itemE = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, mRuneGUID);
        int strengthLv = itemE.GetStrenghLevel();
        ItemTemplate itemT = itemE.GetItemRowData();
        bool isFullLv = DataTemplate.GetInstance().IsRuneStrenthFullLevel(itemT, strengthLv);

        //已满级;
        if (isFullLv)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip2"));
            return;
        }

        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_RuneStrenthMgr.UI_ResPath);
        UI_RuneStrenthMgr.SetShowRuneGUID(mRuneGUID);

        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
    }
    public void OnClickAuthenticate()
    {
        UI_RuneMange._instance.mGuid = mRuneGUID;
        if (mRuneGUID == null)
            return;
        
        ItemEquip itemE = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, mRuneGUID);
        ItemTemplate itemT = itemE.GetItemRowData();

        //一星符文没法鉴定;
        if (itemT.getRune_quality() <= 1)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip3"));
            return;
        }

        int count = DataTemplate.GetInstance().GetRuneMaxRedefineTimes(itemT);

        //是否鉴定满级;
        if (itemE.GetDefineTimes() >= count)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_rune_tip4"));
            return;
        }

        UI_RuneIdentifyMgr.SetShowRuneGUID(mRuneGUID);
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_RuneIdentifyMgr.UI_ResPath);

        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
    
    }

 
    public void UpdateShow(int runeid,ItemTemplate rune)
    {
        if (rune == null) return;
        mRuneGUID = UI_RuneMange._instance.rune[runeid].GetItemGuid();

        runeName.text = GameUtils.getString(rune.getName());
        //runeIcon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + rune.getIcon());
        //runeIcon.transform.localScale = new Vector3(0.8f,0.8f,0f);
        //runeIcon.SetNativeSize();

        mIconItem.SetIcon(common.defaultPath + rune.getIcon());
        mIconItem.SetStarsNum(rune.getRune_quality());
        mIconItem.SetRuneType(rune.getRune_type());
        //int starCount = rune.getRune_quality();
        //for (int n = 0; n < mStarList.Count; n++)
        //{
        //    if (n == starCount - 1)
        //    {
        //        mStarList[n].SetActive(true);
        //    }
        //    else
        //    {
        //        mStarList[n].SetActive(false);
        //    }
        //}

        //for (int m = 1; m <= 4; m++)
        //{
        //    mTypes[m - 1].gameObject.SetActive(m == rune.getRune_type());
        //}

        mIconItem.SetIsSpecial(RuneModule.IsSpecialRune(rune));

        //GameUtils.DestroyChildsObj(mAttriList);
        ItemEquip data = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, mRuneGUID);
        //runeLevel.text = "+" + data.GetStrenghLevel();
        mIconItem.SetLevel(data.GetStrenghLevel());

        //装备人名字
        ObjectCard oc = ObjectSelf.GetInstance().HeroContainerBag.GetItemUser(data);
        if (oc == null)
        {
            userName.gameObject.SetActive(false);
        }
        else
        {
            HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(oc.GetHeroData().TableID);
            if (heroT != null)
                userName.text = GameUtils.getString(heroT.getTitleID()) + "    " + GameUtils.getString("hero_rune_content7");
            userName.gameObject.SetActive(true);
        }

        //是否满级;
        bool isFullLv = DataTemplate.GetInstance().IsRuneStrenthFullLevel(rune, data.GetStrenghLevel());
        GameUtils.SetBtnSpriteGrayState(intensifyBtn, isFullLv);
        showAtt.Clear();
        //--------基础属性;
        RuneData runeData = data.GetRuneData();
        bool titleDone1 = false;
        int split = 0;
        int k = 0,p=0;
        foreach (int id in runeData.BaseAttributeID)
        {
            if (id == -1)
                continue;

            if (!titleDone1)
            {
                k++;
                titleDone1 = true;
                CreateTitle(mAttriList, GameUtils.getString("hero_rune_content8"), k - 1);
            }
        }
        foreach (int id in runeData.BaseAttributeID)
        {
           
            if (id == -1)
                continue;
            BaseruneattributeTemplate bt = DataTemplate.GetInstance().GetBaseruneattributeTemplate(id);
             p++;
            if (bt.getNumshow() != 0)
            {
                CreateBaseAttriObj(mAttriList, GameUtils.getString(bt.getAttriDes()), "+" + bt.getAttriValue().ToString(), p - 1);
            }
        }
        //创建特殊服务的特效效果属性
        foreach (int id in runeData.BaseAttributeID)
        {
            if (id == -1)
                continue;
            BaseruneattributeTemplate bt = DataTemplate.GetInstance().GetBaseruneattributeTemplate(id);
            if (bt.getNumshow() == 0)
            {
                CreateDetailTxts(mAttriList, GameUtils.getString(bt.getAttriDes()));
            }
        }

        //--------附加属性;
        int count = DataTemplate.GetInstance().GetRuneMaxRedefineTimes(rune);
        bool titleDone2 = false;
        int i = 0;
        bool isGray = false;
        int  b = 0;

        foreach (int id in runeData.AppendAttribute)
        {
            i++;

            isGray = i * 3 > data.GetStrenghLevel();

            if (id == -1)
            {
                if (i <= count)
                {
                    if (!titleDone2)
                    {
                        k++;
                        titleDone2 = true;
                        CreateTitle(mAttriList, GameUtils.getString("hero_rune_content9"),k-1);
                    }
                    b++;
                    //未知属性，未鉴定;
                    CreateAddAttriObj(mAttriList, GameUtils.getString("rune_content2"), "", GameUtils.getString("rune_content3"), isGray,b-1);
                }

                continue;
            }

            if (!titleDone2)
            {
                k++;
                titleDone2 = true;
                CreateTitle(mAttriList, GameUtils.getString("hero_rune_content9"),k-1);
            }
            b++;
            AddruneattributeTemplate bt = DataTemplate.GetInstance().GetAddruneattributeTemplate(id);
            bool isPercent = bt.getIspercentage() > 0;
            string val = isPercent ? ((float)bt.getAttriValue() / (float)10f + "%") : bt.getAttriValue().ToString();
            CreateAddAttriObj(mAttriList, GameUtils.getString(bt.getAttriDes1()), GameUtils.getString(bt.getAttriDes2()), bt.getSymbol() + val, isGray,b-1);
        }
        for (int m = 0; m < showAtt.Count; m++)
        {
            showAtt[m].SetActive(true);
        }

        //是否鉴定满级;
        GameUtils.SetBtnSpriteGrayState(authenticateBtn, data.GetDefineTimes() >= count);
    }

    void CreateDetailTxts(GameObject parent, string detail)
    {
        //int totalCount = detail.Length;
        //int tmp = totalCount % GlobalMembers.MAX_RUNE_COUNT_PER_LINE;
        //int lineNum = 0;

        //if (tmp == 0)
        //    lineNum = totalCount / GlobalMembers.MAX_RUNE_COUNT_PER_LINE;
        //else
        //    lineNum = totalCount / GlobalMembers.MAX_RUNE_COUNT_PER_LINE + 1;

        //int startIdx = -1, endIdx = -1;
        //for (int i = 0; i < lineNum; i++)
        //{
        //    startIdx = GlobalMembers.MAX_RUNE_COUNT_PER_LINE * i;
        //    endIdx = GlobalMembers.MAX_RUNE_COUNT_PER_LINE * (i + 1);
        //    if (i == lineNum - 1)
        //    {
        //        CreateDetailTxt(parent, detail.Substring(startIdx));
        //    }
        //    else
        //    {
        //        CreateDetailTxt(parent, detail.Substring(startIdx, endIdx));
        //    }
        //}

        string[] contents = detail.SplitByLength(GlobalMembers.MAX_RUNE_COUNT_PER_LINE);
      
        if (contents == null)
        {
            return;
        }

        int count = contents.Length;

        if (count <= 0)
        {
            return;
        }

        for (int i = 0; i < count; i++ )
        {
            CreateDetailTxt(parent, contents[i],i);
        }
    }
    void CreateDetailTxt(GameObject parent, string detail,int createIndex)
    {
        goCache.Clear();
        for (int i = 0; i < mAttriList.transform.childCount; i++)
        {
            if (mAttriList.transform.GetChild(i).name == "LineTxt")
            {
                goCache.Add(mAttriList.transform.GetChild(i).gameObject);
            }
            mAttriList.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (goCache.Count < createIndex + 1)
        {
            int lastLineTextIndex = -1;
            if (goCache.Count != 0)
            {
                lastLineTextIndex = goCache[goCache.Count - 1].transform.GetSiblingIndex();
            }
            else
            {
                //创建在基础属性的下面
                temp.Clear();
                for (int i = 0; i < mAttriList.transform.childCount; i++)
                {
                    if (mAttriList.transform.GetChild(i).name == "AttPair")
                    {
                        temp.Add(mAttriList.transform.GetChild(i).gameObject);
                    }
                }
                lastLineTextIndex = temp[temp.Count - 1].transform.GetSiblingIndex();
            }
            GameObject go = (GameObject)GameObject.Instantiate(mAttriDetailTxt.gameObject);
            go.transform.parent = parent.transform;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);
            go.transform.SetSiblingIndex(lastLineTextIndex+1);
            go.transform.FindChild("Text").GetComponent<Text>().text = detail;
            go.name = "LineTxt";
            go.SetActive(true);
            showAtt.Add(go);
        }
        else
        {
            if (goCache[createIndex] != null)
            {
                goCache[createIndex].SetActive(true);
                goCache[createIndex].transform.FindChild("Text").GetComponent<Text>().text = detail;
                showAtt.Add(goCache[createIndex]);
            }
        }



        //GameObject go = (GameObject)GameObject.Instantiate(mAttriDetailTxt.gameObject);

        //go.transform.parent = parent.transform;
        //go.transform.localScale = Vector3.one;
        //go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);

        //go.transform.FindChild("Text").GetComponent<Text>().text = detail;
    }

    /// <summary>
    /// 创建属性标题;--基础属性、附加属性;
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="str"></param>
    void CreateTitle(GameObject parent, string str,int createIndex)
    {
      // GameObject goCache= mAttriList.transform.FindChild("AttriTitle").gameObject
       //bool isFind = false;
       goCache.Clear();
       for (int i = 0; i < mAttriList.transform.childCount; i++)
       {
           if (mAttriList.transform.GetChild(i).name == "AttriTitle")
           {
               goCache.Add(mAttriList.transform.GetChild(i).gameObject);
           }
           mAttriList.transform.GetChild(i).gameObject.SetActive(false);
       }

       if (goCache.Count < createIndex + 1)
       {
           GameObject go = (GameObject)GameObject.Instantiate(mAttriTitleTxt.gameObject);
           go.transform.parent = parent.transform;
           go.transform.localScale = Vector3.one;
           go.name = "AttriTitle";
           go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);
           go.GetComponent<Text>().text = str;
           go.SetActive(true);
           showAtt.Add(go);
       }
       else
       {
           if (goCache[createIndex] != null)
           {
               goCache[createIndex].SetActive(true);
               goCache[createIndex].GetComponent<Text>().text = str;
               showAtt.Add(goCache[createIndex]);
           }
       }
    }

    /// <summary>
    /// 创建属性标题;--基础属性;
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="str"></param>
    void CreateBaseAttriObj(GameObject parent, string str1, string str2, int createIndex)
    {
        goCache.Clear();
        //bool isFind = false;
        for (int i = 0; i < mAttriList.transform.childCount; i++)
        {
            if (mAttriList.transform.GetChild(i).name == "AttPair")
            {
                goCache.Add(mAttriList.transform.GetChild(i).gameObject);
            }
           mAttriList.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (goCache.Count < createIndex + 1)
        {
            int lastLineTextIndex = -1;
            if (goCache.Count != 0)
            {
                lastLineTextIndex = goCache[goCache.Count - 1].transform.GetSiblingIndex();
            }
            else
            {
                //创建在标题的下面
                temp.Clear();
                for (int i = 0; i < mAttriList.transform.childCount; i++)
                {
                    if (mAttriList.transform.GetChild(i).name == "AttriTitle")
                    {
                        temp.Add(mAttriList.transform.GetChild(i).gameObject);
                    }
                }
                lastLineTextIndex = temp[temp.Count - 1].transform.GetSiblingIndex();
            }

            GameObject go = GameObject.Instantiate(mRuneAttriObj) as GameObject;

            go.transform.parent = parent.transform;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);
            go.transform.SetSiblingIndex(lastLineTextIndex+1);
            go.name = "AttPair";
            go.transform.FindChild("Left_txt").GetComponent<Text>().text = str1;
            go.transform.FindChild("Right_txt").GetComponent<Text>().text = str2;
            go.SetActive(true);
            showAtt.Add(go);
        }
        else
        {
            if (goCache[createIndex] != null)
            {
                goCache[createIndex].SetActive(true);
                goCache[createIndex].transform.FindChild("Left_txt").GetComponent<Text>().text = str1;
                goCache[createIndex].transform.FindChild("Right_txt").GetComponent<Text>().text = str2;
                showAtt.Add(goCache[createIndex]);
            }
        }
    }

    /// <summary>
    /// 创建属性列;--附加属性;
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    void CreateAddAttriObj(GameObject parent, string str1, string str2, string str3, bool isGray, int createIndex)
    {
        goCache.Clear();
        //bool isFind = false;
        for (int i = 0; i < mAttriList.transform.childCount; i++)
        {
            if (mAttriList.transform.GetChild(i).name == "AddAttPair")
            {
                goCache.Add(mAttriList.transform.GetChild(i).gameObject);
            }
            mAttriList.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (goCache.Count < createIndex + 1)
        {
            GameObject go = GameObject.Instantiate(mAddRuneAttriObj) as GameObject;

            go.transform.parent = parent.transform;
            go.transform.localScale = Vector3.one;
            go.name = "AddAttPair";
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0f);
            go.SetActive(true);
            showAtt.Add(go);

            if (isGray)
            {
                str1 = GameUtils.StringWithGrayColor(str1);
                str2 = GameUtils.StringWithGrayColor(str2);
                str3 = GameUtils.StringWithGrayColor(str3);
            }

            go.transform.FindChild("Left_txt").GetComponent<Text>().text = str1;
            go.transform.FindChild("Mid_txt").GetComponent<Text>().text = str2;
            go.transform.FindChild("Right_txt").GetComponent<Text>().text = str3;
        }
        else
        {
            if (goCache[createIndex] != null)
            {
                if (isGray)
                {
                    str1 = GameUtils.StringWithGrayColor(str1);
                    str2 = GameUtils.StringWithGrayColor(str2);
                    str3 = GameUtils.StringWithGrayColor(str3);
                }

                goCache[createIndex].transform.FindChild("Left_txt").GetComponent<Text>().text = str1;
                goCache[createIndex].transform.FindChild("Mid_txt").GetComponent<Text>().text = str2;
                goCache[createIndex].transform.FindChild("Right_txt").GetComponent<Text>().text = str3;
                goCache[createIndex].SetActive(true);
                showAtt.Add(goCache[createIndex]);
            }
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
