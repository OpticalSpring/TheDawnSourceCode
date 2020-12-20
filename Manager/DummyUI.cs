using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyUI : MonoBehaviour
{
    public Player player;
    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t.text = player.transform.position + "\n" + player.PlayerFSM + "\n" + player.PlayerMode + "\n" + player.PlayerJump + "\n탄약 " + player.ammo + "\n회피 " + player.dodgeStack;
    }
}
