using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace yearTwo
{
	enum Direction
	{
		NORTH = 0,
		EAST = 1,
		SOUTH = 2,
		WEST = 3,
	};

	public class DayOne : ReadBase {

		Direction dir = Direction.NORTH;
		Point p = new Point(0,0);
		Dictionary<string, int> visitCount = new	Dictionary<string, int>();

		// Use this for initialization
		void Start () {
			//Calculate the path and ending point
			foreach (string str in reader.lines) {
				if (str [0].Equals ('R')) {
					TurnRight ();
				} else {
					TurnLeft ();
				}

				if (MoveSingle (int.Parse (str.Substring(1)))) {
						Debug.Log ("First crossed position at: " + p.ToString ());
						Debug.Log("Distance from start is :" + (Mathf.Abs(p.x) + Mathf.Abs(p.y)));
						return;
				}
				Debug.Log ("Moved "+ dir + " to " + p.ToString ());
			}

			//determine distance from starting position
			Debug.Log("Final position: " + p.ToString());
			Debug.Log("Distance from start is :" + (Mathf.Abs(p.x) + Mathf.Abs(p.y)));
		}

		void TurnLeft() {
			dir--;
			if (dir < Direction.NORTH) {
				dir = Direction.WEST;
			}
		}

		void TurnRight() {
			dir++;
			if (dir > Direction.WEST) {
				dir = Direction.NORTH;
			}
		}

		void Move(int num) {
			switch (dir) {

			case Direction.NORTH:
				p.y += num;
				break;
			case Direction.EAST:
				p.x += num;
				break;
			case Direction.SOUTH:
				p.y -= num;
				break;
			case Direction.WEST:
				p.x -= num;
				break;

			default:
				break;
			}
		}

		bool MoveSingle(int num) {

			for(int i = 0; i < num; i++) {

				switch (dir) {

				case Direction.NORTH:
					p.y++;
					break;
				case Direction.EAST:
					p.x++;
					break;
				case Direction.SOUTH:
					p.y--;
					break;
				case Direction.WEST:
					p.x--;
					break;

				default:
					break;
				}

				if (VisitPosition ())
					return true;
			}
			return false;
		}

		bool VisitPosition() {
			int count;
			if (visitCount.TryGetValue (p.ToString(), out count)) {
				visitCount [p.ToString ()] = count++;
				return true;
			}

			visitCount.Add(p.ToString(), 1);

			return false;
		}
	}
}