using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StoryManager : MonoBehaviour
{
    public int eventNumber;
    public Image im;
    public Sprite[] images;
    public Text nText;
    public int nextSceneNumber;
    bool done;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.SoundPlay(0, 8);
        nowCorutine = StartCoroutine(Event_0());
        //SceneManager.LoadSceneAsync(nextSceneNumber);
    }
    Coroutine nowCorutine;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            eventNumber++;
            if (eventNumber > 9)
            {
                if(done == false)
                {
                    done = true;
                    SceneManager.LoadSceneAsync(nextSceneNumber);
                }
            }
            else
            {
                string cName = "Event_" + eventNumber.ToString();
                if (nowCorutine != null)
                {

                    StopCoroutine(nowCorutine);
                }
                nowCorutine = StartCoroutine(cName);

                im.sprite = images[eventNumber];
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (done == false)
            {
                done = true;
                SceneManager.LoadSceneAsync(nextSceneNumber);
            }
        }
    }

    IEnumerator Event_0()
    {
        SoundManager.instance.VoicePlay(0);
        nText.text = CSVManager.instance.LoadText(90);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_1()
    {
        SoundManager.instance.VoicePlay(1);
        nText.text = CSVManager.instance.LoadText(91);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_2()
    {
        SoundManager.instance.VoicePlay(2);
        nText.text = CSVManager.instance.LoadText(92);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_3()
    {
        SoundManager.instance.VoicePlay(3);
        nText.text = CSVManager.instance.LoadText(93);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_4()
    {
        SoundManager.instance.VoicePlay(4);
        nText.text = CSVManager.instance.LoadText(94);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_5()
    {
        SoundManager.instance.VoicePlay(5);
        nText.text = CSVManager.instance.LoadText(95);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_6()
    {
        SoundManager.instance.VoicePlay(6);
        nText.text = CSVManager.instance.LoadText(96);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_7()
    {
        SoundManager.instance.VoicePlay(7);
        nText.text = CSVManager.instance.LoadText(97);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_8()
    {
        SoundManager.instance.VoicePlay(8);
        nText.text = CSVManager.instance.LoadText(98);
        yield return new WaitForSeconds(0);
    }
    IEnumerator Event_9()
    {
        SoundManager.instance.VoicePlay(9);
        nText.text = CSVManager.instance.LoadText(99);
        yield return new WaitForSeconds(0);
    }

}
