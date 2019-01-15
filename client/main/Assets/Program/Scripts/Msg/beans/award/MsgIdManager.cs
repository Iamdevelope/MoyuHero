using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
    public static class MsgIdManager
	{
        enum MyEnum
        {
            HEROBAG_IS_FULL = 100000001,
            ITEMBAG_IS_FULL = 100000002,
        }

        public static string getMsgStr(int msgId){
            switch (msgId) {
                case (int)MyEnum.HEROBAG_IS_FULL:
                    return "hero_bag_tips4";
                case (int)MyEnum.ITEMBAG_IS_FULL:
                    return "hero_bag_tips2";
                default:
                    return "login_content3";
            }
        }
	}
}
