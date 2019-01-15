package xtable;


public class _DatabaseMetaData_ extends xdb.util.DatabaseMetaData {
	@Override
	public boolean isVerifyXdb() {
		return true;
	}
	public _DatabaseMetaData_() {
		// xbeans
		{
			Bean bean = new Bean("ServerInfo", false, false);
			super.addVariableFor(bean
				, "firsttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "starttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("User", false, false);
			super.addVariableFor(bean
				, "username"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "idlist"
				, "vector", "", "long", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "createtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("AUUserInfo", false, false);
			super.addVariableFor(bean
				, "retcode"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "loginip"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "blisgm"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "nickname"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "username"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("mohe", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isopen"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "place"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("smshopdata", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isopen"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "price"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Properties", false, false);
			super.addVariableFor(bean
				, "rolename"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "userid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "username"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "plattypestr"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "mac"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "ostype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "level"
				, "int", "", "", ""
				, "1", "", ""
				);
			super.addVariableFor(bean
				, "exp"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "viplv"
				, "int", "", "", ""
				, "1", "", ""
				);
			super.addVariableFor(bean
				, "vipexp"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "ti"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tichangetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "gold"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "yuanbao"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "shenglingzq"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "ronglian"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "huangjinxz"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "baijinxz"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "qingtongxz"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "chitiexz"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "jyjiejing"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "pvpti"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "pvptitime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tanxianti"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tanxiantitime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "jinengdian"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "jinengdiantime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "moheshop"
				, "map", "int", "mohe", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "smzhangjie"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "battlenum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "smendtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "smshop"
				, "map", "int", "smshopdata", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "smguanka_nocome"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "smshop_notcome"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "createtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "onlinetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "offlinetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tibuynum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "tibuytime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "goldbuynum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "goldbuytime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "signnum7"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "signnum28"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "signtime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "qiyuannum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "qiyuantime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "qiyuanallnum"
				, "int", "", "", ""
				, "3", "", ""
				);
			super.addVariableFor(bean
				, "buybagnum"
				, "short", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "buyherobagnum"
				, "short", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "troopnum"
				, "short", "", "", ""
				, "1", "", ""
				);
			super.addVariableFor(bean
				, "sweepnum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "todaylasttime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "sweepbuynum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "mszqgetnum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "newyindao"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Buff", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "attachtime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "time"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "round"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "amount"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "effects"
				, "map", "int", "float", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "extdata"
				, "map", "int", "float", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BuffAgent", false, false);
			super.addVariableFor(bean
				, "buffs"
				, "map", "int", "Buff", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Item", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "flags"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "position"
				, "int", "", "", ""
				, "-1", "", ""
				);
			super.addVariableFor(bean
				, "number"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "numbermap"
				, "map", "int", "int", ""
				, "", "", "8"
				);
			super.addVariableFor(bean
				, "uniqueid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Bag", false, false);
			super.addVariableFor(bean
				, "money"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "capacity"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "nextid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "items"
				, "map", "int", "Item", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "removedkeys"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("SkillExtData", false, false);
			super.addVariableFor(bean
				, "level"
				, "int", "", "", ""
				, "1", "", ""
				);
			super.addVariableFor(bean
				, "grade"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "exp"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("EquipExtData", false, false);
			super.addVariableFor(bean
				, "level"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "init1"
				, "int", "", "", ""
				, "-1", "", ""
				);
			super.addVariableFor(bean
				, "init2"
				, "int", "", "", ""
				, "-1", "", ""
				);
			super.addVariableFor(bean
				, "init3"
				, "int", "", "", ""
				, "-1", "", ""
				);
			super.addVariableFor(bean
				, "attr1"
				, "int", "", "", ""
				, "-1", "", ""
				);
			super.addVariableFor(bean
				, "attr2"
				, "int", "", "", ""
				, "-1", "", ""
				);
			super.addVariableFor(bean
				, "attr3"
				, "int", "", "", ""
				, "-1", "", ""
				);
			super.addVariableFor(bean
				, "attr4"
				, "int", "", "", ""
				, "-1", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BasicFightProperties", false, false);
			super.addVariableFor(bean
				, "hp"
				, "float", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "attack"
				, "float", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "defend"
				, "float", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "wisdom"
				, "float", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Troop", false, false);
			super.addVariableFor(bean
				, "troopnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "trooptype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "location1"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "location2"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "location3"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "location4"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "location5"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "sh1"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "sh2"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "sh3"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "sh4"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Troops", false, false);
			super.addVariableFor(bean
				, "troops"
				, "list", "", "Troop", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("MailItem", false, false);
			super.addVariableFor(bean
				, "objectid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "dropnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "dropparameter1"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "dropparameter2"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Mail", false, false);
			super.addVariableFor(bean
				, "key"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "sender"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "title"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "msg"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "innerdropidlist"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "items"
				, "list", "", "MailItem", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "endtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isopen"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "strlist"
				, "list", "", "string", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Mails", false, false);
			super.addVariableFor(bean
				, "mails"
				, "list", "", "Mail", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "nextkey"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("lotty", false, false);
			super.addVariableFor(bean
				, "normalrecruitnum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "normalrecruittime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "toprecruitnum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "toprecruittime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "toprecruitheronum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "toptentime"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "freetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "firstget"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "dreamexp"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "dreamfree"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "dream"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "singlelotty"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tenlotty"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tensinglelotty"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "getherolotty"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("LotteryItem", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isget"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "viewnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "superid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("LotteryItemlayer", false, false);
			super.addVariableFor(bean
				, "lotteryitemlist"
				, "list", "", "LotteryItem", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("LotteryItemAll", false, false);
			super.addVariableFor(bean
				, "mapkey"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "mapvalue"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "superlist"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "monthfirsttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "freelotterytime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lastrefreshtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lotteryitemmap"
				, "map", "int", "LotteryItemlayer", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("huoyue", false, false);
			super.addVariableFor(bean
				, "huoyueid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "num"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "numall"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "huoyuetype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isok"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("huoyues", false, false);
			super.addVariableFor(bean
				, "huoyuenum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "getnum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "huoyuetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "huoyuemap"
				, "map", "int", "huoyue", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("boss", false, false);
			super.addVariableFor(bean
				, "lasthpall"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lastiskill"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lastkillnum"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "newhpall"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "nowhp"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bossid1"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bossid2"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bossid3"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bossid4"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bossiskill"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "boss1killname"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "boss2killname"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "time"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("bossshop", false, false);
			super.addVariableFor(bean
				, "time"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "shoplist"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "hunternum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("bossrole", false, false);
			super.addVariableFor(bean
				, "killhpall"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "killboss"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bossnowhp"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "time"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "zhufunum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "shouwangzl"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "chuanshuozs"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bshop"
				, "bossshop", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("bossRankInfo", false, false);
			super.addVariableFor(bean
				, "roleid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "rolename"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "num"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "rankid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("bossRankList", false, false);
			super.addVariableFor(bean
				, "ranklist"
				, "list", "", "bossRankInfo", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "ranktime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bossid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("tanxian", false, false);
			super.addVariableFor(bean
				, "tanxianid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tanxiantype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "endtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "teamnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("stagetanxian", false, false);
			super.addVariableFor(bean
				, "stagetanxian"
				, "list", "", "tanxian", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("teamtanxian", false, false);
			super.addVariableFor(bean
				, "tanxianid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "team"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("stagetxall", false, false);
			super.addVariableFor(bean
				, "txtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "teamallmap"
				, "map", "int", "teamtanxian", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "stagetxallmap"
				, "map", "int", "stagetanxian", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("monthcard", false, false);
			super.addVariableFor(bean
				, "monthcardid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "overtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "getboxlasttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("monthcards", false, false);
			super.addVariableFor(bean
				, "rolemonthcards"
				, "map", "int", "monthcard", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("gameactivity", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "time"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "todaynum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "allnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "cangetnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "activitynum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "allactivitynum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "issee"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("gameactivitys", false, false);
			super.addVariableFor(bean
				, "gameactivitymap"
				, "map", "int", "gameactivity", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Equip", false, false);
			super.addVariableFor(bean
				, "key"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "equipid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "qianghualevel"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "attr1odds"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "attr2odds"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "qhadd"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("EquipColumn", false, false);
			super.addVariableFor(bean
				, "equips"
				, "list", "", "Equip", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "nextkey"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Hero", false, false);
			super.addVariableFor(bean
				, "key"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroexp"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "herolevel"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroviewid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "herojinjiestar"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "herojinjiesmall"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heropinji"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroskill"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heromishu"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heropeiyang"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroequip"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("HeroColumn", false, false);
			super.addVariableFor(bean
				, "heroes"
				, "list", "", "Hero", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "nextkey"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("HeroSkin", false, false);
			super.addVariableFor(bean
				, "heroskinid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "createtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("HeroSkinColumn", false, false);
			super.addVariableFor(bean
				, "heroskins"
				, "list", "", "HeroSkin", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("moheodds", false, false);
			super.addVariableFor(bean
				, "moheoddsmap"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("heroclone", false, false);
			super.addVariableFor(bean
				, "clonelist"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("duihuanlq", false, false);
			super.addVariableFor(bean
				, "lqkey"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "typenum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "clonelist"
				, "list", "", "string", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("roleduihuanlq", false, false);
			super.addVariableFor(bean
				, "lqkey"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "typenum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "num"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("roledhmap", false, false);
			super.addVariableFor(bean
				, "dhmap"
				, "map", "int", "roleduihuanlq", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Artifact", false, false);
			super.addVariableFor(bean
				, "artifacttype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "artifactid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heronum1"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heronum2"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heronum3"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heronum4"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heronum5"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("ArtifactColumn", false, false);
			super.addVariableFor(bean
				, "artifacts"
				, "map", "int", "Artifact", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Shopbuy", false, false);
			super.addVariableFor(bean
				, "shopid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "todaynum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lasttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "buyallnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("ShopbuyColumn", false, false);
			super.addVariableFor(bean
				, "shopbuys"
				, "map", "int", "Shopbuy", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("NewShop", false, false);
			super.addVariableFor(bean
				, "itemid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "costtype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "price"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "num"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isbuy"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("NewShopList", false, false);
			super.addVariableFor(bean
				, "shoplist"
				, "list", "", "NewShop", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lasttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "refreshtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "refreshnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("NewShopMap", false, false);
			super.addVariableFor(bean
				, "shopmap"
				, "map", "int", "NewShopList", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("OtherHero", false, false);
			super.addVariableFor(bean
				, "heroid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "exp"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "herolevel"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "hp"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "physicalattack"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "physicaldefence"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "magicattack"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "magicdefence"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "skill1"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "skill2"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "skill3"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroviewid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("EndlessInfo", false, false);
			super.addVariableFor(bean
				, "battleid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "groupnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "useherokeylist"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "monstergroup"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "trooptype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "monstertrooptype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "pact"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "dropnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "alldropnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "add1"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "add2"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "add3"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "add4"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "herobloodlist"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isend"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "time"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "ishalfcostpact"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "endtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "expectedrank"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroattribute"
				, "map", "int", "OtherHero", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "onranknum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "onranklasttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isnotfirst"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("EndlessRankInfo", false, false);
			super.addVariableFor(bean
				, "roleid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "rolename"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "level"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "groupnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "trooptype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "alldropnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroattribute"
				, "map", "int", "OtherHero", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "onranknum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("EndlessRankList", false, false);
			super.addVariableFor(bean
				, "ranklist"
				, "list", "", "EndlessRankInfo", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "ranktime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("GameLevel", false, false);
			super.addVariableFor(bean
				, "battleid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "useherokeylist"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "dropgold"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "dropcrystal"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "equipidlist"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "trooptype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("FighterInfo", false, false);
			super.addVariableFor(bean
				, "fighterid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "fightertype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "pos"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "heroid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "grouptype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "level"
				, "int", "", "", ""
				, "1", "", ""
				);
			super.addVariableFor(bean
				, "color"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "grade"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "weaponinfo"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "armorinfo"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "horseinfo"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "speed"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "hp"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "bfp"
				, "BasicFightProperties", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "effects"
				, "map", "int", "float", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "finalattrs"
				, "map", "int", "float", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "buffagent"
				, "BuffAgent", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "skills"
				, "list", "", "BattleSkill", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "shape"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BattleSkill", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "level"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "castrate"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BattleInfo", true, false);
			super.addVariableFor(bean
				, "battleid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "battlelevel"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "battletype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "fighterinfos"
				, "map", "int", "FighterInfo", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "fighters"
				, "map", "int", "chuhan.gsp.battle.Fighter", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "deadfighters"
				, "map", "int", "FighterInfo", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "battlereulst"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "round"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "turn"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "engine"
				, "chuhan.gsp.util.FightJSEngine", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("StageBattleInfo", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "maxstar"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "fightnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lastfighttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "allfightnum"
				, "int", "", "", ""
				, "1", "", ""
				);
			super.addVariableFor(bean
				, "buybattlenum"
				, "int", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "buybattlelasttime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addVariableFor(bean
				, "resetnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "sweepnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("StageInfo", false, false);
			super.addVariableFor(bean
				, "id"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "rewardgot"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "stagebattles"
				, "map", "int", "StageBattleInfo", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("StageRole", false, false);
			super.addVariableFor(bean
				, "stages"
				, "map", "int", "StageInfo", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BillData", false, false);
			super.addVariableFor(bean
				, "billid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "goodid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "goodnum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "present"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "price"
				, "float", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "createtime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "state"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "confirmtimes"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "platbillid"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BillRole", false, false);
			super.addVariableFor(bean
				, "bills"
				, "map", "long", "BillData", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "firstcharge"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("GoogleReceiptData", false, false);
			super.addVariableFor(bean
				, "roleid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "packagename"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "productid"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "token"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("AppReceiptData", false, false);
			super.addVariableFor(bean
				, "roleid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "receipt"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("LadderRole", false, false);
			super.addVariableFor(bean
				, "ladderrank"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "laddersoul"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lastsoulchangetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "enermies"
				, "list", "", "long", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "fighttimes"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lastfighttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("LadderInfo", false, false);
			super.addVariableFor(bean
				, "roleid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("SysMsg", false, false);
			super.addVariableFor(bean
				, "time"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "msgid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "params"
				, "list", "", "string", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "text"
				, "string", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isnew"
				, "boolean", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "sended"
				, "boolean", "", "", ""
				, "false", "", ""
				);
			super.addVariableFor(bean
				, "sendroleid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "msgtype"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("MsgRole", false, false);
			super.addVariableFor(bean
				, "sysmsgs"
				, "list", "", "SysMsg", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BloodRole", false, false);
			super.addVariableFor(bean
				, "curlevel"
				, "int", "", "", ""
				, "1", "", ""
				);
			super.addVariableFor(bean
				, "lasthard"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "curstar"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "battle1"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "battle2"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "battle3"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "itemlevel"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "effects"
				, "map", "int", "float", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "failed"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "relivetimes"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lastfighttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "totalstar"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "maxlevel"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "repeatstaraward"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "fixstaraward"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BloodRankRole", false, false);
			super.addVariableFor(bean
				, "roleid"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "maxlevel"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("BloodRankList", false, false);
			super.addVariableFor(bean
				, "curweek"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "rankers"
				, "list", "", "BloodRankRole", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("MacInfo", false, false);
			super.addVariableFor(bean
				, "onlinetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "offlinetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("ChargeActivity", false, false);
			super.addVariableFor(bean
				, "activityid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "totalcharge"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isgainaward"
				, "map", "int", "boolean", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("ChargeActivityRole", false, false);
			super.addVariableFor(bean
				, "activities"
				, "map", "int", "ChargeActivity", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("RebateChargeActivity", false, false);
			super.addVariableFor(bean
				, "awardinfo"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("RebateChargeActivityRole", false, false);
			super.addVariableFor(bean
				, "activities"
				, "map", "int", "RebateChargeActivity", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("FirstFeedActivity", false, false);
			super.addVariableFor(bean
				, "chargetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "rebatetime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isgainaward"
				, "boolean", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("FirstFeedActivityRole", false, false);
			super.addVariableFor(bean
				, "activities"
				, "map", "int", "FirstFeedActivity", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("ConsumeActivity", false, false);
			super.addVariableFor(bean
				, "activityid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "totalconsume"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "isgainaward"
				, "map", "int", "boolean", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("ConsumeActivityRole", false, false);
			super.addVariableFor(bean
				, "activities"
				, "map", "int", "ConsumeActivity", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("TuJianHero", false, false);
			super.addVariableFor(bean
				, "heroid"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "flag"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("TuJianHeros", false, false);
			super.addVariableFor(bean
				, "tujianbox"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tujianhero"
				, "map", "int", "TuJianHero", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "tjheromaxlevel"
				, "list", "", "int", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("ItemNumLimit", false, false);
			super.addVariableFor(bean
				, "itemnums"
				, "map", "int", "int", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "time"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("FirstLadderInfo", false, false);
			super.addVariableFor(bean
				, "starttime"
				, "long", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "zaiweimilsec"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("FirstLadderInfoRole", false, false);
			super.addVariableFor(bean
				, "roleinfos"
				, "map", "long", "FirstLadderInfo", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("FriendReqs", false, false);
			super.addVariableFor(bean
				, "byme"
				, "set", "", "long", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "imby"
				, "set", "", "long", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("FriendInfo", false, false);
			super.addVariableFor(bean
				, "totilinum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "givetilinum"
				, "int", "", "", ""
				, "", "", ""
				);
			super.addVariableFor(bean
				, "lastdaychangetime"
				, "long", "", "", ""
				, "0", "", ""
				);
			super.addBean(bean);
		}
		{
			Bean bean = new Bean("Friends", false, false);
			super.addVariableFor(bean
				, "mine"
				, "map", "long", "FriendInfo", ""
				, "", "", ""
				);
			super.addBean(bean);
		}
		// cbeans
		// tables
		super.addTable("rebatechargeactivities", "DB", "long", false, "RebateChargeActivityRole", "", "");
		super.addTable("ladderroles", "DB", "long", false, "LadderRole", "", "");
		super.addTable("herotroops", "DB", "long", false, "Troops", "", "");
		super.addTable("bag", "DB", "long", false, "Bag", "", "");
		super.addTable("pvpladder", "DB", "int", false, "LadderInfo", "", "");
		super.addTable("gameactivitylist", "DB", "long", false, "gameactivitys", "", "");
		super.addTable("bossrolelist", "DB", "long", false, "bossrole", "", "");
		super.addTable("macinfos", "DB", "string", false, "MacInfo", "", "");
		super.addTable("tujianheros", "DB", "long", false, "TuJianHeros", "", "");
		super.addTable("googlereceiptes", "DB", "long", false, "GoogleReceiptData", "", "");
		super.addTable("bloodroles", "DB", "long", false, "BloodRole", "", "");
		super.addTable("lottylist", "DB", "long", false, "lotty", "", "");
		super.addTable("monthcardlist", "DB", "long", false, "monthcards", "", "");
		super.addTable("chargeactivities", "DB", "long", false, "ChargeActivityRole", "", "");
		super.addTable("gamelevels", "DB", "long", false, "GameLevel", "", "");
		super.addTable("roleonoffstate", "MEMORY", "long", false, "int", "", "");
		super.addTable("skillbag", "DB", "long", false, "Bag", "", "");
		super.addTable("collectbag", "DB", "long", false, "Bag", "", "");
		super.addTable("firstladderinforole", "DB", "int", false, "FirstLadderInfoRole", "", "");
		super.addTable("equipextdatas", "DB", "long", false, "EquipExtData", "", "");
		super.addTable("itemlimits", "DB", "long", false, "ItemNumLimit", "", "");
		super.addTable("friends", "DB", "long", false, "Friends", "", "");
		super.addTable("heroclones", "DB", "long", false, "heroclone", "", "");
		super.addTable("stageroles", "DB", "long", false, "StageRole", "", "");
		super.addTable("herocolumns", "DB", "long", false, "HeroColumn", "", "");
		super.addTable("maillist", "DB", "long", false, "Mails", "", "");
		super.addTable("bossranklists", "DB", "int", false, "bossRankList", "", "");
		super.addTable("equipbag", "DB", "long", false, "Bag", "", "");
		super.addTable("serverinfo", "DB", "int", false, "ServerInfo", "", "");
		super.addTable("appreceiptes", "DB", "long", false, "AppReceiptData", "", "");
		super.addTable("soulbag", "DB", "long", false, "Bag", "", "");
		super.addTable("endlesscolumns", "DB", "long", false, "EndlessInfo", "", "");
		super.addTable("bossdata", "DB", "int", false, "boss", "", "");
		super.addTable("shopbuycolumns", "DB", "long", false, "ShopbuyColumn", "", "");
		super.addTable("msgroles", "DB", "long", false, "MsgRole", "", "");
		super.addTable("duihuanlqs", "DB", "int", false, "duihuanlq", "", "");
		super.addTable("roledhmaps", "DB", "long", false, "roledhmap", "", "");
		super.addTable("firstfeedactivities", "DB", "long", false, "FirstFeedActivityRole", "", "");
		super.addTable("consumeactivities", "DB", "long", false, "ConsumeActivityRole", "", "");
		super.addTable("huoyuelist", "DB", "long", false, "huoyues", "", "");
		super.addTable("lotteryitemlist", "DB", "long", false, "LotteryItemAll", "", "");
		super.addTable("bloodranklist", "DB", "int", false, "BloodRankList", "", "");
		super.addTable("billroles", "DB", "long", false, "BillRole", "", "");
		super.addTable("heroskincolumns", "DB", "long", false, "HeroSkinColumn", "", "");
		super.addTable("equipcolumns", "DB", "long", false, "EquipColumn", "", "");
		super.addTable("newshopcolumns", "DB", "long", false, "NewShopMap", "", "");
		super.addTable("moheoddses", "DB", "long", false, "moheodds", "", "");
		super.addTable("friendreqs", "DB", "long", false, "FriendReqs", "", "");
		super.addTable("auuserinfo", "DB", "int", false, "AUUserInfo", "", "");
		super.addTable("stagetxalllist", "DB", "long", false, "stagetxall", "", "");
		super.addTable("endlessranklists", "DB", "int", false, "EndlessRankList", "", "");
		super.addTable("battles", "MEMORY", "long", false, "BattleInfo", "", "");
		super.addTable("artifactcolumns", "DB", "long", false, "ArtifactColumn", "", "");
		super.addTable("user", "DB", "int", false, "User", "", "");
		super.addTable("properties", "DB", "long", true, "Properties", "", "");
		super.addTable("skillextdatas", "DB", "long", false, "SkillExtData", "", "");
	}
}

