using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

public class DayFour : MonoBehaviour {
	
	void Start () {
		string source = "ckczppom";
		int number = 1;
		MD5 hash = MD5.Create ();

		while (true) {
			string result = GetMd5Hash (hash, source + number);
			if (CheckHash (result)) {
				Debug.Log (number);
				return;
			} else {
				number++;
				continue;
			}
		}
	}

	bool CheckHash(string result){
		for (int i = 0; i < 6; i++) {
			if (!result [i].Equals ('0')) {
				return false;
			}
		}

		return true;
	}

	//grabbed from msdn.microsoft.com
	string GetMd5Hash(MD5 md5Hash, string input) {
		byte[] data = md5Hash.ComputeHash (Encoding.UTF8.GetBytes (input));
		StringBuilder sBuilder = new StringBuilder ();

		for (int i = 0; i < data.Length; i++) {
			sBuilder.Append (data [i].ToString ("x2"));
		}

		return sBuilder.ToString ();
	}
}
