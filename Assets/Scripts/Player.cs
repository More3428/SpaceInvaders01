using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab; 
    public float speed = 5.0f;
    public int lives = 3;
    public Image[] livesUI;
    public GameObject explosionPrefab;

    private bool _laserActive;


    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //to prevent player from rapid firing unless laser is destroyed
        if (!_laserActive)
        {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("invader") ||
            other.gameObject.layer == LayerMask.NameToLayer("missile"))
        {
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            lives -= 1;
            for (int i = 0; i < livesUI.Length; i++)
            {
                if (i < lives)
                {
                    livesUI[i].enabled = true;
                }
                else
                {
                    livesUI[i].enabled = false;
                }
            }
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
            
        
    }
}
