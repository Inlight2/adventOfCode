using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

enum EditMode
{
	TURN_ON,
	TURN_OFF,
	TOGGLE,
}

public class DaySix : ReadBase {

	int [,] lights = new int[1000,1000];
	EditMode mode;

	delegate void LightAction(ref int[,] arr, int x, int y); 

	void Start () {
		LightAction action = new LightAction (TurnOn);

		foreach (string str in reader.lines) {
			if (Regex.IsMatch (str, "^turn on")) {
				//Debug.LogError ("ON: " + str);
				mode = EditMode.TURN_ON;
			} else if (Regex.IsMatch (str, "^turn off")) {
				//Debug.LogError ("OFF: " + str);
				mode = EditMode.TURN_OFF;
			} else if (Regex.IsMatch (str, "^toggle")) {
				//Debug.LogError ("TOGGLE: " + str);
				mode = EditMode.TOGGLE;
			} else {
				Debug.LogError ("Error getting mode: " + str);
				return;
			}

			Match coordinates = Regex.Match (str, @"(\d+),(\d+) through (\d+),(\d+)");

			int xa, xb, ya, yb;
			xa = int.Parse (coordinates.Groups[1].Captures[0].Value);
			ya = int.Parse (coordinates.Groups[2].Captures[0].Value);

			xb = int.Parse (coordinates.Groups[3].Captures[0].Value);
			yb = int.Parse (coordinates.Groups[4].Captures[0].Value);

			switch (mode) {
			case EditMode.TOGGLE:
				action = Toggle;
				break;
			case EditMode.TURN_ON:
				action = TurnOn;
				break;
			case EditMode.TURN_OFF:
				action = TurnOff;
				break;
			default:
				Debug.LogError ("default error");
				break;
			}

			for (int i = xa; i <= xb; i++) {
				for (int j = ya; j <= yb; j++) {
					action (ref lights, i, j);
				}
			}
		}
		int total = 0;
		foreach (int i in lights) {
			total += i;
		}

		Debug.Log (total + " lights");
	}

	void TurnOff(ref int[,] arr, int x, int y) {
		arr [x, y] -= 1;
		if (arr [x, y] < 0) {
			arr [x, y] = 0;
		}
	}

	void TurnOn(ref int[,] arr, int x, int y) {
		arr [x, y] += 1;
	}

	void Toggle(ref int[,] arr, int x, int y) {
		arr [x, y] += 2;
	}
}
