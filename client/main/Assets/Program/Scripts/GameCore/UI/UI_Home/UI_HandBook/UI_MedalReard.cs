using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using UnityEngine.Events;
using DreamFaction.GameNetWork;
using System.Text;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

public class UI_MedalReard : CustomUI 
{
    public static string UI_ResPath = "UI_Home/UI_MedalReard_2_3";
    public static UI_MedalReard Inst;

    private Text m_BadgeTxt;
    private Text m_CurNumTxt1;
    private Text m_MaxNumTxt1;
    private Text m_CurNumTxt2;
    private Text m_MaxNumTxt2;
    private Text m_CurNumTxt3;
    private Text m_MaxNumTxt3;
    private Text m_CurNumTxt4;
    private Text m_MaxNumTxt4;
    private Text m_TilteTxt;                               //标题文本
    private Button m_CloseBtn;                             //关闭按钮
    private Transform m_Grid;                              //Grid
    private GameObject m_Prefab;                           //预设Item
    private Button m_GoldBtn;                              //金勋按钮
    private Button m_SilverBtn;                            //银勋按钮
    private Button m_BronzeBtn;                            //铜勋按钮
    private Button m_PigIronBtn;                           //铁勋按钮
    private GameObject m_GoldSelectImg;                    //金勋选择效果
    private GameObject m_GoldNotSelectImg;                 //金勋未选择效果
    private GameObject m_SilverSelectImg;                  //银勋选择效果
    private GameObject m_SilverNotSelectImg;               //银勋未选择效果
    private GameObject m_BronzeSelectImg;                  //铜勋选择效果
    private GameObject m_BronzeNotSelectImg;               //铜勋未选择效果
    private GameObject m_PigIronSelectImg;                 //铁勋选择效果
    private GameObject m_PigIronNotSelectImg;              //铁勋未选择效果

    private List<MedalexchangeTemplate> m_PigIronList = new List<MedalexchangeTemplate>();  //生铁奖励 4
    private List<MedalexchangeTemplate> m_BronzeList = new List<MedalexchangeTemplate>();   //青铜奖励 3
    private List<MedalexchangeTemplate> m_SilverList = new List<MedalexchangeTemplate>();   //白银奖励 2
    private List<MedalexchangeTemplate> m_GoldList = new List<MedalexchangeTemplate>();     //黄金奖励 1

    private List<UI_MedalItem> m_MedalItems = new List<UI_MedalItem>();                      //Item脚本List

