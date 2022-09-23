using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI message;
    [SerializeField]
    private float alphaSpeed = 0.3f;
    [SerializeField]
    private Image image;
    [SerializeField]
    private AudioSource explosion;
    [SerializeField]
    private AudioSource bgNoise;
    private int progress = 0;


    private string[] messages;
    bool canClick = true;
    bool alphaUp;
    int time;

    Canvas canvas;

    void Start()
    {
        bgNoise.Play();
        if (GameManager.instance.difused <=0) {
            GameManager.instance.difused = 1;
        }
        if(GameManager.instance.difused < 8)
        {
            explosion.Play();
        }
        time = 110000000 / GameManager.instance.difused * 8 * 230;
        if (time <= 0)
        {
            time = -time;
        }
        messages = new string [] {
            "You’ve helped to clear "+GameManager.instance.difused.ToString()+" Landmines during your run.", 
            "Currently there are 110 million mines around the world.", 
            "At your rate it will take " + time +" years to clear all the mines around the world.", 
            "Landmines are designed to maim rather than kill an enemy soldier. This follows the 'logic' that more resources are taken up caring for an injured soldier on the battlefield than dealing with a soldier who has been killed.",
            "In 2020 there were 135,500 mines cleared over an area of 146km2. \nEven at this rate it’ll take 850 years to clear all the mines around the world." ,
            "In 2020 there were 7,073 casualties of mines. 2,492 people were killed and 4,561 people were injured.",
            "About 80% of these casualties were Civilians and 50% of those were Children.",
            "Help us save innocent lives and join us in Finishing the Job Visit http://www.icbl.org/en-gb/finish-the-job/take-action/icbl-calls-to-action.aspx for more information."
        };
        canvas = FindObjectOfType<Canvas>();
        message.text = messages[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && canClick && progress < messages.Length-1) {
            canClick = false;
            alphaUp = false;
        }

        Debug.Log(message.alpha);
        if (!canClick) {
            message.alpha += (alphaUp? 1 : -1) * alphaSpeed * Time.deltaTime;
            message.alpha = Mathf.Clamp(message.alpha, 0, 1);
            if (message.alpha  <=0) {
                alphaUp = true;
                progress++;
                message.text = messages[progress];
                message.alpha = 0;
            }
            if (message.alpha >= 1) {
                canClick = true;
            }

        }

        if (image.color.a > 0) {
            Color tempColor = image.color;
            tempColor.a -= alphaSpeed;
            image.color = tempColor;
        }
    }
}
