package chuhan.gsp.item;


public class normaldrop15 implements mytools.ConvMain.Checkable ,Comparable<normaldrop15>{

	public int compareTo(normaldrop15 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public normaldrop15(){
		super();
	}
	public normaldrop15(normaldrop15 arg){
		this.id=arg.id ;
		this.normaldroptype=arg.normaldroptype ;
		this.normaldroptime=arg.normaldroptime ;
		this.innerdrop=arg.innerdrop ;
		this.innerdropprob=arg.innerdropprob ;
	}
	public void checkValid(java.util.Map<String,java.util.Map<Integer,? extends Object> > objs){
	}
	/**
	 * 
	 */
	public int id  = 0  ;
	
	public int getId(){
		return this.id;
	}
	
	public void setId(int v){
		this.id=v;
	}
	
	/**
	 * 
	 */
	public int normaldroptype  = 0  ;
	
	public int getNormaldroptype(){
		return this.normaldroptype;
	}
	
	public void setNormaldroptype(int v){
		this.normaldroptype=v;
	}
	
	/**
	 * 
	 */
	public int normaldroptime  = 0  ;
	
	public int getNormaldroptime(){
		return this.normaldroptime;
	}
	
	public void setNormaldroptime(int v){
		this.normaldroptime=v;
	}
	
	/**
	 * 
	 */
	public String innerdrop  = null  ;
	
	public String getInnerdrop(){
		return this.innerdrop;
	}
	
	public void setInnerdrop(String v){
		this.innerdrop=v;
	}
	
	/**
	 * 
	 */
	public String innerdropprob  = null  ;
	
	public String getInnerdropprob(){
		return this.innerdropprob;
	}
	
	public void setInnerdropprob(String v){
		this.innerdropprob=v;
	}
	
	
};