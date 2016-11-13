/* DataWriter for Psychological Experiments with Unity.
 * Based loosly on the script MPIDataWriter provided by the University of Würzburg
 * 
 * Made by Nico Balbach for the University of Würzburg
 * version 16-09-2016
 */

using UnityEngine;
using System.Collections;
using System.IO;

public class DataWriter : MonoBehaviour
{
	private StreamWriter writer;

	public void open (string fileDirectory)
	{
		try {
			writer = new StreamWriter (fileDirectory, true);
		} catch (System.Exception e) {
			print (e.ToString ());
		}
	}

	public void close ()
	{
		if (writer != null) {
			writer.Close ();		
			writer = null;
		} else {
			print ("<color=red>Error</color>: Can't close file, there is no file opened yet.");	
		}
	}

	public void writeHeader (string experiment)
	{
		string currentTime = System.DateTime.Now.ToString ();	

		if (writer != null) {
			writer.WriteLine (">>>>>>>>>>>>>> " + experiment + " >>>>>>>>>>>>>>");
			writer.WriteLine ("\n---- Begin: " + currentTime + " ----");
		} else {
			print ("<color=red>Error</color>: Can't write header, there is no file opened yet.");	
		}
	}

	public void writeFooter (string experiment)
	{
		string currentTime = System.DateTime.Now.ToString ();	

		if (writer != null) {
			
			writer.WriteLine ("---- End: " + currentTime + " ----\n");
			writer.WriteLine ("<<<<<<<<<<<<<< " + experiment + " <<<<<<<<<<<<<<");
		} else {
			print ("<color=red>Error</color>: Can't write footer, there is no file opened yet.");	
		}
	}

	public void writeLine (string line)
	{
		writer.WriteLine (line);
	}

	void OnApplicationQuit ()
	{
		if (writer != null)
			close ();	
	}
}
