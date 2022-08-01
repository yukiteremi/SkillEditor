using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
public class OneEditor : EditorWindow
{
    [MenuItem("tool/skillEditor")]
    static void skillEditor()
    {
        if (Application.isPlaying)
        {
            EditorWindow window= EditorWindow.GetWindow(typeof(OneEditor));
            window.Show();
        }
    }
    public int index1 = 0;
    public int index2 = 0;
    public string SkillBtn="";
    public List<string> list = new List<string>() { "All", "Enemy", "Player" };
    public List<string> modelList = new List<string>() { "刀客", "忍者", "白虎王" };
    public List<SkillJson> jsonlist = new List<SkillJson>();
    public GameObject player;
    private void OnEnable()
    {
        //Resources
        if (File.Exists("Assets/skillJson.json"))
        {
            jsonlist = JsonConvert.DeserializeObject<List<SkillJson>>(File.ReadAllText("Assets/skillJson.json"));
        }
        else
        {
            foreach (var item in modelList)
            {
                SkillJson skill = new SkillJson();
                skill.name = item;
                skill.list = new List<SkillBtn>();
                jsonlist.Add(skill);
            }
            
        }
        EditorSingelton.Get().jsonlist = jsonlist;
        player = GameObject.Instantiate(Resources.Load<GameObject>("PlayerModel/" + modelList[index2]), GameObject.Find("Plane").transform, false);
        EditorSingelton.Get().Player = player.GetComponent<Player>();
    }
    private void OnGUI()
    {
        int getIndex1= EditorGUILayout.Popup(index1, list.ToArray());
        if (getIndex1!=index1)
        {
            index1 = getIndex1;
            modelList.Clear();
            if (index1==0)
            {
                modelList = new List<string>() { "刀客", "忍者", "白虎王" };
            }
            else if (index1 == 1)
            {
                modelList = new List<string>() { "白虎王" };
            }
            else if (index1 == 2)
            {
                modelList = new List<string>() { "刀客", "忍者"};
            }
            if (player != null)
            {
                GameObject.Destroy(player);
            }
            index2 = 0;
            player = GameObject.Instantiate(Resources.Load<GameObject>("PlayerModel/" + modelList[index2]), GameObject.Find("Plane").transform, false);
            EditorSingelton.Get().Player = player.GetComponent<Player>();
        }
        int getIndex2= EditorGUILayout.Popup(index2, modelList.ToArray());
        if (getIndex2!= index2)
        {
            index2 = getIndex2;
            if (player!=null)
            {
                GameObject.Destroy(player);
            }
            player = GameObject.Instantiate(Resources.Load<GameObject>("PlayerModel/"+ modelList[index2]),GameObject.Find("Plane").transform,false);
            EditorSingelton.Get().Player = player.GetComponent<Player>();
        }
        SkillBtn=GUILayout.TextField(SkillBtn);
        if (GUILayout.Button("create"))
        {
            if (SkillBtn.Length<1)
            {
                Debug.Log("Encode something pls");
                return;
            }
            foreach (var item in jsonlist)
            {
                if (item.name== modelList[index2])
                {
                    SkillBtn Detail = new SkillBtn();
                    Detail.name = SkillBtn;
                    item.list.Add(Detail);
                    break;
                }
            }
            File.WriteAllText("Assets/skillJson.json", JsonConvert.SerializeObject(jsonlist));
        }
        int len = 0;
        switch (modelList[index2])
        {
            case "刀客":
                len = 0;
                break;
            case "忍者":
                len = 1;
                break;
            case "白虎王":
                len = 2;
                break;
            default:
                break;
        }
        foreach (var item in jsonlist[len].list)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label(item.name);
            if (GUILayout.Button("Open"))
            {
                TwoEditor windows =(TwoEditor)EditorWindow.GetWindow(typeof(TwoEditor));
                windows.Show();
                windows.titleContent = new GUIContent(item.name);
                windows.Init(modelList[len],item);
            }
            if (GUILayout.Button("Delete"))
            {
                foreach (var items in jsonlist[len].list)
                {
                    if (items.name==item.name)
                    {
                        jsonlist[len].list.Remove(items);
                        File.WriteAllText("Assets/skillJson.json", JsonConvert.SerializeObject(jsonlist));
                        break;
                    }
                }
                break;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
