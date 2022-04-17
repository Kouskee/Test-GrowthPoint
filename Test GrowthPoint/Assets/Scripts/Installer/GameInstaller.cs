using System.Collections.Generic;
using Squares;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameInstaller : MonoBehaviour
{
    [Header("Classes")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Counter _counter;
    [SerializeField] private Square _square;
    [SerializeField] private PlayMode _playMode;
    [SerializeField] private SpawnSquares _spawnSquares;
    [Header("Settings")]
    [SerializeField] private TMP_Text _txtPoints;
    [SerializeField] private TMP_Text _txtPlayMode;
    [SerializeField] private RectTransform _parentLower;
    [SerializeField] private RectTransform _spawnFlying;

    private Dictionary<string, SquaresConfig> _configs = new Dictionary<string, SquaresConfig>();

    private void Awake()
    {
        var squaresConfigs = Resources.LoadAll<SquaresConfig>("Squares");
        foreach (var squaresConfig in squaresConfigs)
        {
            _configs.Add(squaresConfig.Id, squaresConfig);
        }
    }

    private void Start()
    {
        Install();
    }

    private void Install()
    {
        var counter = Instantiate(_counter);
        var playMode = Instantiate(_playMode);
        var spawnSquares = Instantiate(_spawnSquares);
        
        counter.Init(_txtPoints);
        playMode.Init(_txtPlayMode);
        spawnSquares.Init(_configs, _square, _parentLower, _spawnFlying);

        _gameManager.Init(counter, playMode, spawnSquares);
    }

}
