package chuhan.gsp.util;

public class DecodeBase64 extends Codec
{
	private static final byte[] DECODE = new byte[Byte.MAX_VALUE];
	static
	{
		byte[] ENCODE = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/' };
		for (byte i = 0; i < Byte.MAX_VALUE; i++)
			DECODE[i] = Byte.MAX_VALUE;
		for (byte i = 0; i < ENCODE.length; i++)
			DECODE[ENCODE[i]] = i;
		DECODE['='] = Byte.MIN_VALUE;
	}

	private final Codec sink;
	private int b0;
	private int b1;
	private int b2;
	private int b3;
	private int n;

	public DecodeBase64(Codec sink)
	{
		this.sink = sink;
	}

	private int update0(byte[] r, int j, byte[] data, int off, int len) throws CodecException
	{
		for (n = len; n > 7; n -= 4)
		{
			b0 = DECODE[data[off++] & 0xff];
			b1 = DECODE[data[off++] & 0xff];
			b2 = DECODE[data[off++] & 0xff];
			b3 = DECODE[data[off++] & 0xff];
			if (b0 == Byte.MAX_VALUE || b1 == Byte.MAX_VALUE || b2 == Byte.MAX_VALUE || b3 == Byte.MAX_VALUE)
				throw new CodecException("bad base64 char");
			r[j++] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
			r[j++] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
			r[j++] = (byte) (b2 << 6 & 0xc0 | b3 & 0x3f);
		}
		if (n > 3)
		{
			n -= 4;
			b0 = DECODE[data[off++] & 0xff];
			b1 = DECODE[data[off++] & 0xff];
			b2 = DECODE[data[off++] & 0xff];
			b3 = DECODE[data[off++] & 0xff];
			if (b0 == Byte.MAX_VALUE || b1 == Byte.MAX_VALUE || b2 == Byte.MAX_VALUE || b3 == Byte.MAX_VALUE)
				throw new CodecException("bad base64 char");
			if (b2 == Byte.MIN_VALUE)
			{
				r[j++] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
				return j;
			}
			else if (b3 == Byte.MIN_VALUE)
			{
				r[j++] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
				r[j++] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
				return j;
			}
			r[j++] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
			r[j++] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
			r[j++] = (byte) (b2 << 6 & 0xc0 | b3 & 0x3f);
		}
		if (n == 1)
		{
			b0 = DECODE[data[off] & 0xff];
		}
		else if (n == 2)
		{
			b0 = DECODE[data[off] & 0xff];
			b1 = DECODE[data[off + 1] & 0xff];
		}
		else if (n == 3)
		{
			b0 = DECODE[data[off] & 0xff];
			b1 = DECODE[data[off + 1] & 0xff];
			b2 = DECODE[data[off + 2] & 0xff];
		}
		return j;
	}

	private int update1(byte[] r, byte[] data, int off, int len) throws CodecException
	{
		switch (len)
		{
		case 0:
			return 0;
		case 1:
			b1 = DECODE[data[off] & 0xff];
			n = 2;
			return 0;
		case 2:
			b1 = DECODE[data[off] & 0xff];
			b2 = DECODE[data[off + 1] & 0xff];
			n = 3;
			return 0;
		}
		b1 = DECODE[data[off] & 0xff];
		b2 = DECODE[data[off + 1] & 0xff];
		b3 = DECODE[data[off + 2] & 0xff];
		if (b0 == Byte.MAX_VALUE || b1 == Byte.MAX_VALUE || b2 == Byte.MAX_VALUE || b3 == Byte.MAX_VALUE)
			throw new CodecException("bad base64 char");
		if (b2 == Byte.MIN_VALUE)
		{
			r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
			return 1;
		}
		else if (b3 == Byte.MIN_VALUE)
		{
			r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
			r[1] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
			return 2;
		}
		r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
		r[1] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
		r[2] = (byte) (b2 << 6 & 0xc0 | b3 & 0x3f);
		return update0(r, 3, data, off + 3, len - 3);
	}

	private int update2(byte[] r, byte[] data, int off, int len) throws CodecException
	{
		switch (len)
		{
		case 0:
			return 0;
		case 1:
			b2 = DECODE[data[off] & 0xff];
			n = 3;
			return 0;
		}
		b2 = DECODE[data[off] & 0xff];
		b3 = DECODE[data[off + 1] & 0xff];
		if (b0 == Byte.MAX_VALUE || b1 == Byte.MAX_VALUE || b2 == Byte.MAX_VALUE || b3 == Byte.MAX_VALUE)
			throw new CodecException("bad base64 char");
		if (b2 == Byte.MIN_VALUE)
		{
			r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
			return 1;
		}
		else if (b3 == Byte.MIN_VALUE)
		{
			r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
			r[1] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
			return 2;
		}
		r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
		r[1] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
		r[2] = (byte) (b2 << 6 & 0xc0 | b3 & 0x3f);
		return update0(r, 3, data, off + 2, len - 2);
	}

	private int update3(byte[] r, byte[] data, int off, int len) throws CodecException
	{
		if (len == 0)
			return 0;
		b3 = DECODE[data[off] & 0xff];
		if (b0 == Byte.MAX_VALUE || b1 == Byte.MAX_VALUE || b2 == Byte.MAX_VALUE || b3 == Byte.MAX_VALUE)
			throw new CodecException("bad base64 char");
		if (b2 == Byte.MIN_VALUE)
		{
			r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
			return 1;
		}
		else if (b3 == Byte.MIN_VALUE)
		{
			r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
			r[1] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
			return 2;
		}
		r[0] = (byte) (b0 << 2 & 0xfc | b1 >> 4 & 0x3);
		r[1] = (byte) (b1 << 4 & 0xf0 | b2 >> 2 & 0xf);
		r[2] = (byte) (b2 << 6 & 0xc0 | b3 & 0x3f);
		return update0(r, 3, data, off + 1, len - 1);
	}

	private int update(byte[] r, byte[] data, int off, int len) throws CodecException
	{
		switch (n)
		{
		case 0:
			return update0(r, 0, data, off, len);
		case 1:
			return update1(r, data, off, len);
		case 2:
			return update2(r, data, off, len);
		}
		return update3(r, data, off, len);
	}

	@Override
	public void update(byte[] data, int off, int len)
	{
		int length = (n + len) / 4 * 3;
		byte[] r = new byte[length];
		try {
			sink.update(r, 0, Math.min(update(r, data, off, len), length));
		} catch (CodecException e) {
			xdb.Trace.error(e.getMessage(), e);
		}
	}

	@Override
	public void flush()
	{
		sink.flush();
		n = 0;
	}

	public static byte[] transform(byte[] data)
	{
		int len = data.length / 4 * 3;
		if (data[data.length - 1] == '=')
			len--;
		if (data[data.length - 2] == '=')
			len--;
		byte[] r = new byte[len];
		try {
			new DecodeBase64(null).update(r, data, 0, data.length);
		} catch (CodecException e) {
			xdb.Trace.error(e.getMessage(), e);
		}
		return r;
	}
}
