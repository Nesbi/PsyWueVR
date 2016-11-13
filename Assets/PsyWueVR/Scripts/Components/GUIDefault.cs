using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIDefault : GUIElement {

	public Text guiInstructions;
	public Text guiStatus;
	public RawImage guiBlackout;

	public void setInstructions(string instructions){
		guiInstructions.text = instructions;
	}

	public void setStatus(string status){
		guiStatus.text = status;
	}

	public void setBlackOut(bool blackout)
	{
		if (blackout)
		{
			// Change UI
			guiStatus.color = Color.white;
			guiInstructions.color = Color.white;
			guiBlackout.CrossFadeAlpha(1,.5f,false);
		}
		else
		{
			// Change UI
			guiStatus.color = Color.black;
			guiInstructions.color = Color.black;
			guiBlackout.CrossFadeAlpha(0,.5f,false);
		}
	}
}
