using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayThree : ReadBase {

	List<Point> visitedPoints = new List<Point>();

	void Start () {
		Point santa = new Point ();
		Point robo = new Point ();
		Point index = santa;
		visitedPoints.Add (new Point ());
		string instructions = reader.lines [0];
		for (int i = 0; i < instructions.Length; i++) {
			switch (instructions [i]) {
			case 'v':
				index.y--;
				break;
			case '^':
				index.y++;
				break;
			case '<':
				index.x--;
				break;
			case '>':
				index.x++;
				break;
			default:
				break;
			}
			if (Overlap (visitedPoints, index)) {
				index = ReferenceEquals (index, santa) ? robo : santa;
				continue;
			} else {
				visitedPoints.Add (index.Copy ());
				index = ReferenceEquals (index, santa) ? robo : santa;
			}
		}

		Debug.Log (visitedPoints.Count);
	}

	bool Overlap(List<Point> points, Point index) {
		foreach (Point p in points) {
			if (p.IsEqual (index)) {
				return true;
			}
		}

		return false;
	}
}
