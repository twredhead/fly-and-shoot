using UnityEngine;

public class GlobalPositioningSystem : MonoBehaviour
{

    void Awake() 
    {
        // make sure GPS is in the middle of the world on awake.
        transform.position = new Vector3(0,0,0);
    }

}
