/* ExperimentController for Psychological Experiments with Unity.
 * Based loosly on the script ExperimentalControlScript by Jakob Guetschow from 2013/2014 provided by the University of Würzburg
 * 
 * Made by Nico Balbach for the University of Würzburg
 * version 23-09-2016
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent (typeof(DataReader))]
[RequireComponent (typeof(DataWriter))]
// Main Experiment Controller inits and executes the experiment (added to the same gameobject)
public class ExperimentController : MonoBehaviour
{
	//// Basic variables, assigned via the inspector
	// Root of the world (Room etc. without subjects)
	public GameObject worldRoot;
	// GameObject with a SubjectRepresentation Script (avatar etc. of male subject)
	public GameObject maleRepresentation;
	// GameObject with a SubjectRepresentation Script (avatar etc. of male subject)
	public GameObject femaleRepresentation;
	// GameObject with the subjects camera
	public GameObject subjectCamera;
	// Default GUI Elemement
	public GUIDefault guiDefault;

	// Datareader
	private DataReader reader;
	// Datawriter
	private DataWriter writer;

	// Experiment
	private Experiment experiment;

	/// Internal Operation Variables
	private Subject subject;

	// Use this for initialization
	void Start ()
	{
		reader = GetComponent<DataReader> ();
		writer = GetComponent<DataWriter> ();

		// Experiment
		experiment = gameObject.GetComponent<Experiment> ();
		experiment.writer = writer;
		experiment.initValues ();
		initSubject ();
		experiment.subject = subject;
		experiment.init ();
		experiment.guiDefault = guiDefault;
		guiDefault.Show ();

		if (experiment == null)
			Debug.Log ("<color=red>Error:</color> Can't find an attached experiment");
			
		// Execute prestart phase
		setBlackOut (experiment.isBlackout);
		guiDefault.setStatus (experiment.status);
		guiDefault.setInstructions (experiment.instruction);
		toggleHeadtrackingCamera (experiment.headtrackingActive);
	}

	// updates the ExperimentController
	void Update ()
	{
		if (experiment.isStarted && !experiment.isFinished) {
			experiment.update ();
		} else if (Input.GetButtonDown ("Submit")) {
			experiment.start ();
		}

		setBlackOut (experiment.isBlackout);
		if (experiment.displayStatus) {
			guiDefault.setStatus (experiment.status);
		} else {
			guiDefault.setStatus ("");
		}
		guiDefault.setInstructions (experiment.instruction);
		toggleHeadtrackingCamera (experiment.headtrackingActive);
	}

	// inits the subject by loading provided data from the MPI Data Loader
	private void initSubject ()
	{
		subject = new Subject ();
		subject.data = readInput ();

		switch (subject.data ["gender"]) {
		case "m":
			subject.representation = maleRepresentation.GetComponent<SubjectRepresentation> ();
			if (subject.representation == null)
				Debug.Log ("<color=red>Error:</color> Can't find a male subject representation.");
			
			femaleRepresentation.SetActive (false);
			maleRepresentation.SetActive (true);
			break;
		case "f":
			subject.representation = femaleRepresentation.GetComponent<SubjectRepresentation> ();
			if (subject.representation == null)
				Debug.Log ("<color=red>Error:</color> Can't find a female subject representation.");
			
			maleRepresentation.SetActive (false);
			femaleRepresentation.SetActive (true);
			break;
		default:
			Debug.Log ("<color=red>Error:</color> Gender of subject is not defined. ('gender=f' or 'gender=m')");
			break;
		}

		subjectCamera.transform.position = subject.representation.viewPoint.transform.position;
		subjectCamera.transform.rotation = subject.representation.viewPoint.transform.rotation;
		subjectCamera.transform.parent = subject.representation.viewPoint.transform;
	}

	// Sets blackout on (true) or off (false)
	private void setBlackOut (bool blackout)
	{
		if (blackout) {
			// Hide world and subject
			HideCameraLayer ("Default");
		} else {
			// Show world and subject
			ShowCameraLayer ("Default");
		}

		// Change UI
		guiDefault.setBlackOut (blackout);
	}


	public Dictionary<string,string> readInput(){
		Dictionary<string,string> input;
		string direction = experiment.folder + experiment.input;

		if (reader.doesFileExist (direction)) {
			input = reader.read (direction);
		}else{
			// File doesn't exist => create default file with gender male
			writer.open (direction);
			writer.writeLine ("# Default Input file");
			writer.writeLine ("gender=m");
			writer.close ();
			input = reader.read (direction);
		}

		return input;
	}

	public void HideCameraLayer (string layer)
	{
		subjectCamera.GetComponentInChildren<Camera> ().cullingMask &= ~(1 << LayerMask.NameToLayer (layer));
	}

	public void ShowCameraLayer (string layer)
	{
		subjectCamera.GetComponentInChildren<Camera> ().cullingMask |= 1 << LayerMask.NameToLayer (layer);
	}

	public void toggleHeadtrackingCamera (bool isTracking)
	{
		if (isTracking) {
			subjectCamera.transform.localScale = new Vector3 (1f, 1f, 1f);
		} else {
			subjectCamera.transform.localScale = new Vector3 (0.00001f, 0.00001f, 0.00001f);
		}
	}

}