package chuhan.gsp.util;

import java.io.ByteArrayOutputStream;
import java.io.UnsupportedEncodingException;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;

import com.goldhuman.Common.Octets;
import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;

/**
 * octets相关的一些常用方法
 * 还包括将octets转成16进制字符串，简单压缩字符串的方法
 * @author LiuChen
 *
 */
public class OctetsUtil
{
	public static Octets toOctets(int value)
	{
		OctetsStream oct = new OctetsStream();
		oct.marshal(value);
		return oct;
	}
	
	public static Octets toOctets(short value)
	{
		OctetsStream oct = new OctetsStream();
		oct.marshal(value);
		return oct;
	}
	
	public static Octets toOctets(byte value)
	{
		OctetsStream oct = new OctetsStream();
		oct.marshal(value);
		return oct;
	}
	
	public static Octets toOctets(long value)
	{
		OctetsStream oct = new OctetsStream();
		oct.marshal(value);
		return oct;
	}
	
	public static Octets toOctets(Marshal value)
	{
		return value.marshal(new OctetsStream());
	}
	
	public static Octets toOctets(String value)
	{
		OctetsStream oct = new OctetsStream();
		oct.marshal(value,"UTF-16LE");
		return oct;
	}
	
	public static Octets toLogOctets(String value)
	{
		try
		{
			return new Octets(value.getBytes("UTF-8"));
		} catch (UnsupportedEncodingException e)
		{
			e.printStackTrace();// 吞掉这个exception，因为"UTF-8"不能写错
		}
		return null;
	}
	
	public static String compactOctetString(String str)
	{
		char c0 = '0';
		StringBuilder sb = new StringBuilder();
		byte c0count = -1;
		for(char c : str.toCharArray())
		{
			if(c != c0)
			{
				if(c0count >= 0)
				{
					sb.append(String.format("%02X", c0count));
					c0count = -1;
				}
				sb.append(c);
			}
			else
			{
				c0count++;
				if(c0count >= 15)
				{
					sb.append(String.format("%02X", c0count));
					c0count = -1;
				}
			}
		}
		if(c0count >= 0)
			sb.append(String.format("%02X", c0count));
		return sb.toString();
	}
	public static String uncompactOctetString(String str)
	{
		char c0 = '0';
		StringBuilder sb = new StringBuilder();
		boolean isc0 = false;
		for(char c : str.toCharArray())
		{
			if(isc0)
			{
				byte c0count = HexChar2Byte(c);
				for(byte i = 0 ; i < c0count;i ++ )
					sb.append(c0);
				isc0 = false;
			}	
			else
			{
				sb.append(c);
				isc0 = (c == c0);
			}
		}
		return sb.toString();
	}
	
	public static byte HexChar2Byte(char c)
	{
		switch(c)
		{
		case '0':
			return 0;
		case '1':
			return 1;
		case '2':
			return 2;
		case '3':
			return 3;
		case '4':
			return 4;
		case '5':
			return 5;
		case '6':
			return 6;
		case '7':
			return 7;
		case '8':
			return 8;
		case '9':
			return 9;
		case 'A':
		case 'a':
			return 10;
		case 'B':
		case 'b':
			return 11;
		case 'C':
		case 'c':
			return 12;
		case 'D':
		case 'd':
			return 13;
		case 'E':
		case 'e':
			return 14;
		case 'F':
		case 'f':
			return 15;
		default:
			throw new RuntimeException("Unsupported HexChar : " + c);
		}
	}
	
	public static Octets compactedString2Octets(String str)
	{
		return string2Octets(uncompactOctetString(str));
	}
	
	public static Octets string2Octets(String str)
	{
		OctetsStream os = new OctetsStream();
		byte[] bytes = string2Bytes(str);
		return os.insert(0,bytes);
	}
	
	public static byte[] string2Bytes(String str)
	{
		if(str.length() % 2 != 0)
			throw new IllegalArgumentException("illegal octets string");
		byte[] bytes = new byte[str.length()/2];
		for(int i = 0 ; i < bytes.length; i++)
		{
			bytes[i] = (byte)Integer.parseInt(str.substring(2*i,2*i+2), 16);
		}
		return bytes;
	}
	
	public static String octets2CompactedString(Octets oct)
	{
		return compactOctetString(octets2String(oct));
	}
	public static String octets2String(Octets oct)
	{
		StringBuilder sb = new StringBuilder();
		for(byte b : oct.getBytes())
		{
			sb.append(String.format("%02X", b));
		}
		return sb.toString();
	}
	
	/**
	 * 利用javazip对bytes进行压缩
	 * 实际运用中，如果bytes过短，压缩完反而会更长
	 * @param data
	 * @return
	 */
	public static byte[] zip(byte[] data)
	{
		byte[] bytes = null;
		try{
			ByteArrayOutputStream bos = new ByteArrayOutputStream();
			ZipOutputStream zip = new ZipOutputStream(bos);
			ZipEntry entry = new ZipEntry("zip");
			entry.setSize(data.length);
			zip.putNextEntry(entry);
			zip.write(data);
			zip.closeEntry();
			zip.close();
			bytes = bos.toByteArray();
			bos.close();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
		return bytes;
	}
	
	public static void main(String[] args)
	{
	}
}
