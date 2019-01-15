using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.UI;
using System.Text;
using DreamFaction.Utils;
using GNET;
using DG.Tweening;

public class UI_Liveness : UI_LivenessManage
{
    public static string UI_ResPath = "UI_Home/UI_Liveness_2_2";
    public static UI_Liveness _instance;
    private GameObject m_Rewards;
    private int[] m_NotmaldropID;
    private Transform MsgBoxGroup;                                                       //消息父节点 
    private Slider m_LivenessSlider;
    private Slider m_LivenessSlider2; //活跃度滚动条  
    public List<HuoYueData> m_MissionID = new List<HuoYueData>();                                //记录活跃度任务
    public int isLivenessBox;                                                            //宝箱是否开启 个位为第一个宝箱 十位为第二个宝箱~~
    public int m_LivenessNum;                                                            //当前已获得的活跃度
    private GameObject m_Liveness;                                                       //活跃度界面
    private GameObject m_Sigin;                                                          //签到界面
    //private List<int> m_MissionID;
    private Transform m_Grid;                                                            //任务列表
    private bool isBox50Get;                                                             //50活跃度的宝箱是否开启
    private bool isBox90Get;                                                             //90活跃度的宝箱是否开启
    private bool isBox120Get;                                                            //120活跃度的宝箱是否开启
    private bool isBox150Get;

    //yao
    private RectTransform Box_List; //
    private RectTransform Item_List; //
    private LoopLayout m_BoxLayout;
    private LoopLayout m_ItemLayout;
    private int[] Activitymission_reward_level;
    private GameObject[] m_BoxGameObject = new GameObject[4];

    public delegate void ButtonClick(GameObject go);
    public event ButtonClick onItemClick;

    public ButtonClick itemClick
    {
        get
        {
            return onItemClick;
        }
        set
        {
            onItemClick = value;
        }
    }

    private UILoop m_boxList;
    private UILoop m_List;
    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
        m_Liveness = selfTransform.FindChild("Liveness").gameObject;
        m_Sigin = selfTransform.FindChild("GameObject").gameObject;
        m_Grid = selfTransform.FindChild("Liveness/Missionlist/Grid");
        m_Rewards = selfTransform.FindChild("Liveness/Rewards").gameObject;
        m_LivenessSlider = selfTransform.FindChild("Liveness/LivenessSlider").GetComponent<Slider>();
        m_LivenessSlider2 = selfTransform.FindChild("Liveness/LivenessSlider2").GetComponent<Slider>();
        m_NotmaldropID = DataTemplate.GetInstance().m_GameConfig.getActivitymission_reward_drop();
        m_UI_Btn_Liveness.transform.GetComponent<Image>().enabled = true;
        m_UI_Btn_Liveness.transform.FindChild("Text").GetComponent<OutLineGlow>().enabled = true;
        m_UI_Btn_Prop.transform.GetComponent<Image>().enabled = false;
        m_UI_Btn_Prop.transform.FindChild("Text").GetComponent<OutLineGlow>().enabled = false;
 
        //yao
        Box_List = selfTransform.FindChild("Liveness/BoxList/Content").GetComponent<RectTransform>();//箱子的父节点 
        Item_List = selfTransform.FindChild("Liveness/Missionlist/list").GetComponent<RectTransform>();//任务列表的父节点 
        m_BoxLayout = selfTransform.FindChild("Liveness/BoxList/Content").GetComponent<LoopLayout>();
        m_ItemLayout = selfTransform.FindChild("Liveness/Missionlist/list").GetComponent<LoopLayout>();
        itemClick += boxItemClickHandler;

        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(M_CapPos);

