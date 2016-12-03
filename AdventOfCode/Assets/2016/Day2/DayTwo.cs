using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace yearTwo
{
	public class DayTwo : ReadBase {

		int curButton = 5;

		void Start () {
			foreach (string str in reader.lines) {
				foreach (char chr in str) {
					if(chr.Equals('U')){
						Up();
					} else if(chr.Equals('D')){
						Down();
					} else if(chr.Equals('L')){
						Left();
					} else if(chr.Equals('R')){
						Right();
					}
					//Debug.Log(chr.ToString() + " to " + curButton);
				}
				Debug.Log("EndPoint:" + curButton);
			}
		}

		void Up() {
			int t = curButton;
			curButton -= 3;
			if( curButton < 1) {
				curButton = t;
			}
		}

		void Down() {
			int t = curButton;
			curButton += 3;
			if(curButton > 9) {
				curButton = t;
			}
		}

		void Left() {
			curButton--;
			if( curButton < 1) {
				curButton = 1;
			} else if( curButton == 3) {
				curButton = 4;
			} else if( curButton == 6) {
				curButton = 7;
			}
		}

		void Right() {
			curButton++;
			if( curButton == 4) {
				curButton = 3;
			} else if( curButton == 7) {
				curButton = 6;
			} else if( curButton > 9) {
				curButton = 9;
			}
		}
	}
}
