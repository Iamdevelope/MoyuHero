using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DreamFaction.GameEventSystem;
using LitJson;
using DreamFaction.UI.Core;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameCore;

namespace Platform
{
    public class SGuest : PlatformBase
	{
        public string uid;
        public string plat;
        public string token;
        public int    result;

        public const int PROTOCOL_TYPE = 10002;
        public SGuest()
            : base(PROTOCOL_TYPE)
        {
          
        }

        public override void marshal(ref JsonData _os_)
        {

        }
        public override void unmarshal(JsonData _os_)
        {
            uid = (string)_os_["uid"];
            plat = (string)_os_["plat"];
            token = (string)_os_["token"];
            result = (int)_os_["result"];
        }

        public override void Process() 
        {
            if (result == 0)
            {
                ConfigsManager.Inst.SetClientConfig(ClientConfigs.State,"1");

                MainGameControler.Inst.mToken = this.token;
                MainGameControler.Inst.mPlatform = this.plat;
                MainGameControler.Inst.mPlatId = this.uid;

                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_SelectLoginServer.UI_ResPath);
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_LoginWin.UI_ResPath);

            }
        }
	}	
}
