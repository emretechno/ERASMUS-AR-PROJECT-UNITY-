using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAttack : MonoBehaviour
{
    public int _health = 100; // Car's health
    public float radius = 70f; // Detection radius
    public GameObject bullet; // Bullet prefab
    private Coroutine _coroutine = null; // Reference to the active coroutine

    //runs every frame
    private void Update()
    {
        DetectCollision(); // Check for collisions with other cars 
    }

    private void DetectCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); // Detect colliders(cars) within the radius

        if (hitColliders.Length == 0 && _coroutine != null) // If no objects are detected and a coroutine is active
        {
            StopCoroutine(_coroutine); // Stop the coroutine
            _coroutine = null; // Reset the coroutine reference

            if (gameObject.CompareTag("Enemy")) // If the object is an enemy
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position); // Stop the enemy's movement
        }

        foreach (var el in hitColliders) // Iterate through detected colliders
        {
            if (gameObject.CompareTag("Player") && el.gameObject.CompareTag("Enemy") ||
                gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Player")) // Check if they are enemies
            {
                if (gameObject.CompareTag("Enemy"))
                    GetComponent<NavMeshAgent>().SetDestination(el.transform.position); // Set enemy's destination to the detected enemy

                if (_coroutine == null) // If no coroutine is active
                    _coroutine = StartCoroutine(StartAttack(el)); // Start the attack coroutine
            }
        }
    }

    IEnumerator StartAttack(Collider enemyPos)
    {
        while (true)
        {
            if (enemyPos == null || enemyPos.gameObject == null)
            {
                // Enemy object has been destroyed, exit the coroutine
                _coroutine = null;
                yield break;
            }

            GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
            obj.GetComponent<BulletController>().position = enemyPos.transform.position;
            yield return new WaitForSeconds(1f);
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Notify the GameManager that an enemy has been killed
        if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy killed, notifying GameManager.");
            GameManager.Instance.EnemyKilled();
        }

        // Destroy the game object
        Debug.Log("Destroying enemy game object.");
        Destroy(gameObject);
    }
}
