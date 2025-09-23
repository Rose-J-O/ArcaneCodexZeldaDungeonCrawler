using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _animationTriggeredEvent;

    public void TriggerEvent()
    {
        _animationTriggeredEvent?.Invoke();
    }
}
