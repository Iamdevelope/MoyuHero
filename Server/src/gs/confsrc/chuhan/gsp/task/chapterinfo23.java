package chuhan.gsp.task;


public class chapterinfo23 implements mytools.ConvMain.Checkable ,Comparable<chapterinfo23>{

	public int compareTo(chapterinfo23 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public chapterinfo23(){
		super();
	}
	public chapterinfo23(chapterinfo23 arg){
		this.id=arg.id ;
		this.chapterName=arg.chapterName ;
		this.chapterDrop=arg.chapterDrop ;
		this.stageID=arg.stageID ;
		this.starnum=arg.starnum ;
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
	public String chapterName  = null  ;
	
	public String getChapterName(){
		return this.chapterName;
	}
	
	public void setChapterName(String v){
		this.chapterName=v;
	}
	
	/**
	 * 
	 */
	public String chapterDrop  = null  ;
	
	public String getChapterDrop(){
		return this.chapterDrop;
	}
	
	public void setChapterDrop(String v){
		this.chapterDrop=v;
	}
	
	/**
	 * 
	 */
	public String stageID  = null  ;
	
	public String getStageID(){
		return this.stageID;
	}
	
	public void setStageID(String v){
		this.stageID=v;
	}
	
	/**
	 * 
	 */
	public String starnum  = null  ;
	
	public String getStarnum(){
		return this.starnum;
	}
	
	public void setStarnum(String v){
		this.starnum=v;
	}
	
	
};