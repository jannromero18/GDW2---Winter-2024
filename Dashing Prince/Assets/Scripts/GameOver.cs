using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class GameOver: MonoBehaviour
{
    public static int score = 0;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Update the count display.
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetScoreText()
    {
        // Update the count text with the current count.
        scoreText.text = "You killed " + score.ToString() + " enemies";

        //reset score just incase game is replayed
        score = 0;
    }
}
