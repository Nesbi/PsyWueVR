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
		phases.Add (new ExperimentPhase (8, showPossibilities));
		phases.Add (new ExperimentPhase (8, blackOut));
		phases.Add (new ExperimentPhase (8, showInstruction));
		phases.Add (new ExperimentPhase (8, endNote));
		phases.Add (new ExperimentPhase (1, rate));
		phases.Add (new ExperimentPhase (8, end));

		// GUI
		guiRate.Hide ();

		status = "Instructions";
		instruction = "Instructions can be declarated here.";
	}

	protected override void startIndividual ()
	{
		isBlackout = false;
		status = "Started";
		instruction = "";
	}


	private void showPossibilities ()
	{
		status = "Possibilities";
		instruction = "You can iterate your own methods with next. Execute everything in next or do everything else you please.";
	}

	private void blackOut ()
	{
		isBlackout = true;
		status = "Blackout";
		instruction = "The blackout can be activated just by setting the boolean \'isBackout\'";
	}

	private void showInstruction ()
	{
		isBlackout = true;
		status = "Instructions";
		instruction = "The instructions can be set by setting the string \'instruction' and the status can be set by setting the string \'status\'";
	}

	private void endNote ()
	{
		isBlackout = true;
		status = "End note";
		instruction = "And now have fun!";
	}

	private void end ()
	{
		isBlackout = false;
		status = "";
		instruction = "This is the end. Thank you for your patience!";
	}

	private void rate ()
	{
		instruction = "";
		status = "";
		isBlackout = false;

		guiRate.Show ();
		guiRate.setHigh ("1 Heigh");
		guiRate.setLow ("0 Low");
		guiRate.setInstructions ("And this is a GUI element for rating. Please rate with <a>/<left> and <d>/<right>. Submit with <space>.");
		guiRate.setStartValue (0.5f);
		guiRate.StartRating ();

		StartCoroutine ("ratePause");
	}

	private IEnumerator ratePause ()
	{
		pause = true;
		//pause until button pressed
		while (!Input.GetButtonDown ("ButtonOK")) {
			//Wait until key pressed
			yield return null;
		}
		float rating = guiRate.EndRating ();
		writer.writeLine ("Enjoyment:" + rating);
		guiRate.Hide ();
		pause = false;

		return false;
	}

}
