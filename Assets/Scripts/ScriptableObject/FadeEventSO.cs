using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color, float, bool> onEventRaised;


    /// <summary>
    /// Turn dark
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        RaiseEvent(Color.black, duration, true);
    }

    /// <summary>
    /// Turn transparent
    /// </summary>
    /// <param name="duration"></param>
    public void FadeOut(float duration)
    {
        RaiseEvent(Color.clear, duration, false);
    }


    public void RaiseEvent(Color target, float duration, bool fadeIn)
    {
        onEventRaised?.Invoke(target, duration, fadeIn);
    }
}
