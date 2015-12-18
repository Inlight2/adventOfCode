using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public abstract class Gate {
	public abstract int Resolve();
}

public class AndGate : Gate {
	string _A;
	string _B;

	public AndGate(string A, string B) {
		_A = A;
		_B = B;
	}

	public override int Resolve() {
		Gate ga, gb;
		int va, vb;
		va = DaySeven.gates.TryGetValue (_A, out ga) ? ga.Resolve() : int.Parse(_A);
		vb = DaySeven.gates.TryGetValue (_B, out gb) ? gb.Resolve() : int.Parse(_B);
		return va & vb;
	}
}

public class OrGate : Gate {
	string _A;
	string _B;

	public OrGate(string A, string B) {
		_A = A;
		_B = B;
	}

	public override int Resolve() {
		Gate ga, gb;
		int va, vb;
		va = DaySeven.gates.TryGetValue (_A, out ga) ? ga.Resolve() : int.Parse(_A);
		vb = DaySeven.gates.TryGetValue (_B, out gb) ? gb.Resolve() : int.Parse(_B);
		return va & vb;
	}
}

public class LeftShiftGate : Gate {
	string _A;
	int value;

	public LeftShiftGate(string A, int v) {
		this._A = A;
		this.value = v;
	}

	public override int Resolve() {
		Gate ga;
		int va;
		va = DaySeven.gates.TryGetValue (_A, out ga) ? ga.Resolve() : int.Parse(_A);

		return va << value;
	}
}

public class RightShiftGate : Gate {
	string _A;
	int value;

	public RightShiftGate(string A, int v) {
		_A = A;
		value = v;
	}

	public override int Resolve() {
		Gate ga;
		DaySeven.gates.TryGetValue (_A, out ga);

		return ga.Resolve () >> value; 
	}
}

public class NotGate : Gate {
	string _A;

	public NotGate(string A) {
		_A = A;
	}

	public override int Resolve ()
	{
		Gate ga;
		DaySeven.gates.TryGetValue (_A, out ga);
		return ~ga.Resolve();
	}
}

public class ValueGate : Gate {
	int value;

	public ValueGate(int v) {
		value = v;
	}

	public override int Resolve () {
		return value;
	}
}

public class Wire : Gate {
	string _A;

	public Wire(string A) {
		_A = A;
	}

	public override	int Resolve () {
		Gate ga;
		DaySeven.gates.TryGetValue (_A, out ga);
		return ga.Resolve ();
	}
}
	

public class DaySeven : ReadBase {
	[SerializeField]
	public static Dictionary<string, Gate> gates = new Dictionary<string, Gate>();

	void Start () {
		foreach (string str in reader.lines) {
			if (str.Contains ("NOT")) {
				Not (str);
			} else if (str.Contains ("AND")) {
				And (str);
			} else if (str.Contains ("OR")) {
				Or (str);
			} else if (str.Contains ("LSHIFT")) {
				LShift (str);
			} else if (str.Contains ("RSHIFT")) {
				RShift (str);
			} else if (Regex.IsMatch (str, @"^(\d+) -> (\w+)")) {
				Value (str);
			} else {
				Wire (str);
			}
		}

		Gate finalGate;
		gates.TryGetValue("a", out finalGate);

		Debug.Log (finalGate.Resolve ());
			
	}

	void Not(string str) {
		string regex = @"^[A-Z]+ (\w+) -> (\w+)";
		Match matches = Regex.Match (str, regex);

		string a, key;

		a = matches.Groups [1].Captures [0].Value;
		key = matches.Groups [2].Captures [0].Value;

		NotGate gate = new NotGate (a);
		gates.Add (key, gate);
	}

	void And(string str) {
		string regex = @"^(\w+) [A-Z]+ (\w+) -> (\w+)";
		Match matches = Regex.Match (str, regex);

		string a, b, key;

		a = matches.Groups [1].Captures [0].Value;
		b = matches.Groups [2].Captures [0].Value;
		key = matches.Groups [3].Captures [0].Value;

		AndGate gate = new AndGate (a, b);
		gates.Add (key, gate);
	}

	void Or(string str) {
		string regex = @"^(\w+) [A-Z]+ (\w+) -> (\w+)";
		Match matches = Regex.Match (str, regex);

		string a, b, key;

		a = matches.Groups [1].Captures [0].Value;
		b = matches.Groups [2].Captures [0].Value;
		key = matches.Groups [3].Captures [0].Value;

		OrGate gate = new OrGate (a, b);
		gates.Add (key, gate);
	}

	void LShift(string str) {
		string regex = @"^(\w+) [A-Z]+ (\d+) -> (\w+)";
		Match matches = Regex.Match (str, regex);

		string a, key;
		int shiftAmount;

		a = matches.Groups [1].Captures [0].Value;
		shiftAmount = int.Parse (matches.Groups [2].Captures [0].Value);
		key = matches.Groups [3].Captures [0].Value;

		LeftShiftGate gate = new LeftShiftGate (a, shiftAmount);
		gates.Add (key, gate);
	}

	void RShift(string str) {
		string regex = @"^(\w+) [A-Z]+ (\d+) -> (\w+)";
		Match matches = Regex.Match (str, regex);

		string a, key;
		int shiftAmount;

		a = matches.Groups [1].Captures [0].Value;
		shiftAmount = int.Parse (matches.Groups [2].Captures [0].Value);
		key = matches.Groups [3].Captures [0].Value;

		RightShiftGate gate = new RightShiftGate (a, shiftAmount);
		gates.Add (key, gate);
	}

	void Value(string str) {
		
		string regex = @"^(\d+) -> (\w+)";
		Match matches = Regex.Match (str, regex);

		int value;
		string key;

		value = int.Parse (matches.Groups [1].Captures [0].Value);
		key = matches.Groups [2].Captures [0].Value;

		ValueGate gate = new ValueGate (value);
		gates.Add (key, gate);
	}

	void Wire(string str) {
		string regex = @"^(\w+) -> (\w+)";
		Match matches = Regex.Match (str, regex);

		string a, key;

		a = matches.Groups [1].Captures [0].Value;
		key = matches.Groups [2].Captures [0].Value;

		Wire gate = new Wire (a);
		gates.Add (key, gate);
	}
}
