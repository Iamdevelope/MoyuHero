using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI;
using UnityEngine.Events;

public enum RuneIconItemSize
{
    Normal,     //正常大小;
    Big,        //1.4倍大小;
}

/// <summary>
/// 展示符文处的小界面;
/// 符文图标，符文等级，符文强化星级;
/// </summary>
public class RuneIconItem
{
    protected static readonly int MaxStarCount = 5;

    protected Sprite m_AddSprite = null;

    protected Image mRuneIcon = null;
    protected Button mRuneBtn = null;
    protected Text mLevel = null;
    //protected Image[] mStarsFg = null;
    //protected Image[] mStarsBg = null;
    protected GameObject[] mStarObjs = null;
    protected GameObject mLevelObj = null;
    protected Transform mIconTrans = null;
    protected GameObject[] mEffect = null;
    protected Image mNorBg = null;
    protected Image mSpecBg = null;
    protected GameObject[] mTypeObjs = null;
    protected CanvasGroup mCanvasGroup = null;

    protected Transform mTrans = null;
    
    ////////////////////////
    
    public int index;



    ////////////////////////
    public RuneIconItem(Transform parent)
    {
        mTrans = parent;

        if (parent == null)
            return;

        mRuneIcon = parent.FindChild("RuneIconList/iconBtn").GetComponent<Image>();

        m_AddSprite = mRuneIcon.sprite;

        mRuneBtn = parent.FindChild("RuneIconList/iconBtn").GetComponent<Button>();
        mLevel = parent.FindChild("RuneStarAndLevel/Level_txt").GetComponent<Text>();
        mLevelObj = parent.FindChild("RuneStarAndLevel").gameObject;
        mIconTrans = parent.FindChild("RuneIconList");
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

        mCanvasGroup = parent.GetComponent<CanvasGroup>();
    }

    public void Destroy()
    {
        if (mTrans != null)
        {
            GameObject.Destroy(mTrans);
            mRuneBtn.onClick.RemoveAllListeners();
            mTrans = null;
        }

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

    public void SetStarsActive(bool isActive)
    {

    }

    public void SetClickable(bool clickAble)
    {
        mCanvasGroup.interactable = clickAble;
    }

    public void SetSize(RuneIconItemSize size)
    {
        switch (size)
        {
            case RuneIconItemSize.Normal:
                mIconTrans.localScale = Vector2.one;
                break;
            case RuneIconItemSize.Big:
                mIconTrans.localScale = Vector2.one * 1.4f;
                break;
            default:
                break;
        }
    }

    //设置是否组织事件传递;
    public void SetIsBlock(bool isBlock)
    {
        mCanvasGroup.blocksRaycasts = isBlock;
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
        //mRuneIcon.SetNativeSize();
    }


    public void SetColor(Color color)
    {
        mRuneIcon.color = color;
        for (int i = 0, j = mTypeObjs.Length; i < j; i++ )
        {
            mTypeObjs[i].GetComponent<Image>().color = color;
        }
        mNorBg.color = color;
        mSpecBg.color = color;
    }

    public void ShowAddIcon()
    {
        SetIcon(m_AddSprite);
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

    public void SetActive(bool isActive)
    {
        mTrans.gameObject.SetActive(isActive);
    }
}
