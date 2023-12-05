using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigdollController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody[] rBody;
    void Start()
    {
        //���g�������W�b�h�{�f�B��S�Ĕz��Ɋi�[
        rBody = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody RB in rBody)
        {
            RB.isKinematic = true;
        }
    }
    private void Update()
    {
        if(Input.anyKeyDown)
        {
            if(rBody[0].isKinematic == true)
            {
                SetRagDoll(false);
            }
            else
                SetRagDoll(true);
        }
    }
    //���O�h�[���̃I���E�I�t�֐�
    void SetRagDoll(bool on_off)
    {
        //rBody�z����̃��W�b�h�{�f�B�S�Ă�isKinematic��؂�ւ���
        foreach (Rigidbody RB in rBody)
        {
            RB.isKinematic = on_off;
        }
    }
}
