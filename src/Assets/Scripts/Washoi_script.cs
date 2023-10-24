using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washoi_script : MonoBehaviour
{

    float Input_washoi;
    float old_washoi;
    public float Input_washoi_once;
    [SerializeField] float wasyoi_hankei = 5.0f;

    MikoshiCollisionDetection MCD;

    // Start is called before the first frame update
    void Start()
    {
        MCD = GetComponent<MikoshiCollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        Input_washoi = Input.GetAxis("Fire1");
        Input_washoi_once = old_washoi - Input_washoi;

        old_washoi = Input_washoi;

        if (Input_washoi_once == 1)
        {
            Check_People();


        }




    }

    private void FixedUpdate()
    {
        

        
    }

    void Check_People()
    {

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, wasyoi_hankei,Vector3.one);

        foreach (var hit in hits)
        {
            if (hit.collider.tag == "People")
            {
                Debug.Log($"���o���ꂽ�I�u�W�F�N�g {hit.collider.name}");

                MCD.peopleCount++;
                Destroy(hit.collider.gameObject);

                Debug.Log(MCD.peopleCount);

                //�l�̐���
                MCD.GenerateMikoshiPeople();

                if (MCD.peopleCount >= MCD.clearConditions && MCD.isFever == false)
                {
                    MCD.isFever = true;
                    MCD.FeverTime();
                }


            }
        }
    }

}



