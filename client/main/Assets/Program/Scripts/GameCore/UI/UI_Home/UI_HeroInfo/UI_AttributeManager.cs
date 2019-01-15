using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.UI;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using System.Collections.Generic;
using GNET;
using DG.Tweening;
using DreamFaction.Utils;
using DreamFaction.LogSystem;
public class UI_AttributeManager : BaseUI
{
    public static UI_AttributeManager _instance;


    //private AttributeItem Hp;
    //private AttributeItem PhysicAtc;
    //private AttributeItem MagicAtc;
    //private AttributeItem MagicDef;
    private int tableID;
    private ObjectCard obj;
    private HeroTemplate hero;
    private Text allHeroMoney;
    private Transform MsgBoxGroup;
    
    //属性一
    private Text propertyType1;
    private Slider propertySlider1;
    private Button addPropertyBtn1;
    private Button maxPropertyBtn1;
    private Text addValue1;
    private Text currentValue1;
    private Text allValue1;
    private Text needValue1;
   // public GameObject texiao1;
    //属性二
    private Text propertyType2;
    private Slider propertySlider2;
    private Button addPropertyBtn2;
    private Button maxPropertyBtn2;
    private Text addValue2;
    private Text currentValue2;
    private Text allValue2;
    private Text needValue2;
   // public GameObject texiao2;
    //属性三
    private Text propertyType3;
    private Slider propertySlider3;
    private Button addPropertyBtn3;
    private Button maxPropertyBtn3;
    private Text addValue3;
    private Text currentValue3;
    private Text allValue3;
    private Text needValue3;
   // public GameObject texiao3;
    //属性四
    private Text propertyType4;
    private Slider propertySlider4;
    private Button addPropertyBtn4;
    private Button maxPropertyBtn4;
    private Text addValue4;
    private Text currentValue4;
    private Text allValue4;
    private Text needValue4;
    //public GameObject texiao4;