    private GameObject[] m_TipsImageArray;                 //按钮New图标数组
    private bool[] m_TipsResultArray;
    private IFunctionTipsController m_TipsController;
    public override void InitUIData()
    {
        base.InitUIData();
        m_TipsResultArray = new bool[4];
        m_TipsImageArray = new GameObject[4];
        Inst = this;
        InitPraseXmlData();
        m_Grid = selfTransform.FindChild("ItemList/Grid");
        m_Prefab = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Home/MedalItem") as GameObject;

        m_TilteTxt = selfTransform.FindChild("PlayerInfoItem/Image/TiliteText").GetComponent<Text>();
        m_CloseBtn = selfTransform.FindChild("PlayerInfoItem/CloseBtn").GetComponent<Button>();
        m_GoldBtn = selfTransform.FindChild("PlayerInfoItem/GlodBtn/Button").GetComponent<Button>();
        m_SilverBtn = selfTransform.FindChild("PlayerInfoItem/SilverBtn/Button").GetComponent<Button>();
        m_BronzeBtn = selfTransform.FindChild("PlayerInfoItem/BronzeBtn/Button").GetComponent<Button>();
        m_PigIronBtn = selfTransform.FindChild("PlayerInfoItem/PigIronBtn/Button").GetComponent<Button>();
        m_GoldSelectImg = selfTransform.FindChild("PlayerInfoItem/GlodBtn/SelectStateImg").gameObject;
        m_GoldNotSelectImg = selfTransform.FindChild("PlayerInfoItem/GlodBtn/NotSelectStateImg").gameObject;
        m_SilverSelectImg = selfTransform.FindChild("PlayerInfoItem/SilverBtn/SelectStateImg").gameObject;
        m_SilverNotSelectImg = selfTransform.FindChild("PlayerInfoItem/SilverBtn/NotSelectStateImg").gameObject;
        m_BronzeSelectImg = selfTransform.FindChild("PlayerInfoItem/BronzeBtn/SelectStateImg").gameObject;
        m_BronzeNotSelectImg = selfTransform.FindChild("PlayerInfoItem/BronzeBtn/NotSelectStateImg").gameObject;
        m_PigIronSelectImg = selfTransform.FindChild("PlayerInfoItem/PigIronBtn/SelectStateImg").gameObject;
        m_PigIronNotSelectImg = selfTransform.FindChild("PlayerInfoItem/PigIronBtn/NotSelectStateImg").gameObject;

        m_TipsImageArray[0] = selfTransform.FindChild("PlayerInfoItem/GlodBtn/newImg").gameObject;
        m_TipsImageArray[1] = selfTransform.FindChild("PlayerInfoItem/SilverBtn/newImg").gameObject;
        m_TipsImageArray[2] = selfTransform.FindChild("PlayerInfoItem/BronzeBtn/newImg").gameObject;
        m_TipsImageArray[3] = selfTransform.FindChild("PlayerInfoItem/PigIronBtn/newImg").gameObject;

        

        m_CloseBtn.onClick.AddListener(new UnityAction(OnClickCloseBtn));
        m_GoldBtn.onClick.AddListener(new UnityAction(OnClickGoldBtn));
        m_SilverBtn.onClick.AddListener(new UnityAction(OnClickSilverBtn));
        m_BronzeBtn.onClick.AddListener(new UnityAction(OnClickBronzeBtn));
        m_PigIronBtn.onClick.AddListener(new UnityAction(OnClickPigIronBtn));

        m_BadgeTxt = selfTransform.FindChild("PlayerInfoItem/BadgeTxt").GetComponent<Text>();
        m_CurNumTxt1 = selfTransform.FindChild("PlayerInfoItem/Badge1/CurNumTxt1").GetComponent<Text>();
        m_MaxNumTxt1 = selfTransform.FindChild("PlayerInfoItem/Badge1/MaxNumTxt1").GetComponent<Text>();
        m_CurNumTxt2 = selfTransform.FindChild("PlayerInfoItem/Badge2/CurNumTxt2").GetComponent<Text>();
        m_MaxNumTxt2 = selfTransform.FindChild("PlayerInfoItem/Badge2/MaxNumTxt2").GetComponent<Text>();
        m_CurNumTxt3 = selfTransform.FindChild("PlayerInfoItem/Badge3/CurNumTxt3").GetComponent<Text>();
        m_MaxNumTxt3 = selfTransform.FindChild("PlayerInfoItem/Badge3/MaxNumTxt3").GetComponent<Text>();
        m_CurNumTxt4 = selfTransform.FindChild("PlayerInfoItem/Badge4/CurNumTxt4").GetComponent<Text>();
        m_MaxNumTxt4 = selfTransform.FindChild("PlayerInfoItem/Badge4/MaxNumTxt4").GetComponent<Text>();
        GameEventDispatcher.Inst.addEventListener(GameEventID.HB_BoxUpdate, UpdateShow);

        captionPath = "caption";
    }

    public override void InitUIView()
    {
        base.InitUIView();
 //       ShowMedalReardNewImg();
        OnClickGoldBtn();
        ShowMedalCountText();
 //       InitDesTxt();
        m_TipsController = CreateFunctionTipsController();
        m_TipsController.Refresh();
        GameEventDispatcher.Inst.addEventListener(GameEventID.HB_GetSTuJianHeros, m_TipsController.Refresh);
    }

    /// <summary>
    /// 解析勋章奖励Xml数据
    /// </summary>
    private void InitPraseXmlData()
    {
        Dictionary<int, IExcelBean> _medalXmlData = DataTemplate.GetInstance().m_MedalexchangeTable.getData();
        foreach (var item in _medalXmlData)
        {
            MedalexchangeTemplate _medalData = item.Value as MedalexchangeTemplate;
            if (_medalData.getExchangeType() == 1)
                m_GoldList.Add(_medalData);
            else if (_medalData.getExchangeType() == 2)
                m_SilverList.Add(_medalData);
            else if (_medalData.getExchangeType() == 3)
                m_BronzeList.Add(_medalData);
            else if (_medalData.getExchangeType() == 4)
                m_PigIronList.Add(_medalData);
        }
    }

