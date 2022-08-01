using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public Transform effectPoint;
    public Queue<AnimationClip> AniQue = new Queue<AnimationClip>();
    public Queue<float> AniTimeQue = new Queue<float>();
    float AniTime = 0;
    bool AniFlag=true;

    public Queue<GameObject> GoQue = new Queue<GameObject>();
    public Queue<float> GoTimeQue = new Queue<float>();
    float GOTime = 0;
    bool GOFlag = true;
    //public List<AnimationClip> clip = new List<AnimationClip>();
    AnimatorOverrideController animatorC;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        effectPoint = transform.Find("effectPoint");
        animatorC = new AnimatorOverrideController();
        animatorC.runtimeAnimatorController = animator.runtimeAnimatorController;
        //if (name.Contains("µ¶¿Í"))
        //{
        //    animatorC["IdleC"] = clip[0];
        //}
        //else if (name.Contains("ÈÌÕß"))
        //{
        //    animatorC["IdleC"] = clip[1];
        //}
        //else if (name.Contains("°×»¢Íõ"))
        //{
        //    animatorC["IdleC"] = clip[2];
        //}
        //animatorC["RunAttF"] = new AnimationClip();
        animator.runtimeAnimatorController = animatorC;
    }

    public void Play(AnimationClip clip,float time)
    {
        AniQue.Enqueue(clip);
        AniTimeQue.Enqueue(time);
    }
    public void BgmOn(AudioClip clip,float time)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void BgmStop()
    {
        audioSource.Stop();
    }
    public void SetEffect(GameObject clip,float time)
    {
        GoQue.Enqueue(clip);
        GoTimeQue.Enqueue(time);
    }
    // Update is called once per frame
    void Update()
    {
        if (AniFlag)
        {
            if (AniTimeQue.Count > 0)
            {
                AniTime = AniTimeQue.Dequeue();
                AniFlag = false;
            }
        }
        else{
            AniTime -= Time.deltaTime;
            if (AniTime <= 0)
            {
                AniFlag = true;
                if (AniQue.Count>0)
                {
                    animatorC["RunAttF"] = AniQue.Dequeue();
                    animator.SetTrigger("Start");
                }
            }
        }

        if (GOFlag)
        {
            if (GoTimeQue.Count > 0)
            {
                GOTime = GoTimeQue.Dequeue();
                GOFlag = false;
            }
        }
        else
        {
            GOTime -= Time.deltaTime;
            if (GOTime <= 0)
            {
                GOFlag = true;
                if (GoQue.Count > 0)
                {
                    GameObject clone= Instantiate(GoQue.Dequeue(),effectPoint,false);
                    Destroy(clone,5);
                }
            }
        }
    }
}
