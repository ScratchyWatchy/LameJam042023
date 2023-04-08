using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Grapple : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 100f;
    [SerializeField] private float maxHookDistance = 50f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask hookableLayers;
    [SerializeField] private LayerMask collidableLayers;
    [SerializeField] private float slowDownRate = 0.1f;
    [SerializeField] private LineRenderer lineRenderer;
    
    private Rigidbody rb;
    private FPSController _fpsController;

    private GameObject hookedGO;
    
    public Vector3 hookPosition;
    public bool isHooked = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _fpsController = GetComponent<FPSController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootHook();
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            if (isHooked)
            {
                DisconnectHook();
            }
        }

        if (isHooked)
        {
            if (!hookedGO.activeSelf) DisconnectHook();
            DrawHook();
            PullTowardsHook();
        }
    }

    private void FixedUpdate()
    {

    }

    private void ShootHook()
    {
        RaycastHit hit;
        
        Debug.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * maxHookDistance);

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxHookDistance, collidableLayers))
        {
            if (!(((1 << hit.transform.gameObject.layer) & hookableLayers) != 0)) return;
            hookedGO = hit.transform.gameObject;
            hookPosition = hit.point;
            PullTowardsHook(true);
            isHooked = true;
            lineRenderer.enabled = true;
            //PullTowardsHook();
        }

    }

    private void PullTowardsHook(bool doubleTheForce = false)
    {
        Vector3 hookDirection = (hookPosition - transform.position).normalized;
        //rb.AddForce(hookDirection * hookSpeed * (doubleTheForce ? 2f : 1f));
        //rb.AddForce(transform.up * hookSpeed * 0.6f);

        Vector3 forceVector = hookDirection * hookSpeed * (doubleTheForce ? 3f : 1f);
        
        forceVector = new Vector3(forceVector.x, forceVector.y * 0.425f, forceVector.z);
        
        _fpsController.AddExtraForce(forceVector);
        
    }

    public void DisconnectHook()
    {
        isHooked = false;
        lineRenderer.enabled = false;
    }
    
    private void DrawHook()
    {
        lineRenderer.SetPosition(0, lineRenderer.transform.position);
        lineRenderer.SetPosition(1, hookPosition);
    }
}

