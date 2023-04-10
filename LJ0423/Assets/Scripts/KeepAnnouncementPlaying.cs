using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KeepAnnouncementPlaying : MonoBehaviour
{
    private double pauseTime = 0;
    private double timelineTime;
    private PlayableDirector _director;

    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }

    private void OnDisable()
    {
        if(_director.time == 0 || _director.time >= _director.duration) return;
        pauseTime = Time.time;
        timelineTime = _director.time;
    }

    public void OnEnable()
    {
        if(pauseTime == 0) return;
        _director.time = timelineTime + Time.time - pauseTime;
        _director.Play();
    }
}
