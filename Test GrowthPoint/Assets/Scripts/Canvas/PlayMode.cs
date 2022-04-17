using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Common;

public class PlayMode : MonoBehaviour
{
    private TMP_Text _txtPlayMode;

    public void Init(TMP_Text txtPlayMode)
    {
        _txtPlayMode = txtPlayMode;
    }

    private void Start()
    {
        StartCoroutine(WaitPressOnScreen());
    }

    private IEnumerator WaitPressOnScreen()
    {
        while (Input.touchCount <= 0 && !Input.GetMouseButton(0))
            yield return null;

        
        DoTween.TextFadeOut(_txtPlayMode).OnComplete(() =>
        {
            EventManager.OnFadeFinish.Invoke(VariousFade.In);
            EventManager.OnStart.Invoke();
        });
    }

    public void Reset()
    {
        DoTween.TextFadeIn(_txtPlayMode).OnComplete(() => EventManager.OnFadeFinish.Invoke(VariousFade.Out));
        StartCoroutine(WaitPressOnScreen());
    }
}
