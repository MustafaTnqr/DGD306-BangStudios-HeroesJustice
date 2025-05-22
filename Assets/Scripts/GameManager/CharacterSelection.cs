using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    public CharacterSwitcher characterSwitcher;

    public void PlayGame()
    {
        int selectedIndex = characterSwitcher.GetSelectedIndex();
        PlayerPrefs.SetInt("SelectedCharacter", selectedIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level1"); 
    }
}
