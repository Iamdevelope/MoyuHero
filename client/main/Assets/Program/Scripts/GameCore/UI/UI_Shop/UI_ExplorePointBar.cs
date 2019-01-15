using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using System.Text;
using DreamFaction.UI.Core;

public class UI_ExplorePointBar : UI_MoneyBarMgr
{
    protected GameObject ExplorePointObj;
    protected Button ExplorePointBtn;
    protected Text ExplorePointTxtCur;
    protected Text ExplorePointTxtTotal;
    protected Text TimeTxt;

    private float m_CurTime = 0f;

    public override void InitUIData()
    {
        base.InitUIData();

        ExplorePointObj = transform.FindChild("ExplorePoint").gameObject;
        ExplorePointBtn = transform.FindChild("ExplorePoint/AddBtn").GetComponent<Button>();
        ExplorePointTxtCur = transform.FindChild("ExplorePoint/CurrentpowTxt").GetComponent<Text>();
        ExplorePointTxtTotal = transform.FindChild("ExplorePoint/MaxpowTxt").GetComponent<Text>();
        TimeTxt = transform.FindChild("ExplorePoint/Time").GetComponent<Text>();
        TimeTxt.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40f);
        GameEventDispatcher.Inst.addEventListener(GameEventID.G_ExplorePoint_Update, OnExplorePointUpdate);

        ExplorePointBtn.onClick.AddListener(OnExploreBtnClick);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        PowerObj.SetActive(false);

        UpdateInfo();
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();

        if (!isShowPowerTime)
        {
            TimeTxt.enabled = false;
            return;
        }

        m_CurTime += Time.deltaTime;
        if (m_CurTime >= 1f)
        {
            m_CurTime = 0f;

            ////更新行动力剩余时间
            int Timer = ObjectSelf.GetInstance().ExplorePointRefreshTimes;
            int CurrentPower = ObjectSelf.GetInstance().ExplorePoint;
            int MaxPower = ObjectSelf.GetInstance().ExplorePointMax;
            if (CurrentPower >= MaxPower)
            {
                TimeTxt.enabled = false;
            }
            else
            {
                int minute = Timer / 60;
                int second = Timer % 60;
                string minuteStr = minute <= 9 ? "0" + minute : minute.ToString();
                string secondStr = second <= 9 ? "0" + second : second.ToString();
                StringBuilder timeStr = new StringBuilder();
                timeStr.Append(minuteStr);
                timeStr.Append(":");
                timeStr.Append(secondStr);
                TimeTxt.text = timeStr.ToString();
                TimeTxt.enabled = true;
            }

        }

    }

    protected override void DestroyUIData()
    {
        base.DestroyUIData();

        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_ExplorePoint_Update, OnExplorePointUpdate);
    }

    private void OnExplorePointUpdate()
    {
        UpdateInfo();
    }

    void UpdateInfo()
    {
        int curExplorePoint = ObjectSelf.GetInstance().ExplorePoint;
        int maxExplorePoint = ObjectSelf.GetInstance().ExplorePointMax;
        
        ExplorePointTxtCur.text = curExplorePoint.ToString();
        ExplorePointTxtTotal.text = "/" + maxExplorePoint.ToString();

        if (curExplorePoint >= 200)
        {
            ExplorePointTxtCur.color = Color.red;
        }
        else if (curExplorePoint >= maxExplorePoint)
        {
            ExplorePointTxtCur.color = Color.yellow;
        }
        else
        {
            ExplorePointTxtCur.color = Color.white;
        }
    }

    void OnExploreBtnClick()
    {
        UI_HomeControler.Inst.AddUI(UI_ExplorePointAddMgr.UI_ResPath);
    }
}
