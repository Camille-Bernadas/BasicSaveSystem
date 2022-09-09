using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption = false;

    private GameData gameData;
    private List<IDataPersistence> dataPersistencesObjects;
    public static DataPersistenceManager instance { get; private set; }
    private FileDataHandler dataHandler;

    private void Awake(){
        if(instance != null){
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        instance = this;
    }

    private void Start(){
        SaveFileData save = FindObjectOfType<SaveFileData>();
        if(save != null){
            fileName = save.GetSaveFileName();
        }
        
        StartLoading(fileName);
    }
    public void StartLoading(string loadingFileName){
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, loadingFileName, useEncryption);
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame(){
        this.gameData = new GameData();
    }
    public void LoadGame(){
        // Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // if no data can be loaded, initialize to a new game
        if(this.gameData == null){
            {
                Debug.Log("No data was found, Initializing data to defaults");
                NewGame();
            }
        }
        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects){
            dataPersistenceObj.LoadData(gameData);
        }

    }
    public void SaveGame(){
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Resources"));
        string file = Path.Combine("Resources", fileName + "screenshot.png");
        ScreenCapture.CaptureScreenshot(Path.Combine(Application.persistentDataPath, file));
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        gameData.ClearLists();
        // pass the data to other scritps so they can update it
        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects){
            dataPersistenceObj.SaveData(gameData);
        }



        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects(){
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistencesObjects);
    }
}
