package chuhan.gsp.util;

import java.net.InetAddress;
import java.net.UnknownHostException;

/**
 * ip地址int/string的转换
 * @author LiuChen
 *
 */
public class InetAddressUtil
{
	public static int bytes2Int(byte[] bytes)
	{
		int i = 0;
		if(bytes != null)
		{
			if(bytes.length == 4)
			{
				i = bytes[0] & 0xFF;
				i |= ((bytes[1] << 8)& 0xFF00);
				i |= ((bytes[2] << 16)& 0xFF0000);
				i |= ((bytes[3] << 24)& 0xFF000000);
			}
		}
		return i;
	}
	public static byte[] int2Bytes(int i)
	{
		byte[] bytes = new byte[4];
		bytes[3] = (byte)((i >>> 24) & 0xFF);
		bytes[2] = (byte)((i >>> 16) & 0xFF);
		bytes[1] = (byte)((i >>> 8) & 0xFF);
		bytes[0] = (byte)(i & 0xFF);
		return bytes;
	}
	
	/**
	 * int类型的ip转化为类似192.168.0.1的地址
	 * @param intip
	 * @return
	 */
	public static String ipInt2String(int intip)
	{
		byte[] bytes = int2Bytes(intip);
		InetAddress addr = null;
		try
		{
			addr = InetAddress.getByAddress(bytes);
		} catch (UnknownHostException e)
		{
			e.printStackTrace();
			return "ErrorIntIp="+intip;
		}
		return addr.getHostAddress();
	}
	
}
