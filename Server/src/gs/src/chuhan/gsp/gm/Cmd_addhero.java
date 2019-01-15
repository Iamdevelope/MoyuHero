package chuhan.gsp.gm;

import java.util.concurrent.ExecutionException;

import chuhan.gsp.hero.PAddHero;

public class Cmd_addhero extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int heroId = Integer.valueOf(args[0]);
		if (heroId == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		int num = 1;
		if(args.length > 1){
			num = Integer.valueOf(args[1]);
			if(num > 20)
			{
				sendToGM("最多加20个英雄");
				return false;
			}
		}
		
		long roleid = getGmroleid();
		if(args.length > 2)
		{
			roleid = GMInterface.getTargetRoleId(args[2]);
			if(roleid <= 0)
				roleid = getGmroleid();
		}
		for(int i = 0 ; i < num; i++)
		{
			final PAddHero addProc = new PAddHero(roleid, heroId,1);
			try {
				addProc.submit().get();
			} catch (InterruptedException e) {
				e.printStackTrace();
			} catch (ExecutionException e) {
				e.printStackTrace();
			}
		}
		return true;
	}

	@Override
	String usage() {
		return "//addhero [heroid] [num:不填是1] [account不填是自己]";
	}

}