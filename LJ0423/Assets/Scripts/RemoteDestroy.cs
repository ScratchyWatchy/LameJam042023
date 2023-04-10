using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteDestroy : MonoBehaviour
{
   public GameObject target;

   public void DestroyRemote()
   {
      Destroy(target);
   }
}
