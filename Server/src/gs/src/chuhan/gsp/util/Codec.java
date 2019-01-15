package chuhan.gsp.util;

public abstract class Codec
{
	public abstract void update(byte[] data, int off, int len);

	public abstract void flush();

	public void update(byte[] data)
	{
		update(data, 0, data.length);
	}
}
