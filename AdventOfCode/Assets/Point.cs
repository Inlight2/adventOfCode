using UnityEngine;
using System.Collections;

public class Point {
	public int x = 0;
	public int y = 0;

	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public Point() {
		this.x = 0;
		this.y = 0;
	}

	override public string ToString() {
		return x + ", " + y;
	}

	public Point Copy() {
		return new Point (x, y);
	}

	public bool IsEqual(Point pnt) {
		return x == pnt.x && y == pnt.y;
	}
}
