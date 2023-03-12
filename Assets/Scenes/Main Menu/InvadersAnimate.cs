using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersAnimate : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    
    private void Awake()
    {     
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        //call animation sprite every second
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
        
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
}
