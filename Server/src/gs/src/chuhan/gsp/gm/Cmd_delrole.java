package chuhan.gsp.gm;

import java.util.Collection;

import chuhan.gsp.KickErrConst;
import chuhan.gsp.attr.PropRole;
import xdb.Table;

public class Cmd_delrole extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 0){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		long roleid = getGmroleid();
		
		final long rid = roleid;
		new xdb.Procedure()
		{
			protected boolean process() throws Exception {
				gnet.link.Onlines.getInstance().kick(rid,KickErrConst.ERR_GM_KICKOUT);//踢下线,TODO kick error code
/*				xtable.Herocolumns.remove(rid);
				xtable.Herotroops.remove(rid);
				xtable.Bag.remove(rid);
				xtable.Tujianheros.remove(rid);
				xtable.Gamelevels.remove(rid);
				xtable.Friends.remove(rid);
				xtable.Stageroles.remove(rid);
				xtable.Maillist.remove(rid);
				xtable.Equipcolumns.remove(rid);
				xtable.Realtimeroles.remove(rid);
				xtable.Battles.remove(rid);
				xtable.Heroskincolumns.remove(rid);
				xtable.Artifactcolumns.remove(rid);
				xtable.Shopbuycolumns.remove(rid);
				xtable.Endlesscolumns.remove(rid);*/
				xbean.Properties prop = xtable.Properties.select(rid);
				if(prop != null){
					xtable.Auuserinfo.delete(prop.getUserid());
					xtable.User.delete(prop.getUserid());
				}
//				xtable.Properties.remove(rid);
//				System.out.println("true_in");
				return true;
			};
		}.submit();
//		System.out.println("true_out");
		return true;
	}

	@Override
	String usage() {
		return "//addexp [addnumber] [account不填是自己]";
	}

}