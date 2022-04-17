using System;

public class Timer
{
    public event Action TimeIsUp;

    private readonly float _time;

    private float _passedTime;
    private bool _isStop;
   
    public Timer(float time)
    {
        _time = time;
    }
    public void Update(float deltaTime)
    {
        if(_isStop) return;
        
        _passedTime += deltaTime;
        if (_passedTime >= _time)
            TimeIsUp?.Invoke();
    }
    public void Reset()
    {
        _passedTime = 0;
        _isStop = false;
    }

    public void Stop()
    {
        _isStop = true;
    }
}
