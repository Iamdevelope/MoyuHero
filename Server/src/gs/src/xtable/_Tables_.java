package xtable;

import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class _Tables_ extends xdb.Tables {
	static volatile boolean isExplicitLockCheck = false;

	public static void startExplicitLockCheck() {
		isExplicitLockCheck = true;
	}

	public static _Tables_ getInstance() {
		return (_Tables_)xdb.Xdb.getInstance().getTables();
	}

	public _Tables_() {
		add(rebatechargeactivities);
		add(ladderroles);
		add(herotroops);
		add(bag);
		add(pvpladder);
		add(gameactivitylist);
		add(bossrolelist);
		add(macinfos);
		add(tujianheros);
		add(googlereceiptes);
		add(bloodroles);
		add(lottylist);
		add(monthcardlist);
		add(chargeactivities);
		add(gamelevels);
		add(roleonoffstate);
		add(skillbag);
		add(collectbag);
		add(firstladderinforole);
		add(equipextdatas);
		add(itemlimits);
		add(friends);
		add(heroclones);
		add(stageroles);
		add(herocolumns);
		add(maillist);
		add(bossranklists);
		add(equipbag);
		add(serverinfo);
		add(appreceiptes);
		add(soulbag);
		add(endlesscolumns);
		add(bossdata);
		add(shopbuycolumns);
		add(msgroles);
		add(duihuanlqs);
		add(roledhmaps);
		add(firstfeedactivities);
		add(consumeactivities);
		add(huoyuelist);
		add(lotteryitemlist);
		add(bloodranklist);
		add(billroles);
		add(heroskincolumns);
		add(equipcolumns);
		add(newshopcolumns);
		add(moheoddses);
		add(friendreqs);
		add(auuserinfo);
		add(stagetxalllist);
		add(endlessranklists);
		add(battles);
		add(artifactcolumns);
		add(user);
		add(properties);
		add(skillextdatas);
	}

	// visible in package
	xdb.TTable<Long, xbean.RebateChargeActivityRole> rebatechargeactivities = new xdb.TTable<Long, xbean.RebateChargeActivityRole>() {
		public String getName() {
			return "rebatechargeactivities";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.RebateChargeActivityRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.RebateChargeActivityRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.RebateChargeActivityRole value = xbean.Pod.newRebateChargeActivityRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.RebateChargeActivityRole newValue() {
			xbean.RebateChargeActivityRole value = xbean.Pod.newRebateChargeActivityRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.LadderRole> ladderroles = new xdb.TTable<Long, xbean.LadderRole>() {
		public String getName() {
			return "ladderroles";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.LadderRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.LadderRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.LadderRole value = xbean.Pod.newLadderRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.LadderRole newValue() {
			xbean.LadderRole value = xbean.Pod.newLadderRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.Troops> herotroops = new xdb.TTable<Long, xbean.Troops>() {
		public String getName() {
			return "herotroops";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Troops value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Troops unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Troops value = xbean.Pod.newTroops();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Troops newValue() {
			xbean.Troops value = xbean.Pod.newTroops();
			return value;
		}

	};

	xdb.TTable<Long, xbean.Bag> bag = new xdb.TTable<Long, xbean.Bag>() {
		public String getName() {
			return "bag";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Bag value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Bag unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Bag value = xbean.Pod.newBag();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Bag newValue() {
			xbean.Bag value = xbean.Pod.newBag();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.LadderInfo> pvpladder = new xdb.TTable<Integer, xbean.LadderInfo>() {
		public String getName() {
			return "pvpladder";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.LadderInfo value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.LadderInfo unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.LadderInfo value = xbean.Pod.newLadderInfo();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.LadderInfo newValue() {
			xbean.LadderInfo value = xbean.Pod.newLadderInfo();
			return value;
		}

	};

	xdb.TTable<Long, xbean.gameactivitys> gameactivitylist = new xdb.TTable<Long, xbean.gameactivitys>() {
		public String getName() {
			return "gameactivitylist";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.gameactivitys value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.gameactivitys unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.gameactivitys value = xbean.Pod.newgameactivitys();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.gameactivitys newValue() {
			xbean.gameactivitys value = xbean.Pod.newgameactivitys();
			return value;
		}

	};

	xdb.TTable<Long, xbean.bossrole> bossrolelist = new xdb.TTable<Long, xbean.bossrole>() {
		public String getName() {
			return "bossrolelist";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.bossrole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.bossrole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.bossrole value = xbean.Pod.newbossrole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.bossrole newValue() {
			xbean.bossrole value = xbean.Pod.newbossrole();
			return value;
		}

	};

	xdb.TTable<String, xbean.MacInfo> macinfos = new xdb.TTable<String, xbean.MacInfo>() {
		public String getName() {
			return "macinfos";
		}

		public OctetsStream marshalKey(String key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key, xdb.Const.IO_CHARSET);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.MacInfo value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public String unmarshalKey(OctetsStream _os_) throws MarshalException {
			String key = "";
			key = _os_.unmarshal_String(xdb.Const.IO_CHARSET);
			return key;
		}

		public xbean.MacInfo unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.MacInfo value = xbean.Pod.newMacInfo();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.MacInfo newValue() {
			xbean.MacInfo value = xbean.Pod.newMacInfo();
			return value;
		}

	};

	xdb.TTable<Long, xbean.TuJianHeros> tujianheros = new xdb.TTable<Long, xbean.TuJianHeros>() {
		public String getName() {
			return "tujianheros";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.TuJianHeros value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.TuJianHeros unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.TuJianHeros value = xbean.Pod.newTuJianHeros();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.TuJianHeros newValue() {
			xbean.TuJianHeros value = xbean.Pod.newTuJianHeros();
			return value;
		}

	};

	xdb.TTable<Long, xbean.GoogleReceiptData> googlereceiptes = new xdb.TTable<Long, xbean.GoogleReceiptData>() {
		public String getName() {
			return "googlereceiptes";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.GoogleReceiptData value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.GoogleReceiptData unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.GoogleReceiptData value = xbean.Pod.newGoogleReceiptData();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.GoogleReceiptData newValue() {
			xbean.GoogleReceiptData value = xbean.Pod.newGoogleReceiptData();
			return value;
		}

	};

	xdb.TTable<Long, xbean.BloodRole> bloodroles = new xdb.TTable<Long, xbean.BloodRole>() {
		public String getName() {
			return "bloodroles";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.BloodRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.BloodRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.BloodRole value = xbean.Pod.newBloodRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.BloodRole newValue() {
			xbean.BloodRole value = xbean.Pod.newBloodRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.lotty> lottylist = new xdb.TTable<Long, xbean.lotty>() {
		public String getName() {
			return "lottylist";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.lotty value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.lotty unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.lotty value = xbean.Pod.newlotty();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.lotty newValue() {
			xbean.lotty value = xbean.Pod.newlotty();
			return value;
		}

	};

	xdb.TTable<Long, xbean.monthcards> monthcardlist = new xdb.TTable<Long, xbean.monthcards>() {
		public String getName() {
			return "monthcardlist";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.monthcards value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.monthcards unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.monthcards value = xbean.Pod.newmonthcards();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.monthcards newValue() {
			xbean.monthcards value = xbean.Pod.newmonthcards();
			return value;
		}

	};

	xdb.TTable<Long, xbean.ChargeActivityRole> chargeactivities = new xdb.TTable<Long, xbean.ChargeActivityRole>() {
		public String getName() {
			return "chargeactivities";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.ChargeActivityRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.ChargeActivityRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.ChargeActivityRole value = xbean.Pod.newChargeActivityRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.ChargeActivityRole newValue() {
			xbean.ChargeActivityRole value = xbean.Pod.newChargeActivityRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.GameLevel> gamelevels = new xdb.TTable<Long, xbean.GameLevel>() {
		public String getName() {
			return "gamelevels";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.GameLevel value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.GameLevel unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.GameLevel value = xbean.Pod.newGameLevel();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.GameLevel newValue() {
			xbean.GameLevel value = xbean.Pod.newGameLevel();
			return value;
		}

	};

	xdb.TTable<Long, Integer> roleonoffstate = new xdb.TTable<Long, Integer>() {
		public String getName() {
			return "roleonoffstate";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(Integer value) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(value);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public Integer unmarshalValue(OctetsStream _os_) throws MarshalException {
			int value = 0;
			value = _os_.unmarshal_int();
			return value;
		}

		public Integer newValue() {
			int value = 0;
			return value;
		}

	};

	xdb.TTable<Long, xbean.Bag> skillbag = new xdb.TTable<Long, xbean.Bag>() {
		public String getName() {
			return "skillbag";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Bag value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Bag unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Bag value = xbean.Pod.newBag();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Bag newValue() {
			xbean.Bag value = xbean.Pod.newBag();
			return value;
		}

	};

	xdb.TTable<Long, xbean.Bag> collectbag = new xdb.TTable<Long, xbean.Bag>() {
		public String getName() {
			return "collectbag";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Bag value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Bag unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Bag value = xbean.Pod.newBag();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Bag newValue() {
			xbean.Bag value = xbean.Pod.newBag();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.FirstLadderInfoRole> firstladderinforole = new xdb.TTable<Integer, xbean.FirstLadderInfoRole>() {
		public String getName() {
			return "firstladderinforole";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.FirstLadderInfoRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.FirstLadderInfoRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.FirstLadderInfoRole value = xbean.Pod.newFirstLadderInfoRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.FirstLadderInfoRole newValue() {
			xbean.FirstLadderInfoRole value = xbean.Pod.newFirstLadderInfoRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.EquipExtData> equipextdatas = new xdb.TTable<Long, xbean.EquipExtData>() {
		public String getName() {
			return "equipextdatas";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.EquipExtData value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.EquipExtData unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.EquipExtData value = xbean.Pod.newEquipExtData();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.EquipExtData newValue() {
			xbean.EquipExtData value = xbean.Pod.newEquipExtData();
			return value;
		}

	};

	xdb.TTable<Long, xbean.ItemNumLimit> itemlimits = new xdb.TTable<Long, xbean.ItemNumLimit>() {
		public String getName() {
			return "itemlimits";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.ItemNumLimit value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.ItemNumLimit unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.ItemNumLimit value = xbean.Pod.newItemNumLimit();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.ItemNumLimit newValue() {
			xbean.ItemNumLimit value = xbean.Pod.newItemNumLimit();
			return value;
		}

	};

	xdb.TTable<Long, xbean.Friends> friends = new xdb.TTable<Long, xbean.Friends>() {
		public String getName() {
			return "friends";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Friends value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Friends unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Friends value = xbean.Pod.newFriends();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Friends newValue() {
			xbean.Friends value = xbean.Pod.newFriends();
			return value;
		}

	};

	xdb.TTable<Long, xbean.heroclone> heroclones = new xdb.TTable<Long, xbean.heroclone>() {
		public String getName() {
			return "heroclones";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.heroclone value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.heroclone unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.heroclone value = xbean.Pod.newheroclone();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.heroclone newValue() {
			xbean.heroclone value = xbean.Pod.newheroclone();
			return value;
		}

	};

	xdb.TTable<Long, xbean.StageRole> stageroles = new xdb.TTable<Long, xbean.StageRole>() {
		public String getName() {
			return "stageroles";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.StageRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.StageRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.StageRole value = xbean.Pod.newStageRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.StageRole newValue() {
			xbean.StageRole value = xbean.Pod.newStageRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.HeroColumn> herocolumns = new xdb.TTable<Long, xbean.HeroColumn>() {
		public String getName() {
			return "herocolumns";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.HeroColumn value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.HeroColumn unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.HeroColumn value = xbean.Pod.newHeroColumn();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.HeroColumn newValue() {
			xbean.HeroColumn value = xbean.Pod.newHeroColumn();
			return value;
		}

	};

	xdb.TTable<Long, xbean.Mails> maillist = new xdb.TTable<Long, xbean.Mails>() {
		public String getName() {
			return "maillist";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Mails value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Mails unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Mails value = xbean.Pod.newMails();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Mails newValue() {
			xbean.Mails value = xbean.Pod.newMails();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.bossRankList> bossranklists = new xdb.TTable<Integer, xbean.bossRankList>() {
		public String getName() {
			return "bossranklists";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.bossRankList value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.bossRankList unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.bossRankList value = xbean.Pod.newbossRankList();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.bossRankList newValue() {
			xbean.bossRankList value = xbean.Pod.newbossRankList();
			return value;
		}

	};

	xdb.TTable<Long, xbean.Bag> equipbag = new xdb.TTable<Long, xbean.Bag>() {
		public String getName() {
			return "equipbag";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Bag value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Bag unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Bag value = xbean.Pod.newBag();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Bag newValue() {
			xbean.Bag value = xbean.Pod.newBag();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.ServerInfo> serverinfo = new xdb.TTable<Integer, xbean.ServerInfo>() {
		public String getName() {
			return "serverinfo";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.ServerInfo value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.ServerInfo unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.ServerInfo value = xbean.Pod.newServerInfo();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.ServerInfo newValue() {
			xbean.ServerInfo value = xbean.Pod.newServerInfo();
			return value;
		}

	};

	xdb.TTable<Long, xbean.AppReceiptData> appreceiptes = new xdb.TTable<Long, xbean.AppReceiptData>() {
		public String getName() {
			return "appreceiptes";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.AppReceiptData value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.AppReceiptData unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.AppReceiptData value = xbean.Pod.newAppReceiptData();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.AppReceiptData newValue() {
			xbean.AppReceiptData value = xbean.Pod.newAppReceiptData();
			return value;
		}

	};

	xdb.TTable<Long, xbean.Bag> soulbag = new xdb.TTable<Long, xbean.Bag>() {
		public String getName() {
			return "soulbag";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Bag value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Bag unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Bag value = xbean.Pod.newBag();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Bag newValue() {
			xbean.Bag value = xbean.Pod.newBag();
			return value;
		}

	};

	xdb.TTable<Long, xbean.EndlessInfo> endlesscolumns = new xdb.TTable<Long, xbean.EndlessInfo>() {
		public String getName() {
			return "endlesscolumns";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.EndlessInfo value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.EndlessInfo unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.EndlessInfo value = xbean.Pod.newEndlessInfo();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.EndlessInfo newValue() {
			xbean.EndlessInfo value = xbean.Pod.newEndlessInfo();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.boss> bossdata = new xdb.TTable<Integer, xbean.boss>() {
		public String getName() {
			return "bossdata";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.boss value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.boss unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.boss value = xbean.Pod.newboss();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.boss newValue() {
			xbean.boss value = xbean.Pod.newboss();
			return value;
		}

	};

	xdb.TTable<Long, xbean.ShopbuyColumn> shopbuycolumns = new xdb.TTable<Long, xbean.ShopbuyColumn>() {
		public String getName() {
			return "shopbuycolumns";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.ShopbuyColumn value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.ShopbuyColumn unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.ShopbuyColumn value = xbean.Pod.newShopbuyColumn();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.ShopbuyColumn newValue() {
			xbean.ShopbuyColumn value = xbean.Pod.newShopbuyColumn();
			return value;
		}

	};

	xdb.TTable<Long, xbean.MsgRole> msgroles = new xdb.TTable<Long, xbean.MsgRole>() {
		public String getName() {
			return "msgroles";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.MsgRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.MsgRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.MsgRole value = xbean.Pod.newMsgRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.MsgRole newValue() {
			xbean.MsgRole value = xbean.Pod.newMsgRole();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.duihuanlq> duihuanlqs = new xdb.TTable<Integer, xbean.duihuanlq>() {
		public String getName() {
			return "duihuanlqs";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.duihuanlq value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.duihuanlq unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.duihuanlq value = xbean.Pod.newduihuanlq();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.duihuanlq newValue() {
			xbean.duihuanlq value = xbean.Pod.newduihuanlq();
			return value;
		}

	};

	xdb.TTable<Long, xbean.roledhmap> roledhmaps = new xdb.TTable<Long, xbean.roledhmap>() {
		public String getName() {
			return "roledhmaps";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.roledhmap value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.roledhmap unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.roledhmap value = xbean.Pod.newroledhmap();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.roledhmap newValue() {
			xbean.roledhmap value = xbean.Pod.newroledhmap();
			return value;
		}

	};

	xdb.TTable<Long, xbean.FirstFeedActivityRole> firstfeedactivities = new xdb.TTable<Long, xbean.FirstFeedActivityRole>() {
		public String getName() {
			return "firstfeedactivities";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.FirstFeedActivityRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.FirstFeedActivityRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.FirstFeedActivityRole value = xbean.Pod.newFirstFeedActivityRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.FirstFeedActivityRole newValue() {
			xbean.FirstFeedActivityRole value = xbean.Pod.newFirstFeedActivityRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.ConsumeActivityRole> consumeactivities = new xdb.TTable<Long, xbean.ConsumeActivityRole>() {
		public String getName() {
			return "consumeactivities";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.ConsumeActivityRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.ConsumeActivityRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.ConsumeActivityRole value = xbean.Pod.newConsumeActivityRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.ConsumeActivityRole newValue() {
			xbean.ConsumeActivityRole value = xbean.Pod.newConsumeActivityRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.huoyues> huoyuelist = new xdb.TTable<Long, xbean.huoyues>() {
		public String getName() {
			return "huoyuelist";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.huoyues value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.huoyues unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.huoyues value = xbean.Pod.newhuoyues();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.huoyues newValue() {
			xbean.huoyues value = xbean.Pod.newhuoyues();
			return value;
		}

	};

	xdb.TTable<Long, xbean.LotteryItemAll> lotteryitemlist = new xdb.TTable<Long, xbean.LotteryItemAll>() {
		public String getName() {
			return "lotteryitemlist";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.LotteryItemAll value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.LotteryItemAll unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.LotteryItemAll value = xbean.Pod.newLotteryItemAll();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.LotteryItemAll newValue() {
			xbean.LotteryItemAll value = xbean.Pod.newLotteryItemAll();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.BloodRankList> bloodranklist = new xdb.TTable<Integer, xbean.BloodRankList>() {
		public String getName() {
			return "bloodranklist";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.BloodRankList value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.BloodRankList unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.BloodRankList value = xbean.Pod.newBloodRankList();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.BloodRankList newValue() {
			xbean.BloodRankList value = xbean.Pod.newBloodRankList();
			return value;
		}

	};

	xdb.TTable<Long, xbean.BillRole> billroles = new xdb.TTable<Long, xbean.BillRole>() {
		public String getName() {
			return "billroles";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.BillRole value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.BillRole unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.BillRole value = xbean.Pod.newBillRole();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.BillRole newValue() {
			xbean.BillRole value = xbean.Pod.newBillRole();
			return value;
		}

	};

	xdb.TTable<Long, xbean.HeroSkinColumn> heroskincolumns = new xdb.TTable<Long, xbean.HeroSkinColumn>() {
		public String getName() {
			return "heroskincolumns";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.HeroSkinColumn value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.HeroSkinColumn unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.HeroSkinColumn value = xbean.Pod.newHeroSkinColumn();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.HeroSkinColumn newValue() {
			xbean.HeroSkinColumn value = xbean.Pod.newHeroSkinColumn();
			return value;
		}

	};

	xdb.TTable<Long, xbean.EquipColumn> equipcolumns = new xdb.TTable<Long, xbean.EquipColumn>() {
		public String getName() {
			return "equipcolumns";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.EquipColumn value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.EquipColumn unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.EquipColumn value = xbean.Pod.newEquipColumn();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.EquipColumn newValue() {
			xbean.EquipColumn value = xbean.Pod.newEquipColumn();
			return value;
		}

	};

	xdb.TTable<Long, xbean.NewShopMap> newshopcolumns = new xdb.TTable<Long, xbean.NewShopMap>() {
		public String getName() {
			return "newshopcolumns";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.NewShopMap value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.NewShopMap unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.NewShopMap value = xbean.Pod.newNewShopMap();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.NewShopMap newValue() {
			xbean.NewShopMap value = xbean.Pod.newNewShopMap();
			return value;
		}

	};

	xdb.TTable<Long, xbean.moheodds> moheoddses = new xdb.TTable<Long, xbean.moheodds>() {
		public String getName() {
			return "moheoddses";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.moheodds value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.moheodds unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.moheodds value = xbean.Pod.newmoheodds();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.moheodds newValue() {
			xbean.moheodds value = xbean.Pod.newmoheodds();
			return value;
		}

	};

	xdb.TTable<Long, xbean.FriendReqs> friendreqs = new xdb.TTable<Long, xbean.FriendReqs>() {
		public String getName() {
			return "friendreqs";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.FriendReqs value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.FriendReqs unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.FriendReqs value = xbean.Pod.newFriendReqs();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.FriendReqs newValue() {
			xbean.FriendReqs value = xbean.Pod.newFriendReqs();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.AUUserInfo> auuserinfo = new xdb.TTable<Integer, xbean.AUUserInfo>() {
		public String getName() {
			return "auuserinfo";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.AUUserInfo value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.AUUserInfo unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.AUUserInfo value = xbean.Pod.newAUUserInfo();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.AUUserInfo newValue() {
			xbean.AUUserInfo value = xbean.Pod.newAUUserInfo();
			return value;
		}

	};

	xdb.TTable<Long, xbean.stagetxall> stagetxalllist = new xdb.TTable<Long, xbean.stagetxall>() {
		public String getName() {
			return "stagetxalllist";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.stagetxall value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.stagetxall unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.stagetxall value = xbean.Pod.newstagetxall();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.stagetxall newValue() {
			xbean.stagetxall value = xbean.Pod.newstagetxall();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.EndlessRankList> endlessranklists = new xdb.TTable<Integer, xbean.EndlessRankList>() {
		public String getName() {
			return "endlessranklists";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.EndlessRankList value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.EndlessRankList unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.EndlessRankList value = xbean.Pod.newEndlessRankList();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.EndlessRankList newValue() {
			xbean.EndlessRankList value = xbean.Pod.newEndlessRankList();
			return value;
		}

	};

	xdb.TTable<Long, xbean.BattleInfo> battles = new xdb.TTable<Long, xbean.BattleInfo>() {
		public String getName() {
			return "battles";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.BattleInfo value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.BattleInfo unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.BattleInfo value = xbean.Pod.newBattleInfo();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.BattleInfo newValue() {
			xbean.BattleInfo value = xbean.Pod.newBattleInfo();
			return value;
		}

	};

	xdb.TTable<Long, xbean.ArtifactColumn> artifactcolumns = new xdb.TTable<Long, xbean.ArtifactColumn>() {
		public String getName() {
			return "artifactcolumns";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.ArtifactColumn value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.ArtifactColumn unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.ArtifactColumn value = xbean.Pod.newArtifactColumn();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.ArtifactColumn newValue() {
			xbean.ArtifactColumn value = xbean.Pod.newArtifactColumn();
			return value;
		}

	};

	xdb.TTable<Integer, xbean.User> user = new xdb.TTable<Integer, xbean.User>() {
		public String getName() {
			return "user";
		}

		public OctetsStream marshalKey(Integer key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.User value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Integer unmarshalKey(OctetsStream _os_) throws MarshalException {
			int key = 0;
			key = _os_.unmarshal_int();
			return key;
		}

		public xbean.User unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.User value = xbean.Pod.newUser();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.User newValue() {
			xbean.User value = xbean.Pod.newUser();
			return value;
		}

	};

	xdb.TTable<Long, xbean.Properties> properties = new xdb.TTable<Long, xbean.Properties>() {
		public String getName() {
			return "properties";
		}

		protected xdb.util.AutoKey<Long> bindAutoKey() {
			return getInstance().getTableSys().getAutoKeys().getAutoKeyLong(getName());
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.Properties value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.Properties unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.Properties value = xbean.Pod.newProperties();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.Properties newValue() {
			xbean.Properties value = xbean.Pod.newProperties();
			return value;
		}

	};

	xdb.TTable<Long, xbean.SkillExtData> skillextdatas = new xdb.TTable<Long, xbean.SkillExtData>() {
		public String getName() {
			return "skillextdatas";
		}

		public OctetsStream marshalKey(Long key) {
			OctetsStream _os_ = new OctetsStream();
			_os_.marshal(key);
			return _os_;
		}

		public OctetsStream marshalValue(xbean.SkillExtData value) {
			OctetsStream _os_ = new OctetsStream();
			value.marshal(_os_);
			return _os_;
		}

		public Long unmarshalKey(OctetsStream _os_) throws MarshalException {
			long key = 0;
			key = _os_.unmarshal_long();
			return key;
		}

		public xbean.SkillExtData unmarshalValue(OctetsStream _os_) throws MarshalException {
			xbean.SkillExtData value = xbean.Pod.newSkillExtData();
			value.unmarshal(_os_);
			return value;
		}

		public xbean.SkillExtData newValue() {
			xbean.SkillExtData value = xbean.Pod.newSkillExtData();
			return value;
		}

	};


}
