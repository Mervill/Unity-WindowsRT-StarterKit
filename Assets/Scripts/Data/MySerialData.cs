using UnityEngine;
using System.Collections;

[System.Serializable]
public class MySerialData {

	[System.Serializable]
	public struct SubStruct{
		public bool optionA;
		public bool optionB;
		public bool optionC;
		public int rate;
	}

	public int someInt = 9001;
	public float someFloat = 3.14159f; // Strangely, LitJSON does not support float serialization by default...
	public string someString = "Pop Culture Refrence";
	public bool someBool = true;

	public Vector3 myVector = Vector3.one;

	public SubStruct mySubStruct = new SubStruct(){
		optionB = true,
		rate = -2
	};

}