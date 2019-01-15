using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameNetWork;
using System.Text;
using DreamFaction.GameEventSystem;

namespace DreamFaction.UI
{
    /// <summary>
    /// 战斗玩家信息，继承自BaseUI
    /// </summary>
    public class UI_PlayerInfo :BaseUI
    {
        public Text mPlayerPower;       // 玩家当前体力
        public Text mPlayerPowerMax;    //玩家最大体力
        public Text mPlayerGold;        // 金币
        public Text mPlayerDiamond;     // 钻石
        public Text CurrentLevel;            //当前等级
        public Text RoleName;                //名字
        public Slider Exbar;                 //经验
        public Text vipTxt;                  //Vip等级
        public Button addMoney;
        public Button addGold;
        public Button addPower;
        public Button vipButton;
        public UnityEngine.Events.UnityAction mBackEvent = null;
        private int CurrentPower;
        private int MaxPower;
        public override void InitUIData()
        {
            base.InitUIData();
            if (addPower!=null)
            {
                addPower.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAddPower));
            }
            GameEventDispatcher.Inst.addEventListener(DreamFaction.GameEventSystem.GameEventID.U_RapidClearRespond, UpdateView);
            //vipButton = selfTransform.FindChild("PlayerInfo/RoleName/VIPIcon").GetComponent<Button>();
            
        }
      void OnDestroy()
      {
          GameEventDispatcher.Inst.clearEvent(DreamFaction.GameEventSystem.GameEventID.U_RapidClearRespond);
      }
        public void DisenableBtn()
        {
            if (addMoney!=null)
            {
                addMoney.enabled = false;
            }
            if (addGold!=null)
            {
                addGold.enabled = false;
            }
            if (addPower!=null)
            {
                addPower.enabled = false;
            }
        }
        public void EnableBtn()
        {
            if (addMoney != null)
            {
                addMoney.enabled = true;
            }
            if (addGold != null)
            {
                addGold.enabled = true;
            }
            if (addPower != null)
            {
                addPower.enabled = true;
            }
        }
        public void OnClickAddPower()
        {
            UI_HomeControler.Inst.AddUI("UI_Home/UI_PowersAdd_2_2");
        }

        public override void InitUIView()
        {
        //    var player = ObjectSelf.GetInstance();
        //    mPlayerPower.text = player.ActionPoint.ToString();
        //    mPlayerPowerMax.text = "/" + player.ActionPointMax.ToString();
        //    CurrentPower = player.ActionPoint;
        //    MaxPower = player.ActionPointMax;
        //    if (CurrentPower >= 200)
        //    {
        //        mPlayerPower.color = Color.red;
        //    }
        //    else if (CurrentPower >= MaxPower)
        //    {
        //        mPlayerPower.color = Color.yellow;
        //    }
        //    else
        //    {
        //        mPlayerPower.color = Color.white;
        //    }
        //    mPlayerLevel.text = player.Level.ToString();
        //    mPlayerGold.text = player.Money.ToString();
        //    mPlayerDiamond.text = player.Gold.ToString();
        //    mPlayerName.text = player.Name;
        //    PlayerTemplate pRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(ObjectSelf.GetInstance().Level);
        //    mPlayerExpSlider.value = player.Exp * 100 / pRow.getExp();
        //    var vipvalue = transform.FindChild("PlayerInfoItem/vip/value");
        //    if (vipvalue)
        //    {
        //        var vip = vipvalue.GetComponent<Text>();
        //        vip.text = ObjectSelf.GetInstance().VipLevel.ToString();
        //    }

            //UpdatePower();
            //UpdateMoney();
            //UpdateDiamond();
            UpdateVipLevel();
            UpdateExbar();
            UpdateCurrentLevel();
            UpdateRoleName();
            vipButton.onClick.AddListener(OnClickVipButton);
        }

        /// <summary>
        /// Vip更新
        /// </summary>
        private void UpdateVipLevel()
        {
            int vipLevel = ObjectSelf.GetInstance().VipLevel;
            if (vipTxt!=null)
            {
                vipTxt.text = vipLevel.ToString();
            }
            
            //string url = "UI/Number/hurt_monster/";
            //InterfaceControler.GetInst().AddLevelNum(vipLevel.ToString(), vipLevelPos, url);
        }

        /// <summary>
        /// 经验条更新
        /// </summary>
        private void UpdateExbar()
        {
            PlayerTemplate pRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(ObjectSelf.GetInstance().Level);
            if (Exbar!=null)
            {
                Exbar.value = (float)ObjectSelf.GetInstance().Exp / (float)pRow.getExp();
            }
            
        }
        /// <summary>
        /// 等级更新
        /// </summary>
        private void UpdateCurrentLevel()
        {
            short Lev = ObjectSelf.GetInstance().Level;
            if (CurrentLevel != null)
            {
                CurrentLevel.text = Lev.ToString();
            }
        }
        /// <summary>
        /// 名字
        /// </summary>
        private void UpdateRoleName()
        {
            string Name = ObjectSelf.GetInstance().Name;
            if (RoleName!=null)
            {
                RoleName.text = Name;
            }
            
        }

        //private void UpdatePower()
        //{
        //    int CurrentPower = ObjectSelf.GetInstance().ActionPoint;
        //    int MaxPower = ObjectSelf.GetInstance().ActionPointMax;
        //    StringBuilder StrTmp = new StringBuilder();
        //    StrTmp.Append("/");
        //    StrTmp.Append(MaxPower);
        //    mPlayerPower.text = CurrentPower.ToString();
        //    mPlayerPowerMax.text = StrTmp.ToString();
        //    if (CurrentPower >= 200)
        //    {
        //        mPlayerPower.color = Color.red;
        //    }
        //    else if (CurrentPower >= MaxPower)
        //    {
        //        mPlayerPower.color = Color.yellow;
        //    }
        //    else
        //    {
        //        mPlayerPower.color = Color.white;
        //    }
        //}


        //private void UpdateMoney()
        //{
        //    long Moneys = ObjectSelf.GetInstance().Money;
        //    mPlayerGold.text = Moneys.ToString();
        //}

        /// <summary>
        /// 钻石更新
        /// </summary>
        //private void UpdateDiamond()
        //{
        //    int Diam = ObjectSelf.GetInstance().Gold;
        //    mPlayerDiamond.text = Diam.ToString();
        //}


        public void onBackCall()
        {
            if (mBackEvent != null)
                mBackEvent();
        }

        public void OnClickVipButton()
        {
            UI_HomeControler.Inst.AddUI(UI_VipPrivilege.GetPath(true));
        }


        public  void UpdateView()
        {
            InitUIView();
        }
    }
}
