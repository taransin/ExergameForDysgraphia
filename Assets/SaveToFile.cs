using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveToFile : MonoBehaviour {

    public static SaveToFile instance;
    public GameSettings settings;
    private string playerData;
    private string configuration;
    private string results;
    private string logs;


    private const string resultPath = "results/result-";

    public void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Init()
    {
        playerData = "";
        configuration = "";
        results = "";
        logs = "";
        print(Application.persistentDataPath);
        string folderPath = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ? Application.persistentDataPath : Application.dataPath) + "/results/";
        Debug.Log(folderPath);

    }

    public void AddPlayerData(string data)
    {
        playerData += data + "\n";
    }

    public void AddConfiguration(string conf)
    {
        configuration += conf + "\n";
    }

    public void AddResults(string result)
    {
        results += result + "\n";
    }

    public void AddLog(string log)
    {
        logs += GetTimestamp()+": " + log + "\n";
    }

    public string GetTimestamp()
    {
        return System.DateTime.Now.ToString(new System.Globalization.CultureInfo("de-DE")) + ":" + System.DateTime.Now.Millisecond;
    }

    public void SaveResultFile()
    {
        Debug.Log("saving");
        string folderPath = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ? Application.persistentDataPath : Application.dataPath) + "/results/";


        string filePath = folderPath + settings.GetPlayerName() +"-"+ GetTimestamp().Replace(' ','_').Replace(':','_')+ ".txt";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string text =
              "PLAYER\n" +
              "------\n" +
              playerData +
              "\nCONFIGURATION\n" +
              "-------------\n" +
              configuration +
              "\nRESULTS\n" +
              "-------\n" +
              results+"\n\n"+
              "LOGS\n"+
              "-----\n"+
              logs;
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        File.WriteAllText(filePath, text);
        print(filePath);
        print(text);
    }
}
