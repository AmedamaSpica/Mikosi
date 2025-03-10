using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
public class LoadController : MonoBehaviour
{
    [SerializeField] private float People_Space;
    [SerializeField] private GameObject center;
    public  GameObject end;
    public  GameObject begin;
    public  GameObject Left;
    public  GameObject Right;
    [SerializeField] private GameObject RightLoadPoint;
    [SerializeField] private GameObject LeftLoadPoint;
    [SerializeField] private GameObject Load;
    [SerializeField] private GameObject TurnLoad;
    [SerializeField] private GameObject RightEndTurnLoad;
    [SerializeField] private GameObject LeftEndTurnLoad;
    [SerializeField] GameObject people;
    [SerializeField] GameObject food;
    [SerializeField] GameObject car;
    [SerializeField] private LoadDirector Director;
    private player player;
    private Vector3 SpawnPosition = Vector3.zero;
    private List<Vector3> CoodinateList = new List<Vector3>();
    public List<GameObject> ObjectList = new List<GameObject>();
    public GameObject TurnStartLoad;
    Quaternion LoadRotate;
    [SerializeField] private int PeopleGenerationMin;
    [SerializeField] private int PeopleGenerationMax;
    [SerializeField] private int FoodGenerationMin;
    [SerializeField] private int FoodGenerationMax;
    [SerializeField] private int CarGenerationMin;
    [SerializeField] private int CarGenerationMax;
    private bool HitCheck = true;
    [SerializeField] private TurnSlider turnSlider;
    [SerializeField] private TurnStick turnStick;
    [SerializeField] private bool randObject = false;
    public enum LoadType
    { 
        Up,
        Right,
        Down,
        Left
    }
    public LoadType Angle;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<player>();
        Director = GameObject.Find("LoadDirector").GetComponent<LoadDirector>();
        turnSlider = GameObject.Find("UICanvas").GetComponent<TurnSlider>();
        Director.LoadPlus(this.gameObject);
        if(randObject)
        {
            PeopleListGenerator();
            Generation();
        }
         
    }
    private void OnTriggerEnter(Collider other)
    {
       if(HitCheck)
        {
            if (other.gameObject.tag == "Player")
            {
                switch (Angle)
                {
                    case LoadType.Up: player.Angle = player.playerType.Up; break;
                    case LoadType.Right: player.Angle = player.playerType.Right; break;
                    case LoadType.Down: player.Angle = player.playerType.Down; break;
                    case LoadType.Left: player.Angle = player.playerType.Left; break;
                    default: break;
                }
                if (Director.turnCheck)
                {
                    Director.SE.Caveat();
                    if (Director.RightTurn)
                    {
                        TurnStartLoad = this.gameObject;
                        Director.StartTurnLoad(TurnStartLoad);
                        LoadRotate = gameObject.transform.rotation;
                        Quaternion q = Quaternion.Euler(0f, 90f, 0f);
                        LoadRotate *= q;
                        GameObject newLoad = Instantiate(RightEndTurnLoad, RightLoadPoint.transform.position , LoadRotate);
                     
                        EndCorner endCorner = newLoad.GetComponent<EndCorner>();
                        turnSlider.endCorner = endCorner;
                        endCorner.turntimes_complete = turnSlider.TurnTime_CompleteEnd;
                        turnSlider.RightTurn();
                        newLoad.gameObject.tag = "R";
                        LoadController loadcontroller = newLoad.GetComponent<LoadController>();
                        if (Angle == LoadType.Left)
                            loadcontroller.Angle = LoadType.Up;
                        else
                            loadcontroller.Angle = Angle + 1;
                        Director.turnCheck = false;
                        Director.turnCount = 0;
                    }
                    else
                    {
                        TurnStartLoad = this.gameObject;
                        Director.StartTurnLoad(TurnStartLoad);
                        LoadRotate = gameObject.transform.rotation;
                        Quaternion q = Quaternion.Euler(0f, -90f, 0f);
                        LoadRotate *= q;
                        GameObject newLoad = Instantiate(LeftEndTurnLoad, LeftLoadPoint.transform.position, LoadRotate);
                        EndCorner endCorner = newLoad.GetComponent<EndCorner>();
                        turnSlider.endCorner = endCorner;
                        endCorner.turntimes_complete = turnSlider.TurnTime_CompleteEnd;
                        turnSlider.LeftTurn();
                        newLoad.gameObject.tag = "L";

                        LoadController loadcontroller = newLoad.GetComponent<LoadController>();
                        if (Angle == LoadType.Up)
                            loadcontroller.Angle = LoadType.Left;
                        else
                            loadcontroller.Angle = Angle - 1;
                        Director.turnCheck = false;
                        Director.turnCount = 0;
                    }


                }
                else if (Director.turnCount >= Director.MaxTurnCount)
                {
                    int rand = Random.Range(0, 2);
                    if (rand > 0)
                        Director.RightTurn = true;
                    else
                        Director.RightTurn = false;
                    LoadRotate = gameObject.transform.rotation;
                    GameObject newLoad = Instantiate(TurnLoad, end.gameObject.transform.position, LoadRotate);
                    turnSlider.turnStick = turnStick;
                    if (Director.RightTurn)
                        newLoad.gameObject.tag = "R";
                    else
                        newLoad.gameObject.tag = "L";
                    LoadController loadcontroller = newLoad.GetComponent<LoadController>();
                    loadcontroller.Angle = Angle;
                    Director.turnCheck = true;

                }
                else
                {
                    LoadRotate = gameObject.transform.rotation;
                    //GameObject newLoad = Instantiate(Load, end.gameObject.transform.position, LoadRotate);
                    GameObject newLoad = Instantiate(Director.Cource_List[Director.CourseCount], end.gameObject.transform.position, LoadRotate);
                    LoadController loadcontroller = newLoad.GetComponent<LoadController>();
                    loadcontroller.Angle = Angle;
                    Director.turnCount++;
                }

                HitCheck = false;
            }
        }
        
       
    }
    private void PeopleListGenerator()
    {
        List<float> LoadX = new List<float>();
        List<float> LoadZ = new List<float>();
        switch (Angle)
        {
            case LoadType.Up:
                {
                    for (float i = Left.transform.position.x; i <= Right.transform.position.x; i++)
                    {
                        LoadX.Add(i);
                    }


                    for (float i = begin.transform.position.z; i < end.transform.position.z; i++)
                    {
                        LoadZ.Add(i);
                    }
                    break;
                }
            case LoadType.Right:
                {
                    for (float i = Left.transform.position.z; i >= Right.transform.position.z; i--)
                    {
                        LoadX.Add(i);
                    }


                    for (float i = begin.transform.position.x; i < end.transform.position.x; i++)
                    {
                        LoadZ.Add(i);
                    }
                    break;
                }
            case LoadType.Down:
                {
                    for (float i = Left.transform.position.x; i >= Right.transform.position.x; i--)
                    {
                        LoadX.Add(i);
                    }


                    for (float i = begin.transform.position.z; i > end.transform.position.z; i--)
                    {
                        LoadZ.Add(i);
                    }
                    break;
                }
            case LoadType.Left:
                {
                    for (float i = Left.transform.position.z; i <= Right.transform.position.z; i++)
                    {
                        LoadX.Add(i);
                    }


                    for (float i = begin.transform.position.x; i > end.transform.position.x; i--)
                    {
                        LoadZ.Add(i);
                    }
                    break;
                }
        }

       if(Angle==LoadType.Up||Angle==LoadType.Down)
        {
            for (int i = 0; i < LoadX.Count; i++)
            {
                for (int j = 0; j < LoadZ.Count; j++)
                {
                    CoodinateList.Add(new Vector3(LoadX[i], this.transform.position.y+3.43f, LoadZ[j]));
                }
            }
        }
        else
        {
            for (int i = 0; i < LoadZ.Count; i++)
            {
                for (int j = 0; j < LoadX.Count; j++)
                {
                    CoodinateList.Add(new Vector3(LoadZ[i], this.transform.position.y + 3.43f, LoadX[j]));
                }
            }
        }

        for (int i = CoodinateList.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            var temp = CoodinateList[i];
            CoodinateList[i] = CoodinateList[j];
            CoodinateList[j] = temp;
        }
    }
    public void Generation()
    {
        if (CoodinateList == null)
            return;
        int Peoplerand = Random.Range(PeopleGenerationMin, PeopleGenerationMax+1);
        for (int i = 0; i < Peoplerand; i++)
        {
            GameObject PeoplePre = Instantiate(people, CoodinateList[i],Quaternion.identity);
            switch (Angle)
            {
                case LoadType.Up: PeoplePre.transform.eulerAngles = new Vector3(0, 180, 0);break;
                case LoadType.Right: PeoplePre.transform.eulerAngles = new Vector3(0, 270, 0); break;
                case LoadType.Down: PeoplePre.transform.eulerAngles = new Vector3(0, 0, 0); break;
                case LoadType.Left: PeoplePre.transform.eulerAngles = new Vector3(0, 90, 0); break;
            }

            ObjectList.Add(PeoplePre);
        }
        int Foodrand = Random.Range(FoodGenerationMin,FoodGenerationMax + 1);
        for (int i = Peoplerand ; i < Peoplerand+Foodrand; i++)
        {
            GameObject FoodPre = Instantiate(food, CoodinateList[i], Quaternion.identity);
            ObjectList.Add(FoodPre);
        }
        int Carrand = Random.Range(CarGenerationMin,CarGenerationMax + 1);
        for (int i = Foodrand; i < Carrand+ Foodrand; i++)
        {
            GameObject CarPre = Instantiate(car, new Vector3(CoodinateList[i].x,-3.33f, CoodinateList[i].z), Quaternion.identity);
            switch (Angle)
            {
                case LoadType.Up: CarPre.transform.eulerAngles = new Vector3(0, 90, 0); break;
                case LoadType.Right: CarPre.transform.eulerAngles = new Vector3(0, 180, 0); break;
                case LoadType.Down: CarPre.transform.eulerAngles = new Vector3(0, 270, 0); break;
                case LoadType.Left: CarPre.transform.eulerAngles = new Vector3(0, 0, 0); break;
            }
            ObjectList.Add(CarPre);

        }
    }

}
