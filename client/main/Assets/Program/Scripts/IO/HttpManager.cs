using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameCore;
using System;
using Platform;
using LitJson;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;

public class HttpManager
{
    static private HttpManager mInst = null;

    static private LinkedList<PlatformBase> ProtocolList = new LinkedList<PlatformBase>();

    static public HttpManager GetInstance()
    {
        if (mInst == null)
        {
            mInst = new HttpManager();
        }
        return mInst;
    }

    public void AddTask(PlatformBase paramRunnable)
    {
        lock (ProtocolList)
        {
            ProtocolList.AddLast(paramRunnable);
        }
    }

    public void run()
    {
        if (ProtocolList.Count != 0)
        {
            lock (ProtocolList)
            {
                do
                {
                    PlatformBase localRunnable = (PlatformBase)ProtocolList.First.Value;
                    if (localRunnable != null)
                    {
                        localRunnable.Process();
                    }
                    ProtocolList.RemoveFirst();
                }
                while (ProtocolList.Count != 0);
            }
        }
    }

    public IEnumerator SendPlatfom(PlatformBase nProtocol)
    {
        string Url = ConfigsManager.Inst.GetClientConfig(ClientConfigs.PlatformIP);

        Dictionary<string, string> heads = new Dictionary<string, string>();
        heads.Add("cscode", Convert.ToString(nProtocol.m_Type));
        heads.Add("Method", "POST");
        heads.Add("Content-Type", "application/x-www-form-urlencoded");

        JsonData newData = new JsonData();
        nProtocol.marshal(ref newData);
        byte[] btBodys = Encoding.UTF8.GetBytes(newData.ToJson());
  
        WWW w = new WWW(Url, btBodys, heads);
        while(!w.isDone)
        {
            yield return false;
        }
        if (w.error != null) {
			Debug.Log("SendPlatfom w.error" + w.error);
						yield return false;
		}

        Dictionary<string, string> responseHeaders = w.responseHeaders;

		foreach (KeyValuePair<string, string> response in responseHeaders)
		{
			Debug.Log("responseHeaders Value : " + response.Value  + " \nresponseHeaders Key : " + response.Key);
		}

        string cscode = responseHeaders["CSCODE"];
        if (cscode != string.Empty)
        {
            JsonData jsonData = JsonMapper.ToObject(w.text);
            PlatformBase protocol = PlatformBase.JsonDataObject(Int32.Parse(cscode));
            if (protocol != null)
            {
                protocol.unmarshal(jsonData);
                AddTask(protocol);
            }
        }
    }
}