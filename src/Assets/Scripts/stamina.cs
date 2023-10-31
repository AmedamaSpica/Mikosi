using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stamina : MonoBehaviour
{

    [SerializeField] int stamina_number_first = 3;
    public int[] stamina_value;
    public int stamina_number_now;
    int image_number = 0;
    [SerializeField]int stamina_heal_needTime = 5;
    Vector3 stamina_image_pos = new Vector3(800,-400,0);


    [SerializeField] Image stamina_image;
    public Image[] image_clone;
    [SerializeField] Transform canvas;

    int time = 0;


    // Start is called before the first frame update
    void Start()
    {
        stamina_value = new int[stamina_number_first + 1] ;
        image_clone = new Image[stamina_number_first] ;
        stamina_number_now = stamina_number_first;

        

       Vector3 vectorCanvas = canvas.localPosition + stamina_image_pos * canvas.localScale.x; //�L�����p�X��Scale�ɍ��킹��B


        foreach (int stamina in stamina_value)
        {
            stamina_value[stamina] = 0;
        }

        for(int i = 0; i < stamina_number_first; i++)
        {

            image_clone[i] =  Instantiate(stamina_image, vectorCanvas, Quaternion.identity, canvas);

            vectorCanvas.x -= 100 * canvas.localScale.x;


            Debug.Log(stamina_image_pos);

        }
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    private void FixedUpdate()
    {
        time++;

        if(time >= 50 )
        {
            
            stamina_value[stamina_number_now] += 1;

            if (stamina_value[stamina_number_now] == stamina_heal_needTime)
            {              
                if (stamina_number_now < stamina_number_first)
                {

                    image_clone[stamina_number_now ].color = new Color(1,1,0);
                    stamina_value[stamina_number_now] = 0;
                    stamina_number_now++;
                   
                }

            }

            time = 0;
        }

        

    }
}
