using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
   public Sprite[] animationSprites;
   public float animationTime = 1.0f;

   private SpriteRenderer _spriteRenderer;
   private int _animationFrame;

   public System.Action killed;
   private pointManager _pointManager;
   public AudioSource enemyExplosion;
   

   private void Awake()
   {     
      _spriteRenderer = GetComponent<SpriteRenderer>();
   }

   private void Start()
   {
      //call animation sprite every second
      InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
      _pointManager = GameObject.Find("pointManager").GetComponent<pointManager>();
   }

   private void AnimateSprite()
   {
      //Update what frame we are on. 
      _animationFrame++; 
      
      //check current frame does not exceed how many sprites we provide
      if (_animationFrame >= this.animationSprites.Length)
      {
         _animationFrame = 0;
      }

      _spriteRenderer.sprite = this.animationSprites[_animationFrame]; 
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.layer == LayerMask.NameToLayer("laser"))
      {
         
         _pointManager.UpdateScore(50);
         this.killed.Invoke();
         enemyExplosion.Play();
         this.gameObject.SetActive(false);
      }
   }
}
