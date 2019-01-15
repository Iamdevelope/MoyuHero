package chuhan.gsp.gm;

import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.ExecutionException;

import xdb.TTable.IWalk;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.attr.YuanBaoAddType;
import chuhan.gsp.main.GameTime;

public class Cmd_addyball extends GMCommand {
	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		
		final int yuanbao = Integer.valueOf(args[0]);
		if (yuanbao == 0){
			sendToGM("参数格式错误:" + usage());
			return false;
		}
		long start = GameTime.currentTimeMillis();
		final List<Long> roleids = new LinkedList<Long>();
		xtable.Properties.getTable().walk(new IWalk<Long, xbean.Properties>() {
			@Override
			public boolean onRecord(Long k, xbean.Properties v) {
				roleids.add(k);
				return true;
			}
		});
		GMCommand.logger.info("开始给"+roleids.size()+"人添加元宝");
		for(final long roleid : roleids)
		{
			try {
				new xdb.Procedure(){
					protected boolean process() throws Exception {
						PropRole prole = PropRole.getPropRole(roleid, false);
						prole.addYuanBao(yuanbao, YuanBaoAddType.GM_COMMAND);
						return true;
					};
				}.submit().get();//顺序一个个加
			} catch (InterruptedException e) {
				e.printStackTrace();
			} catch (ExecutionException e) {
				e.printStackTrace();
			}
		}
		long end = GameTime.currentTimeMillis();
		long second = (end - start)/1000;
		String str = "共计给"+roleids.size()+"人添加"+yuanbao+"元宝，耗时："+second+"秒";
		sendToGM(str);
		GMCommand.logger.info(str);
		return true;
	}

	@Override
	String usage() {
		return "//addyball [addnumber] 给所有人发元宝";
	}

}