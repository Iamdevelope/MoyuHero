using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using UnityEngine.Events;

public class SelectRune : BaseUI
{
	public int index;
	public int tableID;
	public X_GUID guid;
	public ItemTemplate _rune;
	public ItemEquip _data;
	private GameObject _starLevel;  

    static readonly int MaxStarCount = 5;
    private Image mRuneIcon = null;
    private Button mRuneBtn = null;
    private Text mLevel = null;
    //private Image[] mStarsFg = null;
    //private Image[] mStarsBg = null;
    private GameObject[] mStarObjs = null;
    private GameObject mLevelObj = null;
    private GameObject[] mEffect = null;
    private Image mNorBg = null;
    private Image mSpecBg = null;
    private GameObject[] mTypeObjs = null;

	public override void InitUIData()
	{
        mRuneIcon = selfTransform.FindChild("RuneIconList/iconBtn").GetComponent<Image>();
        mRuneBtn = selfTransform.FindChild("RuneIconList/iconBtn").GetComponent<Button>();
        mLevel = selfTransform.FindChild("RuneStarAndLevel/Level_txt").GetComponent<Text>();
        mLevelObj = selfTransform.FindChild("RuneStarAndLevel").gameObject;
        //mStarsFg = new Image[MaxStarCount];
        //mStarsBg = new Image[MaxStarCount];
        mStarObjs = new GameObject[MaxStarCount];
        mNorBg = selfTransform.FindChild("RuneIconList/bg").GetComponent<Image>();
        mSpecBg = selfTransform.FindChild("RuneIconList/bg1").GetComponent<Image>();

        for (int i = 0; i < MaxStarCount; i++)
        {
            //mStarsFg[i] = parent.FindChild("RuneStarAndLevel/Stars/Star_" + i).GetComponent<Image>();
            //mStarsBg[i] = parent.FindChild("RuneStarAndLevel/Stars/Star_" + i + "_BG").GetComponent<Image>();
            mStarObjs[i] = selfTransform.FindChild("RuneStarAndLevel/Stars/Star_" + i).gameObject;
        }

        mEffect = new GameObject[4];
        mTypeObjs = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            mEffect[i] = selfTransform.FindChild("EffectObj" + i).gameObject;
            mTypeObjs[i] = selfTransform.FindChild("RuneIconList/bg/type" + (i + 1)).gameObject;
        }
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

    ////////////////////////
    public void RuneIconItem(Transform parent)
    {
        mRuneIcon = parent.FindChild("RuneIconList/iconBtn").GetComponent<Image>();
        mRuneBtn = parent.FindChild("RuneIconList/iconBtn").GetComponent<Button>();
        mLevel = parent.FindChild("RuneStarAndLevel/Level_txt").GetComponent<Text>();
        mLevelObj = parent.FindChild("RuneStarAndLevel").gameObject;
        //mStarsFg = new Image[MaxStarCount];
        //mStarsBg = new Image[MaxStarCount];
        mStarObjs = new GameObject[MaxStarCount];
        mNorBg = parent.FindChild("RuneIconList/bg").GetComponent<Image>();
        mSpecBg = parent.FindChild("RuneIconList/bg1").GetComponent<Image>();

        for (int i = 0; i < MaxStarCount; i++)
        {
            //mStarsFg[i] = parent.FindChild("RuneStarAndLevel/Stars/Star_" + i).GetComponent<Image>();
            //mStarsBg[i] = parent.FindChild("RuneStarAndLevel/Stars/Star_" + i + "_BG").GetComponent<Image>();
            mStarObjs[i] = parent.FindChild("RuneStarAndLevel/Stars/Star_" + i).gameObject;
        }

        mEffect = new GameObject[4];
        mTypeObjs = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            mEffect[i] = parent.FindChild("EffectObj" + i).gameObject;
            mTypeObjs[i] = parent.FindChild("RuneIconList/bg/type" + (i + 1)).gameObject;
        }
    }

	public void ShowInfo()
	{
		_rune = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(tableID);
        //_icon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + _rune.getIcon());
        //_icon.SetNativeSize();
		_data = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, guid);
		//_level.text = "+" + _data.GetStrenghLevel().ToString();

        //int level = _rune.getRune_quality();
        //for (int i = 0; i < level; i++)
        //{
        //    _starLevel.transform.GetChild(i).gameObject.SetActive(true);
        //}

        SetIcon(common.defaultPath + _rune.getIcon());
        SetRuneType(_rune.getRune_type());
        //SetIsSpecial(_rune.getRune_type() == 5 || _rune.getRune_type() == 6);
        SetIsSpecial(RuneModule.IsSpecialRune(_rune));
        SetStarsNum(_rune.getRune_quality());
	}

     public void Destroy()
    {
        mRuneIcon = null;
        mRuneBtn = null;
        mLevel = null;
        //mStarsFg = null;
        //mStarsBg = null;
        mStarObjs = null;
        mEffect = null;
        mLevelObj = null;
    }

    public void SetStarsNum(int num)
    {
        //bool isActive = false;
        //for(int i = 0; i < MaxStarCount; i++)
        //{
        //    isActive = i < num;
        //    //mStarsBg[i].gameObject.SetActive(isActive);
        //    mStarsFg[i].gameObject.SetActive(isActive);

        //}

        for (int i = 0; i < MaxStarCount; i++)
        {
            mStarObjs[i].SetActive(i == (num - 1));
        }
    }

    public void SetIsSpecial(bool isSpecial)
    {
        mNorBg.gameObject.SetActive(!isSpecial);
        mSpecBg.gameObject.SetActive(isSpecial);
    }

    public void SetLevel(int level)
    {
        mLevel.text = "+" + level;
    }

    public void SetIcon(string icon)
    {
        //mRuneIcon.sprite = UIResourceMgr.LoadSprite(icon);
        //mRuneIcon.SetNativeSize();
        SetIcon(UIResourceMgr.LoadSprite(icon));
    }

    public void SetIcon(Sprite sprite)
    {
        mRuneIcon.sprite = sprite;
        mRuneIcon.SetNativeSize();
    }

    public void AddIconClickListener(UnityAction call)
    {
        mRuneBtn.onClick.AddListener(call);
    }

    public void RemoveIconClickListener(UnityAction call)
    {
        mRuneBtn.onClick.RemoveListener(call);
    }

    public void SetLevelInfoActive(bool active)
    {
        mLevelObj.SetActive(active);
    }

    public void SetRuneType(int runeType)
    {
        SetRuneType((EM_RUNE_TYPE)runeType);
    }

    public void SetRuneType(EM_RUNE_TYPE type)
    {
        int idx = -1;
        switch (type)
        {
            case EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID:
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_BLUE:
                idx = 0;
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_PURPLE:
                idx = 1;
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_GREEN:
                idx = 2;
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_RED:
                idx = 3;
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL:
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE:
                break;
            default:
                break;
        }

        for (int i = 0; i < 4; i++)
        {
            mTypeObjs[i].SetActive(i == idx);
        }
    }

    public void SetEffectShow(EM_RUNE_TYPE type)
    {
        for (int i = 0; i < 4; i++)
        {
            mEffect[i].SetActive(false);
        }

        switch (type)
        {
            case EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID:
                return;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_BLUE:
                mEffect[0].SetActive(true);
                return;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_PURPLE:
                mEffect[1].SetActive(true);
                return;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_GREEN:
                mEffect[2].SetActive(true);
                return;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_RED:
                mEffect[3].SetActive(true);
                return;
            default:
                mEffect[0].SetActive(true);
                return;
        }
    }
}
