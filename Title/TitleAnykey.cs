using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnykey : MonoBehaviour
{
    public Animator fadeAni;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            
            StartScene(1);
        }
    }

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
        
        fadeAni.SetBool("Fade", true);
        yield return new WaitForSecondsRealtime(1);

        SceneManager.LoadSceneAsync(i);
      
    }
}
