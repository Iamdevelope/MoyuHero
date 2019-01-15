using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
namespace GNET
{
    public partial class SRefreshBagExp : Protocol
    {

        public short data;
        public byte bagtype;

        public const int PROTOCOL_TYPE = 796450;

        public SRefreshBagExp()
            : base(PROTOCOL_TYPE)
        {
            data = 0;
            bagtype = 0;
        }

        public override object Clone()
        {
            SRefreshBagExp obj = new SRefreshBagExp();
            return obj;
        }

        public override OctetsStream marshal(OctetsStream _os_)
        {
            _os_.marshal(data);
            _os_.marshal(bagtype);
            return _os_;
        }

        public override OctetsStream unmarshal(OctetsStream _os_)
        {
            data = _os_.unmarshal_short();
            bagtype = _os_.unmarshal_byte();
            return _os_;
        }

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return size <= 32; }

        public override void Process()
        {
            if (bagtype==1)
            {
                ObjectSelf.GetInstance().BagBuyCount = data;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_BagItemSizeShow);
            }
            else
            {
                ObjectSelf.GetInstance().HeroBuyCount = (byte)data;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.KE_HeroBagItemSizeShow);
            }
        }
    }
}
