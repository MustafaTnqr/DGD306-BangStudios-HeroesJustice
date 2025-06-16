using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("UI Navigation")]
    public Button firstSelected;         // Inspector'dan atay�n: Play butonu gibi
    public AudioSource audioSource;
    public AudioClip clickSound;

    void Start()
    {
        // Sahne a��ld���nda D-Pad navigasyonu i�in ilk se�ili butonu ayarla
        if (firstSelected != null)
            EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    void Update()
    {
        // Yeni Input System�den �Submit� (A tu�u) alg�la
        bool submit = false;
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            submit = true;
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
            submit = true;

        if (submit)
        {
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected != null)
            {
                var btn = selected.GetComponent<Button>();
                if (btn != null)
                {
                    // T�klama sesi
                    if (audioSource != null && clickSound != null)
                        audioSource.PlayOneShot(clickSound);

                    // OnClick event�ini tetikle
                    StartCoroutine(InvokeButton(btn));
                }
            }
        }
    }

    IEnumerator InvokeButton(Button btn)
    {
        yield return new WaitForSeconds(0.1f); // K���k gecikme
        btn.onClick.Invoke();
    }

    // �rnek sahne y�ntemleri � UI �zerindeki onClick�e ba�lay�n
    public void PlayGame() => StartCoroutine(ClickAndLoad("CharacterSelection"));
    public void OpenOptions() => StartCoroutine(ClickAndLoad("Options"));
    public void OpenCredits() => StartCoroutine(ClickAndLoad("Credits"));
    public void QuitGame() => StartCoroutine(ClickAndQuit());

    IEnumerator ClickAndLoad(string scene)
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator ClickAndQuit()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}