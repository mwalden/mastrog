using System;
using System.Timers;
public class TimerController
{
	Timer aTimer;
	Action timesUp;
	public TimerController (int timeInSeconds,  Action timesUp)
	{
		this.timesUp = timesUp;
		aTimer = new System.Timers.Timer();
		aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
		aTimer.Interval=timeInSeconds;
		aTimer.Enabled = true;
	}

	private void OnTimedEvent(object source, ElapsedEventArgs e){
		aTimer.Enabled = false;
		timesUp ();

	}
}


