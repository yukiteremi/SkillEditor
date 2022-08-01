using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
public class TwoEditor : EditorWindow
{
    public string myName;
    public SkillBtn skillBtn;
    public string[] arr = new string[4] { "null","Animator","Music","Effect"};
    public List<SkillEntity> EntityList = new List<SkillEntity>();
    int index2=0;
    public void Init(string name, SkillBtn skill)
    {
        skillBtn = skill;
        myName = name;

        foreach (var item in skillBtn.list)
        {
            switch (item.type)
            {
                case "Animator":
                    SkillAnimatorClip AniClip = new SkillAnimatorClip();
                    AniClip.time = item.time;
                    if (item.path!=null)
                    {
                        AniClip.animatorClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(item.path);
                        EntityList.Add(AniClip);
                    }
                    break;
                case "Music":
                    SkillMusicClip MusicClip = new SkillMusicClip();
                    MusicClip.time = item.time;
                    if (item.path != null)
                    {
                        MusicClip.audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(item.path);
                        EntityList.Add(MusicClip);
                    }
                    break;
                case "Effect":
                    SkillEffectClip EffectClip = new SkillEffectClip();
                    EffectClip.time = item.time;
                    if (item.path != null)
                    {
                        EffectClip.goClip = AssetDatabase.LoadAssetAtPath<GameObject>(item.path);
                        EntityList.Add(EffectClip);
                    }
                    break;
                default:
                    break;
            }

        }
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Play"))
        {
            foreach (var item in EntityList)
            {
                item.Do();
            }
        }
        if (GUILayout.Button("Pause"))
        {
            foreach (var item in EntityList)
            {
                item.No();
            }
        }
        EditorGUILayout.EndHorizontal();
        skillBtn.time = EditorGUILayout.Slider(skillBtn.time, 0, 2);
        GUILayout.BeginHorizontal("box");
        int getIndex2 = EditorGUILayout.Popup(index2,arr);
        if (getIndex2!= index2)
        {
            index2 = getIndex2;
        }
        if (GUILayout.Button("Create"))
        {
            if (index2==0)
            {
                Debug.Log("Dont Create Null SkillDetail");
                return;
            }
            switch (index2)
            {
                case 1:
                    SkillDetailBase DetailAni = new SkillDetailBase();
                    DetailAni.name = "";
                    DetailAni.path = "";
                    DetailAni.time = 0;
                    DetailAni.type = "Animator";
                    skillBtn.list.Add(DetailAni);
                    SkillAnimatorClip AniClip = new SkillAnimatorClip();
                    EntityList.Add(AniClip);
                    break;
                case 2:
                    SkillDetailBase DetailMic = new SkillDetailBase();
                    DetailMic.name = "";
                    DetailMic.path = "";
                    DetailMic.time = 0;
                    DetailMic.type = "Music";
                    skillBtn.list.Add(DetailMic);
                    SkillMusicClip MusicClip = new SkillMusicClip();
                    EntityList.Add(MusicClip);
                    break;
                case 3:
                    SkillDetailBase DetailEff = new SkillDetailBase();
                    DetailEff.name = "";
                    DetailEff.path = "";
                    DetailEff.time = 0;
                    DetailEff.type = "Effect";
                    skillBtn.list.Add(DetailEff);
                    SkillEffectClip EffectClip = new SkillEffectClip();
                    EntityList.Add(EffectClip);
                    break;
                default:
                    break;
            }
            File.WriteAllText("Assets/skillJson.json", JsonConvert.SerializeObject(EditorSingelton.Get().jsonlist));
        }
        EditorGUILayout.EndHorizontal();
        int len = 0;
        foreach (var item in skillBtn.list)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label(item.name);
            if (GUILayout.Button("Delete"))
            {
                skillBtn.list.Remove(item);
                EntityList.RemoveAt(len);
                File.WriteAllText("Assets/skillJson.json", JsonConvert.SerializeObject(EditorSingelton.Get().jsonlist));
                return;
            }
            EditorGUILayout.EndHorizontal();
            float num = EditorGUILayout.FloatField(item.time);
            if (num!= item.time)
            {
                item.time = num;
                EntityList[len].time = item.time;
                File.WriteAllText("Assets/skillJson.json", JsonConvert.SerializeObject(EditorSingelton.Get().jsonlist));
            }
            switch (item.type)
            {
                case "Animator":
                    SkillAnimatorClip Anitemp= EntityList[len] as SkillAnimatorClip;
                    AnimationClip temp1 = EditorGUILayout.ObjectField(Anitemp.animatorClip, typeof(AnimationClip),false) as AnimationClip;
                    if (temp1!= Anitemp.animatorClip)
                    {
                        Anitemp.animatorClip = temp1;
                        item.path = AssetDatabase.GetAssetPath(temp1);
                        item.name = temp1.name;
                        File.WriteAllText("Assets/skillJson.json", JsonConvert.SerializeObject(EditorSingelton.Get().jsonlist));
                    }
                    break;
                case "Music":
                    SkillMusicClip Musictemp = EntityList[len] as SkillMusicClip;
                    AudioClip temp2 = EditorGUILayout.ObjectField(Musictemp.audioClip, typeof(AudioClip), false) as AudioClip;
                    if (temp2 != Musictemp.audioClip)
                    {
                        Musictemp.audioClip = temp2;
                        item.path = AssetDatabase.GetAssetPath(temp2);
                        item.name = temp2.name;
                        File.WriteAllText("Assets/skillJson.json", JsonConvert.SerializeObject(EditorSingelton.Get().jsonlist));
                    }
                    break;
                case "Effect":
                    SkillEffectClip Effecttemp = EntityList[len] as SkillEffectClip;
                    GameObject temp3 = EditorGUILayout.ObjectField(Effecttemp.goClip, typeof(GameObject), false) as GameObject;
                    if (temp3 != Effecttemp.goClip)
                    {
                        Effecttemp.goClip = temp3;
                        item.path = AssetDatabase.GetAssetPath(temp3);
                        item.name = temp3.name;
                        File.WriteAllText("Assets/skillJson.json", JsonConvert.SerializeObject(EditorSingelton.Get().jsonlist));
                    }
                    break;
            }
            len++;
        }
    }
}
