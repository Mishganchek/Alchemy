using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    private string[] _savaData;

    private void Start()
    {
        LoadGame();
    }

    private void SaveGame()
    {
        _savaData = new string[_spawner.ReachedCount];

        for (int i = 0; i < _spawner.ReachedCount; i++)
        {
            _savaData[i] = _spawner.ReachedElements[i].name;
            Debug.Log("Сохраненые имена:");
            Debug.Log(_spawner.ReachedElements[i].name);

        }

        File.WriteAllLines(Application.persistentDataPath + "/SaveFile.txt", _savaData);
    }

    private void LoadGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/SaveFile.txt"))
        {
            return;
        }

        _savaData = File.ReadAllLines(Application.persistentDataPath + "/SaveFile.txt");

        if (_savaData.Length == 0)
        {
            return;
        }


        for (int i = 0; i < _savaData.Length; i++)
        {
            if (_savaData[i] != string.Empty)
            {
                _spawner.AddElement(_savaData[i]);
            }
        }
    }

    private void OnDestroy()
    {
        SaveGame();
    }
}
