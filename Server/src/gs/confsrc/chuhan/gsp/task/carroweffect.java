package chuhan.gsp.task;


public class carroweffect implements mytools.ConvMain.Checkable ,Comparable<carroweffect>{

	public int compareTo(carroweffect o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public carroweffect(){
		super();
	}
	public carroweffect(carroweffect arg){
		this.id=arg.id ;
		this.step=arg.step ;
		this.startlevel=arg.startlevel ;
		this.level=arg.level ;
		this.screen=arg.screen ;
		this.button=arg.button ;
		this.usebutton=arg.usebutton ;
		this.item=arg.item ;
		this.skill=arg.skill ;
		this.text=arg.text ;
		this.textposition=arg.textposition ;
		this.imagename=arg.imagename ;
		this.cleareffect=arg.cleareffect ;
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
	public int step  = 0  ;
	
	public int getStep(){
		return this.step;
	}
	
	public void setStep(int v){
		this.step=v;
	}
	
	/**
	 * 
	 */
	public int startlevel  = 0  ;
	
	public int getStartlevel(){
		return this.startlevel;
	}
	
	public void setStartlevel(int v){
		this.startlevel=v;
	}
	
	/**
	 * 
	 */
	public int level  = 0  ;
	
	public int getLevel(){
		return this.level;
	}
	
	public void setLevel(int v){
		this.level=v;
	}
	
	/**
	 * 
	 */
	public int screen  = 0  ;
	
	public int getScreen(){
		return this.screen;
	}
	
	public void setScreen(int v){
		this.screen=v;
	}
	
	/**
	 * 
	 */
	public String button  = null  ;
	
	public String getButton(){
		return this.button;
	}
	
	public void setButton(String v){
		this.button=v;
	}
	
	/**
	 * 
	 */
	public String usebutton  = null  ;
	
	public String getUsebutton(){
		return this.usebutton;
	}
	
	public void setUsebutton(String v){
		this.usebutton=v;
	}
	
	/**
	 * 
	 */
	public int item  = 0  ;
	
	public int getItem(){
		return this.item;
	}
	
	public void setItem(int v){
		this.item=v;
	}
	
	/**
	 * 
	 */
	public int skill  = 0  ;
	
	public int getSkill(){
		return this.skill;
	}
	
	public void setSkill(int v){
		this.skill=v;
	}
	
	/**
	 * 
	 */
	public String text  = null  ;
	
	public String getText(){
		return this.text;
	}
	
	public void setText(String v){
		this.text=v;
	}
	
	/**
	 * 
	 */
	public int textposition  = 0  ;
	
	public int getTextposition(){
		return this.textposition;
	}
	
	public void setTextposition(int v){
		this.textposition=v;
	}
	
	/**
	 * 
	 */
	public String imagename  = null  ;
	
	public String getImagename(){
		return this.imagename;
	}
	
	public void setImagename(String v){
		this.imagename=v;
	}
	
	/**
	 * 
	 */
	public int cleareffect  = 0  ;
	
	public int getCleareffect(){
		return this.cleareffect;
	}
	
	public void setCleareffect(int v){
		this.cleareffect=v;
	}
	
	
};