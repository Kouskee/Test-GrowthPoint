using System;
using System.Collections.Generic;
using System.Linq;
using Squares;
using UnityEngine;
using Common;
using DG.Tweening;
using Random = System.Random;

public class SpawnSquares : MonoBehaviour
{
    private Dictionary<string, SquaresConfig> _configs;
    private Square _square;
    private RectTransform _parentLower;
    private RectTransform _spawnFlying;
    private Timer _timer;
    private Random _random;

    private List<Square> _squaresLower;
    private List<Square> _squaresFLying;
    private Array _colorsFlying;

    private const float TIME_SPAWN = 2.5f;
    private const float DURATION_CHANGE_COLOR = 2f;
    private const int COUNT_LOWER = 4;

    public void Init(Dictionary<string, SquaresConfig> configs, Square square, RectTransform parentLower, RectTransform spawnFlying)
    {
        _configs = configs;
        _square = square;
        _parentLower = parentLower;
        _spawnFlying = spawnFlying;
    }

    private void Awake()
    {
        _squaresLower = new List<Square>(COUNT_LOWER);
        _squaresFLying = new List<Square>();
        _random = new Random();
        _colorsFlying = Enum.GetValues(typeof(ColorsFlying));
    }

    private void Start()
    {
        EventManager.OnStart.AddListener(OnStart);
    }

    private void OnStart()
    {
        _timer = new Timer(TIME_SPAWN);
        _timer.TimeIsUp += SpawnFlying;
        
        SpawnLower();
    }

    private void SpawnLower()
    {
        for (var i = 0; i < COUNT_LOWER; i++)
        {
            var square = Instantiate(_square, _parentLower);
            var image = square.Init(_configs[ColorsLower.Green.ToString()], "Lower", false);
            image.color = _configs[ColorsLower.Purple.ToString()].Color;
            DoTween.ChangeColor(image, _configs[ColorsLower.Green.ToString()].Color, DURATION_CHANGE_COLOR).OnComplete(() =>
            {
                square.CanCLick = true;
                square.Timer(_configs[ColorsLower.Purple.ToString()].Color);
            });
            _squaresLower.Add(square);
        }
    }


    private void SpawnFlying()
    {
        var randomColor = (ColorsFlying)_colorsFlying.GetValue(_random.Next(_colorsFlying.Length));
        
        var square = Instantiate(_square, _spawnFlying);
        var image = square.Init(_configs[randomColor.ToString()], "Flying");
        image.color = _configs[randomColor.ToString()].Color;

        _squaresFLying.Add(square);
        
        _timer.Stop();
        _timer.Reset();
    }

    public void Reset()
    {
        foreach (var square in _squaresLower)
        {
            Destroy(square.gameObject);
        }
        foreach (var square in _squaresFLying.Where(square => square != null))
        {
            Destroy(square.gameObject);
        }
        
        _squaresFLying.Clear();
        _squaresLower.Clear();

        _timer.TimeIsUp -= SpawnFlying;
    }

    private void Update()
    {
        _timer?.Update(Time.deltaTime);
    }
}