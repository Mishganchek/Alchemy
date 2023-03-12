using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string FileName = "/SaveFile.txt";

    [SerializeField] private Spawner _spawner;
    private string[] _saveData;
    

    private void Start()
    {
        LoadGame();
    }

    private void SaveGame()
    {
        _saveData = new string[_spawner.ReachedElements.Count];

        int i = 0;

        foreach (var element in _spawner.ReachedElements)
        { 
            _saveData[i] = element.name;
            i++;
        }

        File.WriteAllLines(Application.persistentDataPath + FileName, _saveData);
    }

    private void LoadGame()
    {
        if (!File.Exists(Application.persistentDataPath + FileName))
        {
            return;
        }

        _saveData = File.ReadAllLines(Application.persistentDataPath + FileName);

        if (_saveData.Length == 0)
        {
            return;
        }

        for (int i = 0; i < _saveData.Length; i++)
        {
            if (_saveData[i] != string.Empty)
            {
                _spawner.AddElement(_saveData[i]);
            }
        }
    }

    private void OnDisable()
    {
        SaveGame();
    }
}
