using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private ElementCounter _elementCounter;
    private string[] _savaData;

    private void Start()
    {
        LoadGame();
    }

    private void SaveGame()
    {
        _savaData = new string[_elementCounter.ReachedElements.Count];

        for (int i = 0; i < _elementCounter.ReachedElements.Count; i++)
        {
            //_savaData[i] = _elementCounter.ReachedElements[i].Prefab.name;
            //Debug.Log("Сохраненые имена:");
            //Debug.Log(_elementCounter.ReachedElements[i].Prefab.name);

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
                _elementCounter.ReachedElements.Clear();
                _elementCounter.AddElement(_savaData[i]);
            }
        }
    }

    private void OnDestroy()
    {
        SaveGame();
    }
}
