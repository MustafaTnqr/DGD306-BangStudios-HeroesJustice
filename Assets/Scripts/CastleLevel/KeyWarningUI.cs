using UnityEngine;
using TMPro;

public class KeyWarningUI : MonoBehaviour //UI Gostermek i�in ai taraf�ndan yard�m al�nd�
{
    public static KeyWarningUI instance;
    public GameObject panel;
    public TextMeshProUGUI messageText;

    void Awake()
    {
        instance = this;
        panel.SetActive(false);
    }

    public void ShowMessage(string msg, float duration = 2f)
    {
        messageText.text = msg;
        panel.SetActive(true);
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), duration);
    }

    void Hide()
    {
        panel.SetActive(false);
    }
}
