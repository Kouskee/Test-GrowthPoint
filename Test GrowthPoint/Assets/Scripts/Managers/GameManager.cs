using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Counter _counter;
    private PlayMode _playMode;
    private SpawnSquares _spawnSquares;
    private Timer _timer;

    private const float DURATION = 5f;

    public void Init(Counter counter, PlayMode playMode, SpawnSquares spawnSquares)
    {
        _counter = counter;
        _playMode = playMode;
        _spawnSquares = spawnSquares;
    }

    private void Start()
    {
        _timer = new Timer(DURATION);
        _timer.TimeIsUp += TimeIsUp;
    }

    private void TimeIsUp()
    {
        _spawnSquares.Reset();
        _counter.Reset();
        _playMode.Reset();
        _timer.Stop();
    }

    private void Update()
    {
        _timer?.Update(Time.deltaTime);
        if (Input.touchCount > 0 || Input.GetMouseButton(0)) _timer.Reset();
    }
}