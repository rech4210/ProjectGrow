using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}


public class SoundManager : MonoBehaviour
{
    [SerializeField]
    List<Sound> sounds;

    [SerializeField]
    AudioSource[] AudioPlayer = null;

    public void PlaySoundclip(string _name)
    {
        foreach(var i in sounds)
        {
            if(i.name == _name)
            {
                for(int j = 0; j < AudioPlayer.Length; j++)
                {
                    if(!AudioPlayer[j].isPlaying)
                    {
                        AudioPlayer[j].clip = i.clip;
                        AudioPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("빈 플레이어 없음 ");
                return;
            }
        }

        Debug.Log("해당 이름의 사운드 없음 ");
    }
}
