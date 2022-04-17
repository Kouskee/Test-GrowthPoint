using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Squares
{
    public class Square : MonoBehaviour, ISquare
    {
        private Image _image;
        private SquaresConfig _config;
        private Timer _timer;
        private Color _colorNotClickable;
        private bool _canClick;
        private string _typeSquare;
        private float _widthScreen;

        #region FieldsFLying

        [SerializeField, Range(10, 100)] private float _speed;
        [SerializeField, Range(0, 100)] private float _amplitude;
        [SerializeField, Range(0, 1f)] private float _frequenz;
        private float _xFlying, _yFlying;

        #endregion

        #region FieldsLower

        private int _count;

        private const float DURATION_CHANGE_COLOR = 2f;
        private const float DURATION_BETWEEN_CHANGE_COLOR = 5f;

        #endregion

        public Image Init(SquaresConfig config, string type, bool canClick = true)
        {
            _typeSquare = type;
            _config = config;
            _canClick = canClick;

            TryGetComponent(out _image);
            return _image;
        }

        private void Start()
        {
            _widthScreen = Screen.width;
        }

        public void Click()
        {
            if (!_canClick) return;

            switch (_typeSquare)
            {
                case "Lower":
                    EventManager.OnClicked.Invoke(_config.Points);
                    TimeIsUp();
                    _canClick = false;
                    break;
                case "Flying":
                    EventManager.OnClicked.Invoke(_config.Points);
                    Destroy(gameObject);
                    break;
                default:
                    EventManager.OnClicked.Invoke(_config.Points);
                    break;
            }
        }

        private void Update()
        {
            _timer?.Update(Time.deltaTime);

            if (_typeSquare != "Flying") return;

            MoveSin();
            if (transform.localPosition.x <= -_widthScreen)
                Destroy(gameObject);
        }

        #region Lower Square

        public void Timer(Color color)
        {
            _colorNotClickable = color;
            _timer = new Timer(DURATION_BETWEEN_CHANGE_COLOR);
            _timer.TimeIsUp += TimeIsUp;
        }

        private void TimeIsUp()
        {
            Color color;
            var canClick = false;
            
            if (_count % 2 != 0)
            {
                color = _config.Color;
                canClick = true;
            }
            else
            {
                color = _colorNotClickable;
                _canClick = false;
            }

            _count++;

            DoTween.ChangeColor(_image, color, DURATION_CHANGE_COLOR)
                .OnComplete(() =>
                {
                    _timer.Reset();
                    _canClick = canClick;
                });

            _timer.Stop();
        }

        #endregion

        #region FlyingSquare

        private void MoveSin()
        {
            _xFlying -= _speed * Time.deltaTime;
            _yFlying = Mathf.Sin(_xFlying * _frequenz) * _amplitude;
            transform.localPosition = new Vector2(_xFlying, _yFlying);
        }

        #endregion

        public bool CanCLick
        {
            set => _canClick = value;
        }
    }
}