using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEntity 
{
    public string name;
    public float time;
    public string type;
    public string path;

    public virtual void Do()
    {
    }
    public virtual void No()
    {
    }
}
public class SkillAnimatorClip : SkillEntity
{
    public AnimationClip animatorClip;
    public override void Do()
    {
        EditorSingelton.Get().Player.Play(animatorClip,time);
    }
}
public class SkillMusicClip : SkillEntity
{
    public AudioClip audioClip;
    public override void Do()
    {
        EditorSingelton.Get().Player.BgmOn(audioClip, time);
    }
    public override void No()
    {
        EditorSingelton.Get().Player.BgmStop();
    }
}
public class SkillEffectClip : SkillEntity
{
    public GameObject goClip;
    public override void Do()
    {
        EditorSingelton.Get().Player.SetEffect(goClip, time);
    }
    
}

public class SkillJson
{
    public string name;
    public List<SkillBtn> list = new List<SkillBtn>();
}
public class SkillBtn
{
    public string name;
    public float time = 1;
    public List<SkillDetailBase> list = new List<SkillDetailBase>();
}
public class SkillDetailBase
{
    public string name;
    public float time;
    public string type;
    public string path;
}
