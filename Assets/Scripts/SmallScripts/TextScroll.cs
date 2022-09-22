using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScroll : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI message, subMessage;
    [SerializeField]
    private float scrollSpeed;

    Canvas canvas;

    void Start()
    {
        if (GameManager.instance.difused <=0) {
            GameManager.instance.difused = 1;
        }
        canvas = FindObjectOfType<Canvas>();
        message.text = "You’ve helped to clear "+GameManager.instance.difused.ToString()+" Landmines during your run. \nCurrently there are 110 millions mine around the world. At your rate it will take " + (110000000 / (GameManager.instance.difused * 8*230)) +" years to clear all the mines around the world  Landmines are designed to maim rather than kill an enemy soldier. This follows the 'logic' that more resources are taken up caring for an injured soldier on the battlefield than dealing with a soldier who has been killed.  In 2020 there were 135,500 mines cleared over an area of 146km2 Even at this rate it’ll take 850 years to clear all the mines around the world.  In 2020 there were 7,073 casualties of mines. 2,492 people were killed and 4,561 people were injured.";
        subMessage.text = "About 80% of these casualties were Civilians and 50% of those were Children. Help us save innocent lives and join us in Finishing the Job Visit http://www.icbl.org/en-gb/finish-the-job/take-action/icbl-calls-to-action.aspx for more information.";
        //text.transform.position = new Vector3(canvas.transform.localScale.x/2, canvas.transform.localScale.y/2 + text.transform.localScale.y/2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        message.transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        subMessage.transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
    }
}
