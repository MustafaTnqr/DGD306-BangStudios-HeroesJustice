using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characters; 
    private int currentIndex = 0;

    void Start()
    {
        ShowCharacter(0); // Ýlk karakteri göster
    }

    public void ShowNext()
    {
        currentIndex++;
        if (currentIndex >= characters.Length) currentIndex = 0;
        ShowCharacter(currentIndex);
    }

    public void ShowPrevious()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = characters.Length - 1;
        ShowCharacter(currentIndex);
    }

    void ShowCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == index);
        }
    }

    public int GetSelectedIndex()
    {
        return currentIndex;
    }
}
