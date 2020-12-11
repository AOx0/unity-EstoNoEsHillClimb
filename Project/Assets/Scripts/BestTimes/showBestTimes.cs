using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showBestTimes : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas bestLevel1Canvas;
    public Canvas bestLevel2Canvas;
    public Canvas bestLevel3Canvas;
    public Canvas bestLevel4Canvas;
    public Canvas bestLevel5Canvas;
    public Canvas bestLevel6Canvas;

    public static float bestLevel1;
    public static float bestLevel2;
    public static float bestLevel3;
    public static float bestLevel4;
    public static float bestLevel5;
    public static float bestLevel6;


    void Start()
    {
        bestLevel1 = 9999999;
        bestLevel2 = 9999999;
        bestLevel3 = 9999999;
        bestLevel4 = 9999999;
        bestLevel5 = 9999999;
        bestLevel6 = 9999999;

        bestLevel1Canvas.enabled = false;
        bestLevel2Canvas.enabled = false;
        bestLevel3Canvas.enabled = false;
        bestLevel4Canvas.enabled = false;
        bestLevel5Canvas.enabled = false;
        bestLevel6Canvas.enabled = false;
    }

    

    void ShowBestTime(float levelBest, Canvas levelCanvas)
    {
        string formatedSting(float value)
        {
            return "Best time: " + (string.Format("{0:N4}", value));
        }

        void showInCanvas(Canvas canvas, string texto)
        {
            canvas.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = texto;
        }

        if (levelBest != 9999999)
        {
            showInCanvas(levelCanvas, formatedSting(levelBest));
            levelCanvas.enabled = true;
        }
        else
        {
            levelCanvas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowBestTime(bestLevel1, bestLevel1Canvas);
        ShowBestTime(bestLevel2, bestLevel2Canvas);
        ShowBestTime(bestLevel3, bestLevel3Canvas);
        ShowBestTime(bestLevel4, bestLevel4Canvas);
        ShowBestTime(bestLevel5, bestLevel5Canvas);
        ShowBestTime(bestLevel5, bestLevel6Canvas);
        
    }
}
