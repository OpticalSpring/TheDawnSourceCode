using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject lightGroups;
    bool cc;
    bool done;
    public GameObject credit;
    public GameObject cam;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Party());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            StartScene(0);
        }
        if(cc == true)
        {
            cam.transform.LookAt(target.transform.position);
            //credit.transform.localPosition += new Vector3(0, 120, 0) * Time.deltaTime;
        }
    }

    IEnumerator Party()
    {
        yield return new WaitForSeconds(3f);
        //SoundManager.instance.SoundPlay(0, 9);
        lightGroups.SetActive(true);
        cc = true;
        credit.SetActive(true);
        
        SoundManager.instance.SoundPlay(0, 10);
        yield return new WaitForSeconds(1.0f);
        player.SetInteger("AniState", 1);
        camani.SetInteger("AniState", 1);
        yield return new WaitForSeconds(32f);
        StartScene(0);
    }


    public Animator fadeAni;
    public Animator player;
    public Animator camani;
    public void StartScene(int i)
    {

        if (done == true)
        {
            return;
        }
        done = true;
        StartCoroutine(DelayStartScene(i));
    }

    IEnumerator DelayStartScene(int i)
    {
        fadeAni.transform.gameObject.SetActive(true);
        fadeAni.SetBool("Fade", true);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadSceneAsync(i);
    }
}
