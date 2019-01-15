package chuhan.gsp.award;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Set;
import java.util.TreeSet;

import chuhan.gsp.game.sxitongjiangli;

public class CompenseConfig {
	public final int id;
	public final boolean enable;
	public final long starttime;
	public final long endtime;
	public final int msgid;
	public final String msgcontent;
	public final int awardid;
	public final Set<Long> roleids = new TreeSet<Long>();
	public final Set<String> accounts = new TreeSet<String>();
	public final Set<Integer> serverids = new TreeSet<Integer>();
	public final Set<String> plattypes = new TreeSet<String>();
	public final int minlevel;
	public final int maxlevel;
	public final int minviplv;
	public final int maxviplv;
	
	public CompenseConfig(sxitongjiangli scfg) throws ParseException
	{
		this.id = scfg.id;
		this.enable = (scfg.enable == 1);
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		this.starttime = sdf.parse(scfg.starttime).getTime();
		this.endtime = sdf.parse(scfg.endtime).getTime();
		this.msgid = scfg.msgid;
		this.msgcontent = scfg.msgtext;
		this.awardid = scfg.awardid;
		if(scfg.roleids != null)
		{
			String[] strs = scfg.roleids.split(";");
			for(String str : strs)
				roleids.add(Long.valueOf(str));
		}
		if(scfg.userid != null)
		{
			String[] strs = scfg.userid.split(";");
			for(String str : strs)
				accounts.add(str);
		}
		if(scfg.zoneid != null)
		{
			String[]strs = scfg.zoneid.split(";");
			for(String str : strs)
				serverids.add(Integer.valueOf(str));
		}
		if(scfg.plattype != null)
		{
			String[]strs = scfg.plattype.split(";");
			for(String str : strs)
				plattypes.add(str);
		}
		this.minlevel = scfg.levelmin;
		this.maxlevel = scfg.levelmax;
		this.minviplv = scfg.vipmin;
		this.maxviplv = scfg.vipmax;
	}
	
}
