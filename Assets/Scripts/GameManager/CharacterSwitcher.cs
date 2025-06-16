using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [Header("Karakter Prefablarý")]
    public GameObject[] characters;

    private int currentIndex = 0;

    void Start()
    {
        ShowCharacter(currentIndex);
    }

    public void ShowNext()
    {
        currentIndex = (currentIndex + 1) % characters.Length;
        ShowCharacter(currentIndex);
    }

    public void ShowPrevious()
    {
        currentIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        ShowCharacter(currentIndex);
    }

    private void ShowCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
            characters[i].SetActive(i == index);
    }

    public int GetSelectedIndex()
    {
        return currentIndex;
    }
}