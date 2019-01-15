/**
 * Class: ClientSocket.java
 * Package: knight.gsp.mbean
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2013-8-9 		yesheng
 *
 * Copyright (c) 2013, Perfect World All Rights Reserved.
*/

package chuhan.gsp.mbean;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.EOFException;
import java.io.IOException;
import java.net.Socket;

import chuhan.gsp.main.SocketServerThread;

/**
 * ClassName:ClientSocket
 * Function: ADD FUNCTION HERE
 *
 * @author   yesheng
 * @version  
 * @since    
 * @Date	 2013-8-9		下午9:01:25
 *
 * @see 	 
 */
public class ClientSocket extends Thread {

	
	Socket socket;
	DataOutputStream dataoutputstream;
	DataInputStream dis;
	/**
	 * Creates a new instance of ClientSocket.
	 *
	 * @param socket
	 */
	
	public ClientSocket(Socket socket) {
		this.socket = socket;
	}
	@Override
	public void run() {
		try {
			dis = new DataInputStream(socket.getInputStream());
			int ch1 = dis.read();
	        int ch2 = dis.read();
	        int ch3 = dis.read();
	        int ch4 = dis.read();
	        SocketServerThread.logger.info("ch1:"+ch1+"ch2:"+ch2+"ch3:"+ch3+"ch4:"+ch4);
	        if ((ch1 | ch2 | ch3 | ch4) < 0)
	            throw new EOFException();
	        int length = ((ch4 << 24) + (ch3 << 16) + (ch2 << 8) + (ch1 << 0));
			byte[] byteArrays = new byte[length];
			int readNum = dis.read(byteArrays);
			if (readNum != length){
				SocketServerThread.logger.info("read socket stream error.readNum:expectNum "+readNum+":"+length);
				close();
				return;
			}
			String xmlString = new String(byteArrays, "UTF-8");
			SocketServerThread.logger.info("socket xmlString:"+xmlString);
			
			String outputXmlStr = handleSocketRequest(xmlString);
			if (outputXmlStr == null){
				SocketServerThread.logger.info("outputXmlStr is null");
				close();
				return;
			}
			SocketServerThread.logger.info("socket outputxmlString:"+outputXmlStr);
			dataoutputstream = new DataOutputStream(socket.getOutputStream());
			byte[] outputXmlBytesArr = outputXmlStr.getBytes("UTF-8");
			int v = outputXmlBytesArr.length;
			byte[] lengthbytearr = intToBytes(v, false);
			byte[] resultArr = new byte[lengthbytearr.length+outputXmlBytesArr.length];
			int i = 0;
			while(i<lengthbytearr.length){
				resultArr[i] = lengthbytearr[i];
				i++;
			}
			int j = 0;
			while(j<outputXmlBytesArr.length){
				resultArr[i] = outputXmlBytesArr[j];
				j++;
				i++;
			}
//			dataoutputstream.write((v >>>  0) & 0xFF);
//			dataoutputstream.write((v >>>  8) & 0xFF);
//			dataoutputstream.write((v >>> 16) & 0xFF);
//			dataoutputstream.write((v >>> 24) & 0xFF);
			dataoutputstream.write(resultArr);
			dataoutputstream.flush();
			
		} catch (IOException e) {
			
			SocketServerThread.logger.error("receive socket msg error", e);
			
		}finally{
			close();
		}
		
	}
    public static byte[] intToBytes(int nNum, boolean byteOrder) {
        byte[] bytesRet = new byte[4];
        if(byteOrder) {
	        bytesRet[0] = (byte) ((nNum >> 24) & 0xFF);
	        bytesRet[1] = (byte) ((nNum >> 16) & 0xFF);
	        bytesRet[2] = (byte) ((nNum >> 8) & 0xFF);
	        bytesRet[3] = (byte) (nNum & 0xFF);
        } else {
	        bytesRet[3] = (byte) ((nNum >> 24) & 0xFF);
	        bytesRet[2] = (byte) ((nNum >> 16) & 0xFF);
	        bytesRet[1] = (byte) ((nNum >> 8) & 0xFF);
	        bytesRet[0] = (byte) (nNum & 0xFF);
        }
        return bytesRet;
    }
	/**
	 * handleSocketRequest:转调策略模式的接口
	 * 
	 * @param xmlString
	 * @return    
	 * String    
	 * @throws 
	 * @since  　
	*/
	
	public String handleSocketRequest(String xmlString) {

		return ClientSocketRequest.parseAndHandleXmlRequest(xmlString);
	}
	void close(){
		try {
			if (dataoutputstream!=null) {
				dataoutputstream.close();
				
			}
			if (dis != null) {
				dis.close();
			}
			if (socket!=null) {
				socket.close();
			}
		} catch (IOException e) {
			SocketServerThread.logger.error("receive socket msg error", e);
			
		}
	}

}

