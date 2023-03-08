using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.layer == LayerMask.NameToLayer("invader") || other.gameObject.layer == LayerMask.NameToLayer("missile") || other.gameObject.layer == LayerMask.NameToLayer("laser"))
      {
         this.gameObject.SetActive(false);
      }
   }
}
