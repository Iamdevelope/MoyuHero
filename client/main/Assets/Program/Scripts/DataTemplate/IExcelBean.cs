using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DreamFaction.LogSystem;
using System.IO;

public abstract class IExcelBean{

    public virtual void parser(BinaryReader data )  //二进制读取文件
    {
        LogManager.LogError("you should overrite BinaryReader function!");
    }

	public virtual int GetID ()
	{
		return int.MaxValue;
	}

    public virtual string GetStringID()
    {
        return string.Empty;
    }

    public string[] parserXMLStringArray(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            string[] ss = str.Split('#');
            for (int i = 0; i < ss.Length; ++i)
            {
                if (ss[i].Equals("-1"))
                {
                    ss[i] = string.Empty;
                }
            }
            return ss;
        }

        return new string[] { string.Empty };
    }

    public int[] parserXMLIntArray(string str)
    {
        if (!string.IsNullOrEmpty(str) && !str.Equals("-1"))
        {
            string[] ss = str.Split('#');
            int[] v = new int[ss.Length];
            for (int i = 0; i < ss.Length; ++i)
            {
                v[i] = int.Parse(ss[i]);
            }
            return v;
        }
        return new int[]{-1};
 
    }

    public float[] parserXMLFloatArray(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            string[] ss = str.Split('#');
            float[] v = new float[ss.Length];
            for (int i = 0; i < ss.Length; ++i)
            {
                v[i] = float.Parse(ss[i]);
            }
            return v;
        }
        return new float[] { 0.0f };
    }

    public void parserXmlDict(string str, out Dictionary<string,int> outdata)
    {
        outdata = new Dictionary<string, int>();
        if (str.Equals("-1"))
        {
            return;
        }
        char[] r = { '{', '}', ':', ',' };
        string[] strArray = str.Split(r, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length % 2 == 0)
        {
            for (int idx = 0; idx < strArray.Length; idx += 2 )
            {
                outdata.Add(strArray[idx], System.Int32.Parse(strArray[idx + 1]));
            }
        }
    }

    public string ReadToString( BinaryReader data )
    {
        int size = data.ReadInt32();
        byte[] nBytes = data.ReadBytes(size);
        string temp = System.Text.Encoding.Unicode.GetString(nBytes);
        if (temp.Equals("-1"))
            return string.Empty;
        return temp;
    }

    public float ReadToSingle( BinaryReader data )
    {
        int size = data.ReadInt32();
        byte[] nBytes = data.ReadBytes(size);
        string param = System.Text.Encoding.Unicode.GetString(nBytes);

        return Convert.ToSingle(param);
    }
}
