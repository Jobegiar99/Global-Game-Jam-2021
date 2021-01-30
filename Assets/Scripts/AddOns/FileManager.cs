using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class FileManager
{
    public static void Write(string name, string text)
    {
        string path = Application.streamingAssetsPath + "/" + name;
        File.WriteAllText(path, text);
        RefreshEditorProjectWindow();
    }


    public static string Read(string name)
    {
        string text = "";
        string path = Application.streamingAssetsPath + "/" + name;

        if (!File.Exists(path))
            return null;
        text = File.ReadAllText(path);
        return text;

    }



    


    public static void WriteJSONArrayElement(string pathName, string arrayName, object element)
    {
        if (element == null)
            return;


        string path = Application.streamingAssetsPath + "/" + pathName;
        string json = JsonUtility.ToJson(element);
        //Si no existe la ubicacion creamos el array
        if (!File.Exists(path))
        {
            string text = "{\"" + arrayName + "\": [\n\t" + json + "\n]}";
            Write(pathName, text);
            RefreshEditorProjectWindow();
        }
        else
        {
            //Remover el ultimo caracter que deberia de ser ]
            FileStream fsOut = File.OpenWrite(path);
            fsOut.Position = fsOut.Length - 3;
            string text = ",\n\t" + json + "\n]}";
            //Convertir a byte
            byte[] data = Encoding.UTF8.GetBytes(text);
            fsOut.Write(data, 0, data.Length);


            fsOut.Close();
        }

    }

    public static void WriteCompleteJSONArray(string pathName, string arrayName, List<object> element)
    {
        string path = Application.streamingAssetsPath + "/" + pathName;
        File.Delete(path);//Eliminamos el archivo anterior por si existe

        RefreshEditorProjectWindow();
        element.ForEach(i => { WriteJSONArrayElement(pathName, arrayName, i); });
    }




    public static void DeleteJSONArrayElement(string pathName, object element)
    {
        string path = Application.streamingAssetsPath + "/" + pathName;
        string json = JsonUtility.ToJson(element);
        Debug.Log(json);

        //Si no existe el archivo no hacemos nada
        if (!File.Exists(path))
            return;

        var file = new List<string>(File.ReadAllLines(path));


    }

    private static void RefreshEditorProjectWindow()
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }





}
