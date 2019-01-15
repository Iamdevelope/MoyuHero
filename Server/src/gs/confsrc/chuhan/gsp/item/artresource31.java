package chuhan.gsp.item;


public class artresource31 implements mytools.ConvMain.Checkable ,Comparable<artresource31>{

	public int compareTo(artresource31 o){
		return this.id-o.id;
	}

	
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	public artresource31(){
		super();
	}
	public artresource31(artresource31 arg){
		this.id=arg.id ;
		this.NameID=arg.NameID ;
		this.DescriptionID=arg.DescriptionID ;
		this.attriType=arg.attriType ;
		this.attriDes=arg.attriDes ;
		this.ispercentage=arg.ispercentage ;
		this.symbol=arg.symbol ;
		this.attriValue=arg.attriValue ;
		this.bonusPassiveSkill=arg.bonusPassiveSkill ;
		this.bonusPassiveSkilldes=arg.bonusPassiveSkilldes ;
		this.artresources=arg.artresources ;
		this.actionresource=arg.actionresource ;
		this.headiconresource=arg.headiconresource ;
		this.headartresource=arg.headartresource ;
		this.FireSignSize=arg.FireSignSize ;
		this.readysound=arg.readysound ;
		this.diesound=arg.diesound ;
		this.fashionresource=arg.fashionresource ;
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
	public String NameID  = null  ;
	
	public String getNameID(){
		return this.NameID;
	}
	
	public void setNameID(String v){
		this.NameID=v;
	}
	
	/**
	 * 
	 */
	public String DescriptionID  = null  ;
	
	public String getDescriptionID(){
		return this.DescriptionID;
	}
	
	public void setDescriptionID(String v){
		this.DescriptionID=v;
	}
	
	/**
	 * 
	 */
	public String attriType  = null  ;
	
	public String getAttriType(){
		return this.attriType;
	}
	
	public void setAttriType(String v){
		this.attriType=v;
	}
	
	/**
	 * 
	 */
	public String attriDes  = null  ;
	
	public String getAttriDes(){
		return this.attriDes;
	}
	
	public void setAttriDes(String v){
		this.attriDes=v;
	}
	
	/**
	 * 
	 */
	public String ispercentage  = null  ;
	
	public String getIspercentage(){
		return this.ispercentage;
	}
	
	public void setIspercentage(String v){
		this.ispercentage=v;
	}
	
	/**
	 * 
	 */
	public String symbol  = null  ;
	
	public String getSymbol(){
		return this.symbol;
	}
	
	public void setSymbol(String v){
		this.symbol=v;
	}
	
	/**
	 * 
	 */
	public String attriValue  = null  ;
	
	public String getAttriValue(){
		return this.attriValue;
	}
	
	public void setAttriValue(String v){
		this.attriValue=v;
	}
	
	/**
	 * 
	 */
	public int bonusPassiveSkill  = 0  ;
	
	public int getBonusPassiveSkill(){
		return this.bonusPassiveSkill;
	}
	
	public void setBonusPassiveSkill(int v){
		this.bonusPassiveSkill=v;
	}
	
	/**
	 * 
	 */
	public String bonusPassiveSkilldes  = null  ;
	
	public String getBonusPassiveSkilldes(){
		return this.bonusPassiveSkilldes;
	}
	
	public void setBonusPassiveSkilldes(String v){
		this.bonusPassiveSkilldes=v;
	}
	
	/**
	 * 
	 */
	public String artresources  = null  ;
	
	public String getArtresources(){
		return this.artresources;
	}
	
	public void setArtresources(String v){
		this.artresources=v;
	}
	
	/**
	 * 
	 */
	public String actionresource  = null  ;
	
	public String getActionresource(){
		return this.actionresource;
	}
	
	public void setActionresource(String v){
		this.actionresource=v;
	}
	
	/**
	 * 
	 */
	public String headiconresource  = null  ;
	
	public String getHeadiconresource(){
		return this.headiconresource;
	}
	
	public void setHeadiconresource(String v){
		this.headiconresource=v;
	}
	
	/**
	 * 
	 */
	public String headartresource  = null  ;
	
	public String getHeadartresource(){
		return this.headartresource;
	}
	
	public void setHeadartresource(String v){
		this.headartresource=v;
	}
	
	/**
	 * 
	 */
	public String FireSignSize  = null  ;
	
	public String getFireSignSize(){
		return this.FireSignSize;
	}
	
	public void setFireSignSize(String v){
		this.FireSignSize=v;
	}
	
	/**
	 * 
	 */
	public String readysound  = null  ;
	
	public String getReadysound(){
		return this.readysound;
	}
	
	public void setReadysound(String v){
		this.readysound=v;
	}
	
	/**
	 * 
	 */
	public String diesound  = null  ;
	
	public String getDiesound(){
		return this.diesound;
	}
	
	public void setDiesound(String v){
		this.diesound=v;
	}
	
	/**
	 * 
	 */
	public String fashionresource  = null  ;
	
	public String getFashionresource(){
		return this.fashionresource;
	}
	
	public void setFashionresource(String v){
		this.fashionresource=v;
	}
	
	
};