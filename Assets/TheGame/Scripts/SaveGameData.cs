using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevProfi.Utils;
using System.IO;

[System.Serializable]
public class SaveGameData
{
    public static SaveGameData current = new SaveGameData();

    public Vector3 playerPosition = Vector3.zero;

    public bool doorIsOpen = false;

    public string lastTriggerID = "";

    public delegate void SaveHandler(SaveGameData savegame);

    public static event SaveHandler onSave;

    public static event SaveHandler onLoad;

    private static string getFilename()
    {

        return Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + "savegame.xml";
    }

    public void save()
    {
        Debug.Log("Speichere Spielstand"+getFilename());

        Player p = Component.FindObjectOfType<Player>();
        playerPosition = p.transform.position;

        if (onSave!=null) onSave (this);

        string xml = XML.Save(this);
        File.WriteAllText(getFilename(), xml);

        Debug.Log(xml);
    }
    
    public static SaveGameData load()
    {
        if (!File.Exists(getFilename()))
            return new SaveGameData();

        Debug.Log("Lade Spielstand " + getFilename());
        
        SaveGameData save = XML.Load<SaveGameData>(File.ReadAllText(getFilename()));
        Player p = Component.FindObjectOfType<Player>();
        p.transform.position = save.playerPosition;

        if (onLoad!=null) onLoad(save);

        return save;
    }
}
