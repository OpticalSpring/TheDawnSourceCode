using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HackingInteraction : MonoBehaviour
{
    public double hackingTime;
    public double hackingTimeNow;
    public bool readyInteraction;
    public bool doneInteraction;

    public GameObject spotUI;
    public GameObject shortUI;
    public Image shortGageBar;
    public GameObject[] FIcon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool f = false;
    private void OnTriggerStay(Collider other)
    {
        if (doneInteraction == true)
        {
            return;
        }
        if (other.gameObject.GetComponent<Player>())
        {
            
            if (Input.GetKey(KeyCode.F))
            {
                if(f == false)
                {
                    f = true;
                    SoundManager.instance.SoundPlay(1, 9);
                }
                Ing();
                other.gameObject.GetComponent<Player>().ActInteraction(gameObject.transform.position);
                if (hackingTimeNow > hackingTime)
                {
                    other.gameObject.GetComponent<Player>().DoneInteraction();
                    Done();
                }
                
            }
            else
            {
                other.gameObject.GetComponent<Player>().DoneInteraction();
                Ready();
            }
        }
        else
        {

            Ready();
        }
    }


    public void Ready()
    {
        hackingTimeNow = 0;
        spotUI.SetActive(true);
        shortUI.SetActive(false);
        FIcon[0].SetActive(true);
        FIcon[1].SetActive(false);
        f = false;
    }

    public void Ing()
    {
        gameObject.GetComponent<DoneEvent>().ActEvent_4();
        FIcon[0].SetActive(false);
        FIcon[1].SetActive(true);
        hackingTimeNow += Time.deltaTime;
        shortUI.SetActive(true);
        shortGageBar.fillAmount = (float)(hackingTimeNow / hackingTime);
        if (readyInteraction == false)
        {
            gameObject.GetComponent<DoneEvent>().ActEvent_3();
        }

    }

    public void Done()
    {
        SoundManager.instance.SoundPlay(1, 10);
        doneInteraction = true;
        readyInteraction = false;
        spotUI.SetActive(false);
        shortUI.SetActive(false);
        gameObject.GetComponent<DoneEvent>().ActEvent_1();
    }


}
