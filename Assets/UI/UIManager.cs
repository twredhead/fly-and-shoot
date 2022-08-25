using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI outOfBoundsWarning;

    void Awake() 
    {   
        // disable on awake. This will only be enabled when out of bounds
        DisplayWarning(false); 
    }



    public void WarnPlayer(bool trueOrFalse, float time, float maxTime)
    {
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


}
