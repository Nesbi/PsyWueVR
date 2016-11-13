using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class GUIElement : MonoBehaviour {

	public void Show(){
		gameObject.SetActive(true);
	}

	public void Hide(){
		gameObject.SetActive(false);
	}
}
