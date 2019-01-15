package chuhan.gsp.game;


public class SDianjiangGailv implements mytools.ConvMain.Checkable ,Comparable<SDianjiangGailv>{

	public int compareTo(SDianjiangGailv o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public SDianjiangGailv(){
		super();
	}
	public SDianjiangGailv(SDianjiangGailv arg){
		this.id=arg.id ;
		this.ppurple=arg.ppurple ;
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
	public int ppurple  = 0  ;
	
	public int getPpurple(){
		return this.ppurple;
	}
	
	public void setPpurple(int v){
		this.ppurple=v;
	}
	
	
};