    /// <summary>
    /// 加载创建Prefab 
    /// </summary>
    /// <param name="medalList"></param>
    private void InitLoadPrefabUI(List<MedalexchangeTemplate> medalList)
    {
        ClearGrid();

        for (int i = 0; i < medalList.Count; i++)
        {
            GameObject _go = Instantiate(m_Prefab) as GameObject;
            _go.transform.SetParent(m_Grid);
            _go.transform.localPosition = Vector3.zero;
            _go.transform.localScale = Vector3.one;
            UI_MedalItem _item = _go.GetComponent<UI_MedalItem>();
            m_MedalItems.Add(_item);
            _item.ShowUIData(medalList[i]);
        }
    }

    /// <summary>
    /// 清除模型
    /// </summary>
    private void ClearGrid()
    {
        for (int i = 0; i < m_MedalItems.Count; i++)
        {
            Destroy(m_MedalItems[i].gameObject);
        }
        m_MedalItems.Clear();
    }

    /// <summary>
    /// 显示勋章个数
    /// </summary>
    private void ShowMedalCountText()
    {
        m_CurNumTxt1.text = ObjectSelf.GetInstance().ChiTieXZ.ToString();
        m_CurNumTxt2.text = ObjectSelf.GetInstance().QingTongXZ.ToString();
        m_CurNumTxt3.text = ObjectSelf.GetInstance().BaiJinXZ.ToString();
        m_CurNumTxt4.text = ObjectSelf.GetInstance().HuangjinXZ.ToString();

        StringBuilder _strBuduilder1 = new StringBuilder();
        StringBuilder _strBuduilder2 = new StringBuilder();
        StringBuilder _strBuduilder3 = new StringBuilder();
        StringBuilder _strBuduilder4 = new StringBuilder();
        _strBuduilder1.Append("/");
        _strBuduilder1.Append(UI_HandBookManager.Inst.m_PigIronCount);
        _strBuduilder2.Append("/");
        _strBuduilder2.Append(UI_HandBookManager.Inst.m_BronzeCount);
        _strBuduilder3.Append("/");
        _strBuduilder3.Append(UI_HandBookManager.Inst.m_SilverCount);
        _strBuduilder4.Append("/");
        _strBuduilder4.Append(UI_HandBookManager.Inst.m_GoldCount);

        m_MaxNumTxt1.text = _strBuduilder1.ToString();
        m_MaxNumTxt2.text = _strBuduilder2.ToString();
        m_MaxNumTxt3.text = _strBuduilder3.ToString();
        m_MaxNumTxt4.text = _strBuduilder4.ToString();
    }

    /// <summary>
    /// 初始化文本
    /// </summary>
    private void InitDesTxt()
    {
        m_GoldBtn.GetComponent<Text>().text = GameUtils.getString("pokedex_content8");
        m_SilverBtn.GetComponent<Text>().text = GameUtils.getString("pokedex_content9");
        m_BronzeBtn.GetComponent<Text>().text = GameUtils.getString("pokedex_content10");
        m_PigIronBtn.GetComponent<Text>().text = GameUtils.getString("pokedex_content11");
    }


    /// <summary>
    /// 关闭按钮
    /// </summary>
    private void OnClickCloseBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    /// <summary>
    /// 金勋按钮
    /// </summary>
    private void OnClickGoldBtn()
    {
        SetActiveImg(m_GoldSelectImg, m_GoldNotSelectImg);
        InitLoadPrefabUI(m_GoldList);

    }

    /// <summary>
    /// 银勋按钮
    /// </summary>
    private void OnClickSilverBtn()
    {
        SetActiveImg(m_SilverSelectImg, m_SilverNotSelectImg);
        InitLoadPrefabUI(m_SilverList);
    }

