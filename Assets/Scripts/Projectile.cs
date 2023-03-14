using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;
    public GameObject explosionPrefab;
    



    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //an event
        if (this.destroyed != null)
        {
            
            this.destroyed.Invoke();
        }
        
        Destroy(this.gameObject);
        if (other.gameObject.layer == LayerMask.NameToLayer("invader"))
        {
            
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            Destroy(other.gameObject);
            
           Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("boundary"))
        {
            Destroy(gameObject);
        }
    }
}
