using System;
using System.Collections;
using System.Collections.Generic;
using Platform;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.UI.Core;
using LitJson;

namespace Platform
{
    public class SExchange : PlatformBase
	{

        public int result;

        public const int PROTOCOL_TYPE = 10012;

        public SExchange()
            : base(PROTOCOL_TYPE)
        {
        }

        public override void marshal(ref JsonData _os_)
        {

        }

        public override void unmarshal(JsonData _os_)
        {
            result = (int)_os_["result"];
        }

        public override void Process()
        {
            //0 成功
            //1 失败
            //提示框

            //InterfaceControler.GetInst().AddMsgBox(string.Format(GameUtils.getString("pay_tips"), username, result == 0 ? GameUtils.getString("") : GameUtils.getString("")), UI_HomeControler.Inst.GetTopTransform());
        }
	}	
}
