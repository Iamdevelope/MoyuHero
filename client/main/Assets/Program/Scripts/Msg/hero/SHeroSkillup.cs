using System;
using System.Collections;
using System.Collections.Generic;

namespace GNET
{
    public partial class SHeroSkillup : Protocol
	{

        public static int END_OK = 1; // 成功
	    public static int END_NOT_OK = 2; // 失败

	    public int result; // 结果
        public byte skillnum; // 技能位置

        public const int PROTOCOL_TYPE = 787782;

        public SHeroSkillup()
            : base(PROTOCOL_TYPE)
		 {

		 } 

		public override object Clone()
		{
            SHeroSkillup obj = new SHeroSkillup();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(skillnum);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            skillnum = _os_.unmarshal_byte();
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
		{
            if (result == END_OK)
            {
                if(UI_SkillUpManager._instance != null)
                    UI_SkillUpManager._instance.ReturnResult(skillnum);                
            }
            
		}
	}	
}
