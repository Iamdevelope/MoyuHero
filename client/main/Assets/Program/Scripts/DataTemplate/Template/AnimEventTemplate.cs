using UnityEngine;
using System.Collections;
using System.IO;

public class AnimEventTemplate : IExcelBean
{
    public int                 m_TableID;
    public int                 m_playerID;
    public string              m_AnimName;
    public float               m_HitTime;
    public string              m_FunctionName;
    public string[]            m_Param = new string[5];

    public override void parser(BinaryReader data)  //二进制读取文件
    {
        m_TableID = data.ReadInt32();
        m_playerID = data.ReadInt32();
        m_AnimName = ReadToString(data);
        m_HitTime = ReadToSingle(data);
        m_FunctionName = ReadToString(data);

        m_Param[0] = ReadToString(data);
        m_Param[1] = ReadToString(data);
        m_Param[2] = ReadToString(data);
        m_Param[3] = ReadToString(data);
        m_Param[4] = ReadToString(data);
    }

    public override int GetID()
    {
        return this.m_TableID;
    }
}
