package chuhan.gsp.gm;

public class Cmd_printladderrank extends GMCommand {

	@Override
	boolean exec(String[] args) {
		if(args.length < 1) {
			sendToGM(usage());
			return false;
		}
		int rank = Integer.valueOf(args[0]);
		xbean.LadderInfo ladderInfo = xtable.Pvpladder.select(rank);
		if(null == ladderInfo) {
			sendToGM("该位置没有人");
			return false;
		}
		sendToGM("roleId:" + ladderInfo.getRoleid());
		return true;
	}

	@Override
	String usage() {
		return "//printladderrank 名次";
	}

}
