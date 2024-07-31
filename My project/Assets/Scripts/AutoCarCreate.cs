using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCarCreate : MonoBehaviour
{
    [System.NonSerialized]
    public bool IsEnemy = false;
    public GameObject car;
    public float time = 5f;
    private AudioSource audio;

    private void Start()
    {
        StartCoroutine(SpawnCar());
        audio = GetComponent<AudioSource>();
    }

    IEnumerator SpawnCar()
    {
        for (int i = 1; i <= 5 ; i++)
        {
            yield return new WaitForSeconds(time);
            Vector3 pos = new Vector3(
                transform.GetChild(0).position.x + UnityEngine.Random.Range(3f, 5f),
                transform.GetChild(0).position.y,
                transform.GetChild(0).position.z + UnityEngine.Random.Range(3f, 5f));
            GameObject spawn = Instantiate(car, pos, Quaternion.identity);

            if (IsEnemy)
            {
                spawn.tag = "Enemy";
                // Notify GameManager about the new enemy
                GameManager.Instance.AddEnemy();
            }
        }
        PlayCarCreationSound();
    }

    void PlayCarCreationSound()
    {
        if (audio != null)
        {
            audio.Play();
        }
    }
}
