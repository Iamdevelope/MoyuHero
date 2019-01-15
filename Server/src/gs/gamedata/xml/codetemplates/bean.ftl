package ${namespace};

<#if !defineOnly>
import com.thoughtworks.xstream.XStream;
import org.apache.poi.ss.usermodel.Row;
</#if>

public class ${classname} <#if baseclass! !="" > extends ${baseclass}<#else>implements mytools.ConvMain.Checkable</#if> <#if xlsfiles[0] != "" && baseclass! == "" >,Comparable<${classname}></#if>{

	<#if xlsfiles[0] != "">
	public int compareTo(${classname} o){
		return this.id-o.id;
	}
	</#if>

	<#if xlsfiles[0] != "" && !defineOnly> 	
	final static private org.apache.log4j.Logger logger = org.apache.log4j.Logger
			.getLogger(${classname}.class);
			
	private static java.util.Map<Integer,${classname}> doconv(String confDir) {		
			java.util.Map<Integer,${classname}> result=new java.util.TreeMap<Integer,${classname}>();
		<#list xlsfiles as file>
			try {
			final String filename=confDir+"/${file}";
			java.io.File xfile=new java.io.File(filename);
			if(!xfile.canRead()) {
				logger.error("�޷���ȡ"+xfile.getAbsolutePath());
				return result;
			}
			org.apache.poi.xssf.usermodel.XSSFWorkbook wb = new org.apache.poi.xssf.usermodel.XSSFWorkbook(filename);
			org.apache.poi.ss.usermodel.Sheet sheet = wb.getSheetAt(0);			
			java.util.ArrayList<String> collnames=new java.util.ArrayList<String>();
			for (Row r:sheet ) {
				if(collnames.isEmpty()){						
					for(org.apache.poi.ss.usermodel.Cell c:r){
						collnames.add(c.getStringCellValue());
					}
					if(collnames.isEmpty())
						throw new RuntimeException("sheet is empty");
				} else {
					${classname} obj;
					try{
						obj=new ${classname}(r,collnames,"",0);
					}catch(NeedId ex){
						continue;
					}
					if(result.put(obj.id,obj)!=null)
						throw new RuntimeException("����${classname}���ҵ����ظ���key="+obj.id);
				}
			}
			} catch(Exception ex){
				logger.error("��${file}��ȡ����ת����${classname}����ʱ����",ex);
			}
		</#list> 
		return result;
	}	
	</#if>	
	
	<#if baseclass! !="" > 
	public ${classname}(${baseclass} arg){
		super(arg);
	}
	</#if>
	
	static class NeedId extends RuntimeException{

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		
	}
	<#if !defineOnly>
	/**
	 * @param row ��ǰ���ڴ����excel�ĵ�ǰrow�����������е����ݶ����������row
	 * @param collnames �����excel��ĵ�һ����ȡ����������
	 * @param prefix ������bean����һ��collection��value,�������bean�����bean���ã���ô����prefixȷ����Ӧ��ϵ
	 * @param index ������bean����һ��collection��value,��ô����indexȷ�������Ӧ��һ��
	 * ���index�������ƣ�����ĳ��������notnull���ԣ���ô�׳�IllegalArgumentException��
	 */
	public ${classname}(Row row,java.util.ArrayList<String> collnames,String prefix,int index){
	<#if baseclass! !="" > 
		super(row,collnames,prefix,index);
	<#else>
		super();
	</#if>	
	
	<#list varList as myvar>
	<#-- ���ȣ�����ÿ������ -->
	<#list myvar.prefixMapping?keys as prkey>
		<#-- Ȼ�����prefixMapping -->
		<#assign pr=myvar.prefixMapping[prkey] />
		<#if prkey_index != 0 > else </#if> if(prefix.equals("${prkey}")){			
							
			<#if myvar.valueType! !="">
				 <#if myvar.valueType=="String" || myvar.valueType=="Integer" || myvar.valueType=="Long" || myvar.valueType=="Double" || myvar.valueType=="Boolean"> 				 
					<#list pr as collname>	 
					<#if myvar.valueType! == "String" >
						{String v=mytools.ConvMain.getCellValue(row,"${collname}",collnames); 
					<#elseif  myvar.valueType == "Integer" >
						{Integer v=mytools.ConvMain.getIntegerCellValue(row,"${collname}",collnames); 
					<#elseif  myvar.valueType == "Long" >
						{Long v=mytools.ConvMain.getLongCellValue(row,"${collname}",collnames);
					<#elseif  myvar.valueType == "Double" >
						{Double v=mytools.ConvMain.getDoubleCellValue(row,"${collname}",collnames);			
					<#elseif  myvar.valueType == "Boolean" >
						{Boolean v=mytools.ConvMain.getBooleanCellValue(row,"${collname}",collnames);
					</#if>
							if(v!=null) {
								if(this.${myvar.name}==null) this.${myvar.name}=${myvar.initValue}; 
								this.${myvar.name}.add(v);
							}
						}
					</#list>
				 <#else>
				 //111${myvar.type}
				 for(int i=0;;i++){
					try{
						if(this.${myvar.name}==null) this.${myvar.name}=${myvar.initValue}; 
						this.${myvar.name}.add(new ${myvar.valueType}(row,collnames,"${pr[0]}",i));
					}catch(IndexOutOfBoundsException ex){
						break;
					}
				 }
				 </#if>
			
			<#else>
			<#list pr as collname>	 
			<#if collname_index != 0 > else </#if> if(index == ${collname_index}){			
			<#if myvar.type! == "String" >
				{String v=mytools.ConvMain.getCellValue(row,"${collname}",collnames); 
			<#elseif  myvar.type == "int" >
				{Integer v=mytools.ConvMain.getIntegerCellValue(row,"${collname}",collnames); 				
			<#elseif  myvar.type == "long" >
				{Long v=mytools.ConvMain.getLongCellValue(row,"${collname}",collnames);
			<#elseif  myvar.type == "double" >
				{Double v=mytools.ConvMain.getDoubleCellValue(row,"${collname}",collnames);		
			<#elseif  myvar.type == "boolean" >
				{Boolean v=mytools.ConvMain.getBooleanCellValue(row,"${collname}",collnames);			
			</#if>
			
			<#if myvar.name != "id">
				<#if myvar.type! == "String" && genxml !="" >
					this.${myvar.name}=v;}
				<#else>
					if(v!=null) this.${myvar.name}=v; else throw new IndexOutOfBoundsException(row.getRowNum()+"�е�${collname}Ϊ��");}
				</#if>	
			<#else>
				if(v!=null) this.${myvar.name}=v; else throw new NeedId();}
			</#if>
			}
			</#list> else throw new IndexOutOfBoundsException(row.getRowNum()+"��index����");
			</#if> <#-- myvar.valueType -->						
		}
	</#list> 
			else throw new RuntimeException("unknown prefix:"+prefix);
	</#list>		
	
	}
	</#if>
	public ${classname}(){
		super();
	}
	public ${classname}(${classname} arg){
	<#if baseclass! !="" > 
		super(arg);
	</#if>
	<#list varList as myvar>
		this.${myvar.name}=arg.${myvar.name} ;
	</#list> 
	}
	public void checkValid(java.util.Map<String,java.util.Map<Integer,? extends Object> > objs){
		<#if baseclass! !="" > 
			super.checkValid(objs);
		</#if>
		<#list varList as myvar>
			<#if myvar.minValue! !="" || myvar.maxValue! !="" || myvar.ref! !="" >
			do{
			<#if myvar.valueType! =="">
				${myvar.type} tmprefvalue=${myvar.name};
			<#else>
				if(${myvar.name}!=null) for(${myvar.valueType} tmprefvalue:${myvar.name}){
			</#if>		
				
			<#if myvar.minValue! !="">
				if(tmprefvalue < ${myvar.minValue}) throw new RuntimeException("${classname}.${myvar.name}="+tmprefvalue+",���Բ��������� ${classname}.${myvar.name} < ${myvar.minValue}");
			</#if>
			<#if myvar.maxValue! !="">
				if(tmprefvalue > ${myvar.maxValue}) throw new RuntimeException("${classname}.${myvar.name}="+tmprefvalue+",���Բ��������� ${classname}.${myvar.name} > ${myvar.maxValue}");
			</#if>
			<#if myvar.ref! !="">
				{
					java.util.Map<Integer,? extends Object> m=objs.get("${myvar.ref}");
					if(m==null) throw new RuntimeException("${classname}.${myvar.name}��ref����ָ�򲻴��ڵ���${myvar.ref}");
					if(m.get(tmprefvalue) == null) throw new RuntimeException("${classname}.${myvar.name}�����ù�ϵ����ʧ��,"+tmprefvalue+"��Ŀ���${myvar.ref}���Ҳ���" );
				}
			</#if>
			<#if myvar.valueType! !="">
				}
			</#if>
			}while(false);
			</#if>
		</#list> 
	}
	<#list varList as myvar>
	/**
	 * ${myvar.comment}
	 */
<#if !defineOnly>	 
	@com.thoughtworks.xstream.annotations.XStreamAsAttribute
</#if>	
	public ${myvar.type} ${myvar.name} <#if myvar.initValue! !="" && myvar.valueType! ==""> = ${myvar.initValue} </#if> ;
	
	public ${myvar.type} get${myvar.name?cap_first}(){
		return this.${myvar.name};
	}
	
	public void set${myvar.name?cap_first}(${myvar.type} v){
		this.${myvar.name}=v;
	}
	
	</#list> 
	
	<#if !defineOnly>
	<#if genxml =="server" > 
	public static java.util.Map<Integer,? extends Object> toXML(String confdir,String outdir,XStream xstream) throws java.io.IOException {
		java.util.Map<Integer,${classname}> ret=doconv(confdir);
		xstream.autodetectAnnotations(false);
		final java.io.FileOutputStream ops=new java.io.FileOutputStream(outdir+"/"+${classname}.class.getCanonicalName()+".xml");		
		final java.io.OutputStreamWriter writer = new java.io.OutputStreamWriter(ops, "UTF-8");
		try{
			writer.write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n");
			writer.flush();
			xstream.toXML(ret, writer);
		}finally{
			writer.close();
		}
		return ret;
	}
	</#if>
	
	<#if genxml =="client" > 
	public static java.util.Map<Integer,? extends Object> toXML(String confdir,String outdir,XStream xstream) throws java.io.IOException{
		java.util.Map<Integer,${classname}> ret=doconv(confdir);
		xstream.autodetectAnnotations(true);
		final java.io.FileOutputStream ops=new java.io.FileOutputStream(outdir+"/"+${classname}.class.getCanonicalName()+".xml");		
		final java.io.OutputStreamWriter writer = new java.io.OutputStreamWriter(ops, "UTF-16LE");
		try{
			writer.append((char) 0xFEFF);
			writer.write("<root>\r\n");
			for(${classname} obj:ret.values()){
				xstream.alias("record", ${classname}.class);
				xstream.toXML(obj, writer);		
				writer.write("\r\n");
			}
			writer.write("</root>\r\n");
		}finally{
			writer.close();
		}
		xstream.autodetectAnnotations(false);
		return ret;
	}
	</#if>
	</#if>		
};