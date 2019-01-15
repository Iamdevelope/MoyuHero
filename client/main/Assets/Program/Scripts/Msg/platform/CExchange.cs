using System;
using System.Collections;
using System.Collections.Generic;
using Platform;
using LitJson;

namespace Platform
{
    public class CExchange : PlatformBase
	{

        public string uid;
        public string token;
        public string billid;
        public int goodsid;
        public string goodsname;
        public int goodsnum;
        public string price;
        public int zoneid;

        public const int PROTOCOL_TYPE = 10011;
        public CExchange()
            : base(PROTOCOL_TYPE)
        {
        }

        public override void marshal(ref JsonData _os_)
        {
            _os_["uid"] = uid;
            _os_["token"] = token;
            _os_["billid"] = billid;
            _os_["goodsid"] = goodsid;
            _os_["goodsname"] = goodsname;
            _os_["goodsnum"] = goodsnum;
            _os_["price"] = price;
            _os_["zoneid"] = zoneid;
        }

        public override void unmarshal(JsonData _os_) { }


        public override void Process() { }
	}	
}
