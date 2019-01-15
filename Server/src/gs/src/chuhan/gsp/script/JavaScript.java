package chuhan.gsp.script;

import javax.script.ScriptException;

import chuhan.gsp.util.Parser;

public class JavaScript
{
	
	private String js;
	
	public JavaScript(String js)
	{
		this.js = "with(Math){" +js + "}";
	}
	
	public Double eval(IJavaScriptEngine engine)
	{
		try
		{
			Object o = engine.eval(js);
			if(o == null) return null;
			if(o instanceof Double)
				return (Double)o;
			if(o instanceof Integer)
				return ((Integer)o).doubleValue();
			else throw new ScriptException("Unsupported Type: "+o.getClass().toString());
		} catch (ScriptException e)
		{
			AbstractJSEngine.logger.error("JS脚本"+ js+"有错：/n"+ Parser.convertStackTrace2String(e));
			return 0.0;
		}
	}
	public Boolean evalToBoolean(IJavaScriptEngine engine)
	{
		try
		{
			Object o = engine.eval(js);
			if(o == null) return null;
			if(o instanceof Boolean)
				return (Boolean)o;
			else throw new ScriptException("Unsupported Type: "+o.getClass().toString());
		} catch (ScriptException e)
		{
			AbstractJSEngine.logger.error("JS脚本"+ js+"有错：/n"+ Parser.convertStackTrace2String(e));
			return null;
		}
	}
	
	public Double eval(javax.script.ScriptEngine engine)
	{
		try
		{
			Object o = engine.eval(js);
			if(o == null) return null;
			if(o instanceof Double)
				return (Double)o;
			if(o instanceof Integer)
				return ((Integer)o).doubleValue();
			else throw new ScriptException("Unsupported Type: "+o.getClass().toString());
		} catch (ScriptException e)
		{
			AbstractJSEngine.logger.error("JS脚本"+ js+"有错：/n"+ Parser.convertStackTrace2String(e));
			return 0.0;
		}
	}
	
	public String getJavaScriptString()
	{
		return js;
	}
	
	@Override
	public String toString()
	{
		return js;
	}
	
	public String getOringinString()
	{
		return this.js.substring(11, this.js.length() -1 );
	}
	
	/**
	 * 在原有公式的基础上后面追加一些片段，例如原来是"i/2",追加 "+2",变为"i/2+2"
	 * @param episode
	 */
	public JavaScript append(String episode)
	{
		StringBuffer sb = new StringBuffer(js);
		sb.insert(sb.length() - 1, episode);
		js = sb.toString();
		return this;
	}

	
}
