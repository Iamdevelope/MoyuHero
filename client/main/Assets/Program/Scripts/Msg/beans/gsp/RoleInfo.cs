using System;
namespace GNET
{
    public class RoleInfo : Marshal
	{
        public long roleid; // ID
        public string rolename; // Ãû³Æ
        public short rolelevel; // µÈ¼¶

        public RoleInfo()
        {
            rolename = "";
        }

        public RoleInfo(long _roleid_, string _rolename_, short _rolelevel_)
        {
            this.roleid = _roleid_;
            this.rolename = _rolename_;
            this.rolelevel = _rolelevel_;
        }

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(roleid);
            os.marshal(rolename);
            os.marshal(rolelevel);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            roleid = os.unmarshal_long();
            rolename = os.unmarshal_String();
            rolelevel = os.unmarshal_short();
			return os;
		}

	}
}
