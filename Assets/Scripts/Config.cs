using System;
using System.IO;
using UnityEngine;

public class Config
{
    public static string configPath = Application.dataPath + "/../Config.json";
    public static Config Instance;
    public string Version = "";

    public string LanguageTip = "Affects Uma names on the list. Language options: 0 - En, 1 - Jp";
    public Language Language = Language.En;

    public string MainPathTip = "Path to game folder, eg. D:/Backup/Cygames/umamusume";
    public string MainPath = "";

    public string WorkModeTip = "Affects how application work, options: 0 - work with game client, 1 - work without game client, Database needs to be updated manually";
    public WorkMode WorkMode = WorkMode.Default;


    public Config()
    {
        //TODO: Updating config file
        Version = Application.version;

        MainPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low"}\Cygames\umamusume";
        if (!File.Exists(configPath))
        {
            File.WriteAllText(configPath, JsonUtility.ToJson(this, true));
        }
        else
        {
            try
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText(configPath), this);
            }
            catch (Exception ex)
            {
                UmaViewerUI.Instance.MessagePannel.SetActive(true);
                UmaViewerUI.Instance.MessageText.text = "Config load error. Using default. " + ex.Message;
                MainPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low"}\Cygames\umamusume";
            }
        }

        Instance = this;
    }

    public void UpdateConfig()
    {
        File.WriteAllText(configPath, JsonUtility.ToJson(this, true));
        UmaViewerUI.Instance.MessagePannel.SetActive(true);
        UmaViewerUI.Instance.MessageText.text = "The configuration has changed. Please restart the application.";
    }
}

public enum Language
{
    En,
    Jp
}

public enum WorkMode
{
    Default,
    Standalone
}