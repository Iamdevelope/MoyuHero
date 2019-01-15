package server;

  
import java.io.BufferedReader;   
import java.io.IOException;   
import java.io.InputStream;   
import java.io.InputStreamReader;   
import java.io.OutputStream;   
import java.net.InetSocketAddress;   
  



import java.net.URI;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;
import java.util.Map;
import java.util.Vector;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import com.sun.net.httpserver.*;
import com.sun.net.httpserver.spi.HttpServerProvider;
  
  
/**  
 * @project SimpleHttpServer  
 * @author sunnylocus  
 * @vresion 1.0 2009-9-2  
 * @description  自定义的http服务器  
 */  
public class httpserver {   
	static final String url = "http://127.0.0.1:8080/tongbu";
//	static final String url = "http://103.10.84.25:8080/tongbu";
    //启动服务，监听来自客户端的请求   
    public static void httpserverService() throws IOException {   
    	/*
        HttpServerProvider provider = HttpServerProvider.provider();   
        HttpServer httpserver =provider.createHttpServer(new InetSocketAddress("0.0.0.0",6666), 100);//监听端口6666,能同时接 受100个请求   
        httpserver.createContext("/myApp", new MyHttpHandler());    
        httpserver.setExecutor(null);   
        httpserver.start();   
        System.out.println("server started");  
        */
        
        
        InetSocketAddress sa = new InetSocketAddress("127.0.0.1",6666);
//        InetSocketAddress sa = new InetSocketAddress("103.10.84.25",6666);
        HttpServer server = HttpServer.create(sa, 0);
        ExecutorService executor = Executors.newCachedThreadPool();
        server.setExecutor(executor);
        server.createContext("/", new MyHttpHandler());
        server.start();
        System.out.println("server started");  
    }   
    //Http请求处理类   
    static class MyHttpHandler implements HttpHandler {   
        public void handle(HttpExchange httpExchange) throws IOException {   
            String responseMsg = "1";   //响应信息   
            
            
            URI uri = httpExchange.getRequestURI();
            String path = uri.getQuery();		//?&
            
            
            System.out.println(path);  
            boolean right = check(getk(path));
            if(!right)
            	responseMsg = "-1";
            
            
//            String[] 

            /*
            InputStream in = httpExchange.getRequestBody(); //获得输入流   
            Headers header = httpExchange.getRequestHeaders();
            HttpContext context = httpExchange.getHttpContext();
            Map<String, Object> test = context.getAttributes();
            boolean is = header.containsKey("username");
//            String str = (String) httpExchange.getAttribute("username");
            BufferedReader reader = new BufferedReader(new InputStreamReader(in));   
            String temp = null;   
            while((temp = reader.readLine()) != null) {   
                System.out.println("client request:"+temp);   
            }   
            */
            httpExchange.sendResponseHeaders(200, responseMsg.length()); //设置响应头属性及响应信息的长度   
            OutputStream out = httpExchange.getResponseBody();  //获得输出流   
            out.write(responseMsg.getBytes());   
            out.flush();   
            httpExchange.close();   
            
 //           if(right)
  //          	sendGet(url);
               
        }   
    }   
    public static void main(String[] args) throws IOException {   
        httpserverService();   
    }   
    /*
    public static void SetAttack(byte pos, boolean bAttacker)
    {
        byte m_Attacker = 0;
        m_Attacker	|=  (byte)(1 << pos);
        m_Attacker |= 0x80;
        if (bAttacker)
        {
            m_Attacker |= 0x80;
        }
        if((m_Attacker & 0x80) == 0x80){
        	if((m_Attacker & 2) == 2){
        		return;
        	}
        }
    }
    */
    
    public static String getk(String strin)
    {
    	String result = null;
    	
    	java.util.HashMap<String, String> hashMap = new java.util.HashMap<String, String>();
    	if(strin.indexOf("&") != -1)
    	{
    		String[] strmap = strin.split("&");
    		for(String strtest : strmap)
            {
    			if(strtest.indexOf("=") != -1)
    			{
    				String[] in = strtest.split("=");
    				if(in.length == 2)
    				{
    					hashMap.put(in[0], in[1]);
    				}
    			}
            }
    	}
    	else
    	{
    		if(strin.indexOf("=") != -1)
			{
				String[] in = strin.split("=");
				if(in.length == 2)
				{
					hashMap.put(in[0], in[1]);
				}
			}
    	}
        
        
    	result = hashMap.get("k");
    	
    	
    	return result;
    }
    
    public static boolean check(String str)
    {
    	/*
    	java.util.LinkedList<String> strlist = new java.util.LinkedList<String>();
    	strlist.addFirst("user1");
    	strlist.addFirst("user2");
    	strlist.addFirst("user3");
    	strlist.addFirst("user4");
    	strlist.addFirst("user5");
    	strlist.addFirst("user6");
    	strlist.addFirst("user7");
    	strlist.addFirst("user8");
    	strlist.addFirst("user9");
    	strlist.addFirst("user10");
    	
    	strlist.addFirst("cys001");
    	strlist.addFirst("cys002");
    	strlist.addFirst("cys003");
    	
    	if(str.contains("zy"))
    		return true;
    	
    	if(strlist.contains(str))
    		return true;
    		*/
  	
    	return true;
    }
    
    public static String sendGet(String url) {
        String result = "";
        BufferedReader in = null;
        try {
            String urlNameString = url + "?" + "source=1&trade_no=id0100&amount=3&partner=4&paydes=5";
            urlNameString += "&debug=6&sign=46961ae5f7ab133cf34e88107c22dc2d";
            
            URL realUrl = new URL(urlNameString);
            // 打开和URL之间的连接
            URLConnection connection = realUrl.openConnection();
            // 设置通用的请求属性
            connection.setRequestProperty("accept", "*/*");
            connection.setRequestProperty("connection", "Keep-Alive");
            connection.setRequestProperty("user-agent",
                    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1;SV1)");
            // 建立实际的连接
            connection.connect();
            // 获取所有响应头字段
            Map<String, List<String>> map = connection.getHeaderFields();
            // 遍历所有的响应头字段
            for (String key : map.keySet()) {
                System.out.println(key + "--->" + map.get(key));
            }
            // 定义 BufferedReader输入流来读取URL的响应
            in = new BufferedReader(new InputStreamReader(
                    connection.getInputStream()));
            String line;
            while ((line = in.readLine()) != null) {
                result += line;
            }
        } catch (Exception e) {
            System.out.println("发送GET请求出现异常！" + e);
            e.printStackTrace();
        }
        // 使用finally块来关闭输入流
        finally {
            try {
                if (in != null) {
                    in.close();
                }
            } catch (Exception e2) {
                e2.printStackTrace();
            }
        }
        return result;
    }
    
    
}  
