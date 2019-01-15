package chuhan.gsp.buff;


public class AbstractBuff implements IBuff
{

	@Override
	public int getId()
	{
		return -1;
	}
	
	@Override
	public IBuff copy()
	{
		return null;
	}

	@Override
	public BuffResult attach(BuffAgent agent)
	{
		return null;
	}

}
