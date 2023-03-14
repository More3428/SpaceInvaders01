using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
   public int bunkerLives = 4;
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.layer == LayerMask.NameToLayer("invader") || other.gameObject.layer == LayerMask.NameToLayer("missile") || other.gameObject.layer == LayerMask.NameToLayer("laser"))
      {
         bunkerLives--;
         if (bunkerLives == 0)
         {
            this.gameObject.SetActive(false);
         }
         
      }
   }
}
