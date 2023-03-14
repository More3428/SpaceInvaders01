using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class InvadersSpawn : MonoBehaviour
{

   public Invader[] prefabs;
   public int rows = 5;
   public int columns = 11;

   public AnimationCurve speed;
   public float missileRate = 1.0f;
   public Projectile missilePrefab;

   
   private Vector3 _direction = Vector2.right;
   AudioSource missileSound;
   
   
   //calculate amount of invaders killed
   public int invadersKilled { get; private set; }
   public int totalInvaders => this.rows * this.columns;
   public float percentKilled => (float)this.invadersKilled / (float)this.totalInvaders;
   public int enemiesAlive => this.totalInvaders - this.invadersKilled;


   private void Awake()
   {
      //Centering and spawning invaders in rows and columns
      for (int row = 0; row < this.rows; row++)
      {
         float width = 2.0f * (this.columns -1);
         float height = 2.0f * (this.rows - 1); 
         Vector2 centering = new Vector2(-width / 2, -height / 2);
         Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);
         for (int col = 0; col < this.columns; col++)
         {
         Invader invader = Instantiate(this.prefabs[row], this.transform);
         invader.killed += InvaderKilled;
         Vector3 position = rowPosition;
         position.x += col * 2.0f;
         invader.transform.localPosition = position;
         }
      }
   }

   private void Start()
   {
      InvokeRepeating(nameof(MissileAttack), this.missileRate, this.missileRate );
      missileSound = GetComponent<AudioSource>(); 
   }

   private void Update()
   {
      //update invaders position to keep moving down as time goes by. 
      this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

      //left and right edge of the screen.
      Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
      Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
      
      //check invaders position to change position
      foreach (Transform invader in this.transform)
      {
         //first check if invader is not disabled
         if (!invader.gameObject.activeInHierarchy)
         {
            
            continue;
         }
         //check the edge of the screen if hits edge call advance row
         if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1))
         {
            AdvanceRow();
         }else if(_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1))
         {
            AdvanceRow();
         }
      }
   }

   private void AdvanceRow()
   {
      _direction.x *= -1.0f;

      Vector3 position = this.transform.position;
      position.y -= 1.0f;
      this.transform.position = position;
   }

   private void MissileAttack()
   {
      foreach (Transform invader in this.transform)
      {
         if (!invader.gameObject.activeInHierarchy)
         {
            
            continue;
         }
         //check if missile fires one out of total will spawn
         //as more die chances get higher to shoot missile
         //when one spawns we break from the loop
         if (Random.value < (1.0f / (float)this.enemiesAlive))
         {
            missileSound.Play();
            Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
            break;
         }
      }
   }
   private void InvaderKilled()
   {
      this.invadersKilled++;
      //if all invaders are killed this will reset the game. 
      if (this.invadersKilled >= this.totalInvaders)
      {
         //Here we want to load credit scene so change this
         SceneManager.LoadScene("Credits", LoadSceneMode.Single);
      }
   }
}
