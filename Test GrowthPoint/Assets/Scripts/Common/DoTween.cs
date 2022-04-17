using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoTween
{
    private const float DURATION = 0.5f;

    public static Sequence TextFadeIn(TMP_Text text)
    {
        if (text == null) return null;
        
        var sequence = DOTween.Sequence();

        text.enabled = true;
        sequence.Append(text.DOFade(1, DURATION));

        return sequence;
    }

    public static Sequence TextFadeOut(TMP_Text text)
    {
        if (text == null) return null;
        
        var sequence = DOTween.Sequence();

        sequence.Append(text.DOFade(0, DURATION));
        sequence.AppendCallback(() => text.enabled = false);

        return sequence;
    }

    public static Sequence ChangeColor(Image image, Color colorEnd, float duration)
    {
        if (image == null) return null;
        
        var sequence = DOTween.Sequence();
        
        sequence.Append(image.DOColor(colorEnd, duration));

        return sequence;
    }
}