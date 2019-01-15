package chuhan.gsp.item;


public class heroaddstage67 implements mytools.ConvMain.Checkable ,Comparable<heroaddstage67>{

	public int compareTo(heroaddstage67 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public heroaddstage67(){
		super();
	}
	public heroaddstage67(heroaddstage67 arg){
		this.id=arg.id ;
		this.Born=arg.Born ;
		this.Qosition=arg.Qosition ;
		this.Quality=arg.Quality ;
		this.HalosPn=arg.HalosPn ;
		this.Level=arg.Level ;
		this.Gold=arg.Gold ;
		this.Stuff=arg.Stuff ;
		this.Numbers=arg.Numbers ;
		this.Attribute=arg.Attribute ;
		this.Value=arg.Value ;
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
	public int Qosition  = 0  ;
	
	public int getQosition(){
		return this.Qosition;
	}
	
	public void setQosition(int v){
		this.Qosition=v;
	}
	
	/**
	 * 
	 */
	public int Quality  = 0  ;
	
	public int getQuality(){
		return this.Quality;
	}
	
	public void setQuality(int v){
		this.Quality=v;
	}
	
	/**
	 * 
	 */
	public int HalosPn  = 0  ;
	
	public int getHalosPn(){
		return this.HalosPn;
	}
	
	public void setHalosPn(int v){
		this.HalosPn=v;
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
	public int Gold  = 0  ;
	
	public int getGold(){
		return this.Gold;
	}
	
	public void setGold(int v){
		this.Gold=v;
	}
	
	/**
	 * 
	 */
	public String Stuff  = null  ;
	
	public String getStuff(){
		return this.Stuff;
	}
	
	public void setStuff(String v){
		this.Stuff=v;
	}
	
	/**
	 * 
	 */
	public String Numbers  = null  ;
	
	public String getNumbers(){
		return this.Numbers;
	}
	
	public void setNumbers(String v){
		this.Numbers=v;
	}
	
	/**
	 * 
	 */
	public String Attribute  = null  ;
	
	public String getAttribute(){
		return this.Attribute;
	}
	
	public void setAttribute(String v){
		this.Attribute=v;
	}
	
	/**
	 * 
	 */
	public String Value  = null  ;
	
	public String getValue(){
		return this.Value;
	}
	
	public void setValue(String v){
		this.Value=v;
	}
	
	
};