    //功能提示
    private GameObject[] m_PropertyTipsArray;
    private IFunctionTipsController m_TipsController;
    private FunctionTipsManager m_FunctionTipsManager;
    public override void InitUIData()
    {
        _instance = this;
        base.InitUIData();
        allHeroMoney = selfTransform.FindChild("All").GetComponent<Text>();
        MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
        //属性一
        propertyType1 = selfTransform.FindChild("AttributeItem1/PropertyType").GetComponent<Text>();
        propertySlider1 = selfTransform.FindChild("AttributeItem1/HPSlider").GetComponent<Slider>();
        addPropertyBtn1 = selfTransform.FindChild("AttributeItem1/DevelopButton").GetComponent<Button>();
        maxPropertyBtn1 = selfTransform.FindChild("AttributeItem1/UpperBtn").GetComponent<Button>();
        addValue1 = selfTransform.FindChild("AttributeItem1/AddValue").GetComponent<Text>();
        currentValue1 = selfTransform.FindChild("AttributeItem1/CurrentValue").GetComponent<Text>();
        allValue1 = selfTransform.FindChild("AttributeItem1/AllValue").GetComponent<Text>();
        needValue1 = selfTransform.FindChild("AttributeItem1/NeedValue").GetComponent<Text>();
        addPropertyBtn1.onClick.AddListener(new UnityEngine.Events.UnityAction(AddPropertyBtn1));
        maxPropertyBtn1.onClick.AddListener(new UnityEngine.Events.UnityAction(MaxPropertyBtn));
        maxPropertyBtn1.gameObject.SetActive(false);
        //属性二
        propertyType2 = selfTransform.FindChild("AttributeItem2/PropertyType").GetComponent<Text>();
        propertySlider2 = selfTransform.FindChild("AttributeItem2/HPSlider").GetComponent<Slider>();
        addPropertyBtn2 = selfTransform.FindChild("AttributeItem2/DevelopButton").GetComponent<Button>();
        maxPropertyBtn2 = selfTransform.FindChild("AttributeItem2/UpperBtn").GetComponent<Button>();
        addValue2 = selfTransform.FindChild("AttributeItem2/AddValue").GetComponent<Text>();
        currentValue2 = selfTransform.FindChild("AttributeItem2/CurrentValue").GetComponent<Text>();
        allValue2 = selfTransform.FindChild("AttributeItem2/AllValue").GetComponent<Text>();
        needValue2 = selfTransform.FindChild("AttributeItem2/NeedValue").GetComponent<Text>();
        addPropertyBtn2.onClick.AddListener(new UnityEngine.Events.UnityAction(AddPropertyBtn2));
        maxPropertyBtn2.onClick.AddListener(new UnityEngine.Events.UnityAction(MaxPropertyBtn));
        maxPropertyBtn2.gameObject.SetActive(false);
        //属性三
        propertyType3 = selfTransform.FindChild("AttributeItem3/PropertyType").GetComponent<Text>();
        propertySlider3 = selfTransform.FindChild("AttributeItem3/HPSlider").GetComponent<Slider>();
        addPropertyBtn3 = selfTransform.FindChild("AttributeItem3/DevelopButton").GetComponent<Button>();
        maxPropertyBtn3 = selfTransform.FindChild("AttributeItem3/UpperBtn").GetComponent<Button>();
        addValue3 = selfTransform.FindChild("AttributeItem3/AddValue").GetComponent<Text>();
        currentValue3 = selfTransform.FindChild("AttributeItem3/CurrentValue").GetComponent<Text>();
        allValue3 = selfTransform.FindChild("AttributeItem3/AllValue").GetComponent<Text>();
        needValue3 = selfTransform.FindChild("AttributeItem3/NeedValue").GetComponent<Text>();
        addPropertyBtn3.onClick.AddListener(new UnityEngine.Events.UnityAction(AddPropertyBtn3));
        maxPropertyBtn3.onClick.AddListener(new UnityEngine.Events.UnityAction(MaxPropertyBtn));
        maxPropertyBtn3.gameObject.SetActive(false);
        //属性四
        propertyType4 = selfTransform.FindChild("AttributeItem4/PropertyType").GetComponent<Text>();
        propertySlider4 = selfTransform.FindChild("AttributeItem4/HPSlider").GetComponent<Slider>();
        addPropertyBtn4 = selfTransform.FindChild("AttributeItem4/DevelopButton").GetComponent<Button>();
        maxPropertyBtn4 = selfTransform.FindChild("AttributeItem4/UpperBtn").GetComponent<Button>();
        addValue4 = selfTransform.FindChild("AttributeItem4/AddValue").GetComponent<Text>();
        currentValue4 = selfTransform.FindChild("AttributeItem4/CurrentValue").GetComponent<Text>();
        allValue4 = selfTransform.FindChild("AttributeItem4/AllValue").GetComponent<Text>();
        needValue4 = selfTransform.FindChild("AttributeItem4/NeedValue").GetComponent<Text>();
        addPropertyBtn4.onClick.AddListener(new UnityEngine.Events.UnityAction(AddPropertyBtn4));
        maxPropertyBtn4.onClick.AddListener(new UnityEngine.Events.UnityAction(MaxPropertyBtn));
        maxPropertyBtn4.gameObject.SetActive(false);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshHero, ShowAllProperty);
        //texiao1.SetActive(false);
        //texiao2.SetActive(false);
        //texiao3.SetActive(false);
        //texiao4.SetActive(false);

