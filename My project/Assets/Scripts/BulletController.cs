using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BulletController : MonoBehaviour
{
    [NonSerialized] public Vector3 position;
    public int damage = 5;
    public float speed = 30f;
    AudioSource audioSource;

    private void Start()
    {
        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }



    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);

        if (transform.position == position)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            CarAttack attack = other.GetComponent<CarAttack>();
            attack._health -= damage;

            Transform healthBar = other.transform.GetChild(1).transform;
            healthBar.localScale = new Vector3(
                healthBar.localScale.x - 0.3f, 
                healthBar.localScale.y, 
                healthBar.localScale.z);

            // Play the audio clip on impact
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }



            if (attack._health <= 0)
                Destroy(other.gameObject);
        }
    }   
}
