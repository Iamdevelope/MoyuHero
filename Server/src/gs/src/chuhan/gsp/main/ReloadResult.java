package chuhan.gsp.main;

public class ReloadResult
{
	private boolean success;
	
	private StringBuffer msg = new StringBuffer("");

	public ReloadResult(boolean success)
	{
		this.success = success;
	}
	public ReloadResult(boolean success, String msg)
	{
		this.success = success;
		this.msg.append(msg);
	}

	public String getMsg()
	{
		return msg.toString();
	}

	public void setMsg(String msg)
	{
		this.msg = new StringBuffer(msg);
	}
	
	public ReloadResult appendMsg(String msg)
	{
		this.msg.append(msg);
		return this;
	}
	
	/**
	 * 扩展时，msg会append,success两者与
	 * @param result
	 * @return
	 */
	public ReloadResult appendResult(ReloadResult result)
	{
		this.msg.append(result.getMsg());
		this.success = this.success && result.isSuccess();
		return this;
	}

	public boolean isSuccess()
	{
		return success;
	}
	public void setSuccess(boolean isSucc)
	{
		this.success = isSucc; 
	}
	
}
