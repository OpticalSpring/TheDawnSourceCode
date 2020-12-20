using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUISystem : MonoBehaviour
{
    public MekaBoss boss;
    public Image bossUI_HP;
    public Image bossUI_EF;
    public Text bossUI_Percent;
    public int aniState;
    // Start is called before the first frame update
    void Start()
    {
        boss = MekaBoss.instance;
    }
    float hpbarFillNow;
    // Update is called once per frame
    void Update()
    {
        float hpbarFill = (float)boss.HP_Point / boss.HP_PointMax;
        hpbarFillNow = Mathf.Lerp(hpbarFillNow, hpbarFill, Time.deltaTime);
        bossUI_EF.fillAmount = hpbarFillNow;
        bossUI_HP.fillAmount = hpbarFill;
        bossUI_Percent.text = 100 * ((float)boss.HP_Point / boss.HP_PointMax) + " %";
        PlayerUISystem.instance.bossUI.GetComponent<Animator>().SetInteger("AniState", aniState);
    }
}