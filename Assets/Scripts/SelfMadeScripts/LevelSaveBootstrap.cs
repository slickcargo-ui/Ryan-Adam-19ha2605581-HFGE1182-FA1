using UnityEngine;
using System.IO;
using System.Collections;

public class LevelSaveBootstrap : MonoBehaviour
{
    [SerializeField] private WinCondition winCondition;
    [SerializeField] private bool isFirstLevel;
    [SerializeField] private bool isLastLevel;

    private bool levelFinished;

    private IEnumerator Start()
    {
        yield return null; // Wait for one frame to ensure all other scripts have initialized
        Debug.Log($"[Bootstrap] scene={gameObject.scene.name} first={isFirstLevel} loadRequested={MainMenuController.LoadRequested} gmFound={GameManager.Instance != null}");

        GameManager gm = GameManager.Instance;
        if (gm == null) yield break; // Exit if GameManager is not found

        if (MainMenuController.LoadRequested)
        {
            MainMenuController.LoadRequested = false; // Reset the flag
            if (SaveSystem.LoadPlayer() != null) gm.LoadPlayer();
            if (SaveSystem.LoadEnemy() != null) gm.LoadEnemies();

            Rigidbody rb = gm.playerObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; // Reset linear velocity
            }
        }
        else if (!isFirstLevel)
        {
            PlayerData data = SaveSystem.LoadPlayer();
            Debug.Log($"[Bootstrap] carry-over: save is from {data?.sceneName}, health={data?.health}");
            if (data != null)
            {
                gm.playerObject.GetComponent<HealthHandler>().SetHealth(data.health);
            }
        }

        // Checkpoint save when level begins
        gm.SavePlayer();
        gm.SaveEnemies();

    }

    private void Update()
    {
       if (levelFinished || winCondition == null) return;
        
       if (winCondition.sceneEnemy == null)
        {
            levelFinished = true;
            Debug.Log("[Bootstrap] enemy gone, saving transition");
            if (isLastLevel)
            {
                DeleteSave("/PlayerData.dat");
                DeleteSave("/EnemyData.dat");
            }
            else if (GameManager.Instance != null)
            {
                GameManager.Instance.SavePlayer();
            }
        }
    }

    private void DeleteSave(string fileName)
    {
      string path = Application.persistentDataPath + fileName;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}

