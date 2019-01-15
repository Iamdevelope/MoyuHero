/**
 * Class: SocketServerThread.java
 * Package: knight.gsp.main
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2013-8-9 		yesheng
 *
 * Copyright (c) 2013, Perfect World All Rights Reserved.
*/

package chuhan.gsp.main;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

import org.apache.log4j.Logger;

import chuhan.gsp.mbean.ClientSocket;
import chuhan.gsp.mbean.ClientSocketRequest;

/**
 * ClassName:SocketServerThread
 * Function: ADD FUNCTION HERE
 *
 * @author   yesheng
 * @version  
 * @since    
 * @Date	 2013-8-9		下午8:39:05
 *
 * @see 	 
 */
public class SocketServerThread extends Thread {

	public static Logger logger = Logger.getLogger(SocketServerThread.class);
	int port;
	ServerSocket serverSocket;
	Socket socket;
	public SocketServerThread(int port) {

		super();
		this.port = port;
		ClientSocketRequest.init();
	}
	
	@Override
	public void run() {
		try {
			serverSocket = new ServerSocket(port);
			//serverSocket.set
		} catch (IOException e) {
			
			logger.error("can't listen to "+port, e);
			
		}
		logger.info("ServerSocket start listen "+port +" ip:"+serverSocket.getInetAddress().toString());
		
		try {
			while (!isInterrupted()) {
				socket = serverSocket.accept();
				Thread clientSocket = new ClientSocket(socket);
				clientSocket.start();
			}
		} catch (Exception e) {
			logger.error("serversocket running error");
			
		}finally{
			close();
		}
		logger.info("SocketServerThread stopped");
	}
	/**
	 * close:(这里用一句话描述这个方法的作用)
	 *    
	 * void    
	 * @throws 
	 * @since  　
	*/
	
	private void close() {
		try {
			if (socket != null)
				socket.close();
			if (serverSocket != null)
				serverSocket.close();
		} catch (IOException e) {
			
			logger.error("SocketServerThread stopped",e);
		}
	}
	
}

