using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{   
    [SerializeField] float speed = 40f;
    [SerializeField] float maxDistance = 600f;
    Transform navAid;
    Vector3 gpsPosition;

    void Awake()
    {
        navAid = FindObjectOfType<GlobalPositioningSystem>().transform;
        gpsPosition = navAid.position;
    }

    void Update()
    {
        transform.Translate(0, 0, speed*Time.deltaTime);
    }



    void GoInCircle()
    {
        Vector3 originVector = (gpsPosition + new Vector3(0, transform.position.y, 0)) - transform.position;
        Quaternion from = transform.localRotation;
        Quaternion to = Quaternion.LookRotation(originVector);

        transform.localRotation = Quaternion.Slerp(from, to, Time.deltaTime / 10);
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

}
