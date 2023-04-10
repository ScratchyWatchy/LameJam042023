using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorPlayOnAwakeOneShot : MonoBehaviour
{
    private bool played = false;
    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        if (!played)
        {
            played = true;
            _playableDirector.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
