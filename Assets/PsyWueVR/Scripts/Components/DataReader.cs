/* DataReader for Psychological Experiments with Unity.
 * 
 * Made by Nico Balbach for the University of Würzburg
 * version 15-11-2016
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataReader : MonoBehaviour
{
	public bool dumbToConsole = false;
	private StreamReader reader;

	public Dictionary<string,string> read (string fileDirectory)
	{
		Dictionary<string,string> data = new Dictionary<string, string> ();

		try {
			reader = new StreamReader (fileDirectory);
			if (dumbToConsole)
				print ("<color=blue>DataReader</color>: opening file '" + fileDirectory + "'");	

			// Read data untill null (end of stream)
			string curLine = null;
			do {
				curLine = reader.ReadLine ();
				if (curLine != null) {
					// Delete comments
					string[] lineData = curLine.Split ('#');

					// Split Data
					lineData = lineData [0].Split ('=');

					// Read line
					// lineData.length == 2 => valid data
					// lineData.length == 0 => empty line
					// else invalid line
					if (lineData.Length == 2) {
						data.Add (lineData [0], lineData [1]);

						if (dumbToConsole)
							Debug.Log ("<color=blue>Info:</color> Line Data found: [" + lineData [0] + "] => [" + lineData [1] + "]");
					} else if (lineData.Length != 0) {
						if (dumbToConsole)
							Debug.Log ("<color=blue>Info:</color> Line has no Data: \'" + curLine + "\'");
					}
				}

			} while(curLine != null);
									
			reader.Close ();
			if (dumbToConsole)
				print ("<color=blue>DataReader</color>: closing file");	
			
		} catch (System.Exception e) {
			print (e.ToString ());
		}

		return data;
	}

	public bool doesFileExist (string fileDirectory)
	{
		bool fileexists;
		try {
			StreamReader testReader = new StreamReader (fileDirectory);
			fileexists = true;
		} catch (System.IO.FileNotFoundException  e) {
			fileexists = false;
		}

		return fileexists;
	}

}