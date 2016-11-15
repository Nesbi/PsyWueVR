using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ExampleExperiment : Experiment
{

	public GUIRate guiRate;

	public override void initValues ()
	{
		name = "Example Experiment";
		input = "input.txt";
	}

	public override void init ()
	{
		//A function can be made with Lamda function ( ()=>{} ) or by calling a void function 

		//Functions/Actions made with Lamda
		phases.Add (new ExperimentPhase (5, ()=>{
			status = "Possibilities";
			instruction = "You can iterate your own methods with next. Execute everything in next or do everything else you please.";
		}));

		phases.Add (new ExperimentPhase (5, ()=>{
			isBlackout = true;
			status = "Blackout";
			instruction = "The blackout can be activated just by setting the boolean \'isBackout\'";
		}));
			
		phases.Add (new ExperimentPhase (1, ()=>{
			isBlackout = false;
			status = "Blackout off";
			instruction = "";
		}));

		phases.Add (new ExperimentPhase (5, ()=>{
			isBlackout = true;
			status = "Instructions";
			instruction = "The instructions can be set by setting the string \'instruction' and the status can be set by setting the string \'status\'";
		}));
	
		phases.Add (new ExperimentPhase (5, ()=>{
			isBlackout = true;
			status = "Walk instructions";
			instruction = "The phase timer can be paused and while different actions accour.\nPlease walk to the red box in the next phase to resume. (<a>,<w>,<s>,<d>)";
		}));
			
		//Functions/Actions made with void Methods
		phases.Add (new ExperimentPhase (1, walkToRead));
		phases.Add (new ExperimentPhase (5, guiDialog));
		phases.Add (new ExperimentPhase (1, rate));
		phases.Add (new ExperimentPhase (5, end));

		status = "Instructions";
		instruction = "Instructions can be declarated here. Please press <Space> to continue.";
	}

	private void walkToRead()
	{
		subject.representation.GetComponent<Movement> ().canMove = true;
		isBlackout = false;
		status = "";
		instruction = "";
		pause = true;
	}

	private void guiDialog ()
	{	
		subject.representation.GetComponent<Movement> ().canMove = false;
		isBlackout = false;
		status = "GUI Dialog";
		instruction = "Different gui elements can be added. For example a rating gui like the following.";
	}

	private void rate ()
	{
		instruction = "";
		status = "";
		isBlackout = false;

		guiRate.Show ();
		guiRate.setHigh ("1 Heigh");
		guiRate.setLow ("0 Low");
		guiRate.setInstructions ("Please rate with <a>/<left> and <d>/<right>. Submit with <space>.");
		guiRate.setStartValue (0.5f);
		guiRate.StartRating ();

		StartCoroutine ("ratePause");
	}

	private IEnumerator ratePause ()
	{
		pause = true;
		//pause until button pressed
		while (!Input.GetButtonDown ("Submit")) {
			//Wait until key pressed
			yield return null;
		}
		float rating = guiRate.EndRating ();
		writer.writeLine ("Enjoyment:" + rating);
		guiRate.Hide ();
		pause = false;

		return false;
	}

	private void end()
	{
		instruction = "Thank you for your patience!";
	}
}
