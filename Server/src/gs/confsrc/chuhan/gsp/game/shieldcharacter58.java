package chuhan.gsp.game;


public class shieldcharacter58 implements mytools.ConvMain.Checkable ,Comparable<shieldcharacter58>{

	public int compareTo(shieldcharacter58 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public shieldcharacter58(){
		super();
	}
	public shieldcharacter58(shieldcharacter58 arg){
		this.id=arg.id ;
		this.word=arg.word ;
		this.name=arg.name ;
		this.chat=arg.chat ;
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
	public String word  = null  ;
	
	public String getWord(){
		return this.word;
	}
	
	public void setWord(String v){
		this.word=v;
	}
	
	/**
	 * 
	 */
	public int name  = 0  ;
	
	public int getName(){
		return this.name;
	}
	
	public void setName(int v){
		this.name=v;
	}
	
	/**
	 * 
	 */
	public int chat  = 0  ;
	
	public int getChat(){
		return this.chat;
	}
	
	public void setChat(int v){
		this.chat=v;
	}
	
	
};