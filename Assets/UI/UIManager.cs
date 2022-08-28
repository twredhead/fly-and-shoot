using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    LevelManager lvlManager;
    [SerializeField] TextMeshProUGUI outOfBoundsWarning;
    [SerializeField] TextMeshProUGUI scoreBoard;

    void Awake() 
    {   
        lvlManager = FindObjectOfType<LevelManager>();

        // disable on awake. This will only be enabled when out of bounds
        DisplayWarning(false); 
    }

    /*****************************************************************************************************************************/
    /*********************************************************** Warn Player Methods *********************************************/
    /*****************************************************************************************************************************/    
    public void WarnPlayer(bool trueOrFalse, float time, float maxTime)
    {
        DisplayScore();
        DisplayWarning(trueOrFalse);
        UpdateWarning(time, maxTime);
    }

    void DisplayWarning(bool trueOrFalse)
    {
        outOfBoundsWarning.enabled = trueOrFalse;
    }

    void UpdateWarning(float time, float maxTime)
    {   
        float timeLeft = time - maxTime/2;
    
        if(timeLeft > 0)
        {   
            outOfBoundsWarning.text = "Out of Bounds, Turn Around: " + timeLeft.ToString("F1");
        }
        else
        {
            outOfBoundsWarning.text = "Out of Time: You Will DIE!";
        }  
    }

    /*****************************************************************************************************************************/
    /*********************************************************** Score Methods ***************************************************/
    /*****************************************************************************************************************************/
    void DisplayScore()
    {
        scoreBoard.enabled = true;
        
        if(lvlManager.CurrentScore < 1)
        {
            
            scoreBoard.text = "Start!";

        }
        else
        {
        
            scoreBoard.text = "Score: " + lvlManager.CurrentScore;

        }
    }




}
