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
        Invoke("ReloadLevel", loadDelay);

        // To Do: Add some explosion FX and stop the player motion
    }

    void ReloadLevel()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
