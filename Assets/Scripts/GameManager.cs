// WindowsRT Starter Kit
// Victory Square Games
// Distributed under the Boost Software License, Version 1.0.
// (See accompanying LICENSE file or a copy at http://www.boost.org/LICENSE_1_0.txt)

using UnityEngine;
using System.Collections;
using System.Reflection;

using LitJson;

public class GameManager : MonoBehaviour {

	string serialJSON = string.Empty;
	MySerialData deserialData;

	void Awake(){
#if !UNITY_EDITOR
		// If we get this far, we have at least started up correctly.
		Debug.Log("WindowsRT Starter Kit ---------- ---------- ---------- ---------- ---------- ----------");
#endif

		#if UNITY_METRO
		//LitJson.UnityTypeBindings.Register(); // Usually happens automaticly, not on WinRT for some reason...
		#endif
		WinRTInterop.Setup();

		// Print some reflection data to the 
		// console to see if the function exists.
		FieldInfo[] dataInfo = typeof(MySerialData).GetFields();
		Debug.Log("Reflected FieldInfo from MySerialData:");
		foreach(FieldInfo inf in dataInfo){
			Debug.Log(string.Format("{0} ({1})",inf.Name,inf.FieldType.Name));
		}

	}

	void OnGUI(){
		Rect r = new Rect(20,20,(Screen.width/3)-10,(Screen.height)-10);
		GUILayout.BeginArea(r);

		// Did you know: Using @"" allows you to type freely,
		// the string will have all special characters (\n, \t etc)
		// preserved.
		GUILayout.Label(@"WindowsRT Starter Kit
Victory Square Inc
MIT/Boost");

		//--

		GUILayout.Label("LitJson");
		if(GUILayout.Button("Serialize MySerialData")){
			MySerialData myData = new MySerialData();

			JsonWriter jwriter = new JsonWriter();
			jwriter.PrettyPrint = true;

			JsonMapper.ToJson(myData,jwriter);
			serialJSON = jwriter.ToString();

			// If you don't want to use PrettyPrint
			// you can serialize objects with:
			// serialJSON = JsonMapper.ToJson(myData);
		}
		if(serialJSON != string.Empty) GUILayout.Label(serialJSON);

		GUI.enabled = serialJSON != string.Empty;
		if(GUILayout.Button("Deserialize")){
			deserialData = JsonMapper.ToObject<MySerialData>(serialJSON);
		}
		if(deserialData != null) GUILayout.Label("MySerialData deserialized!");
		GUI.enabled = true;

		//---

		GUILayout.EndArea();
	}

}
