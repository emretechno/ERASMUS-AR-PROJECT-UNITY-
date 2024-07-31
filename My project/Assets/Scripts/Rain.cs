using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public Light dirLight;
    private ParticleSystem ps;
    private bool is_rain = false;
    private AudioSource audioSource;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(Weather());
    }

    private void Update()
    {
        if (is_rain && dirLight.intensity > 0.25f)
        {
            LightIntensity(-1);
        }
        else if (!is_rain && dirLight.intensity < 0.5f)
        {
            LightIntensity(1);
        }
    }

    private void LightIntensity(int val)
    {
        dirLight.intensity += 0.1f * Time.deltaTime * val;
    }

    private IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 13f));

            if (is_rain) //If raining , it should be stop  ; means music should be stop
            {
                ps.Stop();
                audioSource.Stop(); // Stop the audio when it stops raining
            }
            else // If it is not raining , it should be start ; means music should be start.
            {
                ps.Play();
                if (!audioSource.isPlaying) // Only play the audio if it is not already playing
                {
                    audioSource.Play();
                }
            }

            is_rain = !is_rain;
        }
    }
}
