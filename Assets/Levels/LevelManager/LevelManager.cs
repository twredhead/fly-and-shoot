using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float deathReloadTime = 1f;
    [SerializeField] float outOfBoundsReloadTime = 0f;
    [SerializeField] int winCondition = 1000;
    [SerializeField] int losCondition = 0;


    float currentScore = 0;
    public float CurrentScore { get { return currentScore; } } 

    Scene currentScene;
    int currentSceneIndex;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;
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

        if(currentScore < losCondition)
        {
            StartCoroutine(ReloadCoroutine(loseSceneIndex, deathReloadTime));
        }
        if(currentScore >= winCondition)
        {
            StartCoroutine(ReloadCoroutine(winSceneIndex, deathReloadTime));
        }
    }

    IEnumerator ReloadCoroutine(int index, float waitTime)
    {
        
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(index);
        
    }

    /******************************************************************************************************************************/
    /******************************************************* Public Methods *******************************************************/
    /******************************************************************************************************************************/

    public void DeathReload()
    {
        StartCoroutine(ReloadCoroutine(currentSceneIndex, deathReloadTime));   
    }

    public void OutOfBoundsReload()
    {
        StartCoroutine(ReloadCoroutine(currentSceneIndex, outOfBoundsReloadTime));
    }

    public void UpdateScore(int addToScore)
    {   
        currentScore += addToScore;
    }

    public float Timer(float time)
    {

        time -= Time.deltaTime;

        return time;

    }

}
