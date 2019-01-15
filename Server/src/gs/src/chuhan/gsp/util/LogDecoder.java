package chuhan.gsp.util;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;

public class LogDecoder {

	public static int line_num = 104002; 
	private static class MacUser
	{
		String mac;
		Set<String> usernames = new HashSet<String>();
	}
	
	public static void main(String[] args) {
		
		Map<String,MacUser> macusers = new TreeMap<String, LogDecoder.MacUser>();
		
		File file = new File("rolelogin.log");
		FileReader reader;
		try {
			reader = new FileReader(file);
			BufferedReader buffedreader = new BufferedReader(reader);
			String line = buffedreader.readLine();
			int i = 0;
			try {
			while(line!=null)
			{
				i++;
				boolean decode = i>=line_num;
				
				if(i - line_num <1 && i-line_num >-1)
				{
					System.out.println("break");
				}
				
				MacUser newuser = parseMacUser(line, decode);
				if(newuser.mac!=null && !newuser.usernames.isEmpty())
				{
					MacUser olduser = macusers.get(newuser.mac);
					if(olduser == null)
						macusers.put(newuser.mac, newuser);
					else
						olduser.usernames.addAll(newuser.usernames);
				}
				line = buffedreader.readLine();
			}
			}
			catch(Exception e)
			{
				e.printStackTrace();
			}
			reader.close();
			System.out.println("total line = "+i);
			
			File writefile = new File("macuser.txt");
			if(writefile.exists())
				writefile.delete();
			FileWriter fwriter = new FileWriter(writefile);
			int j = 0;
			for(MacUser user : macusers.values())
			{
				StringBuffer sb = new StringBuffer(user.mac).append(";");
				for(String uname : user.usernames)
				{
					sb.append(uname).append(";");
				}
				sb.append("\n");
				fwriter.write(sb.toString());
				fwriter.flush();
				j++;
			}
			System.out.println("total mac = " + j);
			fwriter.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	static String mac_string = "mac=";
	static String account_string = "account=";
	static MacUser parseMacUser(String line, boolean decode)
	{
		MacUser macuser = new MacUser();
		
		String[] strs = line.split(":");
		for(String str :strs)
		{
			if(str.contains("mac="))
			{
				macuser.mac = str.substring(4);
			}
			else if(str.contains("account="))
			{
				String username = str.substring(8);
				if(decode)
				{
					username = new String(DecodeBase64.transform(username.getBytes()));
				}
				macuser.usernames.add(username);
			}
		}
		
		return macuser;
	}
	
}
