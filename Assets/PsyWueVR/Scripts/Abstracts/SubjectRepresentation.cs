/* Abstract subject representation in the Unity engine
 * A subclass of this class should be added to a female and male subject
 * 
 * Made by Nico Balbach for the University of Würzburg
 * version 23-09-2016
 */
using UnityEngine;

public abstract class SubjectRepresentation: MonoBehaviour
{
	// Avatar representation of the subject
	public GameObject avatar;
	// Position of the camera for this subject e.g. a child of head
	public GameObject viewPoint;
}
