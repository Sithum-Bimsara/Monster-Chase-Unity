using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        // Debug.Log("Button is pressed");
        int selectedCharacter = 
            int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        // Debug.Log(selectedCharacter);
        GameManager.instance.CharIndex = selectedCharacter;
        // Debug.Log("CharIndex set to: " + GameManager.instance.CharIndex);
        SceneManager.LoadScene("Gameplay");
    }
}
