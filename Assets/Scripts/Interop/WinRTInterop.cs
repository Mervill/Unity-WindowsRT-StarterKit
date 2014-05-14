using UnityEngine;
using System;
using System.Collections;

// Contains utilties for windows interop.
using UnityEngine.WSA;
// Prevents collision between UnityEngine.Application and UnityEngine.WSA.Application.
using WinApplication = UnityEngine.WSA.Application;

//
// Example of how to setup
// Unity<->WinRT/XMAL interop.
//
public class WinRTInterop {

	public delegate void WindowResizeDelegate(float width,float height,bool isSnapped);
	public static event WindowResizeDelegate onWindowResized;

	public delegate void WindowActivatedDelegate(WindowActivationState state);
	public static event WindowActivatedDelegate onWindowActivated;

	static float nativeWidth = -1;


	public static void Setup(){
		#if UNITY_METRO
		Debug.Log("WinRTInterop -> Setup()");
		nativeWidth = Screen.width;

		WinApplication.windowSizeChanged += (width,height) => {
			bool snapped = (width != nativeWidth);
			if(onWindowResized != null)
				onWindowResized(width,height,snapped);
		};

		WinApplication.windowActivated += (state) => {
			if(onWindowActivated != null)
				onWindowActivated(state);
		};

		onWindowResized += DebugSize;
		#endif
	}

	static void DebugSize(float width,float height,bool isSnapped){

		string snapped = (isSnapped) ? "" : " not";
		string str = "Window resized! Horizontal resolution is now {0} and the window is{1} snapped.";

		Debug.Log(string.Format(str,Screen.width,snapped));
	}


}
