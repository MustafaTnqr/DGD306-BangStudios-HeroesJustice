using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour //Karakter seçim ekraný için youtube videosundan yardým alýndý
{
    public GameObject[] characters; 
    private int currentIndex = 0;

    void Start()
    {
        
         ShowCharacter(currentIndex); 

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

        currentIndex = index;

        
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        PlayerPrefs.Save();
    }


    public int GetSelectedIndex()
    {
        return currentIndex;
    }
}
