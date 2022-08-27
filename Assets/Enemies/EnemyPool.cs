using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    /******************************************************* Serialized Fields ********************************************************/

    [SerializeField] int poolSize = 10;

    [SerializeField] GameObject enemyPrefab;
    
    [SerializeField] float spawnTime = 5f;

    /******************************************************* Private Fields ***********************************************************/
    GameObject[] pool;

    /******************************************************************************************************************************/
    /******************************************************* Awake() **************************************************************/
    /******************************************************************************************************************************/    
    void Awake()
    {
        PopulatePool();
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize]; // enemy pool

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    /******************************************************************************************************************************/
    /******************************************************* Start() **************************************************************/
    /******************************************************************************************************************************/

    void Start() 
    {   
        StartCoroutine(EnemyDispenser());
    }


    void ActivateEnemiesInPool()
    {

        foreach(GameObject enemy in pool)
        {
            if(!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }

    }

    IEnumerator EnemyDispenser()
    {   
        while(true)
        {   
            ActivateEnemiesInPool();

            yield return new WaitForSeconds(spawnTime);
        }
        
    }


}
