using UnityEngine;

public abstract class Popup : MonoBehaviour
{
    public virtual void Show(float _duration) { }

    public virtual void Hide(float _duration) { }
}