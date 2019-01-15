package chuhan.gsp.hero;

import java.util.HashSet;
import java.util.Set;

public class TroopRelation
{
	public Set<Integer> targets = new HashSet<Integer>();
	public int effectId;
	public float effectvalue;
	public boolean init(String relationstr)
	{
		try
		{
			String[] strs = relationstr.split("@");
			effectId = Integer.valueOf(strs[0]);
			if(effectId % 10 == 2)
				effectvalue = Float.valueOf(strs[1]) / 100;
			for(int i = 2 ; i <strs.length;i++)
				targets.add(Integer.valueOf(strs[i]));
		}catch(Exception e)
		{
			e.printStackTrace();
			return false;
		}
		return true;
	}
	
}
