using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DG.Tweening;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.Utils;
namespace DreamFaction.UI
{
    /// <summary>
    /// 背包界面，继承自BaseUI
    /// </summary>tw
    public class UI_Bag : BaseUI  
    {
        public static UI_Bag _instance;
        public GameObject addbag;
        public GameObject itemGet;
        private Button backBtn;
        public static string UI_ResPath = "UI_Home/UI_Bag_2_3";

        private Button runeBtn;
        private Button propBtn;
        private Button addBagBtn;
        private Text bagNumText;
        private GameObject runeBtSelect;
        private GameObject goodsBtSelect;

        private GameObject rune;
        private GameObject prop;
        private GameObject top_money;
        public Transform MsgBoxGroup;                                                       //消息父节点
        public Transform m_captionPoston;//常驻跑马灯位置
        private Button m_Button_All;
        private Transform m_Button_All_Normal;
        private Transform m_Button_All_Select;
        private Button m_Button_Conslum;
        private Transform m_Button_Conslum_Normal;
        private Transform m_Button_Conslum_Select;
        private Button m_Button_Marital;
        private Transform m_Button_Marital_Normal;
        private Transform m_Button_Marital_Select;
        private Button m_Button_Fragement;
        private Transform m_Button_Fragement_Normal;
        private Transform m_Button_Fragement_Select;
        private Image[] m_Bt_Noramls = new Image[4];
        private Transform[] m_Bt_Selects = new Transform[4];
        private Button[] m_Buttons = new Button[4];
        private Transform m_NullItemShow;//道具背包没有物品时

       public  IFunctionTipsController m_TipsController;
        public override void InitUIData()
        {
            base.InitUIData();
            _instance = this;
            //MsgBoxList = new List<GameObject>();
            MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
            backBtn = selfTransform.FindChild("new/TopPanel/TopTittle/BackBtn").GetComponent<Button>();
            backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
            rune = selfTransform.FindChild("Rune").gameObject;
            prop = selfTransform.FindChild("Goods").gameObject;
            runeBtn = selfTransform.FindChild("UI_BG_Top/BtnGroup/UI_Btn_Rune").GetComponent<Button>();
            runeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickShowRune));