        m_FunctionTipsManager = FunctionTipsManager.GetInstance();
        m_PropertyTipsArray = new GameObject[4];
        m_PropertyTipsArray[0] = selfTransform.FindChild("AttributeItem1/DevelopButton/TipsImage").gameObject;
        m_PropertyTipsArray[1] = selfTransform.FindChild("AttributeItem2/DevelopButton/TipsImage").gameObject;
        m_PropertyTipsArray[2] = selfTransform.FindChild("AttributeItem3/DevelopButton/TipsImage").gameObject;
        m_PropertyTipsArray[3] = selfTransform.FindChild("AttributeItem4/DevelopButton/TipsImage").gameObject;
        GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);

        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshHero, RefreshTipsController);
        GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, RefreshTipsController);
    }

    protected void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshHero, ShowAllProperty);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshHero, RefreshTipsController);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, RefreshTipsController);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        //int tableId = UI_HeroListManager._instance.heroList[0].tableId;
        //if (tableId!=null)
        //{
        //    ShowProperty(tableId, ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[0]);
        //}
        ShowProperty(UI_HeroInfoManager._instance.GetCurCard());

        m_TipsController = CreateFunctionTipsController();
        RefreshTipsController();
    }

    public void ShowProperty(ObjectCard mObj)
    {
       
        this.obj = mObj;
        ShowAllProperty();
        
    }

    public void ShowAllProperty()
    {
        if (obj == null)
            return;

        int all=ObjectSelf.GetInstance().HeroMoney;
        allHeroMoney.text =all.ToString();
        hero = obj.GetHeroRow();
        EnabledBtn();
        ShowProperty1();
        ShowProperty2();
        ShowProperty3();
        ShowProperty4();
        //texiao1.SetActive(false);
        //texiao2.SetActive(false);
        //texiao3.SetActive(false);
        //texiao4.SetActive(false);
        
    }

    private void ShowProperty1()
    {
        int attributetrainID = obj.GetHeroData().GetTrainCount()[0];

        int currentValue = 0;
        if (attributetrainID!=0)
        {
            AttributetrainTemplate attrOld = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID);
            currentValue = attrOld.getAttriValue();
            currentValue1.text = currentValue.ToString() + "/";
            AttributetrainTemplate attr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID + 1);
            addValue1.text = (attr.getAttriValue() - currentValue).ToString();
            needValue1.text = attr.getCost().ToString();
        }
        else
        {
            //int currentValue = obj.GetHeroData().TrainingMaxHP;
            currentValue1.text = currentValue.ToString() + "/";
            attributetrainID = hero.getTrainSlot1() * 1000 + 1;
            AttributetrainTemplate attr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID);
            addValue1.text = (attr.getAttriValue()).ToString();
            needValue1.text = attr.getCost().ToString();
        }
        
        int allAttributetrainID = hero.getTrainSlot1() * 1000 + hero.getTrainMaximum1()-1;
        AttributetrainTemplate allAttr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(allAttributetrainID);
        allValue1.text = allAttr.getAttriValue().ToString();
        propertySlider1.value = ((float)currentValue) / allAttr.getAttriValue();

        if (int.Parse(allHeroMoney.text) < int.Parse(needValue1.text))
        {
            needValue1.color = Color.red;
        }
        else
        {
            needValue1.color = Color.white;
        }
        if (currentValue == allAttr.getAttriValue())
        {
            needValue1.gameObject.SetActive(false);
        }
        else
        {
            needValue1.gameObject.SetActive(true);
        }

        if (attributetrainID>=allAttributetrainID)
        {
            addPropertyBtn1.gameObject.SetActive(false);
            maxPropertyBtn1.gameObject.SetActive(true);
            addValue1.gameObject.SetActive(false);
        }
        else
        {
            addPropertyBtn1.gameObject.SetActive(true);
            maxPropertyBtn1.gameObject.SetActive(false);
            addValue1.gameObject.SetActive(true);
        }
    }

    private void ShowProperty2()
    {
        int[] heroType = hero.getClientSignType();
        
        switch (heroType[1])
        {
            case 0:
                propertyType2.text = GameUtils.getString("hero_train_type2");//物攻
                
                break;
            case 1:
                propertyType2.text = GameUtils.getString("hero_train_type3");//法攻
                
                break;
            default:
                break;
        }

        int currentValue = 0;
        int attributetrainID = obj.GetHeroData().GetTrainCount()[1];
        if (attributetrainID != 0)
        {
            AttributetrainTemplate attrOld = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID);
            currentValue = attrOld.getAttriValue();
            currentValue2.text = currentValue.ToString() + "/";
            AttributetrainTemplate attr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID + 1);
            addValue2.text = (attr.getAttriValue() - currentValue).ToString();
            needValue2.text = attr.getCost().ToString();
        }
        else
        {
            currentValue2.text = currentValue.ToString() + "/";
            attributetrainID = hero.getTrainSlot2() * 1000 + 1;
            AttributetrainTemplate attr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID);
            addValue2.text = attr.getAttriValue().ToString();
            needValue2.text = attr.getCost().ToString();
        }

        

        int allAttributetrainID = hero.getTrainSlot2() * 1000 + hero.getTrainMaximum2()-1;
        AttributetrainTemplate allAttr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(allAttributetrainID);
        allValue2.text =   allAttr.getAttriValue().ToString();
        propertySlider2.value = ((float)currentValue) / allAttr.getAttriValue();

        if (int.Parse(allHeroMoney.text) < int.Parse(needValue2.text))
        {
            needValue2.color = Color.red;
        }
        else
        {
            needValue2.color = Color.white;
        }
        if (currentValue == allAttr.getAttriValue())
        {
            needValue2.gameObject.SetActive(false);
        }
        else
        {
            needValue2.gameObject.SetActive(true);
        }

        if (attributetrainID >= allAttributetrainID)
        {
            addPropertyBtn2.gameObject.SetActive(false);
            maxPropertyBtn2.gameObject.SetActive(true);
            addValue2.gameObject.SetActive(false);
        }
        else
        {
            addPropertyBtn2.gameObject.SetActive(true);
            maxPropertyBtn2.gameObject.SetActive(false);
            addValue2.gameObject.SetActive(true);
        }
    }
    private void ShowProperty3()
    {
        int currentValue = 0;
        
        int attributetrainID = obj.GetHeroData().GetTrainCount()[2];
        if (attributetrainID != 0)
        {
            AttributetrainTemplate attrOld = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID);
            currentValue = attrOld.getAttriValue();
            currentValue3.text = currentValue.ToString() + "/";
            AttributetrainTemplate attr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID + 1);
            addValue3.text = (attr.getAttriValue() - currentValue).ToString();
            needValue3.text = attr.getCost().ToString();
        }
        else
        {
            currentValue3.text = currentValue.ToString() + "/";
            attributetrainID = hero.getTrainSlot3() * 1000 + 1;
            AttributetrainTemplate attr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID);
            addValue3.text = attr.getAttriValue().ToString();
            needValue3.text = attr.getCost().ToString();
        }

      

        int allAttributetrainID = hero.getTrainSlot3() * 1000 + hero.getTrainMaximum3()-1;
        AttributetrainTemplate allAttr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(allAttributetrainID);
        allValue3.text = allAttr.getAttriValue().ToString();
        propertySlider3.value = ((float)currentValue) / allAttr.getAttriValue();

        if (int.Parse(allHeroMoney.text) < int.Parse(needValue3.text))
        {
            needValue3.color = Color.red;
        }
        else
        {
            needValue3.color = Color.white;
        }
        if (currentValue == allAttr.getAttriValue())
        {
            needValue3.gameObject.SetActive(false);
        }
        else
        {
            needValue3.gameObject.SetActive(true);
        }

        if (attributetrainID >= allAttributetrainID)
        {
            addPropertyBtn3.gameObject.SetActive(false);
            maxPropertyBtn3.gameObject.SetActive(true);
            addValue3.gameObject.SetActive(false);
        }
        else
        {
            addPropertyBtn3.gameObject.SetActive(true);
            maxPropertyBtn3.gameObject.SetActive(false);
            addValue3.gameObject.SetActive(true);
        }
    }
    private void ShowProperty4()
    {
        int currentValue = 0;
        
        int attributetrainID = obj.GetHeroData().GetTrainCount()[3];
        if (attributetrainID != 0)
        {
            AttributetrainTemplate attrOld = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID);
            currentValue = attrOld.getAttriValue();
            currentValue4.text = currentValue.ToString() + "/";
            AttributetrainTemplate attr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID + 1);
            addValue4.text = (attr.getAttriValue() - currentValue).ToString();
            needValue4.text = attr.getCost().ToString();
        }
        else
        {
            currentValue4.text = currentValue.ToString() + "/";
            attributetrainID = hero.getTrainSlot4() * 1000 + 1;
            AttributetrainTemplate attr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(attributetrainID);
            addValue4.text = (attr.getAttriValue() - currentValue).ToString();
            needValue4.text = attr.getCost().ToString();
        }

      
        int allAttributetrainID = hero.getTrainSlot4() * 1000 + hero.getTrainMaximum4()-1;
        AttributetrainTemplate allAttr = (AttributetrainTemplate)DataTemplate.GetInstance().m_AttributetrainTable.getTableData(allAttributetrainID);
        allValue4.text = allAttr.getAttriValue().ToString();
        propertySlider4.value = ((float)currentValue) / allAttr.getAttriValue();

        if (int.Parse(allHeroMoney.text) < int.Parse(needValue4.text))
        {
            needValue4.color = Color.red;
        }
        else
        {
            needValue4.color = Color.white;
        }
        if (currentValue == allAttr.getAttriValue())
        {
            needValue4.gameObject.SetActive(false);
        }
        else
        {
            needValue4.gameObject.SetActive(true);
        }

        if (attributetrainID >= allAttributetrainID)
        {
            addPropertyBtn4.gameObject.SetActive(false);
            maxPropertyBtn4.gameObject.SetActive(true);
            addValue4.gameObject.SetActive(false);
        }
        else
        {
            addPropertyBtn4.gameObject.SetActive(true);
            maxPropertyBtn4.gameObject.SetActive(false);
            addValue4.gameObject.SetActive(true);
        }
    }

    public void AddPropertyBtn1()
    {
        if (int.Parse(allHeroMoney.text) < int.Parse(needValue1.text))
        {
            AddMsgBox(GameUtils.getString("hero_train_tip2")); 
        }
        else
        {
            AddProperty(1);
            DisableBtn();
            //texiao1.SetActive(true);
           // UI_EffectManager._instance.InstanceEffect_Link("PropertyUp01", UI_HeroListManager._instance.GetPoint());
        }
       
        
    }
    public void AddPropertyBtn2()
    {
        if (int.Parse(allHeroMoney.text) < int.Parse(needValue2.text))
        { 
            AddMsgBox(GameUtils.getString("hero_train_tip2"));
        }
        else
        {
            AddProperty(2);
            DisableBtn();
            //texiao2.SetActive(true);
            //UI_EffectManager._instance.InstanceEffect_Link("PropertyUp01", UI_HeroListManager._instance.GetPoint());
        }
        
        
    }
    public void AddPropertyBtn3()
    {
        if (int.Parse(allHeroMoney.text) < int.Parse(needValue3.text))
        {
            AddMsgBox(GameUtils.getString("hero_train_tip2"));
        }
        else
        {
            AddProperty(3);
            DisableBtn();
            //texiao3.SetActive(true);
           // UI_EffectManager._instance.InstanceEffect_Link("PropertyUp01", UI_HeroListManager._instance.GetPoint());
        }
       
    }
    public void AddPropertyBtn4()
    {
        if (int.Parse(allHeroMoney.text) < int.Parse(needValue4.text))
        {
            AddMsgBox(GameUtils.getString("hero_train_tip2"));
        }
        else
        {
             AddProperty(4);
             DisableBtn();
             //texiao4.SetActive(true);
             //UI_EffectManager._instance.InstanceEffect_Link("PropertyUp01", UI_HeroListManager._instance.GetPoint());
        }
       
    }

    public void AddProperty(byte num)
    {
        CPeiyangHero chero = new CPeiyangHero();
        chero.herokey = (int)obj.GetGuid().GUID_value;
        chero.slotnum = num;
        IOControler.GetInstance().SendProtocol(chero);
    }

    public void DisableBtn()
    {
        addPropertyBtn1.enabled = false;
        addPropertyBtn2.enabled = false;
        addPropertyBtn3.enabled = false;
        addPropertyBtn4.enabled = false;
    }

    public void EnabledBtn()
    {
        addPropertyBtn1.enabled = true;
        addPropertyBtn2.enabled = true;
        addPropertyBtn3.enabled = true;
        addPropertyBtn4.enabled = true;
    }

    public void MaxPropertyBtn()
    {
        AddMsgBox(GameUtils.getString("hero_train_tip1"));
    }


    public void AddMsgBox(string text)
    {
        DreamFaction.GameCore.InterfaceControler.GetInst().AddMsgBox(text);
    }


    public void ShowUI()
    {
        this.gameObject.SetActive(true);

    }

    public void HideUI()
    {
        UI_EffectManager._instance.DisableEffect("PropertyUp01");
        if (this.gameObject != null)
        {
            this.gameObject.SetActive(false);
        }

    }



    void OnSelectCardHeroChanged(GameEvent ev)
    {
        if (ev == null || ev.data == null)
        {
            LogManager.LogError("当前选中的英雄为空");
            return;
        }

        ObjectCard card = ev.data as ObjectCard;

        if (card == null)
        {
            LogManager.LogError("当前选中的英雄为空");
            return;
        }

        ShowProperty(card);

    }

    //生成功能提示控制器
    IFunctionTipsController CreateFunctionTipsController()
    {
        var _manager = FunctionTipsManager.GetInstance();
        if (_manager == null)
            return null;

        FunctionTipsControllerBoolArrayType _controller = 
            new FunctionTipsControllerBoolArrayType(m_PropertyTipsArray, _manager.CheckEveryAttributeTrain);


        return _controller;
    }
    private void RefreshTipsController()
    {
        if (m_TipsController == null || m_FunctionTipsManager == null)
        {
            CloseAllTips();
            return;
        }

        if (m_FunctionTipsManager.CheckHeroIsInDefaultTeam())
            m_TipsController.Refresh();
        else
            CloseAllTips();
    }
    private void CloseAllTips()
    {
        for (int i = 0; i < m_PropertyTipsArray.Length; i++)
        {
            m_PropertyTipsArray[i].SetActive(false);
        }
    }
}
