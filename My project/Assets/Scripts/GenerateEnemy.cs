using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public Transform[] points;
    public GameObject factory;

    private void Start()
    {
        StartCoroutine(SpawnFactory());
    }

    IEnumerator SpawnFactory()
    {
        for (int i = 0; i < points.Length; i++)
        {
            yield return new WaitForSeconds(4f);
            GameObject spawn = Instantiate(factory);
            Destroy(spawn.GetComponent<PlaceObjects>());
            spawn.transform.position = points[i].position;
            spawn.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
            AutoCarCreate autoCarCreate = spawn.GetComponent<AutoCarCreate>();
            if (autoCarCreate != null)
            {
                autoCarCreate.enabled = true;
                autoCarCreate.IsEnemy = true;
                // Notify GameManager about the new enemy
                GameManager.Instance.AddEnemy();
            }
        }
    }
}
