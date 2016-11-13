/* Helper Class for ExperimentController. Saves a duration and action (method/function) as Experiment Phases.
 * 
 * Made by Nico Balbach for the University of Würzburg
 * version 23-09-2016
 */
using UnityEngine;
using System;

public class ExperimentPhase
{
	// Duration of the current experiment phase
	public float duration{ get; private set; }

	//
	public Action action{ get; private set; }

	public ExperimentPhase (float duration, Action action)
	{
		this.duration = duration;
		this.action = action;
	}
}