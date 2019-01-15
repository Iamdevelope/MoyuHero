package chuhan.gsp.gm;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.LinkedList;
import java.util.List;

import xdb.TTable.IWalk;
import chuhan.gsp.main.GameTime;

public class Cmd_countrole extends GMCommand {
	@Override
	boolean exec(String[] args) {
		long start = GameTime.currentTimeMillis();
		final List<Long> roleids = new LinkedList<Long>();
		xtable.Properties.getTable().walk(new IWalk<Long, xbean.Properties>() {
			@Override
			public boolean onRecord(Long k, xbean.Properties v) {
				roleids.add(k);
				return true;
			}
		});
		File file = new File("rolelevel.txt");
		if(file.exists())
			file.delete();
		try {
			file.createNewFile();
			FileWriter filewriter = new FileWriter(file);
			GMCommand.logger.info("开始统计"+roleids.size()+"人");
			for(final long roleid : roleids)
			{
				Integer lv = xtable.Properties.selectLevel(roleid);
				if(lv == null)
					continue;
				filewriter.write(roleid+","+lv+"\n");
				filewriter.flush();
			}
			filewriter.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
		long end = GameTime.currentTimeMillis();
		long second = (end - start)/1000;
		String str = "共统计"+roleids.size()+"人，耗时："+second+"秒";
		sendToGM(str);
		GMCommand.logger.info(str);
		return true;
	}

	@Override
	String usage() {
		return "//addyball [addnumber] 给所有人发元宝";
	}

}