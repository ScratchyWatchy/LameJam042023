using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwitcher : MonoBehaviour
{
    public Transform oldObjectParent;
    public Transform currentObjectParent;

    private bool isInCurrent;
    
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
        isInCurrent = !isInCurrent;

        if (isInCurrent)
        {
            foreach (Transform child in currentObjectParent)
            {
                child.gameObject.SetActive(true);
            }
            foreach (Transform child in oldObjectParent)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform child in currentObjectParent)
            {
                child.gameObject.SetActive(false);
            }
            foreach (Transform child in oldObjectParent)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
