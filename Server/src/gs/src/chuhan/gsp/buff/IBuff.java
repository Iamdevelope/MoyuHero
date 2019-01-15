package chuhan.gsp.buff;


public interface IBuff
{
	public int getId();
	public IBuff copy();
	public BuffResult attach(BuffAgent agent);
}
