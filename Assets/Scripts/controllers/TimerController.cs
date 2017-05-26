using System;
using System.Timers;
public class TimerController
{
	Timer aTimer;
	Action timesUp;
	public TimerController(Action timesUp){
		this.timesUp = timesUp;
		aTimer = new System.Timers.Timer();
		aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
	}

	public TimerController(){
		aTimer = new System.Timers.Timer();
		aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
	}


	public void setAction(Action timesUp){
		this.timesUp = timesUp;
	}
	public void beginTimer (int timeInSeconds)
	{
		aTimer.Interval=timeInSeconds;
		aTimer.Enabled = true;
	}

	private void OnTimedEvent(object source, ElapsedEventArgs e){
		aTimer.Enabled = false;
		if (timesUp != null)
			timesUp ();
	}

	public void endTimer(){
		aTimer.Enabled = false;
	}
}


