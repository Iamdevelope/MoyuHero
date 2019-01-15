using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction;
using DreamFaction.UI;
public class UI_LevelItem
{
    public delegate void OnClick(int id);
    public event OnClick onClick = null;

    protected Text m_TitleTxt = null;
    protected Image mSelectImg = null;
    protected Image m_Icon = null;
    protected Button m_IconBtn = null;
    protected Image[] mStarsImg = null;
    protected Image mIsNewImg = null;       //是否新开启;
    protected Text mRemindTimes = null;
    protected Text mRemindTitle = null;     //剩余扫荡次数;

    private GameObject mGo = null;

    const int MaxStars = 3;

    private StageTemplate mStageT = null;

    public UI_LevelItem(GameObject go)
    {
        mGo = go;

        Transform trans = mGo.transform;

        m_TitleTxt = trans.FindChild("Panel/Level").GetComponent<Text>();
        m_Icon = trans.FindChild("icon").GetComponent<Image>();
        m_IconBtn = trans.FindChild("icon").GetComponent<Button>();
        mSelectImg = trans.FindChild("Panel/selectIcon").GetComponent<Image>();

        mStarsImg = new Image[MaxStars];
        for (int i = 0; i < MaxStars; i++)
        {
            mStarsImg[i] = trans.FindChild("Panel/start_" + i).GetComponent<Image>();
        }
        mIsNewImg = trans.FindChild("Panel/tag").GetComponent<Image>();
        mRemindTitle = trans.FindChild("Panel/limit").GetComponent<Text>();
        mRemindTimes = trans.FindChild("Panel/limit/value").GetComponent<Text>();
        m_IconBtn.onClick.AddListener(OnIconBtnClick);

        InitStr();
    }

    void InitStr()
    {
        //mRemindTitle.text = GameUtils.getString("");
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
        //mGo.transform.parent = parent;
        //mGo.transform.localScale = Vector3.one;
        //mGo.transform.localPosition = Vector3.zero;
    }

    void OnIconBtnClick()
    {
        if (mStageT == null)
        {
            return;
        }

        if (onClick != null)
        {
            onClick(mStageT.m_stageid);
        }
    }

    public void SetOnEndDrag(EventTriggerListener.EventDataDelegate handler)
    {
        EventTriggerListener.Get(m_IconBtn.gameObject).onEndDrag = handler;

    }

    public void SetOnClick(OnClick clickHandler)
    {
        onClick = clickHandler;
    }

    public void UpdatePerSec()
    {
        if (mStageT == null)
        {
            return;
        }

        //是不是神秘商店;
        if (StageModule.IsMysteriousShop(mStageT.m_stageid))
        {
            SpecialStage ss = ObjectSelf.GetInstance().BattleStageData.GetSpecialStageData();

            if (ss.m_Time <= 0)
            {
                mGo.SetActive(false);
            }

            return;
        }

        switch (StageModule.GetStageStageType(mStageT))
        {
            case EM_STAGE_STAGETYPE.MAIN:
            case EM_STAGE_STAGETYPE.SIDE:
                break;
            case EM_STAGE_STAGETYPE.MYSTERIOUS:
                //神秘关卡计时;

                break;
            case EM_STAGE_STAGETYPE.SPECIAL:
                //特殊关卡计时;
                SpecialStage ss = ObjectSelf.GetInstance().BattleStageData.GetSpecialStageData();

                if (ss.m_Time <= 0)
                {
                    mGo.SetActive(false);
                }
                break;
            case EM_STAGE_STAGETYPE.ACTIVE:
                break;
            case EM_STAGE_STAGETYPE.BOSS:
                break;
            case EM_STAGE_STAGETYPE.LITMIT_TIMES:
                break;
            default:
                break;
        }
    }

