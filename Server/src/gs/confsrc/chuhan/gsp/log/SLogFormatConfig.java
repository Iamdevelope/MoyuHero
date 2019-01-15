package chuhan.gsp.log;


public class SLogFormatConfig implements mytools.ConvMain.Checkable ,Comparable<SLogFormatConfig>{

	public int compareTo(SLogFormatConfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SLogFormatConfig(){
		super();
	}
	public SLogFormatConfig(SLogFormatConfig arg){
		this.id=arg.id ;
		this.format=arg.format ;
		this.type=arg.type ;
	}
	public void checkValid(java.util.Map<String,java.util.Map<Integer,? extends Object> > objs){
	}
	/**
	 * 日志的id
	 */
	public int id  = 0  ;
	
	public int getId(){
		return this.id;
	}
	
	public void setId(int v){
		this.id=v;
	}
	
	/**
	 * 日志的格式
	 */
	public String format  = null  ;
	
	public String getFormat(){
		return this.format;
	}
	
	public void setFormat(String v){
		this.format=v;
	}
	
	/**
	 * 日志的类型
	 */
	public int type  = 0  ;
	
	public int getType(){
		return this.type;
	}
	
	public void setType(int v){
		this.type=v;
	}
	
	
};