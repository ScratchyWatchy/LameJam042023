using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeSwitcher : MonoBehaviour
{
    public List<Transform> oldObjectParent;
    public List<Transform> currentObjectParent;
    public AudioSource sfxSource;
    public Volume volume;
    public float jumpDelay = 1f;
    public bool isLocked = false;

    private bool isInCurrent;
    private float lastJump;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SwitchObjects();
        }
    }

    private void SwitchObjects()
    {
        if(isLocked) return;
        if(Time.time - jumpDelay < lastJump) return;
        Distort();
        lastJump = Time.time;
        sfxSource.Play();
        isInCurrent = !isInCurrent;

        if (isInCurrent)
        {
            foreach (Transform parent in currentObjectParent)
            {
                foreach (Transform child in parent)
                {
                    child.gameObject.SetActive(true);
                }
            }

            foreach (Transform parent in oldObjectParent)
            {
                foreach (Transform child in parent)
                {
                    child.gameObject.SetActive(false);
                }
            }
            
        }
        
        else
        {
            foreach (Transform parent in currentObjectParent)
            {
                foreach (Transform child in parent)
                {
                    child.gameObject.SetActive(false);
                }
            }
            foreach (Transform parent in oldObjectParent)
            {
                foreach (Transform child in parent)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    private void Distort()
    {
        StartCoroutine(DoDistort());
    }

    private IEnumerator DoDistort()
    {
        for (float i = 0; i < 0.1; i += 0.035f)
        {
            volume.weight = i;
            yield return new WaitForEndOfFrame();
        }
        for (float i = 0.1f; i > 0; i -= 0.035f)
        {
            volume.weight = i;
            yield return new WaitForEndOfFrame();
        }
        volume.weight = 0;
    }

    public void SetLockState(bool newState)
    {
        isLocked = newState;
    }
}
