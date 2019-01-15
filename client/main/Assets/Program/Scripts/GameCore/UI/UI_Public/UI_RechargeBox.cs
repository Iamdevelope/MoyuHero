using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using UnityEngine.Events;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    /// <summary>
    /// 通用提示窗口
    /// </summary>
    public class UI_RechargeBox : BaseUI
    {
        public static string UI_ResPath = "UI_General/UI_MassageObj_1_5";
        public static string UI_Name = "UI_MassageObj_1_5";

        public static EM_RECHARGEBOX_OPEN_TYPE CurOpenType = EM_RECHARGEBOX_OPEN_TYPE.NONE;

        private static UI_RechargeBox m_Inst = null;

        private Text DesTxt;                                    //显示描述
        private Text Description_text;                          //显示信息文本
        private Text ConNum;                                    //消耗数量文本
        private Text LeftBtn_text;                              //左侧功能按钮文本
        private Text RightBtn_text;                             //右侧关闭按钮文本
        private Text TotalMoneyTxt;                             //右上角金钱信息;
        private Image TotalMoneyImg;                            //右上角金钱信息;
        private GameObject TotalMoneyObj;                       //右上角金钱信息，默认关闭;
        private Image Consume_Image;                            //显示消耗图片
        private Button Left_Btn;                                //Box左侧功能按钮
        private Button Right_Btn;                              //Box右侧关闭按钮
        private Text LeftBtn_Midtext;                           //左边按钮 确定 居中
        private Image Left_Btn_Img;                             //左边按钮的图片
        private GameObject Left_NormalBt;
        private GameObject Left_UnNormalBt;

        private bool isMoneyBarActive = false;                  //是否显示金钱信息;

        public static object Data
        {
            get;
            set;
        }

        public static UI_RechargeBox Inst
        {
            get
            {
                return m_Inst;
            }
        }

        public override void InitUIData()
        {
            m_Inst = this;

            CurOpenType = EM_RECHARGEBOX_OPEN_TYPE.NONE;

            Description_text = selfTransform.FindChild("MaxBagnum").GetComponent<Text>();
            DesTxt = selfTransform.FindChild("DesTxt").GetComponent<Text>();

            Consume_Image = selfTransform.FindChild("AddHeroBagBtn/unnormal/xiaohao/ConsumeImage").GetComponent<Image>();
            ConNum = selfTransform.FindChild("AddHeroBagBtn/unnormal/xiaohao/ConNum").GetComponent<Text>();
            Left_Btn = selfTransform.FindChild("AddHeroBagBtn").GetComponent<Button>();
            Right_Btn = selfTransform.FindChild("ReturnBtn").GetComponent<Button>();
            Left_NormalBt = selfTransform.FindChild("AddHeroBagBtn/normal").gameObject;
            Left_UnNormalBt = selfTransform.FindChild("AddHeroBagBtn/unnormal").gameObject;
            LeftBtn_text = selfTransform.FindChild("AddHeroBagBtn/unnormal/Text").GetComponent<Text>();
            LeftBtn_Midtext = selfTransform.FindChild("AddHeroBagBtn/normal/Text").GetComponent<Text>();
            RightBtn_text = selfTransform.FindChild("ReturnBtn/Text").GetComponent<Text>();
            TotalMoneyObj = selfTransform.FindChild("MoneyBar").gameObject;
            TotalMoneyImg = selfTransform.FindChild("MoneyBar/Gold/Image").GetComponent<Image>();
            TotalMoneyTxt = selfTransform.FindChild("MoneyBar/Gold/bg/Text").GetComponent<Text>();
            Left_Btn_Img = selfTransform.FindChild("AddHeroBagBtn").GetComponent<Image>();

            SetRightClick(OnCloes);
        }

        public override void InitUIView()
        {
            base.InitUIView();
            
            TotalMoneyObj.SetActive(isMoneyBarActive);
            DesTxt.enabled = false;
        }

        /// <summary>
        /// 设置右上角显示钱币信息;
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="num"></param>
        public void SetMoneyInfo(int resourceType, int num)
        {
            TotalMoneyImg.sprite = GameUtils.GetSpriteByResourceType(resourceType);
            TotalMoneyTxt.text = num.ToString();
        }

        public void SetMoneyInfoActive(bool isActive)
        {
            if (isActive == isMoneyBarActive)
            {
                return;
            }
            isMoneyBarActive = isActive;
            TotalMoneyObj.SetActive(isActive);
        }

        //设置显示文本信息
        public void SetDescription_text(string value) 
        {
            Description_text.text = value; 
        }
        //设置是否显示消耗资源
        public void SetIsNeedDescription(bool isNeed)
        {
            if (isNeed)
            {
                //ConNum.enabled = true;
                //Consume_Image.enabled = true;
                Left_NormalBt.SetActive(false);
                Left_UnNormalBt.SetActive(true);
            }
            else
            {
                //ConNum.enabled = false;
                //Consume_Image.enabled = false;
                Left_NormalBt.SetActive(true);
                Left_UnNormalBt.SetActive(false); 
            }
        }
        //设置消耗物品显示数量
        public void SetConNum(string value)
        {
            ConNum.text = value;
            Left_NormalBt.SetActive(false);
            Left_UnNormalBt.SetActive(true);
        }

        public int GetConNum()
        {
            return int.Parse(ConNum.text);
        }
        /// <summary>
        /// 设置左侧按钮图片为整体不分割 并且字体居中
        /// </summary>
        public void SetLeftYesMid()
        {
            Left_Btn_Img.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_xuanze");
            LeftBtn_text.gameObject.SetActive(false);
            LeftBtn_Midtext.gameObject.SetActive(true);
        }

        //设置消耗物品图片
        public void SetConsume_Image(Sprite temp)
        {
            Consume_Image.sprite = temp;
        }
        //设置左侧按钮文本显示
        public void SetLeftBtn_text(string value)
        {
            LeftBtn_text.text = value;
            LeftBtn_Midtext.text = value;
        }
        //设置右侧按钮文本显示
        public void SetRightBtn_text(string value)
        {
            RightBtn_text.text = value;
        }
        //左侧按钮添加功能事件
        public void SetLeftClick(UnityAction temp)
        {
            Left_Btn.onClick.AddListener(temp);
        }
        //右侧侧按钮添加功能事件
        public void SetRightClick(UnityAction temp)
        {
            Right_Btn.onClick.AddListener(temp);
        }
        public void OnCloes()
        {
            UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
        }

        public void SetDesTxt(string text)
        {
            DesTxt.enabled = true;
            DesTxt.text = text;
        }

        void OnDestroy()
        {
            m_Inst = null;
        }
    }
}

