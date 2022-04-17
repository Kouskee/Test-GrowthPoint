using TMPro;
using UnityEngine;
using Common;

public class Counter : MonoBehaviour
{
    private int _points;
    private TMP_Text _txtPoints;

    public void Init(TMP_Text text)
    {
        _txtPoints = text;
    }

    private void Start()
    {
        EventManager.OnFadeFinish += OnFadeFinish;

        EventManager.OnClicked.AddListener(Clicked);
    }

    private void OnFadeFinish(VariousFade swap)
    {
        switch (swap)
        {
            case VariousFade.In:
                DoTween.TextFadeIn(_txtPoints);
                break;
            case VariousFade.Out:
                DoTween.TextFadeOut(_txtPoints);
                break;
        }
    }

    private void Clicked(int points)
    {
        _points = Mathf.Clamp(_points + points, 0, int.MaxValue);
        _txtPoints.text = _points.ToString();
    }

    private void OnDestroy()
    {
        EventManager.OnFadeFinish -= OnFadeFinish;
    }

    public void Reset()
    {
        _points = 0;
        _txtPoints.text = _points.ToString();
        DoTween.TextFadeOut(_txtPoints);
    }
}