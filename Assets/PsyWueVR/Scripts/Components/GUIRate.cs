using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIRate : GUIElement
{

	public Slider guiSlider;
	public Text guiInstructions;
	public Text guiHigh;
	public Text guiLow;
	public bool isStarted = false;
	private float value;
	public int questionID = 0;

	void Update ()
	{
		if (isStarted) {
			float valueDelta = Input.GetAxis ("Horizontal") * Time.deltaTime * 1.0f;
			float newValue = value + valueDelta;
			if (newValue > 1)
				newValue = 1;
			value = newValue;
			setSlider (value);
		}
	}

	public void setInstructions (string instructions)
	{
		guiInstructions.text = instructions;
	}

	public void setHigh (string high)
	{
		guiHigh.text = high;
	}

	public void setLow (string low)
	{
		guiLow.text = low;
	}

	public void setStartValue (float startValue)
	{
		if (!isStarted) {
			value = startValue;
		} else {
			print ("Start value can't be set. Rating already started");
		}
	}

	private void setSlider (float value)
	{
		if (value > 1)
			value = 1f;
		guiSlider.value = value;
	}

	public void StartRating ()
	{
		isStarted = true;
		questionID++;
	}

	public float EndRating ()
	{
		isStarted = false;
		return value;
	}

}
