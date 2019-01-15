using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using Platform;
using DreamFaction.GameEventSystem;
using System.Text.RegularExpressions;

namespace DreamFaction.UI
{
    public class AccountBinding : BaseUI
    {
        public static string UI_ResPath = "SystemSetting/UI_AccountBinding_2_2";
        public static AccountBinding instance;
        private Button m_RegistBtn; //注册按钮
        private Button m_BackBtn; // 返回按钮
        private InputField m_userName_InTxt; // 用户名输入框
        private InputField m_userPass_InTxt; // 密码输入框
        private InputField m_userRePass_InTxt; // 重新输入的密码输入框

        private Text m_Account;
        private Text m_Password;
        private Text m_RePassword;
        private Text M_NamePromptext;//账号提示信息
        private Text M_PassWordPromptText;//密码提示信息
        private Text M_RePassWordPromptText;//重复输入密码提示信息

        private GameObject m_NameDuiGouImage;//账号对勾，错勾
        private GameObject m_NameCuoGouImage;
        private GameObject m_PassWordCuoGouImage;//密码对勾，错勾
        private GameObject m_PassWordDuiGouImage;
        private GameObject m_RePassWordDuiGouImage;//重复密码对勾，错勾
        private GameObject m_RePassWordCuoGouImage;

        private string m_account = string.Empty;
        private string m_password = string.Empty;
        private string m_Repassword = string.Empty;

        private bool m_IsAccount = false;
        private bool m_isPassWord = false;
        private string m_UserName = string.Empty;


