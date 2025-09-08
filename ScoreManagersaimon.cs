using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ScoreManagersaimon : MonoBehaviour
{
    [Header("テキスト設定")]
    public TextMeshProUGUI _GemCount;
    public TextMeshProUGUI _GemCount1;
    public TextMeshProUGUI _ScoreCount;


    [Header("ジェム,スコア設定")]
    public int GemCount = 0;
    int TotalScore = 0;
    public int O_scoreCount;
    public int B_scoreCount;
    public string characterName;
    private string Myname;
    private GameObject ScoerText;
    private GameObject GemText;
    private GameObject GemText1;
    private bool On = false;

    private void Update()
    {

        if (_GemCount != null)
        {
            _GemCount.text = "x" + GemCount;
            _GemCount1.text = "" + GemCount;
            _ScoreCount.text = TotalScore + "pt";

        }


            if (SceneManager.GetActiveScene().name == "Main" && On == false)
        {
            Debug.Log("呼ばれた");
            Myname = this.gameObject.name;
            if (name == "Hunter1(Clone)")
            {
                ScoerText = GameObject.Find("P1_ Score");
                _ScoreCount = ScoerText.GetComponent<TextMeshProUGUI>();
                GemText = GameObject.Find("P1_GemCount");
                _GemCount = GemText.GetComponent<TextMeshProUGUI>();
                GemText1 = GameObject.Find("P1_GemCount (1)");
                _GemCount1 = GemText1.GetComponent<TextMeshProUGUI>();
            }
            else if (name == "Hunter2(Clone)")
            {
                ScoerText = GameObject.Find("P1_ Score3");
                _ScoreCount = ScoerText.GetComponent<TextMeshProUGUI>();
                GemText = GameObject.Find("P1_GemCount3");
                _GemCount = GemText.GetComponent<TextMeshProUGUI>();
                GemText1 = GameObject.Find("P1_GemCount3 (1)");
                _GemCount1 = GemText1.GetComponent<TextMeshProUGUI>();
            }
            else if (name == "Hunter3(Clone)")
            {
                ScoerText = GameObject.Find("P1_ Score4");
                _ScoreCount = ScoerText.GetComponent<TextMeshProUGUI>();
                GemText = GameObject.Find("P1_GemCount4");
                _GemCount = GemText.GetComponent<TextMeshProUGUI>();
                GemText1 = GameObject.Find("P1_GemCount4 (1)");
                _GemCount1 = GemText1.GetComponent<TextMeshProUGUI>();
            }
            else if (name == "snake(Clone)")
            {
                ScoerText = GameObject.Find("P1_ Score2");
                _ScoreCount = ScoerText.GetComponent<TextMeshProUGUI>();
                GemText = GameObject.Find("P1_GemCount2");
                _GemCount = GemText.GetComponent<TextMeshProUGUI>();
                GemText1 = GameObject.Find("P1_GemCount2 (1)");
                _GemCount1 = GemText1.GetComponent<TextMeshProUGUI>();
            }
            On = true;
        }
    }


    public void Hunter_O_CountUp()
    {
        GemCount += 1;
        TotalScore += O_scoreCount;
    }
    public void Hunter_B_CountUp()
    {
        GemCount += 1;
        TotalScore += B_scoreCount;
    }
    public void GemCountUp()
    {
        GemCount += 1;
    }

    public void GemCountDawn()
    {
        GemCount -= 1;
    }

    public int GetTotalScore()
    {
        return TotalScore;
    }
}
