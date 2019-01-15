using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork.Data;
using DreamFaction.LogSystem;

public class TogImgObj
{
    private Image img;

    public TogImgObj(Transform trans, Transform parent)
    {
        img = trans.FindChild("Image").GetComponent<Image>();

        trans.SetParent(parent, false);
    }

    public void SetImgSprite(Sprite sp)
    {
        img.sprite = sp;
        img.SetNativeSize();
    }
}

public class UI_ImageView : BaseUI
{

    GameObject imgListObj;
    GameObject togListObj;
    GameObject togImgObj;

    public bool IsPixelPerfect = false;
    public float ChangeTime = 2f;
    public bool IsResetTimeOnDrag = true;
    public Sprite SelectImage = null;
    public Sprite DisSelectImg = null;
    public bool NeedTogImgs = false;
    public bool TogImgsDone = false;

    private int mImgIdx = -1;
    private List<Image> mImgList = new List<Image>();
    private List<TogImgObj> mTogList = new List<TogImgObj>();
    private float mTimer = 0f;
    private bool mStart = false;
    private bool mInitDone = false;

    int ImgIdx
    {
        get
        {
            return mImgIdx;
        }
        set
        {
            if (value != mImgIdx)
            {
                onImgChange(mImgIdx, value);

                mImgIdx = value;
            }
        }
    }

    public void Start()
    {
        mStart = true;
    }

    public void Stop()
    {
        mStart = false;
    }

    void onImgChange(int oldIdx, int newIdx)
    {
        if (NeedTogImgs && TogImgsDone)
        {
            UpdateTogImgs();
        }

        if (oldIdx != -1)
        {
            HideImg(oldIdx);
        }
        try
        {
            ShowImg(newIdx);
        }
        catch (System.Exception ex)
        {
            LogManager.LogError(newIdx);
        }
    }

    /// <summary>
    /// 显示图片的逻辑;
    /// </summary>
    /// <param name="idx"></param>
    void ShowImg(int idx)
    {
        mImgList[idx].gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏图片的逻辑;
    /// </summary>
    /// <param name="idx"></param>
    void HideImg(int idx)
    {
        mImgList[idx].gameObject.SetActive(false);
    }

    void hideAllImg(bool isPixelPerfect)
    {
        for (int i = 0; i < mImgList.Count; i++ )
        {
            mImgList[i].gameObject.SetActive(false);

            if (isPixelPerfect)
            {
                mImgList[i].SetNativeSize();
            }
        }
    }

    void turnNextImg()
    {
        ImgIdx = (ImgIdx + 1) % mImgList.Count;
    }

    void turnPrevImg()
    {
        int idx = ImgIdx - 1;
        ImgIdx = (idx < 0 ? mImgList.Count + idx : idx) % mImgList.Count;
    }

    void ResetTimer()
    {
        mTimer = 0f;
    }

    void AddTriggerListener()
    {
        for (int i = 0; i < mImgList.Count; i++)
        {
            EventTriggerListener.Get(mImgList[i].gameObject).onDrag = onImgDrag;
        }
    }

    void onImgDrag(GameObject go, PointerEventData ped)
    {
        if (ped.delta.x > 0f)
        {
            if (IsResetTimeOnDrag)
                ResetTimer();
            turnNextImg();
        }
        else if (ped.delta.x < 0f)
        {
            if (IsResetTimeOnDrag)
                ResetTimer();
            turnPrevImg();
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();

        Init();
    }

    void Init()
    {
        if (mInitDone)
            return;
        
        mInitDone = true;

        imgListObj = transform.FindChild("ImageList").gameObject;
        togListObj = transform.FindChild("GridList").gameObject;
        togImgObj = transform.FindChild("Items/ToggleObj").gameObject;

        mImgList.Clear();
        mImgList = GetImgList();

        hideAllImg(IsPixelPerfect);
        AddTriggerListener();
    }

    public void SetTogImgs()
    {
        NeedTogImgs = true;
        TogImgsDone = true;

        GameUtils.DestroyChildsObj(togListObj);

        mTogList.Clear();
        for (int i = 0, j = mImgList.Count; i < j; i++)
        {
            GameObject go = GameObject.Instantiate(togImgObj) as GameObject;
            TogImgObj obj = new TogImgObj(go.transform, togListObj.transform);
            mTogList.Add(obj);
        }

        UpdateTogImgs();
    }

    void UpdateTogImgs()
    {
        for (int i = 0; i < mTogList.Count; i++)
        {
            mTogList[i].SetImgSprite(i == ImgIdx ? SelectImage : DisSelectImg);
        }
    }

    public void Reset()
    {
        if (!mInitDone)
            InitUIData();

        mImgList.Clear();
        mImgList = GetImgList();

        hideAllImg(IsPixelPerfect);
        AddTriggerListener();
        ImgIdx = 0;
    }

    List<Image> GetImgList()
    {
        return new List<Image>(imgListObj.GetComponentsInChildren<Image>(true));
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();

        if (!mStart || mImgList.Count <= 0)
            return;

        mTimer += Time.deltaTime;

        if(mTimer >= ChangeTime)
        {
            ResetTimer();
            turnNextImg();
        }
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        mImgList.Clear();
        mImgList = null;
    }

    void OnDestroy()
    {
        UIState = UIStateEnum.ReadyForClose;
    }
}
