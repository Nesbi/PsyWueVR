/* Helper Class for ExperimentController. Saves a representation and data about the subject.
 * 
 * Made by Nico Balbach for the University of Würzburg
 * version 23-09-2016
 */
using UnityEngine;
using System.Collections.Generic;

// Subject class for the easy access (is loaded by MPI Data Reader)
public class Subject
{
	// Subject representation that represents the current subject (typically loaded as male or female dependend on the Subject data file)
	public SubjectRepresentation representation;
	// Subject Data
	public Dictionary<string,string> data;
}