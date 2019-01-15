package chuhan.gsp;

import java.io.File;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Properties;

public class PChangeUserID extends xdb.Procedure{
	
	
	@Override
	protected boolean process() throws Exception {
		Properties changeprop = chuhan.gsp.main.ConfigManager.getInstance().getPropConf("change");
		if(changeprop == null)
			return true;
		Map<Integer,Integer> userchangemap = new HashMap<Integer, Integer>();
		for(Entry<Object,Object> entry : changeprop.entrySet())
		{
			int olduserid = Integer.valueOf((String)(entry.getKey()));
			int newuserid = Integer.valueOf((String)(entry.getValue()));
			
			userchangemap.put(olduserid, newuserid);
		}
		int i = 0;
		for(Map.Entry<Integer, Integer> entry : userchangemap.entrySet())
		{	
			int olduserid = entry.getKey();
			int newuserid = entry.getValue();
			xbean.User xuser = xtable.User.get(olduserid);
			if(xuser != null)
			{
				i++;
				xtable.User.remove(olduserid);
				xtable.User.add(newuserid, xuser);
				for(long roleid : xuser.getIdlist())
				{
					xbean.Properties xprop = xtable.Properties.get(roleid);
					if(xprop != null)
						xprop.setUserid(newuserid);
				}
			}
			xbean.AUUserInfo xauuser = xtable.Auuserinfo.get(olduserid);
			if(xauuser != null)
			{
				xtable.Auuserinfo.remove(olduserid);
				xtable.Auuserinfo.add(newuserid, xauuser);
			}
		}
		System.out.println("move user:"+i);
		File file = new File("properties/change.properties");
		if(file.exists())
			file.delete();
		return true;
	}
}