            propBtn = selfTransform.FindChild("UI_BG_Top/BtnGroup/UI_Btn_Prop").GetComponent<Button>();
            propBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickShowProp));

            addBagBtn = selfTransform.FindChild("UI_Btn_Augment/NumberBg/add").GetComponent<Button>();
            addBagBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAddbag));
            bagNumText = selfTransform.FindChild("UI_Btn_Augment/NumberBg/Number").GetComponent<Text>();
            runeBtSelect = selfTransform.FindChild("UI_BG_Top/BtnGroup/UI_Btn_Rune/Selected").gameObject;
            goodsBtSelect = selfTransform.FindChild("UI_BG_Top/BtnGroup/UI_Btn_Prop/Selected").gameObject;

            top_money = selfTransform.FindChild("UI_BG_Top/MoneyBarUI").gameObject;
            m_captionPoston = selfTransform.FindChild("captionPostion");

            m_Button_All = selfTransform.FindChild("new/tableBts/all").GetComponent<Button>();
            m_Button_All.onClick.AddListener(OnAll);
            m_Button_Conslum = selfTransform.FindChild("new/tableBts/conslum").GetComponent<Button>();
            m_Button_Conslum.onClick.AddListener(OnConslum);
            m_Button_Marital = selfTransform.FindChild("new/tableBts/matril").GetComponent<Button>();
            m_Button_Marital.onClick.AddListener(OnMarital);
            m_Button_Fragement = selfTransform.FindChild("new/tableBts/fragment").GetComponent<Button>();
            m_Button_Fragement.onClick.AddListener(OnFragment);
            m_Bt_Noramls[0] = selfTransform.FindChild("new/tableBts/all/Image").GetComponent<Image>();
            m_Bt_Noramls[1] = selfTransform.FindChild("new/tableBts/conslum/Image").GetComponent<Image>();
            m_Bt_Noramls[2] = selfTransform.FindChild("new/tableBts/matril/Image").GetComponent<Image>();
            m_Bt_Noramls[3] = selfTransform.FindChild("new/tableBts/fragment/Image").GetComponent<Image>();
            m_Bt_Selects[0] = selfTransform.FindChild("new/tableBts/all/select");
            m_Bt_Selects[1] = selfTransform.FindChild("new/tableBts/conslum/select");
            m_Bt_Selects[2] = selfTransform.FindChild("new/tableBts/matril/select");
            m_Bt_Selects[3] = selfTransform.FindChild("new/tableBts/fragment/select");
            m_Buttons[0] = m_Button_All;
            m_Buttons[1] = m_Button_Conslum;
            m_Buttons[2] = m_Button_Marital;
            m_Buttons[3] = m_Button_Fragement;
            m_NullItemShow = selfTransform.FindChild("NullItem");
            m_NullItemShow.gameObject.SetActive(false);

            uiMark = Core.UIMark.PlayerBag;
            HomeControler.Inst.PushFunly(1, 114);
            GameEventDispatcher.Inst.addEventListener(GameEventID.KE_KnapsackUpdateShow, RemoveItemShow);
            GameEventDispatcher.Inst.addEventListener(GameEventID.KE_BagItemSizeShow, BagItemSizeShow);
            GameEventDispatcher.Inst.addEventListener(GameEventID.KE_KnapsackAdd, AddItemShow);
        }

        public void RemoveItemShow(GameEvent e)
        {
            //GoldShow();

            BagItemSizeShow();
            int bagid = (int)(e.data);

            if (bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON)
            {
                if (UI_ItemsManage._instance != null)
                {
                    UI_ItemsManage._instance.m_SelectIndex = 0;
                    UI_ItemsManage._instance.RemoveItem();
                    UI_ItemsMassage._instance.useBtn.interactable = true;

                }                
            }
            else
            {
                if (UI_RuneMange._instance != null)
                {
                    UI_RuneMange._instance.SelectRune(UI_RuneMange._instance.m_CurType);
                }         
            }
            m_TipsController.Refresh();
        }

        public void AddItemShow(GameEvent e)
        {
            //GoldShow();
            BagItemSizeShow();
            int bagid = (int)(e.data);
            if (bagid == (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON)
            {
                if (UI_ItemsManage._instance != null)
                {
                    UI_ItemsManage._instance.m_SelectIndex = 0;
                    UI_ItemsTypeSelect._instance.UpdateItemType(UI_ItemsTypeSelect._instance.itemTypeNum);
                    UI_ItemsMassage._instance.useBtn.interactable = true;
                }                
            }
            else
            {
                if (UI_RuneMange._instance != null)
                {
                    UI_RuneMange._instance.SelectRune(UI_RuneMange._instance.m_CurType);
                }                
            }

        }

        protected void OnDestroy()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.KE_KnapsackUpdateShow, RemoveItemShow);
            GameEventDispatcher.Inst.clearEvent(GameEventID.KE_BagItemSizeShow);
            GameEventDispatcher.Inst.clearEvent(GameEventID.KE_KnapsackAdd);
        }
      
        public void AddMsgBox(string text)
        {

            DreamFaction.GameCore.InterfaceControler.GetInst().AddMsgBox(text);
        }
        public override void InitUIView()
        {
            base.InitUIView();
            //GoldShow();
            OnAll();
            BagItemSizeShow();

            m_TipsController = CreateFunctionTipsController();
            m_TipsController.Refresh();

            ////设置常驻跑马灯位置
            //UI_CaptionManager cap = UI_CaptionManager.GetInstance();
            //if (cap != null)
            //    cap.AwakeUp(m_captionPoston);

        }

        public void GoldShow()
        {

          
            var player = ObjectSelf.GetInstance();        
            //goldText.text = player.Money.ToString();
            //diamondText.text = player.Gold.ToString();
            

        }

        public void BagItemSizeShow()
        {
            if (ObjectSelf.GetInstance().BagBuyCount == DataTemplate.GetInstance().m_GameConfig.getCommon_item_packset_max_purchase())
            {
                addBagBtn.gameObject.SetActive(false);
            }
            var player = ObjectSelf.GetInstance(); 
            if (player.CommonItemContainer.GetBagItemSum() <= player.CommonItemContainer.GetBagItemSizeMax())
            {
                bagNumText.text = "<color=#f3863a>" + player.CommonItemContainer.GetBagItemSum() + "</color>/" + player.CommonItemContainer.GetBagItemSizeMax();
            }
            else
            {
                bagNumText.text = "<color=red>" + player.CommonItemContainer.GetBagItemSum() + "</color>/" + player.CommonItemContainer.GetBagItemSizeMax();
            }
        }

        // 1：准备播放进场动画
        public override void OnPlayingEnterAnimation()
        {
            //transform.localScale = new Vector3(0, 0, 0);
        }

        // 2: 准备删除UI
        public override void OnReadyForClose()
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }
        private void OnClickAddbag()
        {   
           addbag.SetActive(true);
        }
        private void OnClickBackBtn()
        {
            //UI_CaptionManager cap = UI_CaptionManager.GetInstance();
            //if (cap != null)
            //    cap.Release(m_captionPoston);
            UI_HomeControler.Inst.ReMoveUI(gameObject);
            //UIState = UIStateEnum.PlayingExitAnimation;
            rune.SetActive(true);
            prop.SetActive(false);
            ObjectSelf.GetInstance().CommonItemContainer.SetNewItemPreviw(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP);
            ObjectSelf.GetInstance().CommonItemContainer.SetNewItemPreviw(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON);
            
        }

        private void OnClickShowRune()
        {
            if (rune.activeSelf) return;
            rune.SetActive(true);
            prop.SetActive(false);
            //runeOutline.enabled = true;
            //propOutline.enabled = false;
            runeBtSelect.SetActive(true);
            goodsBtSelect.SetActive(false);
            UI_RuneMange._instance.m_SelectIndex = 0;
            UI_RuneTypeSelect._instance.UpdateRuneType(UI_RuneTypeSelect._instance.itemTypeNum);
            UI_ItemsTypeSelect._instance.allbtn.OnClose();
            //TODO
        }

        private void OnClickShowProp()
        {
            if (prop.activeSelf) return;
            rune.SetActive(false);
            prop.SetActive(true);
            UI_ItemsManage._instance.m_SelectIndex = 0;
            //runeOutline.enabled = false;
            //propOutline.enabled = true;
            runeBtSelect.SetActive(false);
            goodsBtSelect.SetActive(true);
            UI_ItemsTypeSelect._instance.UpdateItemType(UI_ItemsTypeSelect._instance.itemTypeNum);
            UI_RuneTypeSelect._instance.MainBtn.OnClose();
            //TODO

        }

        public void OnOpenItemGet()
        {
            itemGet.SetActive(true);
        }

        //添加金币
        private void OnClickMoneyAddBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_QuikBuyGoldMgr.UI_ResPath);
        }

        //添加钻石
        private void OnClickDiamondAddBtn()
        {
            UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
        }

        IFunctionTipsController CreateFunctionTipsController()
        {
            GameObject _go, _goRune;
            FunctionTipsController _controller = new FunctionTipsController();

            var _manager = FunctionTipsManager.GetInstance();
            _go = selfTransform.FindChild("UI_BG_Top/BtnGroup/UI_Btn_Prop/TipsImage").gameObject;
            _controller.AddControlledObject(_go, _manager.CheckIsNewCommon);
            _goRune = selfTransform.FindChild("UI_BG_Top/BtnGroup/UI_Btn_Rune/TipsImage").gameObject;
            _controller.AddControlledObject(_goRune,_manager.CheckIsNewRune);
            return _controller;
        }
        /// <summary>
        /// 设置顶部的金币和钻石bar是否显示
        /// </summary>
        /// <param name="isshow"></param>
        public void SetMoneyBarShowOrHide(bool isshow)
        {
            top_money.SetActive(isshow);
        }
        void OnAll()
        {
            SetSelect(0);
            m_Bt_Noramls[0].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0001");
            m_Bt_Noramls[1].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0011");
            m_Bt_Noramls[2].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0002");
            m_Bt_Noramls[3].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0004");
            UI_ItemsManage._instance.SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_ALL);
            ResetSize();
        }
        void OnConslum()
        {
            SetSelect(1);
            m_Bt_Noramls[0].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0000");
            m_Bt_Noramls[1].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0010");
            m_Bt_Noramls[2].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0002");
            m_Bt_Noramls[3].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0004");
            UI_ItemsManage._instance.SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_CONSUME);
            ResetSize();
        }
        void OnMarital()
        {
            SetSelect(2);
            m_Bt_Noramls[0].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0000");
            m_Bt_Noramls[1].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0011");
            m_Bt_Noramls[2].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0003");
            m_Bt_Noramls[3].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0004");
            UI_ItemsManage._instance.SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_MATERIAL);
            ResetSize();
        }
        void OnFragment()
        {
            SetSelect(3);
            m_Bt_Noramls[0].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0000");
            m_Bt_Noramls[1].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0011");
            m_Bt_Noramls[2].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0002");
            m_Bt_Noramls[3].sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_DJBB_0005");
            
            UI_ItemsManage._instance.SelectItem(EM_SORT_COMMON_ITEM.EM_SORT_COMMON_ITEM_FRAGMENT);
            ResetSize();
        }
        void SetSelect(int index)
        {
            for (int i = 0; i < m_Buttons.Length; i++)
            {
                if (index == i)
                {
                    m_Buttons[i].GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0013");
                }
                else
                {
                    m_Buttons[i].GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0012");
                }
                m_Buttons[i].image.SetNativeSize();
            }
        }
        void ResetSize()
        {
            for (int i = 0; i < m_Bt_Noramls.Length; i++)
            {
                m_Bt_Noramls[i].SetNativeSize();
                //m_Buttons[i].image.SetNativeSize();
                m_Buttons[i].transform.GetComponent<Image>().SetNativeSize();
            }
        }
        /// <summary>
        /// 设置是否显示没有道具时的提示
        /// </summary>
        /// <param name="isShow"></param>
        public void SetShowHideNullItemTip(bool isShow)
        {
            m_NullItemShow.gameObject.SetActive(isShow);
            prop.SetActive(!isShow);
        }

    }
  
}