        // ===================== 继承 ==================
        public override void InitUIData()
        {
            base.InitUIData();
            instance = this;

            //固定字 账号/密码/确认密码
            m_Account = selfTransform.FindChild("UserName/Text").GetComponent<Text>();
            m_Password = selfTransform.FindChild("PassWord/Text").GetComponent<Text>();
            m_RePassword = selfTransform.FindChild("RePassWord/Text").GetComponent<Text>();

            //提示信息
            M_NamePromptext = selfTransform.FindChild("UserName/NamePromptext").GetComponent<Text>();
            M_PassWordPromptText = selfTransform.FindChild("PassWord/PassWordPromptText").GetComponent<Text>();
            M_RePassWordPromptText = selfTransform.FindChild("RePassWord/RePassWordPromptText").GetComponent<Text>();

            m_RegistBtn = selfTransform.FindChild("RegistBtn").GetComponent<Button>();
            m_RegistBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRegistBtn));
            m_BackBtn = selfTransform.FindChild("UI_BG_Top/BackBtn").GetComponent<Button>();
            m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnBackBtn));

            m_userName_InTxt = selfTransform.FindChild("UserName/InputField").GetComponent<InputField>();
            m_userPass_InTxt = selfTransform.FindChild("PassWord/InputField").GetComponent<InputField>();
            m_userRePass_InTxt = selfTransform.FindChild("RePassWord/InputField").GetComponent<InputField>();

            m_NameDuiGouImage = selfTransform.FindChild("UserName/NameDuiGouImage").gameObject;
            m_NameCuoGouImage = selfTransform.FindChild("UserName/NameCuoGouImage").gameObject;
            m_PassWordDuiGouImage = selfTransform.FindChild("PassWord/PassWordDuiGouImage").gameObject;
            m_PassWordCuoGouImage = selfTransform.FindChild("PassWord/PassWordCuoGouImage").gameObject;
            m_RePassWordDuiGouImage = selfTransform.FindChild("RePassWord/RePassWordDuiGouImage").gameObject;
            m_RePassWordCuoGouImage = selfTransform.FindChild("RePassWord/RePassWordCuoGouImage").gameObject;

            m_userName_InTxt.onValueChange.AddListener(new UnityEngine.Events.UnityAction<string>(OnUserNameChanged));
            m_userName_InTxt.onEndEdit.AddListener(new UnityEngine.Events.UnityAction<string>(OnEndAccountEdit));
            m_userPass_InTxt.onValueChange.AddListener(new UnityEngine.Events.UnityAction<string>(OnPassWordChanged));
            m_userPass_InTxt.onEndEdit.AddListener(new UnityEngine.Events.UnityAction<string>(OnEndPassWordEdit));
            m_userRePass_InTxt.onValueChange.AddListener(new UnityEngine.Events.UnityAction<string>(OnRePassWordChanged));
            m_userRePass_InTxt.onEndEdit.AddListener(new UnityEngine.Events.UnityAction<string>(OnEndRePassWordEdit));
            
        }


        public override void InitUIView()
        {
            base.InitUIView();
           // m_Account.text = GameUtils.getString("aa");
           // m_Password.text = GameUtils.getString("aa");
           // m_RePassword.text = GameUtils.getString("aa");
            //M_NamePromptext = GameUtils.getString("aa");
            //M_PassWordPromptText = GameUtils.getString("aa");
           // M_RePassWordPromptText = GameUtils.getString("aa");
        }
        // ===================== 按钮回调 =================
        // 用户名文本框发生变化后的回调
        public void OnUserNameChanged(string username)
        {
            if (username == string.Empty)
            {
                m_NameDuiGouImage.SetActive(false);
                m_NameCuoGouImage.SetActive(false);
                m_account = string.Empty;
            }

        }

        public void OnPassWordChanged(string passWord)
        {
            if (passWord == string.Empty)
            {
                m_PassWordCuoGouImage.SetActive(false);
                m_PassWordDuiGouImage.SetActive(false);
                m_password = string.Empty;
            }
        }

        public void OnRePassWordChanged(string RePassWord)
        {
            if (RePassWord == string.Empty)
            {
                m_RePassWordDuiGouImage.SetActive(false);
                m_RePassWordCuoGouImage.SetActive(false);
                m_Repassword = string.Empty;
            }
        }

        public void OnEndAccountEdit(string userName)
        {
            if (IsNumAndEnCh(userName))
            {
                if (userName.Length >= 6)
                {
                    m_UserName = userName;
                    M_NamePromptext.text = string.Empty;

                    //临时加的 8/11
                    m_NameDuiGouImage.SetActive(true);
                    m_NameCuoGouImage.SetActive(false);
                    m_account = m_UserName;
                }
                else
                {
                    m_NameDuiGouImage.SetActive(false);
                    m_NameCuoGouImage.SetActive(true);
                    M_NamePromptext.text = "账号小于6位";
                }

            }
            else
            {
                if (userName != string.Empty)
                {
                    m_NameDuiGouImage.SetActive(false);
                    m_NameCuoGouImage.SetActive(true);
                    M_NamePromptext.text = "您输入了字母和数字以外的字符,请重新输入";
                    m_userName_InTxt.text = string.Empty;
                }
            }
            
        }

        public void OnEndPassWordEdit(string passWord)
        {
            if (IsNumAndEnCh(passWord))
            {
                if (passWord != string.Empty)
                {
                    if (passWord.Length >= 6)
                    {
                        m_password = passWord;
                        m_PassWordCuoGouImage.SetActive(false);
                        m_PassWordDuiGouImage.SetActive(true);
                        M_PassWordPromptText.text = string.Empty;
                    }
                    else
                    {
                        M_PassWordPromptText.text = "密码小于6位";
                        m_PassWordCuoGouImage.SetActive(true);
                        m_PassWordDuiGouImage.SetActive(false); 
                    }
                }
                else
                {
                    M_PassWordPromptText.text = "密码不能为空,请重新输入";
                    m_userPass_InTxt.text = string.Empty;
                    m_PassWordCuoGouImage.SetActive(true);
                    m_PassWordDuiGouImage.SetActive(false); 
                }
            }
            else
            {
                if (passWord != string.Empty)
                {
                    M_PassWordPromptText.text = "您输入了字母和数字以外的字符,请重新输入";
                    m_userPass_InTxt.text = string.Empty;
                    m_PassWordCuoGouImage.SetActive(true);
                    m_PassWordDuiGouImage.SetActive(false);
                }
            }
        }

        public void OnEndRePassWordEdit(string RePassWord)
        {
            if (IsNumAndEnCh(RePassWord))
            {
                if (m_password == RePassWord)
                {
                    m_Repassword = RePassWord;
                    m_isPassWord = true;
                    m_RePassWordDuiGouImage.SetActive(true);
                    m_RePassWordCuoGouImage.SetActive(false);
                    M_RePassWordPromptText.text = string.Empty;
                }
                else
                {
                    M_RePassWordPromptText.text = "密码不一致,请重新输入";
                    m_userRePass_InTxt.text = string.Empty;
                    m_RePassWordDuiGouImage.SetActive(false);
                    m_RePassWordCuoGouImage.SetActive(true);
                }
                
            }
            else
            {
                if (RePassWord != string.Empty)
                {
                    M_RePassWordPromptText.text = "您输入了字母和数字以外的字符,请重新输入";
                    m_userRePass_InTxt.text = string.Empty;
                    m_RePassWordDuiGouImage.SetActive(false);
                    m_RePassWordCuoGouImage.SetActive(true);
                }
            }
        }

        //0 成功
        //1 用户名错误
        //2 用户名已存在
        public void IsAccountExist(int key)
        {
            if (key == 0 )
            {
                m_account = m_UserName;
                m_IsAccount = true;
                m_NameDuiGouImage.SetActive(true);
                m_NameCuoGouImage.SetActive(false);
                M_NamePromptext.text = string.Empty;
            }
            if (key == 1)
            {
                m_UserName = string.Empty;
                m_IsAccount = false;
                m_NameDuiGouImage.SetActive(false);
                m_NameCuoGouImage.SetActive(true);
                M_NamePromptext.text = "用户名错误";
                m_userName_InTxt.text = string.Empty; 
            }
            if (key == 2)
            {
                m_UserName = string.Empty;
                m_IsAccount = false;
                m_NameDuiGouImage.SetActive(false);
                m_NameCuoGouImage.SetActive(true);
                M_NamePromptext.text = "用户名已存在";
                m_userName_InTxt.text = string.Empty;
            }
        }

        private bool IsNumAndEnCh(string input)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        } 
        
        // 注册按钮
        private void OnClickRegistBtn()
        {
            if (m_account != string.Empty && m_password != string.Empty)
            {
                //if (m_IsAccount && m_isPassWord)
                //{
                    //真注册逻辑
                    //CRegister Register = new CRegister();
                    //Register.username = m_account;
                    //Register.password = m_password;
                    //IOControler.GetInstance().SendPlatform(Register);
                     
                    // 假注册 实际点击的是快速开始
                if (SettingOptionItem.instance == null)
                {
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, AccountBinding.UI_ResPath);
                    CGuest msgLogin = new CGuest();
                    msgLogin.device_key = AppManager.Inst.DeviceUniqueIdentifier;
                    IOControler.GetInstance().SendPlatform(msgLogin);
                }
                else
                {
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, AccountBinding.UI_ResPath);
                }

                //}
            }
            else
            {
                M_RePassWordPromptText.text = "账号或密码不能为空,请重新输入";
            }
        }

        //注册返回信息处理
        //0 成功
        //1 用户名或密码错误
        //2 用户名已存在
        //3 创建用户错误
        public void RegisterMsg(int key)
        { 
            if (key == 1)
            {
                M_RePassWordPromptText.text = "用户名或密码错误";
            }
            if (key == 2)
            {
                M_RePassWordPromptText.text = "用户名已存在";
            }
            if (key == 3)
            {
                M_RePassWordPromptText.text = "创建用户错误";
            }
        }

        // 返回按钮
        private void OnBackBtn()
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, AccountBinding.UI_ResPath);
            if (SettingOptionItem.instance == null)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_SelectLoginServer.UI_ResPath);
            }
            UI_LoginWin.m_StarShow = true;
            UI_SelectLoginServer.m_StarShow = true;
        }

    }
}

