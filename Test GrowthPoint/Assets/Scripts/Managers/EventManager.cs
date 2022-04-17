using System;
using Common;
using UnityEngine.Events;

public class EventManager
{
    public static readonly UnityEvent<int> OnClicked = new UnityEvent<int>();
    public static readonly UnityEvent OnStart = new UnityEvent();
    
    public static Action<VariousFade> OnFadeFinish;
}
