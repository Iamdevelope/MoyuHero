package chuhan.gsp.util;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.util.Map;
import java.util.TreeMap;

public class PropProcess {

	
	public static void main(String[] args) {
		
		Map<Integer,Integer> macusers = new TreeMap<Integer, Integer>();
		
		File file = new File("oldkey2newkey.properties");
		FileReader reader;
		try {
			reader = new FileReader(file);
			BufferedReader buffedreader = new BufferedReader(reader);
			String line = buffedreader.readLine();
			int i = 0;
			try {
			while(line!=null)
			{
				if(!line.contains("="))
				{
					line = buffedreader.readLine();
					continue;
				}
				String[] strs = line.split("=");
				int oldid = Long.valueOf(strs[0]).intValue();
				int newid = Long.valueOf(strs[1]).intValue();
				macusers.put(oldid, newid);
				line = buffedreader.readLine();
				i++;
			}
			}
			catch(Exception e)
			{
				e.printStackTrace();
			}
			reader.close();
			System.out.println("total line = "+i);
			
			File writefile = new File("change.properties");
			if(writefile.exists())
				writefile.delete();
			FileWriter fwriter = new FileWriter(writefile);
			int j = 0;
			for(Map.Entry<Integer, Integer> user : macusers.entrySet())
			{
				StringBuffer sb = new StringBuffer().append(user.getKey()/4096).append("=").append(user.getValue()/4096).append("\n");
				fwriter.write(sb.toString());
				fwriter.flush();
				j++;
			}
			System.out.println("total user = " + j);
			fwriter.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
