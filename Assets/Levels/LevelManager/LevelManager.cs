using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float deathReloadTime = 1f;
    [SerializeField] float outOfBoundsReloadTime = 0f;

    Scene currentScene;
    int currentSceneIndex;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;
    }

    public void DeathReload()
    {
        StartCoroutine(ReloadCoroutine(currentSceneIndex, deathReloadTime));   
    }

    public void OutOfBoundsReload()
    {
        StartCoroutine(ReloadCoroutine(currentSceneIndex, outOfBoundsReloadTime));
    }

    
    IEnumerator ReloadCoroutine(int index, float waitTime)
    {
        
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(index);
        
    }

    public float Timer(float time)
    {

        time -= Time.deltaTime;

        return time;

    }
}
