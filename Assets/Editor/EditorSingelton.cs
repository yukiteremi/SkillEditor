using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSingelton 
{
    private static EditorSingelton ins;
    public static EditorSingelton Get()
    {
        if (ins==null)
        {
            ins = new EditorSingelton();
        }
        return ins;
    }

    public List<SkillJson> jsonlist = new List<SkillJson>();
    public Player Player;
}
