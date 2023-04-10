using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTimeCalculator : MonoBehaviour
{
    private float levelStartTime;
    private AudioSource _source;

    private void Awake()
    {
        levelStartTime = Time.time;
        _source = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {

    }

    public void OnEnable()
    {
        var goalTime = ((Time.time - levelStartTime) % _source.clip.length) ;
        _source.time = goalTime;
    }
}
