
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int highScore;

    public void Save(string filename)
    {
        FileStream stream =
            new FileStream
                (string.Format("{0}/{1}.save", Application.persistentDataPath, filename),
                FileMode.Create);
        using(stream)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }
    }

    public static SaveData Load(string filename)
    {
        string newFilename = string.Format("{0}/{1}.save", Application.persistentDataPath, filename);
        if (Application.persistentDataPath.Contains(newFilename))
        {
            FileStream stream =
                new FileStream
                    (string.Format("{0}/{1}.save", Application.persistentDataPath, filename),
                    FileMode.Open,
                    FileAccess.Read);

            using (stream)
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream) as SaveData;
            }
        }
        else
        {
            Debug.LogError("ERROR: File " + filename + " could not be read. Load failed.");
            return new SaveData();
        }
    }
}
