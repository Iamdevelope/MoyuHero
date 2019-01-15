package chuhan.gsp.game;


public class herorecruit51 implements mytools.ConvMain.Checkable ,Comparable<herorecruit51>{

	public int compareTo(herorecruit51 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public herorecruit51(){
		super();
	}
	public herorecruit51(herorecruit51 arg){
		this.id=arg.id ;
		this.comment=arg.comment ;
		this.initialweight1=arg.initialweight1 ;
		this.pulsrange1=arg.pulsrange1 ;
		this.weightpuls1=arg.weightpuls1 ;
		this.initialweight2=arg.initialweight2 ;
		this.pulsrange2=arg.pulsrange2 ;
		this.weightpuls2=arg.weightpuls2 ;
		this.initialweight3=arg.initialweight3 ;
		this.pulsrange3=arg.pulsrange3 ;
		this.weightpuls3=arg.weightpuls3 ;
		this.initialweight4=arg.initialweight4 ;
		this.pulsrange4=arg.pulsrange4 ;
		this.weightpuls4=arg.weightpuls4 ;
		this.herolevel=arg.herolevel ;
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
	public String comment  = null  ;
	
	public String getComment(){
		return this.comment;
	}
	
	public void setComment(String v){
		this.comment=v;
	}
	
	/**
	 * 
	 */
	public int initialweight1  = 0  ;
	
	public int getInitialweight1(){
		return this.initialweight1;
	}
	
	public void setInitialweight1(int v){
		this.initialweight1=v;
	}
	
	/**
	 * 
	 */
	public int pulsrange1  = 0  ;
	
	public int getPulsrange1(){
		return this.pulsrange1;
	}
	
	public void setPulsrange1(int v){
		this.pulsrange1=v;
	}
	
	/**
	 * 
	 */
	public int weightpuls1  = 0  ;
	
	public int getWeightpuls1(){
		return this.weightpuls1;
	}
	
	public void setWeightpuls1(int v){
		this.weightpuls1=v;
	}
	
	/**
	 * 
	 */
	public int initialweight2  = 0  ;
	
	public int getInitialweight2(){
		return this.initialweight2;
	}
	
	public void setInitialweight2(int v){
		this.initialweight2=v;
	}
	
	/**
	 * 
	 */
	public int pulsrange2  = 0  ;
	
	public int getPulsrange2(){
		return this.pulsrange2;
	}
	
	public void setPulsrange2(int v){
		this.pulsrange2=v;
	}
	
	/**
	 * 
	 */
	public int weightpuls2  = 0  ;
	
	public int getWeightpuls2(){
		return this.weightpuls2;
	}
	
	public void setWeightpuls2(int v){
		this.weightpuls2=v;
	}
	
	/**
	 * 
	 */
	public int initialweight3  = 0  ;
	
	public int getInitialweight3(){
		return this.initialweight3;
	}
	
	public void setInitialweight3(int v){
		this.initialweight3=v;
	}
	
	/**
	 * 
	 */
	public int pulsrange3  = 0  ;
	
	public int getPulsrange3(){
		return this.pulsrange3;
	}
	
	public void setPulsrange3(int v){
		this.pulsrange3=v;
	}
	
	/**
	 * 
	 */
	public int weightpuls3  = 0  ;
	
	public int getWeightpuls3(){
		return this.weightpuls3;
	}
	
	public void setWeightpuls3(int v){
		this.weightpuls3=v;
	}
	
	/**
	 * 
	 */
	public int initialweight4  = 0  ;
	
	public int getInitialweight4(){
		return this.initialweight4;
	}
	
	public void setInitialweight4(int v){
		this.initialweight4=v;
	}
	
	/**
	 * 
	 */
	public int pulsrange4  = 0  ;
	
	public int getPulsrange4(){
		return this.pulsrange4;
	}
	
	public void setPulsrange4(int v){
		this.pulsrange4=v;
	}
	
	/**
	 * 
	 */
	public int weightpuls4  = 0  ;
	
	public int getWeightpuls4(){
		return this.weightpuls4;
	}
	
	public void setWeightpuls4(int v){
		this.weightpuls4=v;
	}
	
	/**
	 * 
	 */
	public int herolevel  = 0  ;
	
	public int getHerolevel(){
		return this.herolevel;
	}
	
	public void setHerolevel(int v){
		this.herolevel=v;
	}
	
	
};