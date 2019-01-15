using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class StageTemplate : IExcelBean
{
    #region stage attribute

    public int m_stageid;				  //关卡id	int
    public string m_stagename;				 //关卡名称	string
    public string m_stageinfo;				 //关卡描述	string
    public int m_premissionid;			//前置关卡id	int
    public int m_playerlevel;			//玩家等级	int
    public int m_stagetype;				//关卡类型	int
    public string m_stagemap;				 //地图编号	string
    public string[] m_stageevent;			//地图事件编号	string
    public string[] m_extraloadresource;     //额外加载资源
    public int m_playerexp;				//玩家经验	int
    public int m_heroexp;				 //英雄经验	int
    public int m_goldreward;            //金币奖励
    public int m_cost;				      //行动力消耗	int
    public string m_stageicon;				 //关卡图标	string
    public int[] m_monstergroup;			//怪物组	array_int
    public int[] m_stagedrop;				 //关卡奖励(服务器)	array_int
    public int m_specialcondition;       //特殊奖励条件参数
    public int[] m_specialstagedrop;       //特殊关卡奖励(服务器)
    public int m_dropcheck;				 //掉落检查	int
    public int m_limittime;				 //限制次数	int
    //显示用掉落
    public string m_displaydrop;
    public int[] m_displayMonster;	     //显示怪物	array_int
    public int m_bossbox;				 //宝箱id	int
    public int m_winCondition;           //胜利条件
    public int[] m_waveFury;               //每波怪物初始怒气
    public int m_aiCheck;                //AI检查间隔
    public int m_mysteriousStage;        //神秘关卡
    public int m_mysteriousShop;         //神秘商店
    public int m_fightTime;              //战斗时间
    public int m_expcrystal;             //经验结晶
    public int[] m_resetCost;
    public int m_isBoss;                //是否Boss关卡
    public int m_iskcdh;                //是否有开场动画
    public int m_chapterUId;            //章节id序列号;
    //关卡图标位置
    private float[] m_stageiconposition;
    private int m_stageiconscale;       //关卡图标缩放比例--百分比;

    #endregion

    private int m_iRoundTime = 0;             // 关卡的怪物轮次  

    public object m_CustomData = null;    //自定义数据;

    public StageTemplate()
    {
    }

    public override void parser(BinaryReader data)
    {
        m_stageid = data.ReadInt32();
        m_stagename = ReadToString(data);
        m_stageinfo = ReadToString(data);
        m_premissionid = data.ReadInt32();
        m_playerlevel = data.ReadInt32();
        m_stagetype = data.ReadInt32();
        m_stagemap = ReadToString(data);
        m_stageevent = parserXMLStringArray(ReadToString(data));
        m_extraloadresource = parserXMLStringArray(ReadToString(data));
        m_playerexp = data.ReadInt32();
        m_goldreward = data.ReadInt32();
        m_heroexp = data.ReadInt32();
        m_expcrystal = data.ReadInt32();
        m_cost = data.ReadInt32();
        m_stageicon = ReadToString(data);
        m_stageiconposition = parserXMLFloatArray(ReadToString(data));
        m_stageiconscale = data.ReadInt32();
        m_monstergroup = parserXMLIntArray(ReadToString(data));
        m_stagedrop = parserXMLIntArray(ReadToString(data));
        m_specialcondition = data.ReadInt32();
        m_specialstagedrop = parserXMLIntArray(ReadToString(data));
        m_dropcheck = data.ReadInt32();
        m_limittime = data.ReadInt32();
        m_displaydrop = ReadToString(data);
        m_displayMonster = parserXMLIntArray(ReadToString(data));
        m_bossbox = data.ReadInt32();
        m_winCondition = data.ReadInt32();
        m_waveFury = parserXMLIntArray(ReadToString(data));
        m_aiCheck = data.ReadInt32();
        m_mysteriousStage = data.ReadInt32();
        m_mysteriousShop = data.ReadInt32();
        m_fightTime = data.ReadInt32();
        m_isBoss = data.ReadInt32();
        m_resetCost = parserXMLIntArray(ReadToString(data));
        m_iskcdh = data.ReadInt32();
        m_chapterUId = data.ReadInt32();
        // 计算怪物轮次
        foreach (var id in m_monstergroup)
        {
            if (id == 0) this.m_iRoundTime++;
        }
        this.m_iRoundTime++;
    }

    public override int GetID()
    {
        return this.m_stageid;
    }

    public int GetRoundTime()
    {
        return m_iRoundTime;
    }

    public float[] getStageiconposition()
    {
        return this.m_stageiconposition;
    }

    public int getStageIconScale()
    {
        return this.m_stageiconscale;
    }
 
    //public Dictionary<string, int> getDisplaydrop()
    //{
    //    return this.m_displaydrop;
    //}
}