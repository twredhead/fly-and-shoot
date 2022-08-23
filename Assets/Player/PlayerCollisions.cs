using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{   
    [SerializeField] float loadDelay = 1f;
    Scene currentScene;
    int currentSceneIndex;
    
    void OnCollisionEnter(Collision other)
    {
        // when a collision happens the player dies and has to restart the level.
        StartCoroutine(ReloadLevel());

        // To Do: Add some explosion FX and stop the player motion
    }

    IEnumerator ReloadLevel()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;
        
        yield return  new WaitForSeconds(loadDelay);
        
        // after delay reload scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