        Activitymission_reward_level = DataTemplate.GetInstance().GetGameConfig().getActivitymission_reward_level();

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_GetLivenessBox, RenewalUIShow);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_GetLiveness, RenewalUIShow);
    }



    // 2：初始化UI显示内容
    public override void InitUIView()
    {
        base.InitUIView();
        

    }
    private void CreatBoxItem()
    {
        m_BoxLayout.cellCount = Activitymission_reward_level.Length;
        m_BoxLayout.updateCellEvent = UpdateBoxItem;
        m_BoxLayout.Reload();
    }

    private void UpdateBoxItem(int index, RectTransform cell)
    {
        UI_LivenessBoxItem _UI_LivenessBoxItem = cell.GetComponent<UI_LivenessBoxItem>();
        if (_UI_LivenessBoxItem == null)
        {
            _UI_LivenessBoxItem = cell.gameObject.AddComponent<UI_LivenessBoxItem>();
        }
        _UI_LivenessBoxItem.Data(Activitymission_reward_level[index], index);
    }

    private void CreatBoxInfo()
    {
        for (int i = 0; i < Box_List.transform.childCount; ++i)
        {
            if (Box_List.GetChild(i).gameObject.name == "ItemContaner" + i.ToString())
            {
                m_BoxGameObject[i] = Box_List.GetChild(i).gameObject;
            }
        }

        for (int i = 0; i < m_BoxGameObject.Length; i++)
        {
            UI_LivenessBoxItem _UI_LivenessBoxItem = m_BoxGameObject[i].GetComponent<UI_LivenessBoxItem>();
            if (_UI_LivenessBoxItem == null)
            {
                _UI_LivenessBoxItem = m_BoxGameObject[i].gameObject.AddComponent<UI_LivenessBoxItem>();
            }
            _UI_LivenessBoxItem.Data(Activitymission_reward_level[i], i);
        }

    }

    private void CreatActivityRewardItem()
    {
        m_ItemLayout.cellCount = m_MissionID.Count;
        m_ItemLayout.updateCellEvent = UpdateActivityRewardItem;
        m_ItemLayout.Reload();
    }

    private void UpdateActivityRewardItem(int index, RectTransform cell)
    {
        UI_LivenessItem _UI_LivenessItem = cell.GetComponent<UI_LivenessItem>();
        if (_UI_LivenessItem == null)
        {
            _UI_LivenessItem = cell.gameObject.AddComponent<UI_LivenessItem>();
        }
        _UI_LivenessItem.Data(m_MissionID[index]);
    }

    public void CopyData(LinkedList<Huoyue> huoyuelist)
    {
        foreach (var value in huoyuelist)
        {
            HuoYueData _data = new HuoYueData();
            _data.Copy(value);
            m_MissionID.Add(_data);
        }
        SortMissionData(ref m_MissionID);
        CreatActivityRewardItem(); 
    }

    private void SortMissionData(ref List<HuoYueData> m_MissionID)
    {
        for (int i = 0; i < m_MissionID.Count -1; i++)
        {
            if (m_MissionID[i].m_isok == 0)
            {
                continue;
            }
            else
            {
                for (int j = i + 1; j < m_MissionID.Count;j++ )
                {
                    if (m_MissionID[j].m_isok == 0)
                    {
                        HuoYueData temp = m_MissionID[i];
                        m_MissionID[i] = m_MissionID[j];
                        m_MissionID[j] = temp;
                    }
                }
            }
        }  
    }

    public class HuoYueData
    {
        public int m_huoyueid; // 活跃id
        public int m_num; // 发生次数
        public int m_numall; // 总次数
        public int m_huoyuetype; // 任务类型
        public int m_isok; // 是否完成
        public void Copy(Huoyue _data)
        {
            this.m_huoyueid = _data.huoyueid;
            this.m_num = _data.num;
            this.m_numall = _data.numall;
            this.m_huoyuetype = _data.huoyuetype;
            this.m_isok = _data.isok;
        }
    }


    private void boxItemClickHandler(GameObject go)
    {
        UI_LivenessBoxItem boxItem = go.GetComponent<UI_LivenessBoxItem>();
        if (boxItem == null) return;


        UI_LivenessBoxShow item = m_Rewards.GetComponent<UI_LivenessBoxShow>();
        if (boxItem.isOpend())
        {
            InterfaceControler.GetInst().AddMsgBox("已领取过该奖励", this.gameObject.transform);
        }
        else
        {
            m_Rewards.SetActive(true);
            item.Show(boxItem);
        }
    }

    protected void OnDestroy()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(M_CapPos);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_GetLivenessBox, RenewalUIShow);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_GetLiveness, RenewalUIShow);
    }
    //UI表现以及更新
    public void RenewalUIShow()
    {
        m_NewNumText.text = m_LivenessNum.ToString();
        m_LivenessSlider.value = m_LivenessNum;

        if (m_LivenessNum > 150)
        {
            if (m_LivenessNum > 170)
            {
                m_LivenessSlider2.value = m_LivenessNum - m_slider.maxValue;
                m_NewNumTextRectTF.anchoredPosition = new Vector2(LivenessSliderTF.sizeDelta.x + (LivenessSlider_2_TF.sizeDelta.x * (170 - m_slider.maxValue) / m_slider_2.maxValue) - 30, m_NewNumTextRectTF.anchoredPosition.y);
            }
            else
            {
                m_LivenessSlider2.value = m_LivenessNum - m_slider.maxValue;
                m_NewNumTextRectTF.anchoredPosition = new Vector2(LivenessSliderTF.sizeDelta.x + (LivenessSlider_2_TF.sizeDelta.x * (m_LivenessNum - m_slider.maxValue) / m_slider_2.maxValue) - 30, m_NewNumTextRectTF.anchoredPosition.y);
            }
        }
        else
        {
            m_NewNumTextRectTF.anchoredPosition = new Vector2(LivenessSliderTF.sizeDelta.x * m_LivenessNum / m_slider.maxValue, m_NewNumTextRectTF.anchoredPosition.y);
            m_LivenessSlider2.value = 0;
        }
        //CreatBoxItem();
        CreatBoxInfo();
    }

    //跳转到活跃度任务界面
    protected override void OnClickUI_Btn_Liveness()
    {
        base.OnClickUI_Btn_Liveness();
        m_Liveness.SetActive(true);
        m_Sigin.SetActive(false);
        M_CapPos.gameObject.SetActive(true);
        m_UI_Btn_Liveness.transform.GetComponent<Image>().enabled = true;
        m_UI_Btn_Liveness.transform.FindChild("Text").GetComponent<OutLineGlow>().enabled = true;
        m_UI_Btn_Prop.transform.GetComponent<Image>().enabled = false;
        m_UI_Btn_Prop.transform.FindChild("Text").GetComponent<OutLineGlow>().enabled = false;
        m_HuoYueDuText.color = new Color(1f,1f,1f);
        m_QianDaoText.color =  new Color(0.72f, 0.72f, 0.74f);
    }
    //跳转到签到界面

    protected override void OnClickUI_Btn_Prop()
    {
        base.OnClickUI_Btn_Prop();
        m_Liveness.SetActive(false);
        M_CapPos.gameObject.SetActive(false);
        m_Sigin.SetActive(true);
        

        if (UI_HomeControler.Inst)
        {
            UI_HomeControler.Inst.ReMoveUI(UI_SignInManager.Path);
        } 
        for (int i = 0; i < m_Sigin.transform.childCount;i++ )
        {           
            Destroy(m_Sigin.transform.GetChild(i).gameObject);
        }

        GameObject go = UI_HomeControler.Inst.AddUI(UI_SignInManager.Path);
        if (go)
        {
            go.transform.parent = m_Sigin.transform;
            go.transform.FindChild("SignInPanel/Background").gameObject.SetActive(false);
            go.transform.FindChild("SignInPanel/SignInTopPanel").gameObject.SetActive(false);
            go.transform.GetComponent<UI_SignInManager>().InitSignInManager(false);
        }
 
        m_UI_Btn_Liveness.transform.GetComponent<Image>().enabled = false;
        m_UI_Btn_Liveness.transform.FindChild("Text").GetComponent<OutLineGlow>().enabled = false;
        m_UI_Btn_Prop.transform.GetComponent<Image>().enabled = true;
        m_UI_Btn_Prop.transform.FindChild("Text").GetComponent<OutLineGlow>().enabled = true;
        m_HuoYueDuText.color = new Color(0.72f, 0.72f, 0.74f);
        m_QianDaoText.color = new Color(1f, 1f, 1f);
    }

    //返回按钮
    protected override void OnClickUI_Btn_Back()
    {
        base.OnClickUI_Btn_Back();
        UI_HomeControler.Inst.ReMoveUI(UI_SignInManager.Path);
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    # region old
    ////英雄包裹满时
    //public void IsHeroBagMax()
    //{
    //    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_bag_tips4"), MsgBoxGroup);
    //}
    ////道具符文背包满时
    //public void IsItemBagMax()
    //{
    //    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("hero_bag_tips2"), MsgBoxGroup);
    //}
    #endregion
}
