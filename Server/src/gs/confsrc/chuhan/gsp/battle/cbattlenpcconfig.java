package chuhan.gsp.battle;


public class cbattlenpcconfig implements mytools.ConvMain.Checkable ,Comparable<cbattlenpcconfig>{

	public int compareTo(cbattlenpcconfig o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cbattlenpcconfig(){
		super();
	}
	public cbattlenpcconfig(cbattlenpcconfig arg){
		this.id=arg.id ;
		this.chuhan=arg.chuhan ;
		this.name=arg.name ;
		this.color=arg.color ;
		this.icon=arg.icon ;
		this.pic=arg.pic ;
		this.battletype=arg.battletype ;
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
	public int chuhan  = 0  ;
	
	public int getChuhan(){
		return this.chuhan;
	}
	
	public void setChuhan(int v){
		this.chuhan=v;
	}
	
	/**
	 * 
	 */
	public String name  = null  ;
	
	public String getName(){
		return this.name;
	}
	
	public void setName(String v){
		this.name=v;
	}
	
	/**
	 * 
	 */
	public int color  = 0  ;
	
	public int getColor(){
		return this.color;
	}
	
	public void setColor(int v){
		this.color=v;
	}
	
	/**
	 * 
	 */
	public int icon  = 0  ;
	
	public int getIcon(){
		return this.icon;
	}
	
	public void setIcon(int v){
		this.icon=v;
	}
	
	/**
	 * 
	 */
	public int pic  = 0  ;
	
	public int getPic(){
		return this.pic;
	}
	
	public void setPic(int v){
		this.pic=v;
	}
	
	/**
	 * 
	 */
	public int battletype  = 0  ;
	
	public int getBattletype(){
		return this.battletype;
	}
	
	public void setBattletype(int v){
		this.battletype=v;
	}
	
	
};