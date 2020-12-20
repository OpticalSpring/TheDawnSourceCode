using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public GameObject game;
    public GameObject voice;
    public GameObject newSound;
    private void Awake()
    {
        instance = this;
        SetSoundVolume();
    }

    private void Update()
    {
        SetBattleBGM();
        SetBattleBGM2();
    }
    public void SoundPlay(int i, int j)
    {
        game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().Play();
        game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().pitch = 1;
        if (i == 0)
        {
            game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = (float)soundVolume1 / 100f;
        }
        else
        {
            game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = (float)soundVolume2 / 100f;
        }
    }

    public void SoundPlay(int i, int j, float pitch)
    {
        game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().Play();
        game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().pitch = Random.Range(1 - pitch, 1 + pitch);
        if (i == 0)
        {
            game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = (float)soundVolume1 / 100f;
        }
        else
        {
            game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = (float)soundVolume2 / 100f;
        }
    }

    public void SoundPlay3D(int i, int j, Vector3 vec)
    {
        GameObject temp = Instantiate(game.transform.GetChild(i).GetChild(j).gameObject);
        temp.transform.parent = newSound.transform;
        temp.transform.position = vec;
        temp.GetComponent<AudioSource>().Play();
        Destroy(temp, 1);
    }

    public void SoundPlay3D(int i, int j, Vector3 vec, double desTime)
    {
        GameObject temp = Instantiate(game.transform.GetChild(i).GetChild(j).gameObject);
        temp.transform.parent = newSound.transform;
        temp.transform.position = vec;
        temp.GetComponent<AudioSource>().Play();
        Destroy(temp, (float)desTime);
    }

    public void SoundPlay3D(int i, int j, Vector3 vec, float pitch)
    {
        GameObject temp = Instantiate(game.transform.GetChild(i).GetChild(j).gameObject);
        temp.transform.parent = newSound.transform;
        temp.transform.position = vec;
        temp.GetComponent<AudioSource>().Play();
        temp.GetComponent<AudioSource>().pitch = Random.Range(1 - pitch, 1 + pitch);
        Destroy(temp, 3);
    }



    public void SoundStop(int i, int j)
    {
        StartCoroutine(I1(i, j));
    }
    public void SoundForceStop(int i, int j)
    {
        game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().Stop();
           //game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = 0;
    }

    public void RandomPlay(int i, int x, int y)
    {
        int j = Random.Range(x, y + 1);
        SoundPlay(i, j);
    }
    public void RandomPlay(int i, int x, int y, float pitch)
    {
        int j = Random.Range(x, y + 1);
        SoundPlay(i, j, pitch);
    }

    public void RandomPlayNew(int i, int x, int y, Vector3 vec)
    {
        int j = Random.Range(x, y + 1);
        SoundPlay3D(i, j, vec);
    }

    public void RandomPlayNew(int i, int x, int y, Vector3 vec, float pitch)
    {
        int j = Random.Range(x, y + 1);
        SoundPlay3D(i, j, vec, pitch);
    } 
    int soundVolume1;
    int soundVolume2;
    int soundVolume3;
    public void SetSoundVolume()
    {
        soundVolume1 = PlayerPrefs.GetInt("Sound_1");
        soundVolume2 = PlayerPrefs.GetInt("Sound_2");
        soundVolume3 = PlayerPrefs.GetInt("Sound_3");
        for (int i = 0; i < game.transform.GetChild(0).childCount; i++)
        {
            game.transform.GetChild(0).GetChild(i).GetComponent<AudioSource>().volume = (float)soundVolume1/100f;
        }
        for (int j = 1; j < game.transform.childCount; j++)
        {
            for (int i = 0; i < game.transform.GetChild(j).childCount; i++)
            {
                game.transform.GetChild(j).GetChild(i).GetComponent<AudioSource>().volume = (float)soundVolume2/100f;
            }
        }
        for (int j = 0; j < voice.transform.childCount; j++)
        {
            for (int i = 0; i < voice.transform.GetChild(j).childCount; i++)
            {
                voice.transform.GetChild(j).GetChild(i).GetComponent<AudioSource>().volume = (float)soundVolume3 / 100f;
            }
        }
    }

    IEnumerator I1(int i, int j)
    {
        for (int k = 100; k >= 0; --k)
        {
            if (game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume > (float)k / 100.0f)
                game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = (float)k / 100.0f;

            yield return new WaitForSeconds(0.01f);
        }
        game.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().Stop();
    }


    public void VoicePlay(int j)
    {
        VoiceStop();
        voice.transform.GetChild(0).GetChild(j).gameObject.GetComponent<AudioSource>().Play();
        voice.transform.GetChild(0).GetChild(j).gameObject.GetComponent<AudioSource>().volume = (float)soundVolume3 / 100f;
    }

    public void VoiceRandomPlay(int x, int y)
    {
        VoiceStop();
        int j = Random.Range(x, y + 1);
        VoicePlay(j);
    }
    public void VoiceStop()
    {
        for (int j = 0; j < voice.transform.childCount; j++)
        {
            for (int i = 0; i < voice.transform.GetChild(j).childCount; i++)
            {
                voice.transform.GetChild(j).GetChild(i).GetComponent<AudioSource>().Stop();
            }
        }
    }
    public int assultValue;

       public float b1 = 0, b2 = 0;

    public void SetBattleBGM()
    {
        
        b1 = Mathf.Lerp(b1, 1 - assultValue, Time.fixedDeltaTime * 0.5f);
        b2 = Mathf.Lerp(b2, assultValue, Time.fixedDeltaTime * 0.5f);
        game.transform.GetChild(0).GetChild(2).GetComponent<AudioSource>().volume = b1 * ((float)soundVolume1 / 100f);
        game.transform.GetChild(0).GetChild(1).GetComponent<AudioSource>().volume = b2 * ((float)soundVolume1 / 100f);
    }

    public void SetBattleBGM2()
    {

        b1 = Mathf.Lerp(b1, 1, Time.fixedDeltaTime * 0.5f);
        b2 = Mathf.Lerp(b2, assultValue, Time.fixedDeltaTime * 0.5f);
        game.transform.GetChild(0).GetChild(3).GetComponent<AudioSource>().volume = b1 * ((float)soundVolume1 / 100f);
        game.transform.GetChild(0).GetChild(4).GetComponent<AudioSource>().volume = b2 * ((float)soundVolume1 / 100f);
    }

    public void SetBGMLowPassFilter(int v)
    {
        for (int i = 0; i < game.transform.GetChild(0).childCount; i++)
        {
            game.transform.GetChild(0).GetChild(i).gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = v;
        }
    }
}
