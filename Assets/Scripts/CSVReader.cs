using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
 
public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
 
    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        string path = "csvData/";
        TextAsset data = Resources.Load (path + file) as TextAsset;
 
        var lines = Regex.Split (data.text, LINE_SPLIT_RE);
 
        if(lines.Length <= 1) return list;
 
        var header = Regex.Split(lines[0], SPLIT_RE);
        for(var i=1; i < lines.Length; i++) {
 
            var values = Regex.Split(lines[i], SPLIT_RE);
            if(values.Length == 0 ||values[0] == "") continue;
 
            var entry = new Dictionary<string, object>();
            for(var j=0; j < header.Length && j < values.Length; j++ ) {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if(int.TryParse(value, out n)) {
                    finalvalue = n;
                } else if (float.TryParse(value, out f)) {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add (entry);
        }
        return list;
    }
/*
    public static List<Dictionary<string, Dictionary<string, object>>> Read3rdArray(string file)
    {
        var lists = new List<Dictionary<string, Dictionary<string, object>>>();

        string path = "csvData/";
        TextAsset data = Resources.Load (path + file) as TextAsset;
        
        var lines = Regex.Split (data.text, LINE_SPLIT_RE);

        var header = Regex.Split(lines[0], SPLIT_RE);
        //var id = header[0];
        for(var a=0; a < lines.Length; a++)
        {
            var values = Regex.Split(lines[a+1], SPLIT_RE);
            if(values.Length == 0 ||values[0] == "") continue;
            
            var id = values[0];
            var lv = values[1];
            var list = new Dictionary<string, object>();
            
            for(var b = 2; b < values.Length; b++)
            {
                string value = values[b];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if(int.TryParse(value, out n)) {
                    finalvalue = n;
                } else if (float.TryParse(value, out f)) {
                    finalvalue = f;
                }
                list[lv] = finalvalue;
            }
            lists[a][id] = list;
        }
        return lists;
    }

    public static Dictionary<string, Dictionary<string, object>> ReadArray(string file)
    {
        var lists = new Dictionary<string, Dictionary<string, object>>();

        string path = "csvData/";
        TextAsset data = Resources.Load (path + file) as TextAsset;
        
        var lines = Regex.Split (data.text, LINE_SPLIT_RE);

        var header = Regex.Split(lines[0], SPLIT_RE);
        //var id = header[0];
        for(var a=0; a < lines.Length - 1 ; a++)
        {
            var values = Regex.Split(lines[a+1], SPLIT_RE);
            if(values.Length == 0 ||values[0] == "") continue;
            
            var id = values[0];
            var lv = values[1];
            var list = new Dictionary<string, object>();
            //Debug.Log("id is " + id + "lv is " + lv);
            
            for(var b = 2; b < values.Length; b++)
            {
                string value = values[b];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if(int.TryParse(value, out n)) {
                    finalvalue = n;
                } else if (float.TryParse(value, out f)) {
                    finalvalue = f;
                }
                list[lv] = finalvalue;
                //Debug.Log("list key is" + lv);
                //Debug.Log("list[" + id + "][" + header[b] + "] is " +lists[id][header[b]]);
                //lists[id][lv] = finalvalue;
                //ㄴ얜 왜 안되지
            }
            lists[id] = list;
            //Debug.Log("lists key is" + id);
            //Debug.Log("count");
        }
        return lists;
    }
*/

}
