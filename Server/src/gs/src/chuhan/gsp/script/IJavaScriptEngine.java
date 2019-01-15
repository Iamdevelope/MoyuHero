package chuhan.gsp.script;

import javax.script.ScriptException;

public interface IJavaScriptEngine
{
	public Double getDouble(String arg);
	
	public Object get(String arg);
	
	public Object eval(String js) throws ScriptException;
	
	public Object evalWithMath(String js) throws ScriptException;
	
	public Double evalToDouble(String js);
	
	public javax.script.ScriptEngine getEngine();
	
	public void put(String key, Object value);
}
