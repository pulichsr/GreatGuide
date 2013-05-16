/**
 * 
 */
package com.greatguide.backend.location;


/**
 * @author sandeep
 * 
 */
public interface IGPSStateListener {

	public void receiveSignalStrength(SignalStrength signalStrength);
	public void receiveGPSStatus(boolean flag);  
   
}
         