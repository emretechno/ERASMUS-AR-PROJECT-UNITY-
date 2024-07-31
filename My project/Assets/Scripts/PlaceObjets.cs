using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public LayerMask layer;  // Layer per il raycast
    public LayerMask invalidLayerMask;  // Layer per gli oggetti che non devono essere sovrapposti
    public LayerMask validLayerMask;  // Layer per l'oggetto specifico su cui può essere piazzato
    public float rotateSpeed = 90f;
    public Material validMaterial;  // Materiale verde
    public Material invalidMaterial;  // Materiale rosso
    public Renderer targetChildRenderer; // Renderer del figlio su cui cambiare il materiale
    private Material[] originalMaterials;  // Materiali originali del renderer del figlio
    private bool canPlace = false;  // Stato di validità del piazzamento
    private AudioSource audio;

    private void Start()
    {
        // Memorizza i materiali originali del renderer del figlio
        if (targetChildRenderer != null)
        {
            originalMaterials = targetChildRenderer.materials;
        }

        PositionObject();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PositionObject();

        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            // Ripristina i materiali originali del renderer del figlio
            if (targetChildRenderer != null)
            {
                targetChildRenderer.materials = originalMaterials;
            }

            gameObject.GetComponent<AutoCarCreate>().enabled = true;
            audio.Play();
            Destroy(gameObject.GetComponent<PlaceObjects>());
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        }
    }

    private void PositionObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, layer))
        {
            transform.position = hit.point;

            // Verifica se l'oggetto è piazzato su un oggetto valido
            canPlace = true;  // Presume che il piazzamento sia valido
            Collider[] colliders = Physics.OverlapSphere(hit.point, 0.5f);
            foreach (Collider collider in colliders)
            {
                if (((1 << collider.gameObject.layer) & invalidLayerMask) != 0)
                {
                    canPlace = false;
                    break;
                }
                if (((1 << collider.gameObject.layer) & validLayerMask) != 0)
                {
                    canPlace = true;
                    break;
                }
            }

            // Cambia il colore del renderer del figlio in base alla validità del piazzamento
            if (targetChildRenderer != null)
            {
                if (canPlace)
                {
                    // Cambia tutti i materiali a validMaterial
                    Material[] validMaterials = new Material[targetChildRenderer.materials.Length];
                    for (int i = 0; i < validMaterials.Length; i++)
                    {
                        validMaterials[i] = validMaterial;
                    }
                    targetChildRenderer.materials = validMaterials;
                }
                else
                {
                    // Cambia tutti i materiali a invalidMaterial
                    Material[] invalidMaterials = new Material[targetChildRenderer.materials.Length];
                    for (int i = 0; i < invalidMaterials.Length; i++)
                    {
                        invalidMaterials[i] = invalidMaterial;
                    }
                    targetChildRenderer.materials = invalidMaterials;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Controlla se la collisione coinvolge l'oggetto con il quale interagisci
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("La casa ha avuto una collisione con un altro oggetto!");
            // Puoi eseguire altre azioni qui in risposta alla collisione
        }
    }

}
