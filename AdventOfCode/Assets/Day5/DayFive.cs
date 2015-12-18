using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DayFive : ReadBase {

	List<string> strs;

	void Start () {
		strs = reader.lines;
		int niceStrings = 0;

		foreach (string str in strs) {
			if (IsNice (str)) {
				niceStrings++;
			}
		}

		Debug.Log (niceStrings);
	}

	bool IsNice(string str) {
		int vowelCount = 0;
		bool doubleLetter = false;
		bool disallowedString = false;

		List<char> vowels = new List<char>{ 'a', 'e', 'i', 'o', 'u' };
		List<string> disallow = new List<string>{ "ab", "cd", "pq", "xy" };

		for (int i = 0; i < str.Length; i++) {
			//count vowels
			if (vowels.Contains (str [i])) {
				vowelCount++;
			}
				
			if (i < str.Length - 1) {
				//check for double letters
				if (str [i] == str [i + 1]) {
					doubleLetter = true;
				}

				//check for disallowed sets
				if (disallow.Contains(str[i].ToString() + str[i+1].ToString())) {
					disallowedString = true;
				}
			}
		}
		//Debug.Log (str);
		//Debug.Log(vowelCount + " : " + doubleLetter + " : " + disallowedString);

		return vowelCount >= 3 && doubleLetter && !disallowedString;
	}

	bool IsExtraNice(string str) {
		//uuuuggggghhhhhh don't make me learn regex
		return false;
	}
}
