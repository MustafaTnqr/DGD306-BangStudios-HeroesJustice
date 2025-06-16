using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}
