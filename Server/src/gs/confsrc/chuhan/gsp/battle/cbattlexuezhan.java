package chuhan.gsp.battle;


public class cbattlexuezhan implements mytools.ConvMain.Checkable ,Comparable<cbattlexuezhan>{

	public int compareTo(cbattlexuezhan o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public cbattlexuezhan(){
		super();
	}
	public cbattlexuezhan(cbattlexuezhan arg){
		this.id=arg.id ;
		this.pic=arg.pic ;
		this.battle=arg.battle ;
		this.color=arg.color ;
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
	public int battle  = 0  ;
	
	public int getBattle(){
		return this.battle;
	}
	
	public void setBattle(int v){
		this.battle=v;
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
	
	
};