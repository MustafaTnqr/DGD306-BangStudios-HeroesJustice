using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler   //Menu i�in buton sesleri ve kodlar� chatgpt taraf�nda yazd�r�ld�
{
    public AudioClip hoverSound;
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.volume = 0.3f; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
