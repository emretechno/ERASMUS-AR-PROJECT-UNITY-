using UnityEngine;
using UnityEngine.UI;

public class PlaceFactoryButtonController : MonoBehaviour
{
    public Button placeFactoryButton; // Referans olarak butonu alır
    public int maxClicks = 5; // Maksimum tıklama sayısı
    private int currentClicks = 0; // Mevcut tıklama sayısı

    private void Start()
    {
        // Butonun tıklama olayına dinleyici ekle
        placeFactoryButton.onClick.AddListener(OnPlaceFactoryButtonClick);
    }

    private void OnPlaceFactoryButtonClick()
    {
        currentClicks++; // Tıklama sayısını arttır

        if (currentClicks >= maxClicks)
        {
            // Tıklama sayısı maksimuma ulaştığında butonu devre dışı bırak
            placeFactoryButton.interactable = false;
            Debug.Log("Button disabled after " + maxClicks + " clicks.");
        }
    }
}
