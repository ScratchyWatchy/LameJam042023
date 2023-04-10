using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEventRaiser : MonoBehaviour
{
    public UnityEvent onEnterEvent = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        onEnterEvent.Invoke();
        Destroy(gameObject);
    }
}
