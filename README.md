# PsyWueVR

PsyWueVR is a framework for easy development of psychological experiments in Virtual Reality with Unity.
Developed for the Department of Psychology I at the University of Würzburg.

![unityui](https://cloud.githubusercontent.com/assets/23743591/20633332/a5fb113a-b345-11e6-8696-22c2445bc1df.PNG)

## Installation

* Requirements:
	* Unity 5.0 or higher.
* Create a new or open an existing Unityproject
* Download the unitypackage and run it

![import](https://cloud.githubusercontent.com/assets/23743591/20633345/ba9cb7d8-b345-11e6-85e6-e441f09d73b5.PNG)

## Features

### Create sequencial experiments

``` cs
public class ExampleExperiment : Experiment
{
	[..]

	public override void init ()
	{
		// Exexute method phase1 and wait 10 seconds befor executing next phase
		phases.Add (new ExperimentPhase (10, phase1));
		// Execute phase2 and wait 5 seconds
		phases.Add (new ExperimentPhase (5, phase2));
		// Execute phase 3 and wait 1 second
		phases.Add (new ExperimentPhase (1, phase3));
	}
	
	private void phase1(){[..]}
	private void phase2(){[..]}
	private void phase3(){
		// pause phase counter until unpaused
		pause = true;
		[..]
	}
}
```

### Easy creation of experiments

``` cs
public class ExampleExperiment : Experiment
{

	public override void initValues ()
	{
		[..]
	}

	public override void init ()
	{
		[..]
	}
}

```

### Easy Data Reader/Writer

```
*exampleinput.txt*
gender=m
testdata=xxx
```

``` cs
Debug.Log(subject.representation.data["gender"]);
Debug.Log(subject.representation.data["testdata"]);
```

```
*Console*
m
xxx
```

### Predefined GUI class

Use predefined GUIs or easily create new GUI elements.

``` cs
isBlackout = true;
status = "Status";
instruction = "Instruction";		
```

![gui](https://cloud.githubusercontent.com/assets/23743591/20633344/b8818a5a-b345-11e6-8676-d4e06d957f73.PNG)

### SubjectRepresentation
Subject representation scripts for male and female with easy object access.


``` cs
public class ExampleSubjectRepresentation : SubjectRepresentation {
		public GameObject pen, paper;
}		
```

``` cs
public class ExampleExperiment : Experiment
{
	[..]
	
	private void test(){
		// Acces on subject objects
		GameObject avatar = subject.representation.avatar;
		GameObject pen = subject.representation.pen;
		GameObject paper = subject.representation.paper;
	}
}
```
