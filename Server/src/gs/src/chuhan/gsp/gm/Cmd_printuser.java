package chuhan.gsp.gm;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.LinkedList;
import java.util.List;

import xbean.AUUserInfo;
import xdb.TTable.IWalk;
import chuhan.gsp.main.GameTime;

public class Cmd_printuser extends GMCommand {
	@Override
	boolean exec(String[] args) {
		long start = GameTime.currentTimeMillis();
		final List<Integer> userids = new LinkedList<Integer>();
		xtable.Auuserinfo.getTable().walk(new IWalk<Integer, xbean.AUUserInfo>() {
			@Override
			public boolean onRecord(Integer k, AUUserInfo v) {
				userids.add(k);
				return true;
			}
			
		});
		File file = new File("users.txt");
		if(file.exists())
			file.delete();
		try {
			file.createNewFile();
			FileWriter filewriter = new FileWriter(file);
			GMCommand.logger.info("开始统计"+userids.size()+"账户");
			for(final int userid : userids)
			{
				xbean.AUUserInfo xuserinfo = xtable.Auuserinfo.select(userid);
				if(xuserinfo == null)
					continue;
				try
				{
				xbean.User xuser = xtable.User.select(userid);
				if(xuser == null || xuser.getIdlist().isEmpty())
					continue;
				long roleId =  xuser.getIdlist().get(0);
				xbean.Properties xprop = xtable.Properties.select(roleId);
				if(xprop == null)
					continue;
				if(xprop.getLevel() < 30)
					continue;
				filewriter.write(xuserinfo.getUsername()+";");
				filewriter.flush();
				}
				catch(xio.MarshalError e)
				{
					GMCommand.logger.error("USERNAME="+xuserinfo.getUsername()+",");
					//e.printStackTrace();
				}
			}
			filewriter.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
		long end = GameTime.currentTimeMillis();
		long second = (end - start)/1000;
		String str = "共统计"+userids.size()+"账户，耗时："+second+"秒";
		sendToGM(str);
		GMCommand.logger.info(str);
		return true;
	}

	@Override
	String usage() {
		return "//printuser";
	}

}