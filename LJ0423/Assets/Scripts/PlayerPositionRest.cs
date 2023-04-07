using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionRest : MonoBehaviour
{
    public Transform respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var player = other.gameObject;
        player.GetComponent<Grapple>().DisconnectHook();
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.transform.position = respawnPosition.position;
        player.GetComponent<Rigidbody>().isKinematic = false;
    }
}