    public void SetTemplateData(StageTemplate data, EM_STAGE_STAGETYPE flag = EM_STAGE_STAGETYPE.NONE)
    {
        mStageT = data;

        if (data == null)
        {
            mGo.SetActive(false);
            return;
        }

        //如果是神秘商店特殊处理;
        if (flag == EM_STAGE_STAGETYPE.MYSTERIOUS)
        {
            m_TitleTxt.text = "";

            mStarsImg[0].gameObject.SetActive(false);
            mStarsImg[1].gameObject.SetActive(false);
            mStarsImg[2].gameObject.SetActive(false);

            mIsNewImg.gameObject.SetActive(false);

            //这里data.m_stageid存的是场景id;
            ChapterinfoTemplate chapterT = StageModule.GetChapterinfoTemplateById(data.m_stageid);
            float[] val = chapterT.getShopposition();
            Vector2 pos1 = new Vector2(val[0], val[1]);
            mGo.transform.localPosition = pos1;
            
            return;
        }

        //不是神秘商店另外处理;
        int chapterId = DataTemplate.GetInstance().GetChapterIdByStageT(data);

        EM_STAGE_TYPE type = StageModule.GetStageType(data);
        switch (type)
	    {
            case EM_STAGE_TYPE.MAIN_QUEST1:
            case EM_STAGE_TYPE.MAIN_QUEST2:
            case EM_STAGE_TYPE.MAIN_QUEST3:
             //m_TitleTxt.text = string.Format("{0}-{1}", chapterId, StageModule.GetStageNumInChapter(data));
             m_TitleTxt.text = StageModule.GetStageNumInChapter(data).ToString();
             break;
            case EM_STAGE_TYPE.SIDE_QUEST:
             m_TitleTxt.text = GameUtils.getString("stage_type_branch");
             break;
            //case EM_STAGE_TYPE.SPEC_QUEST:
            // break;
            //case EM_STAGE_TYPE.ACTIVE_QUEST_DIJING:
            // break;
            //case EM_STAGE_TYPE.ACTIVE_QUEST_YANLONG:
            // break;
            //case EM_STAGE_TYPE.LIMIT_TEST:
            // break;
            //case EM_STAGE_TYPE.BOSS_SHOUWANGZHE:
            // break;
            //case EM_STAGE_TYPE.BOSS_CHUANSHUO:
            // break;
            default:
             m_TitleTxt.text = "";
             break;
	    }

        //位置;
        Vector2 pos = new Vector2(data.getStageiconposition()[0], data.getStageiconposition()[1]);
        mGo.transform.localPosition = pos;
        m_Icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + data.m_stageicon);
        m_Icon.SetNativeSize();
        float delta = (float)(data.getStageIconScale()) / 100f;
        m_Icon.transform.localScale = Vector3.one * delta;
        //是否已开启;
        if (ObjectSelf.GetInstance().BattleStageData.IsStageOpen(data.m_stageid))
        {
            GameUtils.SetBtnSpriteGrayState(m_IconBtn, false);
            //星星;
            int starNum = 0;
            if (ObjectSelf.GetInstance().BattleStageData.IsCopyScenePass(data.m_stageid, out starNum))
            {
                for (int i = 0; i < MaxStars; i++)
                {
                    mStarsImg[i].gameObject.SetActive(false);
                }
                //switch (starNum)
                //{
                //    case 0:
                //        mStarsImg[0].gameObject.SetActive(false);
                //        mStarsImg[1].gameObject.SetActive(false);
                //        mStarsImg[2].gameObject.SetActive(false);
                //        break;
                //    case 1:
                //        mStarsImg[0].gameObject.SetActive(true);
                //        mStarsImg[1].gameObject.SetActive(false);
                //        mStarsImg[2].gameObject.SetActive(false);
                //        break;
                //    case 2:
                //        mStarsImg[0].gameObject.SetActive(false);
                //        mStarsImg[1].gameObject.SetActive(true);
                //        mStarsImg[2].gameObject.SetActive(true);
                //        break;
                //    case 3:
                //        mStarsImg[0].gameObject.SetActive(true);
                //        mStarsImg[1].gameObject.SetActive(true);
                //        mStarsImg[2].gameObject.SetActive(true);
                //        break;
                //    default:
                //        mStarsImg[0].gameObject.SetActive(false);
                //        mStarsImg[1].gameObject.SetActive(false);
                //        mStarsImg[2].gameObject.SetActive(false);
                //        break;
                //}
                if (starNum > 0 && starNum <= 3)
                {
                    mStarsImg[starNum - 1].gameObject.SetActive(true);
                }
            }
            else
            {
                mStarsImg[0].gameObject.SetActive(false);
                mStarsImg[1].gameObject.SetActive(false);
                mStarsImg[2].gameObject.SetActive(false);
            }
        }
        else
        {
            GameUtils.SetBtnSpriteGrayState(m_IconBtn, true);
            mStarsImg[0].gameObject.SetActive(false);
            mStarsImg[1].gameObject.SetActive(false);
            mStarsImg[2].gameObject.SetActive(false);
        }
        
        //是否限制次数关卡;
        if (data.m_limittime > 0)
        {
            StageData sd = ObjectSelf.GetInstance().BattleStageData.GetStageDataByStageId(data.m_stageid);

            int useTimes = sd == null ? 0 : sd.m_FightSum;

            mRemindTimes.text = (data.m_limittime - useTimes).ToString();

            mRemindTitle.gameObject.SetActive(true);
        }
        else
        {
            mRemindTitle.gameObject.SetActive(false);
        }

        //是否是新开启的关卡;
        UpdateIsNew();
    }

    public void SetActive(bool active)
    {
        mGo.SetActive(active);
    }

    void UpdateIsNew()
    {
        //是否是新开启的关卡;
        if (ObjectSelf.GetInstance().BattleStageData.isStageNew(mStageT.m_stageid))
        {
            mIsNewImg.gameObject.SetActive(true);
        }
        else
        {
            mIsNewImg.gameObject.SetActive(false);
        }
    }

    public void Destroy()
    {
        onClick = null;
    }

    //设置选中状态显示;
    public void SetSelectState(int selectedId)
    {
        if (mStageT == null)
        {
            mSelectImg.gameObject.SetActive(false);
            return;
        }

        bool isSelected = selectedId == mStageT.m_stageid;
        //mSelectImg.gameObject.SetActive(isSelected);
        mSelectImg.gameObject.SetActive(false);

        //清除new标记;
        if (isSelected)
        {
            UpdateIsNew();
        }
    }
	
}
