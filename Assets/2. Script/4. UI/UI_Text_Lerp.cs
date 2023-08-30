using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Text_Lerp : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public float duration = 2f;
    public float score = 1000;
    public float scoreTo = 800;

    
    private void Update()
    {
        StartCoroutine(UpdateTextCoroutine(score, scoreTo, duration));
    }

    public void StartLerp(float from, float to, float time) 
    {
        //scoreText.text = StartCoroutine(UpdateTextCoroutine(from, to, time));
    }

    private  IEnumerator UpdateTextCoroutine(float from, float to, float time)
    {
        float currentTime = Time.timeSinceLevelLoad;
        float elapsedTime = 0.0f;
        float lastTime = currentTime;

        while (time > 0 && elapsedTime < time)
        {
            //Update Time
            currentTime = Time.timeSinceLevelLoad;
            elapsedTime += currentTime - lastTime;
            lastTime = currentTime;

            //Update current value
            score = Mathf.Lerp(from, to, elapsedTime / time);

            //Update the UI text component
            scoreText.text = "+ " + (score.ToString("."));

            yield return null;
        }

        scoreText.text = "+ " + to.ToString();
    }

}
