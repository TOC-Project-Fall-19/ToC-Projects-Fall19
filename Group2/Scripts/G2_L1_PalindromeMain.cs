using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class G2_L1_PalindromeMain
{
	Dictionary<string, string> rules;
	int[] finalState;
	int state;
	int position;
	string Input;

	public G2_L1_PalindromeMain(string Input, Dictionary<string, string> rules, int[] finalState)
	{
		this.Input = Input;
		this.rules = rules;
		this.finalState = finalState;
		state = 0;
		position = 3;
	}

	private string GetSymbol()
	{
		return Input[position].ToString();
	}

	private void AddSymbol(){
		Input += "d";
	}

	public void WorkMachine(ref bool error, ref char dir, ref int state, ref bool end, ref char text)
	{

        
        if (rules.ContainsKey(state + GetSymbol()))
		{
			dir = rules[state + GetSymbol()][1];
			this.state = int.Parse(rules[state + GetSymbol()].Substring(2,rules[state + GetSymbol()].Length - 2).ToString());
			text = rules[state + GetSymbol()][0];
			state = this.state;

			if(Input[position] == 'd' && text != 'd')
            {
				AddSymbol();
				//add = true;
			}

			StringBuilder remplace = new StringBuilder(Input);
			remplace[position] = text;
			Input = remplace.ToString();

			if (finalState.Contains(state))
				end = true;

			if (dir == 'R')
				position++;

			if (dir == 'L')               
				position--;
		}
		else {
			error = true;
		}
	}
}
