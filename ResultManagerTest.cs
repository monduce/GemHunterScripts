using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResultManagerTest : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI[] PtText;

    public Transform[] rankPositions;

    public GameObject Slime;
    public GameObject Dwarf;
    public GameObject Fox;
    public GameObject Human;

    void Start()
    {
        List<PlayerScore> scores = GameManagersaimon.Instance.playerScores;
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        winnerText.text = "Winner";

        for (int i = 0; i < scores.Count; i++)
        {
            PtText[i].text = /*{i + 1}st: {scores[i].name} -*/ $"{ scores[i].score}pt";
        }

        for (int i = 0; i < scores.Count; i++)
        {
            GameObject characterPrefab = null;

            switch (scores[i].name)
            {
                case "slime":
                    characterPrefab = Slime;
                    Debug.Log("slime");
                    break;
                case "dwarf":
                    characterPrefab = Dwarf;
                    Debug.Log("dwarf");
                    break;
                case "fox":
                    characterPrefab = Fox;
                    Debug.Log("fox");
                    break;
                case "human":
                    characterPrefab = Human;
                    Debug.Log("human");
                    break;
                default:
                    Debug.LogWarning($"Unknown player name: {scores[i].name}");
                    break;
            }

            if (characterPrefab != null && i < rankPositions.Length)
            {
                GameObject character = Instantiate(characterPrefab);
                character.transform.SetParent(rankPositions[i], false);

                if (i == 0)
                {
                    character.transform.localScale = Vector3.one;
                }
                else
                {
                    character.transform.localScale = Vector3.one * 0.23f;
                }

                SpriteRenderer sr = character.GetComponentInChildren<SpriteRenderer>();
                if (sr != null)
                {
                    if (i == 0)
                    {
                        sr.sortingOrder = 0;
                    }
                    else
                    {
                        sr.sortingOrder = i + 1;
                    }
                }
            }
        }
    }
}
