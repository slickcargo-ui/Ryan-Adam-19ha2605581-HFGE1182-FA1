using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string firstLevelSceneName = "Level1";
    [SerializeField] private Button LoadButton;

    public static bool LoadRequested;

    private void Start()
    {
        Time.timeScale = 1f; // Ensure the game is not paused when returning to the main menu
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible

        if (LoadButton != null)
        {
            LoadButton.interactable = SaveExists();
        }
    }

    private bool SaveExists()
    {
        return File.Exists(Application.persistentDataPath + "/PlayerData.dat");
    }

    public void StartNewGame()
    {
        DeleteFile("/PlayerData.dat");
        DeleteFile("/EnemyData.dat");
        LoadRequested = false;
        SceneManager.LoadScene(firstLevelSceneName);
        Debug.Log("Start New Game: " + firstLevelSceneName);
    }

    public void LoadGame()
    {
        Debug.Log("[Load] clicked");

        PlayerData data = SaveSystem.LoadPlayer();
        Debug.Log($"[Load] data is null: {data == null}");
        if (data == null) return;

        Debug.Log($"[Load] loading scene '{data.sceneName}'");
        LoadRequested = true;
        SceneManager.LoadScene(data.sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log ("Quit Game");
    }

    private void DeleteFile(string fileName)
    {
        string filePath = Application.persistentDataPath + fileName;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
