package chuhan.gsp.gm;

import chuhan.gsp.battle.ResultType;
import chuhan.gsp.battle.SSendBattleScript;
import chuhan.gsp.battle.TargetDemo;

public class Cmd_testbattle extends GMCommand {
	@Override
	boolean exec(String[] args) {
		final long roleId = (args.length > 0)? Long.valueOf(args[0]) : getGmroleid();
		new xdb.Procedure()
		{
			@Override
			protected boolean process() throws Exception {
				
				SSendBattleScript ssript = new SSendBattleScript();
				ssript.hostspeed = 66;
				ssript.guestspeed = 55;
				chuhan.gsp.battle.BattleDemo round1demo1 = new chuhan.gsp.battle.BattleDemo();
				round1demo1.attacker = (byte)1;
				round1demo1.targets.add(new TargetDemo((byte)6, (short)-500, (byte)ResultType.RESULT_DEATH));
				ssript.round1.add(round1demo1);
				
				chuhan.gsp.battle.BattleDemo round1demo2 = new chuhan.gsp.battle.BattleDemo();
				round1demo2.attacker = (byte)7;
				round1demo2.targets.add(new TargetDemo((byte)2, (short)-99, (byte)0));
				ssript.round1.add(round1demo2);
				
				chuhan.gsp.battle.BattleDemo round1demo3 = new chuhan.gsp.battle.BattleDemo();
				round1demo3.attacker = (byte)3;
				round1demo3.targets.add(new TargetDemo((byte)8, (short)-500, (byte)ResultType.RESULT_DEATH));
				ssript.round1.add(round1demo3);
				
				chuhan.gsp.battle.BattleDemo round1demo4 = new chuhan.gsp.battle.BattleDemo();
				round1demo4.attacker = (byte)2;
				round1demo4.targets.add(new TargetDemo((byte)7, (short)-500, (byte)ResultType.RESULT_DEATH));
				ssript.round1.add(round1demo4);
				
				psendWhileCommit(roleId, ssript);
				return true;
			}
		}.submit();
		
		return true;
	}

	@Override
	String usage() {
		return "//testbattle";
	}

}