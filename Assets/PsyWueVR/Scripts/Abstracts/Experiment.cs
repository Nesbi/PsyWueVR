/* Abstract Experiment class for the Experiment
 * A subclass of this class should be attachted to the same GameObject as the ExperimentController
 * 
 * Made by Nico Balbach for the University of Würzburg
 * version 15-11-2016
 */
using UnityEngine;
using System.Collections.Generic;

public abstract class Experiment : MonoBehaviour
{
	// Name of the experiment
	protected string name = "Experiment";
	// the time the experiment has been started
	protected float startTime;
	// List of all experiment phases
	protected List<ExperimentPhase> phases = new List<ExperimentPhase> ();
	// phase Iterator
	private IEnumerator<ExperimentPhase> phaseIterator;
	// current phase counter
	protected int current;
	// bool about the experiment start (is set automatically)
	[HideInInspector] public bool isStarted = false;
	// boolean about the experiment state (is set automatically after the last pointer, but can be set manually)
	[HideInInspector] public bool isFinished = false;
	// Default GUI (can also be set with 'isBlackout', 'instruction' and 'status')
	[HideInInspector] public GUIDefault guiDefault;
	// boolean about the blackout
	public bool isBlackout = true;
	// the instruction text at the current time
	public string instruction;
	// the experiment status text at the current time
	public string status;
	// boolean if the status should be displayed
	public bool displayStatus = true;
	// the subject of this experiment
	public Subject subject;
	// boolen about pausing the experiment
	public bool pause = true;
	// total time the experiment has paused (to counter the advancing of time while pausing)
	public float pauseTime = 0f;
	// boolean about headtracking the activity of headtracking
	public bool headtrackingActive = true;
	// DataWriter for experiment output
	[HideInInspector] public DataWriter writer;
	// folder for the input and output data
	public string folder = "";
	// name of the input file
	public string input = "input.txt";
	// name of the output file
	public string output = "output.txt";

	// init values of the experiment (executed before init() )
	public virtual void initValues ()
	{
		// e.g. init different values like input/output or name here 
	}

	// init the experiment (inputfile is already read at this point)
	public abstract void init ();

	// starts the experiment
	public void start ()
	{
		phaseIterator = phases.GetEnumerator ();
		startTime = Time.time;
		isStarted = true;
		startIndividual ();
		pause = false;
		writer.open (folder + output);
		writer.writeHeader (name);
	}

	// individual start function to get additional control. executed after read input, before creation of writer
	protected virtual void startIndividual ()
	{
		// e.g dynamic output name (input data like subject number in name)	
	}

	// updates the state of the experiment. returns true until the experiment is finished (should at best not be overwritten)
	public bool update ()
	{
		if (pause) {
			pauseTime += Time.deltaTime;
		} else {
			pauseTime = 0;
		}

		ExperimentPhase phase = phaseIterator.Current;
		if (phase == null || Time.time - pauseTime >= startTime + phase.duration) {
			if (phaseIterator.MoveNext ()) {
				phase = phaseIterator.Current;
				phase.action.Invoke ();
				startTime = Time.time;
				current++;
			} else {
				isFinished = true;
				writer.writeFooter (name);
				writer.close ();
			}
		}

		return !isFinished;
	}
}
