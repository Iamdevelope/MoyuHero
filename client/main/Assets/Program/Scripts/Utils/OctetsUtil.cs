using UnityEngine;
using System.Collections;
using System;
using System.Text;
using GNET;
using System.Globalization;

/**
 * octets相关的一些常用方法
 * 还包括将octets转成16进制字符串，简单压缩字符串的方法
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
	
	public static Octets toOctets(string value)
	{
		OctetsStream oct = new OctetsStream();
		oct.marshal(value,"UTF-16LE");
		return oct;
	}

    public static string compactOctetString(string str)
	{
		char c0 = '0';
		StringBuilder sb = new StringBuilder();
		byte c0count = 0;
		foreach(var c in str.ToCharArray())
		{
			if(c != c0)
			{
				if(c0count >= 0)
				{
					sb.Append(string.Format("%02X", c0count));
					c0count = 0;
				}
				sb.Append(c);
			}
			else
			{
				c0count++;
				if(c0count >= 15)
				{
                    sb.Append(string.Format("%02X", c0count));
					c0count = 0;
				}
			}
		}
		if(c0count >= 0)
			sb.Append(String.Format("%02X", c0count));
		return sb.ToString();
	}
    public static string uncompactOctetString(string str)
	{
		char c0 = '0';
		StringBuilder sb = new StringBuilder();
		Boolean isc0 = false;
		foreach(var c in str.ToCharArray()) 
		{
			if(isc0)
			{
				byte c0count = HexChar2Byte(c);
				for(byte i = 0 ; i < c0count;i ++ )
					sb.Append(c0);
				isc0 = false;
			}	
			else
			{
				sb.Append(c);
				isc0 = (c == c0);
			}
		}
		return sb.ToString();
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
            throw new MarshalException();
		}
	}

    public static Octets compactedString2Octets(string str)
	{
		return string2Octets(uncompactOctetString(str));
	}

    public static Octets string2Octets(string str)
	{
		OctetsStream os = new OctetsStream();
		byte[] bytes = string2Bytes(str);
		return os.insert(0,bytes);
	}
	
	public static byte[] string2Bytes(string str)
	{
		if(str.Length % 2 != 0)
            throw new MarshalException();
        byte[] bytes = new byte[str.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
		{
            bytes[i] = (byte)Int32.Parse(str.Substring(2 * i, 2 * i + 2), NumberStyles.AllowParentheses);
		}
		return bytes;
	}

    public static string octets2CompactedString(Octets oct)
	{
		return compactOctetString(octets2String(oct));
	}
    public static string octets2String(Octets oct)
	{
		StringBuilder sb = new StringBuilder();
		foreach(var b in oct.getBytes())
		{
			sb.Append(String.Format("%02X", b));
		}
		return sb.ToString();
	}
	
}
