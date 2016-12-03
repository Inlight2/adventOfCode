using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class DayEight : ReadBase {

	void Start() {
		int characters = 0;
		foreach(string stri in reader.lines) {
			string str = stri.Substring (1, stri.Length - 2);
			str = Regex.Replace (str, "\\\\\"", "@");
			str = Regex.Replace (str, "\\\\\\\\", "?");
			str = Regex.Replace (str, @"\\x[0-9a-f]{2,4}", @"^");
			Debug.Log (str);
			characters += stri.Length - str.Length;
		}

		Debug.Log (characters);
	}
}
