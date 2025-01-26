using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationUnityEvent : MonoBehaviour
{
    public UnityEvent UnityEvent = null;
    public void ActivateEvent()
    {
        UnityEvent.Invoke();
    }
}
