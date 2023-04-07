using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTracker : MonoBehaviour
{
    public Rigidbody playerRB;
    public Text text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text =  ((int)playerRB.velocity.magnitude).ToString();
    }
}
//rb.AddForce(currentVelocity * Time.fixedDeltaTime, ForceMode.VelocityChange);