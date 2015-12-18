using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class DayTwo : ReadBase {

	public string test = "";

	void Start () {
		int[] sides = new int[3];
		int min, total = 0, ribbon = 0;
		foreach (string str in reader.lines) {
			string[] lengthstr = str.Split ('x');
			int[] lengths = new int[3];
			for (int i = 0; i < lengthstr.Length; i++) {
				lengths [i] = int.Parse (lengthstr [i]);
			}
			Array.Sort (lengths);
			//length 0
			//width 1
			//hieght 2
			sides[0] = 2 * lengths [0] * lengths [1];
			sides[1] = 2 * lengths [1] * lengths [2];
			sides[2] = 2 * lengths [2] * lengths [0];

			min = Mathf.Min (sides);
			ribbon += lengths [0]*2 + lengths [1]*2 + (lengths [0] * lengths [1] * lengths [2]);
			total += sides.Sum () + (min/2);

		}

		Debug.Log ("Paper:" + total);
		Debug.Log ("Ribbon:" + ribbon);
	}
}
