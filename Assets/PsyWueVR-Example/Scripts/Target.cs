using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public Experiment experiment;

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.gameObject.CompareTag ("Player")) {
			experiment.GetComponent<ExampleExperiment> ().pause = false;		
		}
	}

	void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.CompareTag ("Player")) {
			experiment.GetComponent<Experiment> ().pause = false;		
		}
	}
}
