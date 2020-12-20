using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionData : MonoBehaviour
{
    public GameObject[] iButton;
    public GameObject[] tap;
    public int bPressed = 0;

    [System.Serializable]
    public struct GamePlayTap
    {
        public int mouseSensitivityNormal;
        public int mouseSensitivityAim;
        public Text mouseSensitivityNormalText;
        public Text mouseSensitivityAimText;
        public Slider mouseSensitivityNormalSlider;
        public Slider mouseSensitivityAimSlider;
    }
    public GamePlayTap gamePlayTap;

    [System.Serializable]
    public struct SoundTap
    {
        public int BGM;
        public int SFX;
        public int Voice;
        public Text BGMText;
        public Text SFXText;
        public Text VoiceText;
        public Slider BGMSlider;
        public Slider SFXSlider;
        public Slider VoiceSlider;
    }
    public SoundTap soundTap;


    [System.Serializable]
    public struct LangTap
    {
        public Dropdown langDrop;
    }
    public LangTap langTap;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        gamePlayTap.mouseSensitivityNormal = (int)(gamePlayTap.mouseSensitivityNormalSlider.value);
        gamePlayTap.mouseSensitivityAim =  (int)(gamePlayTap.mouseSensitivityAimSlider.value);
        gamePlayTap.mouseSensitivityNormalText.text = gamePlayTap.mouseSensitivityNormal + "";
        gamePlayTap.mouseSensitivityAimText.text = gamePlayTap.mouseSensitivityAim + "";

        soundTap.BGM = (int)(soundTap.BGMSlider.value);
        soundTap.SFX = (int)(soundTap.SFXSlider.value);
        soundTap.Voice = (int)(soundTap.VoiceSlider.value);
        soundTap.BGMText.text = soundTap.BGM + "";
        soundTap.SFXText.text = soundTap.SFX + "";
        soundTap.VoiceText.text = soundTap.Voice + "";



    }

    private void OnEnable()
    {
        LoadData();
    }

    private void OnDisable()
    {
        SaveData();
        if(SoundManager.instance != null)
        {
            SoundManager.instance.SetSoundVolume();
        }
    }


    void SaveData()
    {
        PlayerPrefs.SetInt("GamePlay_1", gamePlayTap.mouseSensitivityNormal);
        PlayerPrefs.SetInt("GamePlay_2", gamePlayTap.mouseSensitivityAim);
        PlayerPrefs.SetInt("Sound_1", soundTap.BGM);
        PlayerPrefs.SetInt("Sound_2", soundTap.SFX);
        PlayerPrefs.SetInt("Sound_3", soundTap.Voice);
        PlayerPrefs.SetInt("language", langTap.langDrop.value);
    }

    void LoadData()
    {
        gamePlayTap.mouseSensitivityNormalSlider.value = PlayerPrefs.GetInt("GamePlay_1");
        gamePlayTap.mouseSensitivityAimSlider.value = PlayerPrefs.GetInt("GamePlay_2");
        soundTap.BGMSlider.value = PlayerPrefs.GetInt("Sound_1");
        soundTap.SFXSlider.value = PlayerPrefs.GetInt("Sound_2");
        soundTap.VoiceSlider.value = PlayerPrefs.GetInt("Sound_3");
        langTap.langDrop.value = PlayerPrefs.GetInt("language");
    }

    public void SetTap(int num)
    {
        for (int i = 0; i < tap.Length; i++)
        {
            tap[i].SetActive(false);
            iButton[i].SetActive(false);
        }
        tap[num].SetActive(true);
        iButton[num].SetActive(true);
        bPressed = num;
        SoundManager.instance.SoundPlay(8, 1);
    }

    public void ResetTap(int num)
    {
        switch (num)
        {
            case 0:
                gamePlayTap.mouseSensitivityNormalSlider.value = 60f;
                gamePlayTap.mouseSensitivityAimSlider.value = 30f;
                break;
            case 1:
                soundTap.BGMSlider.value = 100;
                soundTap.SFXSlider.value = 100;
                soundTap.VoiceSlider.value = 100;
                break;
            case 2:

                break;
            default:
                break;
        }
        SoundManager.instance.SoundPlay(8, 1);
    }

    public void ResetAll()
    {
        for (int i = 0; i < 3; i++)
        {
            ResetTap(i);
        }
    }
}
