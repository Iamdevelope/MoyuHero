package chuhan.gsp.item;


public class heroupgradexp68 implements mytools.ConvMain.Checkable ,Comparable<heroupgradexp68>{

	public int compareTo(heroupgradexp68 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public heroupgradexp68(){
		super();
	}
	public heroupgradexp68(heroupgradexp68 arg){
		this.id=arg.id ;
		this.Born=arg.Born ;
		this.Level=arg.Level ;
		this.Exp=arg.Exp ;
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
	public int Born  = 0  ;
	
	public int getBorn(){
		return this.Born;
	}
	
	public void setBorn(int v){
		this.Born=v;
	}
	
	/**
	 * 
	 */
	public int Level  = 0  ;
	
	public int getLevel(){
		return this.Level;
	}
	
	public void setLevel(int v){
		this.Level=v;
	}
	
	/**
	 * 
	 */
	public int Exp  = 0  ;
	
	public int getExp(){
		return this.Exp;
	}
	
	public void setExp(int v){
		this.Exp=v;
	}
	
	
};