using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public Vector3 playerPosition;
    }
    private string saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Define save location
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

    Debug.Log($"Save file location: {saveLocation}");
    LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData();
        {
            saveData.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }
    public void LoadGame()
    {
    if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
        }
        else
        {
            SaveGame();
        }
    }
}
