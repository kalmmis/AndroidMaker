using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class ExcelParser : MonoBehaviour
{
    /*
       static string m_resourcePath = "Assets/Data";

       public static string GetResource(string type, int i)
       {
           string additionalPath = "level".Equals(type) ? "/level" : "/dialog";
           FileStream f = new FileStream(m_resourcePath + additionalPath + i + ".csv", FileMode.Open, FileAccess.Read);
           StreamReader reader = new StreamReader(f, System.Text.Encoding.UTF8);
           string stageStr = reader.ReadToEnd();
           return stageStr;
       }
   */

        public void writeStringToFile(string str, string filename)
    {
#if !WEB_BUILD
        string path = pathForDocumentsFile(filename);
        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(str);

        sw.Close();
        file.Close();
#endif
    }

    public string readStringFromFile(string filename)//, int lineindex)
    {
#if !WEB_BUILD
        string path = pathForDocumentsFile(filename);

        if (File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string str = null;
            str = sr.ReadLine();

            sr.Close();
            file.Close();

            return str;
        }

        else
        {
            return null;
        }
#else
return null;
#endif
    }

    public string pathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
        }

        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }

        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }



    public static string GetResource(string type, int i)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string additionalPath = "level".Equals(type) ? "/level" : "/dialog";

            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));


            FileStream f = new FileStream(path + "Documents" + additionalPath + i + ".csv", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(f, System.Text.Encoding.UTF8);
            string stageStr = reader.ReadToEnd();
            return stageStr;
        }

        else if (Application.platform == RuntimePlatform.Android)
        {
            //string additionalPath = "level".Equals(type) ? "/level" : "/dialog";
            //string androidPath = "/Resources/Data";

            //string path = Application.persistentDataPath;
            //path = path.Substring(0, path.LastIndexOf('/'));

            var list = new List<Dictionary<string, object>>();
            TextAsset data = Resources.Load("StoryData/" + type + i) as TextAsset;

            string str = data.text;
            return str;
            //FileStream f = new FileStream(data, FileMode.Open, FileAccess.Read);
            //StreamReader reader = new StreamReader(f, System.Text.Encoding.UTF8);
            //string stageStr = reader.ReadToEnd();
            //return stageStr;

        }

        else
        {
            string additionalPath = "level".Equals(type) ? "/level" : "/dialog";
            string windowsPath = "/Assets/Resources/StoryData";

            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));

            FileStream f = new FileStream(path + windowsPath + additionalPath + i + ".csv", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(f, System.Text.Encoding.UTF8);
            string stageStr = reader.ReadToEnd();
            return stageStr;


        }
    }
}
