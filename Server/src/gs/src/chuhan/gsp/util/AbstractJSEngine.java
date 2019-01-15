package chuhan.gsp.util;

import javax.script.ScriptException;

import chuhan.gsp.log.Logger;

public class AbstractJSEngine
{

	static Logger logger = Logger.getLogger(AbstractJSEngine.class);
	
	protected final javax.script.ScriptEngine engine;
	
	public AbstractJSEngine()
	{
		javax.script.ScriptEngineManager em = new javax.script.ScriptEngineManager();
		engine = em.getEngineByName("JavaScript");
	}
	
	public Double getDouble(String arg)
	{
		Object o = engine.get(arg);
		if(o == null) return null;
		if(o instanceof Double)
			return (Double)o;
		if(o instanceof Integer)
			return ((Integer)o).doubleValue();
		return null;
	}
	public Object get(String arg)
	{
		return engine.get(arg);
	}
	public Object eval(String js) throws ScriptException
	{
		return engine.eval(js);
	}
	public Object evalWithMath(String js) throws ScriptException
	{
		return engine.eval("with(Math){" + js + "}");
	}
	public Double evalToDouble(String js)
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
			logger.error("JS脚本"+ js+"有错：/n"+ e.toString());
			return 0.0;
		}
	}
	public javax.script.ScriptEngine getEngine()
	{
		return engine;
	}
	public void put(String key, Object value)
	{
		engine.put(key, value);
	}

}