    /// <summary>
    /// 铜勋按钮
    /// </summary>
    private void OnClickBronzeBtn()
    {
        SetActiveImg(m_BronzeSelectImg, m_BronzeNotSelectImg);
        InitLoadPrefabUI(m_BronzeList);
     }

    /// <summary>
    /// 铁勋按钮
    /// </summary>
    private void OnClickPigIronBtn()
    {
        SetActiveImg(m_PigIronSelectImg, m_PigIronNotSelectImg);
        InitLoadPrefabUI(m_PigIronList);
    }


    /// <summary>
    /// 设置选择按钮的背景显示
    /// </summary>
    /// <param name="img">背景效果</param>
    private void SetActiveImg(GameObject selectObj, GameObject selectObjNot)
    {
        m_GoldSelectImg.SetActive(false);
        m_SilverSelectImg.SetActive(false);
        m_BronzeSelectImg.SetActive(false);
        m_PigIronSelectImg.SetActive(false);

        m_GoldNotSelectImg.SetActive(true);
        m_SilverNotSelectImg.SetActive(true);
        m_BronzeNotSelectImg.SetActive(true);
        m_PigIronNotSelectImg.SetActive(true);

        selectObj.SetActive(true);
        selectObjNot.SetActive(false);

    }
    /// <summary>
    /// 弹窗提示
    /// </summary>
    /// <param name="text"></param>
    public void PopupShow(string text)
    {
        InterfaceControler.GetInst().AddMsgBox(text,transform);
    }
    /// <summary>
    /// 刷新显示
    /// </summary>
    /// <param name="e">宝箱奖励ID</param>
    private void UpdateShow(GameEvent e)
    {
        for (int i = 0; i < m_MedalItems.Count; i++)
        {
            if (m_MedalItems[i].GetMdedalData().getId() == (int)e.data)
            {
                m_MedalItems[i].UpdateUIdataShow();
            }
        }

    }

    private bool[] CheckNewMedalReard()
    { 
        for (int i = 0; i < m_TipsResultArray.Length; i++)
            m_TipsResultArray[i] = false;
        Dictionary<int, IExcelBean> _medalXmlData = DataTemplate.GetInstance().m_MedalexchangeTable.getData();
        foreach (var item in _medalXmlData)
        {
            MedalexchangeTemplate _medalData = item.Value as MedalexchangeTemplate;
            var list = ObjectSelf.GetInstance().GetHandBookBoxList();
            if (!ObjectSelf.GetInstance().GetHandBookBoxList().Contains(_medalData.getId()))
            {
                int _exchangeType = _medalData.getExchangeType();
                if(m_TipsResultArray[_exchangeType-1])
                    continue;
                switch(_exchangeType)
                {
                    case 1:
                        m_TipsResultArray[0] = ObjectSelf.GetInstance().HuangjinXZ >= _medalData.getNeedNum();
                        break;
                    case 2:
                        m_TipsResultArray[1] = ObjectSelf.GetInstance().BaiJinXZ >= _medalData.getNeedNum();
                        break;
                    case 3:
                        m_TipsResultArray[2] = ObjectSelf.GetInstance().QingTongXZ >= _medalData.getNeedNum();
                        break;
                    case 4:
                        m_TipsResultArray[3] = ObjectSelf.GetInstance().ChiTieXZ >= _medalData.getNeedNum();
                        break;
                    default:
                        break;
                }
                if (m_TipsResultArray[0] & m_TipsResultArray[1] & m_TipsResultArray[2] & m_TipsResultArray[3])
                    break;
            }
        }
        return m_TipsResultArray;
    }
 
    //生成功能提示控制器
    private IFunctionTipsController CreateFunctionTipsController()
    {
        FunctionTipsControllerBoolArrayType _controller = new FunctionTipsControllerBoolArrayType(m_TipsImageArray, CheckNewMedalReard);
        return _controller;
    }

    void OnDestroy()
    {
        base.OnDestroy();
        GameEventDispatcher.Inst.removeEventListener(GameEventID.HB_BoxUpdate, UpdateShow);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.HB_GetSTuJianHeros, m_TipsController.Refresh);
    }


}
