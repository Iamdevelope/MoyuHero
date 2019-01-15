package chuhan.gsp.item;


public class cherolinkconfig implements mytools.ConvMain.Checkable ,Comparable<cherolinkconfig>{

	public int compareTo(cherolinkconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cherolinkconfig(){
		super();
	}
	public cherolinkconfig(cherolinkconfig arg){
		this.id=arg.id ;
		this.heroid=arg.heroid ;
		this.linkid=arg.linkid ;
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
	public int heroid  = 0  ;
	
	public int getHeroid(){
		return this.heroid;
	}
	
	public void setHeroid(int v){
		this.heroid=v;
	}
	
	/**
	 * 
	 */
	public int linkid  = 0  ;
	
	public int getLinkid(){
		return this.linkid;
	}
	
	public void setLinkid(int v){
		this.linkid=v;
	}
	
	
};