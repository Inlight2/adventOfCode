using UnityEngine;
using System.Collections;

public class DayOne : MonoBehaviour {

	LineReader reader;
	const char up = '(';

	void Awake() {
		reader = GetComponent<LineReader> ();
	}

	void Start() {
		int count = 0;
		int index = 0;
		foreach (string str in reader.lines) {
			for (int i = 0; i < str.Length; i++) {
				if (str[i].Equals(up)) {
					count++;
				} else {
					count--;
				}

				if (count == -1 && index == 0) {
					index = i + 1;
				}
			}
		}

		Debug.Log (count + "first basement:" + index);
	}
}
