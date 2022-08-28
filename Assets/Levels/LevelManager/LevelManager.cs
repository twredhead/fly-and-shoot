using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float deathReloadTime = 1f;
    [SerializeField] float outOfBoundsReloadTime = 0f;


    float currentScore = 0;
    public float CurrentScore { get { return currentScore; } } 

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

    public void UpdateScore(int addToScore)
    {   
        currentScore += addToScore;
    }

    void Update()
    {
        WinOrLose();    
    }

    void WinOrLose()
    {
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        int loseSceneIndex = totalSceneCount - 2;
        int winSceneIndex = totalSceneCount - 1;

        if(currentScore < 0)
        {
            StartCoroutine(ChangeSceneDelay(loseSceneIndex));
        }
        if(currentScore >= 5000)
        {
            StartCoroutine(ChangeSceneDelay(winSceneIndex));
        }
    }

    IEnumerator ChangeSceneDelay(int sceneIndex)
    {
        yield return new WaitForSeconds(1);
        
        SceneManager.LoadScene(sceneIndex);
    }
}
