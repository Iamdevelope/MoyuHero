using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using System.Text.RegularExpressions;
using System.Text;
using GNET;

public class SpreeCode : BaseUI 
{

        public static string UI_ResPath = "SystemSetting/UI_SpreeCodeBox_2_3";
        public static SpreeCode Inst;

        private Button m_OkBtn; //确定按钮
        private Button m_ReInputBtn; //重新输入按钮
        private Button m_ReturnBtn; //返回按钮


        private GameObject m_ReInput;

        private Text m_OkBtnTxt; // 确定按钮文本
        private Text m_TitleTxt; // Title文本框
        private InputField m_SpreeCode_InTxt; //输入框的内容

        private string m_account = "";
        private StringBuilder _StringBuilder = new StringBuilder();

        //显示礼包item部分
        private GameObject m_AwardWindow;
        private GameObject m_AwardItem;
        private GameObject m_itemParent;
        private UniversalItemCell m_Cell;

        private Button m_CloseLBShowWindow;

        private Text m_ReceiveSuccessText;
        private Text m_WindowTitleText;
        private Text m_WindowTiShiText;
        private Text m_ShuRuTiShiText;

           
        // ================ 继承 =======================
        // 初始化UI数据和绑定关系等
        public override void InitUIData()
        {
            base.InitUIData();
            Inst = this;

            m_OkBtnTxt = selfTransform.FindChild("SpreeCodeWindow/ButtonOk/Text").GetComponent<Text>();
            m_TitleTxt = selfTransform.FindChild("SpreeCodeWindow/Image/Title_txt").GetComponent<Text>();
            m_SpreeCode_InTxt = selfTransform.FindChild("SpreeCodeWindow/InputField").GetComponent<InputField>();


            m_OkBtn = selfTransform.FindChild("SpreeCodeWindow/ButtonOk").GetComponent<Button>();
            m_OkBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickOKBtn));

            m_ReInputBtn = selfTransform.FindChild("SpreeCodeWindow/ReenterButton").GetComponent<Button>();
            m_ReInputBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickReInputBtn));

            m_ReturnBtn = selfTransform.FindChild("SpreeCodeWindow/BackButton").GetComponent<Button>();
            m_ReturnBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickReturnBtn));

            m_ReInput = selfTransform.FindChild("SpreeCodeWindow/ReenterButton").gameObject;

            m_SpreeCode_InTxt.onValueChange.AddListener(new UnityEngine.Events.UnityAction<string>(OnSpreeCodeChanged));
            // 屏蔽除了canvas0层之外图层的UI点击事件！
            //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_UnBlockCanvasRaycasts, BaseUIControler.UICanvasFlag.Canvas0);

            //礼包Item部分  ----------------------------
            m_AwardWindow = selfTransform.FindChild("AwardWindow").gameObject;
            //m_AwardItem = selfTransform.FindChild("AwardWindow/UI_moreItem/Grid/item").gameObject;
            m_itemParent = selfTransform.FindChild("AwardWindow/UI_moreItem/Grid").gameObject;

            m_CloseLBShowWindow = selfTransform.FindChild("AwardWindow/UI_Top/BackBtn").GetComponent<Button>();
            m_CloseLBShowWindow.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseLBShowWindow));

            m_ReceiveSuccessText = selfTransform.FindChild("AwardWindow/Image/Text").GetComponent<Text>();
            m_ShuRuTiShiText = selfTransform.FindChild("SpreeCodeWindow/InputField/Placeholder").GetComponent<Text>();
            m_WindowTiShiText = selfTransform.FindChild("AwardWindow/PromptOBJ/Text").GetComponent<Text>();

        }

        // 初始化UI显示内容
        public override void InitUIView()
        {
            base.InitUIView();
            m_ReceiveSuccessText.text = GameUtils.getString("fight_perfectbox_form_title");
            m_WindowTiShiText.text = GameUtils.getString("fight_perfectbox_form_content");
            m_ShuRuTiShiText.text = GameUtils.getString("System_setting_cotnent1");
        }

        //// 准备关闭UI
        //public override void OnReadyForClose()
        //{
        //    base.OnReadyForClose();
        //    UI_HomeControler.Inst.ReMoveUI(gameObject);
        //}



        // ================== 私有接口 ==================

        // ===================== 按钮回调 =================
         // 礼包码文本框发生变化后的回调
        public void OnSpreeCodeChanged(string spreeCode)
        {
            if (spreeCode != string.Empty)
            {
                m_ReInput.SetActive(true);
            }
            else
            {
                m_ReInput.SetActive(false);
            }

            string _str = string.Empty;
            _StringBuilder.Remove(0, _StringBuilder.Length);

            char[] _char = spreeCode.ToCharArray();
            foreach (char c in _char)
            {
                if (IsNumAndEnCh(c.ToString()))
                {
                    _StringBuilder.Append(c.ToString());
                    _str = _StringBuilder.ToString();
                }
            }

            m_SpreeCode_InTxt.text = _str;
            m_account = _str;
        }
        /// <summary>
        /// 是否为数字和字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsNumAndEnCh(string input)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        } 
        //点击确定按钮
        private void OnClickOKBtn()
        {
            if (m_account == string.Empty)
            {
                InterfaceControler.GetInst().AddMsgBox("礼包码不能为空，请重新输入", this.transform);
            }
            if (m_account != string.Empty)
            {
                CDuihuanlb _CDuihuanlb = new CDuihuanlb();
                _CDuihuanlb.str = m_account;
                IOControler.GetInstance().SendProtocol(_CDuihuanlb);
            }
        }
        //重新输入按钮
        private void OnClickReInputBtn()
        {
            m_SpreeCode_InTxt.text = "";
        }
        /// <summary>
        /// 打开礼包物品显示窗口
        /// </summary>
        public void OnOpenLbItemWindow()
        {
            m_AwardWindow.SetActive(true);

            foreach (Transform child in m_itemParent.transform)
            {
                if (child.gameObject.name == "item(Clone)")
                {
                    Destroy(child.gameObject);
                }
            }

            for (int j = 0; j < ObjectSelf.GetInstance().GetSettingData().m_innerdropidlist.Count; j++)
            {
                m_Cell = UniversalItemCell.GenerateItem(m_itemParent.transform);

                InnerdropTemplate value = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(ObjectSelf.GetInstance().GetSettingData().m_innerdropidlist[j]);
                if (value == null) return;

                int itemid = value.getObjectid();//掉落物ID
                int type = value.getObjectid() / 1000000;
                switch (type)
                {
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                        ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                        if (_temp_res != null)
                        {
                            m_Cell.InitByID(itemid, value.getDropnum());
                            m_Cell.SetText(GameUtils.getString(_temp_res.getName()), "", "");
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                        {
                            ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                            if (itemTable != null)
                            {
                                m_Cell.InitByID(itemid, -1);
                                m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                            }
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                        {
                            ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemid);
                            if (itemTable != null)
                            {
                                m_Cell.InitByID(itemid, value.getDropnum());
                                m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                            }
                        }
                        break;
                    case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                        {
                            HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(itemid);
                            if (hero != null)
                            {
                                m_Cell.InitByID(itemid, value.getDropnum());
                                m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            
        }
        /// <summary>
        /// 关闭礼包物品显示窗口
        /// </summary>
        public void OnClickCloseLBShowWindow()
        {
            m_AwardWindow.SetActive(false);
        }

        public void LBCodeError()
        {
            //InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("nickname_word1"), this.transform);
            InterfaceControler.GetInst().AddMsgBox(("礼包码错误，请重新输入"), this.transform);
            m_SpreeCode_InTxt.text = string.Empty ;
        }

        //关闭窗口按钮
        private void OnClickReturnBtn()
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }
}
