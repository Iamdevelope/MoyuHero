using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork.Data;



public enum ATTRIBUTETYPE : byte
{
    HP,
    PhysicAtk,
    PhysicDef,
    MagicDef,
}
public class AttributeItem : BaseUI {

    private Text mCurNum;
    private Text mAddNum;
    private Text mConsumeNum;
    private Button mDevelopBtn;
    private Image mLimitImage;

    private HeroData info;
    private ATTRIBUTETYPE type;
	// Use this for initialization
	void Awake () {
        mCurNum = transform.FindChild("Image/attribute_value").GetComponent<Text>();
        mAddNum = transform.FindChild("Image/add_value").GetComponent<Text>();
        mConsumeNum = transform.FindChild("DevelopButton/Image/Text").GetComponent<Text>();
        mDevelopBtn = transform.FindChild("DevelopButton").GetComponent<Button>();
        mLimitImage = transform.FindChild("UpperBtn").GetComponent<Image>();
        mLimitImage.gameObject.SetActive(false);

        mDevelopBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onUpperCall));
	}
    private void onUpperCall()
    {
        int cur = 0;
        int add = 0;
        switch (type)
        {
            case ATTRIBUTETYPE.HP:
                {
                    cur = info.TrainingMaxHP;
                    add = 1;//(int)info.hPGrowth;
                }
                break;
            case ATTRIBUTETYPE.PhysicAtk:
                {
                    cur = info.TrainingPhysicalAttack;
                    add = 2;//(int)info.physicalAttackGrowth;
                }
                break;
            case ATTRIBUTETYPE.PhysicDef:
                {
                    cur = info.TrainingPhysicalDefence;
                    add = 3;//(int)info.physicalDefenceGrowth;
                }
                break;
            case ATTRIBUTETYPE.MagicDef:
                {
                    cur = info.TrainingMagicAttack;
                    add = 4;//(int)info.magicDefenceGrowth;
                }
                break;
        }
        cur += add;
        add = Random.Range(add, (int)(add * 1.1));
        mCurNum.text = cur.ToString();
        mAddNum.text = add.ToString();
        mConsumeNum.text = Random.Range(100, 1000).ToString();
        //switch (type)
        //{
        //    case ATTRIBUTETYPE.HP:
        //        {
        //            info.TrainingMaxHP = cur;
        //            info.hPGrowth = add;
        //        }
        //        break;
        //    case ATTRIBUTETYPE.PhysicAtk:
        //        {
        //            info.initPhysicalAttack = cur;
        //            info.physicalAttackGrowth = add;
        //        }
        //        break;
        //    case ATTRIBUTETYPE.PhysicDef:
        //        {
        //            info.initPhysicalDefence = cur;
        //            info.physicalDefenceGrowth = add;
        //        }
        //        break;
        //    case ATTRIBUTETYPE.MagicDef:
        //        {
        //            info.initMagicDefence = cur;
        //            info.magicDefenceGrowth = add;
        //        }
        //        break;
        //}
    }
    public void initData(HeroData hero, ATTRIBUTETYPE type)
    {
        info = hero;
        this.type = type;
        int cur = 0;
        int add = 0;
        int consume = Random.Range(100, 1000);
        bool canup = Random.Range(0.0f, 1.0f) > 0.5;
        switch (type)
        {
            case ATTRIBUTETYPE.HP:
                {
                    cur = info.TrainingMaxHP;
                    add = 1;//(int)info.hPGrowth;
                }
                break;
            case ATTRIBUTETYPE.PhysicAtk:
                {
                    cur = info.TrainingPhysicalAttack;
                    add = 2;//(int)info.physicalAttackGrowth;
                }
                break;
            case ATTRIBUTETYPE.PhysicDef:
                {
                    cur = info.TrainingPhysicalDefence;
                    add = 3;//(int)info.physicalDefenceGrowth;
                }
                break;
            case ATTRIBUTETYPE.MagicDef:
                {
                    cur = info.TrainingMagicDefence;
                    add = 4;//(int)info.magicDefenceGrowth;
                }
                break;
        }
        mCurNum.text = cur.ToString();
        mAddNum.text = add.ToString();
        mConsumeNum.text = consume.ToString();
        if (canup)
        {
            mDevelopBtn.gameObject.SetActive(true);
            mLimitImage.gameObject.SetActive(false);
        }
        else
        {
            mDevelopBtn.gameObject.SetActive(false);
            mLimitImage.gameObject.SetActive(true);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
