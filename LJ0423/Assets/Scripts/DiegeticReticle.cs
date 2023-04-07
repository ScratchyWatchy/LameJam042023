using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiegeticReticle : MonoBehaviour
{
   [SerializeField] private Transform reticleHittable;
   [SerializeField] private Transform reticleNotHittable;
   [SerializeField] private GameObject reticleHittableGO;
   [SerializeField] private GameObject reticleNotHittableGO;
   
   
   [SerializeField] private Grapple grappleScript;
   [SerializeField] private float maxHookDistance = 50f;
   [SerializeField] private Transform cameraTransform;
   [SerializeField] private LayerMask hookableLayers;
   [SerializeField] private LayerMask collidableLayers;


   private bool hittingSomething = false;
   private bool canHook = true;
   private Vector3 reticlePosition;

   private void Start()
   {
      reticleHittableGO.SetActive(false);
      reticleNotHittableGO.SetActive(false);
   }

   private void Update()
   {
      GetReticlePosition();

      if (canHook && hittingSomething)
      {
         reticleHittableGO.SetActive(true);
         reticleHittable.position = reticlePosition;
         
         reticleNotHittableGO.SetActive(false);
      }
      else if (!canHook && hittingSomething)
      {
         reticleNotHittableGO.SetActive(true);
         reticleNotHittable.position = reticlePosition;
         
         reticleHittableGO.SetActive(false);
      }
      else if (!hittingSomething)
      {
         reticleHittableGO.SetActive(false);
         reticleNotHittableGO.SetActive(false);
      }

   }

   private void GetReticlePosition()
   {
      RaycastHit hit;

      if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, collidableLayers))
      {
         hittingSomething = true;
         reticlePosition = hit.point;
         //On hookable layer
         if ((((1 << hit.transform.gameObject.layer) & hookableLayers) != 0) && Vector3.Distance(transform.position, hit.point) < maxHookDistance)
         {
            canHook = true;
            return;
         }
         canHook = false;
         return;
      }

      hittingSomething = false;
   }
}
