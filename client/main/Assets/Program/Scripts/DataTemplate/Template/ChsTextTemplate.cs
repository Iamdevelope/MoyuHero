using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class ChsTextTemplate : IExcelBean
{
    public int m_id;
    public string       m_TextID;           //序列号索引
    public int       m_type;             //类型
    public Dictionary<string, string> languageMap;

    public ChsTextTemplate()
    {
        languageMap = new Dictionary<string, string>();
    }
    public override void parser(BinaryReader data)  //二进制读取文件
    {
        m_id = data.ReadInt32();
        m_TextID = ReadToString(data);
        m_type = data.ReadInt32();
        languageMap.Add("Chinese", ReadToString(data));
        languageMap.Add("ChineseTW", ReadToString(data));
        languageMap.Add("Japanese", ReadToString(data));
        languageMap.Add("Korean", ReadToString(data));
        languageMap.Add("English", ReadToString(data));
    }

    public override string GetStringID()
    {
        return this.m_TextID;
    }
}
