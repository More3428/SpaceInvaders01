using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
   public string gameSceneName;
   private float delayBeforeLoading = 15f; 
   private float timeElapsed; 
   
   

   private void Start()
   {
      GameObject.DontDestroyOnLoad(this.gameObject);
   }
   
   public void LoadGameScene()
   {
      
      StartCoroutine(LoadAndSetup()); 
   }

   public void LoadCreditScene()
   {
      SceneManager.LoadScene("Credits", LoadSceneMode.Single);
   }

   public void LoadMenuScene()
   {
      SceneManager.LoadScene("Menu", LoadSceneMode.Single); 
   }

   IEnumerator LoadAndSetup()
   {
      SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
      Debug.Log("Load Started");
      yield return null; 
      Player player = GameObject.FindObjectOfType<Player>();
      Debug.Log($"Found {player.name}");
      yield return null;
      player = GameObject.FindObjectOfType<Player>();
      Debug.Log($"Found {player.name}");

   }
}
