using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiegeticReticle : MonoBehaviour
{
   [SerializeField] private Transform reticleHittable;
   [SerializeField] private Transform reticleNotHittable;
   [SerializeField] private GameObject reticleHittableGO;
   [SerializeField] private GameObject reticleNotHittableGO;
   
   public float minSize = 1f;
   public float maxSize = 5f;
   public float distanceScalingFactor = 1f;
   
   
   
   [SerializeField] private Grapple grappleScript;
   [SerializeField] private float maxHookDistance = 50f;
   [SerializeField] private Transform cameraTransform;
   [SerializeField] private LayerMask hookableLayers;
   [SerializeField] private LayerMask collidableLayers;


   private bool hittingSomething = false;
   private bool canHook = true;
   private Vector3 reticlePosition;
   private float originalSize;

   private void Start()
   {
      reticleHittableGO.SetActive(false);
      reticleNotHittableGO.SetActive(false);
      originalSize = reticleHittable.localScale.magnitude;
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
         reticleNotHittable.position = cameraTransform.position + cameraTransform.forward * maxHookDistance;
         reticleNotHittableGO.SetActive(true);
      }

      var distanceToReticle = Vector3.Distance(cameraTransform.position, reticlePosition);
      
      float size = Mathf.Clamp(originalSize + (distanceToReticle * distanceScalingFactor), minSize, maxSize);
      reticleNotHittable.localScale = new Vector3(size, size, size);
      reticleHittable.localScale = new Vector3(size, size, size);
   }

   private void GetReticlePosition()
   {
      RaycastHit hit;
      
      var hits = Physics.RaycastAll(cameraTransform.position, cameraTransform.forward, collidableLayers).ToList();

      hits = hits.OrderBy(x => Vector3.Distance(cameraTransform.position, x.point)).ToList();

      hits = hits.Where(x => x.transform.gameObject.activeSelf).ToList();

      if (hits.Count != 0)
      {
         hittingSomething = true;
         if (Vector3.Distance(cameraTransform.position, hits[0].point) > maxHookDistance)
         {
            reticlePosition = cameraTransform.position + cameraTransform.forward * maxHookDistance;
         }
         else reticlePosition = hits[0].point;
         //On hookable layer
         if ((((1 << hits[0].transform.gameObject.layer) & hookableLayers) != 0) && Vector3.Distance(cameraTransform.position, hits[0].point) < maxHookDistance)
         {
            canHook = true;
            return;
         }
         canHook = false;
         return;
      }

      if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, collidableLayers))
      {
         
      }

      hittingSomething = false;
   }

  
}
