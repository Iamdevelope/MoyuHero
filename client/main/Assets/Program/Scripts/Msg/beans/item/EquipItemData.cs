using System;
namespace GNET
{
    public class EquipItemData : Marshal
	{
        public int init1; // 基础属性1，默认-1
        public int init2; // 基础属性2，默认-1
        public int init3; // 基础属性3，默认-1
        public int attr1; // 附属属性1，默认-1
        public int attr2; // 附属属性2，默认-1
        public int attr3; // 附属属性3，默认-1
        public int attr4; // 附属属性4，默认-1

        public EquipItemData()
        {

        }

        public EquipItemData(int _init1_, int _init2_, int _init3_, int _attr1_, int _attr2_, int _attr3_, int _attr4_)
        {
            this.init1 = _init1_;
            this.init2 = _init2_;
            this.init3 = _init3_;
            this.attr1 = _attr1_;
            this.attr2 = _attr2_;
            this.attr3 = _attr3_;
            this.attr4 = _attr4_;
        }

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(init1);
            _os_.marshal(init2);
            _os_.marshal(init3);
            _os_.marshal(attr1);
            _os_.marshal(attr2);
            _os_.marshal(attr3);
            _os_.marshal(attr4);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            init1 = _os_.unmarshal_int();
            init2 = _os_.unmarshal_int();
            init3 = _os_.unmarshal_int();
            attr1 = _os_.unmarshal_int();
            attr2 = _os_.unmarshal_int();
            attr3 = _os_.unmarshal_int();
            attr4 = _os_.unmarshal_int();
            return _os_;
		}

	}
}
