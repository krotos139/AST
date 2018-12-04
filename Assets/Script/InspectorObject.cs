using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InspectorObject : MonoBehaviour {
    public GameObject background;
    public GameObject basket;
    public GameObject blowButton;

    //public GameObject level1ground;
    public GameObject level1layer1;
    public GameObject level1layer2up;
    public GameObject level1pine1;
    public GameObject level1pine2;
    public GameObject level1pine3;
    public GameObject level1clouds1;
    public GameObject level1clouds2;
    public GameObject level1clouds3;

    public GameObject level2eventLighter;

    public GameObject brik;
    public GameObject brikBorder;
    public GameObject brik2;
    public GameObject winBorder;
    public Text StroreText;
    public AudioSource soundBlowBallon;

    private bool isNight;
    private bool isNightNoLight;
    private Color objectsColor;
    public float worldSpeed;

    // Use this for initialization
    void Start () {
        worldSpeed = 0.02f;
        isNight = false;
        isNightNoLight = false;
        objectsColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        float xShift = 9.0f;
        float startShift = 15.0f;
        float gateHeight = 8.0f;

        int levelIndex = CommonSingleton.level_index;
        //levelIndex = 1; //delete me !!!

        bool startFromLevel2 = false;
        if(levelIndex == 0)
        {
            int variant = Random.RandomRange(0, 2);
            // почти рандом

            Debug.Log("LEVEL VARIANT " + variant);

            if (variant == 0)
            {
                GenerateLevel2_random_1(); //Перегенендр
            }
            else
            {
                GenerateLevel2_random_3(); //Перегенендр
                
            }
            MusicManager musicManager = FindObjectOfType<MusicManager>();
            musicManager.StartLevel(1);
        }
        if (levelIndex == 1)
        {
            if (!startFromLevel2)
            {
                var objects = FindObjectsOfType<MouseDragAndDrop>();
                foreach (MouseDragAndDrop ob in objects)
                {
                    Debug.Log("Staff=" + ob.name);

                    int index = 0;
                    bool found = false;
                    foreach (string name in CommonSingleton.staffNames)
                    {
                        Debug.Log("STAFF SINGLETON NAME : " + name);
                        if (name.Equals(ob.name))
                        {
                            found = true;
                            Debug.Log("FOUND");
                            break;
                        }
                        index++;
                    }



                    if (found)
                    {

                        Debug.Log("ALIVE staff!");

                        ob.transform.rotation = CommonSingleton.staffQuat.ElementAt<Quaternion>(index);
                        ob.transform.position = CommonSingleton.staffPos.ElementAt<Vector2>(index);
                    }
                    else {

                        Destroy(ob.gameObject);
                    }
                }
            }
            xShift = 4.0f;
            gateHeight = 7.0f;
            int prob = 90;
            //GenerateLevel2(startShift, xShift, gateHeight, 40, prob);
            GenerateLevel3(startShift, xShift, gateHeight, 40, prob);
            //GenerateLevelBoss();
            MusicManager musicManager = FindObjectOfType<MusicManager>();
            musicManager.StartLevel(2);
        }
    }

    // Update is called once per frame
    private float lt = 0.0f;
    private int storePoints = 0;
    private bool blowEnable = false;
	void Update () {
        if (Time.fixedTime > lt)
        {
            lt = Time.fixedTime + 0.3f;
            storePoints += 1;
            if (blowEnable)
            {
                storePoints += 1;
            }
        }
        
        StroreText.text = "Store:"+ storePoints;

        if (isNight)
        {
            if (!isNightNoLight && (objectsColor.r<0.6f))
            {
                objectsColor.r *= 1.1f;
                objectsColor.g *= 1.1f;
                objectsColor.b *= 1.1f;
                SetColorOfObjects(objectsColor);
            }
            if (isNightNoLight && (objectsColor.r > 0.2f))
            {
                objectsColor.r *= 0.9f;
                objectsColor.g *= 0.9f;
                objectsColor.b *= 0.9f;
                SetColorOfObjects(objectsColor);
            }
        }

    }

    public void dropObject(float dmass)
    {
        storePoints += (int)dmass;
    }

    // Вызвать это для окончания игры
    public void GameOver()
    {
        FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
        flyingBall.GameOver();
        //level.GameOver(); //@TODO
        if (CommonSingleton.level_index == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("game_over_1");
        }
        if (CommonSingleton.level_index == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("game_over_2");
        }
    }

    // Вызвать это для окончания игры
    public void GameWin()
    {
        Debug.Log("GameWin");
        FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
        flyingBall.GameWin();
        //level.GameWin(); //@TODO
        //UnityEngine.SceneManagement.SceneManager.LoadScene("you_won");
        // --------------
        CommonSingleton.clearStaff();
        CommonSingleton.level_index++;

        var objects = FindObjectsOfType<MouseDragAndDrop>();

        foreach (MouseDragAndDrop ob in objects)
        {
            if (!ob.dropped)
            {
                CommonSingleton.AddStaff(ob.gameObject.transform.position, ob.gameObject.transform.rotation, ob.gameObject.transform.name);
            }
        }

        if (CommonSingleton.level_index == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("title_before_2");
        }
        if (CommonSingleton.level_index == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("title_before_3");
        }
        //--------------------------


    }

    public void ButtonBlowDown()
    {
        Debug.Log("ButtonBlowDown");
        FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
        flyingBall.BlowDown();
        blowEnable = true;
        soundBlowBallon.loop = true;
        soundBlowBallon.Play();
    }

    public void ButtonBlowUp()
    {
        Debug.Log("ButtonBlowUp");
        FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
        flyingBall.BlowUp();
        blowEnable = false;
        soundBlowBallon.loop = false;
    }

    /*public void GenerateLevel1(float startShift, float deltaX, float gateHeight, int gatesCount, int probabilityGates)
    {
        for (int i = 0; i < gatesCount; i++)
        {
            float state = 1.0f - (gatesCount / i);
            if (gatesCount)
            
            float cur_x = startShift + i * deltaX;
            GameObject cur_brik_border_up = Instantiate<GameObject>(brikBorder);
            Level1 cur_brik_border_instance_up = cur_brik_border_up.GetComponent<Level1>();
            cur_brik_border_instance_up.SetPosition(cur_x, 10.0f, 0.0f);
            GameObject cur_brik_border_down = Instantiate<GameObject>(brikBorder);
            Level1 cur_brik_border_instance_down = cur_brik_border_down.GetComponent<Level1>();
            cur_brik_border_instance_down.SetPosition(cur_x, -10.0f, 0.0f);

            if (Random.Range(0, 99) > probabilityGates)
            {
                continue;
            }

            float cur_y = Random.Range(-3.0f, 3.0f);
            float cur_h_add = 0.0f;// Random.Range(0.0f, 4.0f);
            //if(brik == null)
            //{
            //    Debug.Log("Brick"+i+" : cur_x = "+ cur_x + "; cur_y = "+ cur_y);
            //}
            if (Random.Range(0, 100) < probabilityGates)
            {
                GameObject cur_brik_up = Instantiate<GameObject>(brik);
                Level1 cur_brik_instance_up = cur_brik_up.GetComponent<Level1>();
                cur_brik_instance_up.SetPosition(cur_x, cur_y + gateHeight + cur_h_add, 0.0f);
                GameObject cur_brik_down = Instantiate<GameObject>(brik);
                Level1 cur_brik_instance_down = cur_brik_down.GetComponent<Level1>();
                cur_brik_instance_down.SetPosition(cur_x, cur_y - gateHeight - cur_h_add, 0.0f);
            }
            else {
                GameObject cur_brik_center = Instantiate<GameObject>(brik2);
                Level1 cur_brik2_instance = cur_brik_center.GetComponent<Level1>();
                cur_brik2_instance.SetPosition(cur_x, cur_y - cur_h_add, 0.0f);
            }
        }
        float win_x = startShift + (gatesCount - 1) * deltaX;
        GameObject cur_win = Instantiate<GameObject>(winBorder);
        WinBorder cur_win_instance = cur_win.GetComponent<WinBorder>();
        cur_win_instance.SetPosition(win_x, 0.0f, 0.0f);
    }*/

    private void SetColorOfObjects(Color color)
    {
        SpriteRenderer sr = basket.GetComponent<SpriteRenderer>();
        sr.color = color;

        sr = blowButton.GetComponent<SpriteRenderer>();
        sr.color = color;
        

        MouseDragAndDrop[] allChildren = basket.GetComponentsInChildren<MouseDragAndDrop>();
        foreach (MouseDragAndDrop child in allChildren)
        {
            sr = child.GetComponent<SpriteRenderer>();
            sr.color = color;
            // do what you want with the transform
        }
    }

    public void setNightNoLight(bool val)
    {
        isNightNoLight = val;
    }

    public void GenerateLevel2(float startShift, float deltaX, float gateHeight, int gatesCount, int probabilityGates)
    {
        isNight = false;
        isNightNoLight = false;
        objectsColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        SetColorOfObjects(objectsColor);
        // Трава
        Renderer level1layer1r = level1layer1.GetComponent<Renderer>();
        float level1layer1width = level1layer1r.bounds.size.x;
        for (int i = 0; i < 4.0*(gatesCount+2)*deltaX/ level1layer1width; i++)
        {
            float cur_x = -startShift + i * level1layer1width;
            float cur_y = level1layer1r.bounds.size.y;
            GameObject cur_level1layer1 = Instantiate<GameObject>(level1layer1);
            Level1 cur_level1layer1_script = cur_level1layer1.GetComponent<Level1>();
            cur_level1layer1_script.SetPosition(cur_x, -10.0f+ cur_y*0.5f, 0.0f);
            cur_level1layer1_script.xShift = 0.04f*2.0f;
        }
        // Небо
        Renderer level1layer2r = level1layer2up.GetComponent<Renderer>();
        float level1layer2width = level1layer2r.bounds.size.x;
        for (int i = 0; i < (gatesCount + 2) * deltaX / level1layer1width; i++)
        {
            float cur_x = -startShift + i * level1layer2width;
            float cur_y = level1layer2r.bounds.size.y;
            GameObject cur_level1layer2 = Instantiate<GameObject>(level1layer2up);
            Level1 cur_level1layer2_script = cur_level1layer2.GetComponent<Level1>();
            cur_level1layer2_script.SetPosition(cur_x, 10.0f - cur_y * 0.5f, 0.0f);
            cur_level1layer2_script.xShift = 0.04f * 0.5f;
        }

        int centerPhaseGates = 0;
        for (int i = 0; i < gatesCount; i++)
        {

            float cur_x = startShift + i * deltaX;
            GameObject cur_brik_border_up = Instantiate<GameObject>(brikBorder);
            Level1 cur_brik_border_instance_up = cur_brik_border_up.GetComponent<Level1>();
            cur_brik_border_instance_up.SetPosition(cur_x, 11.0f, 0.0f);
            cur_brik_border_instance_up.xShift = 0.04f;

            if (Random.Range(0, 99) > probabilityGates)
            {
                continue;
            }

            float cur_h_add = Random.Range(-4.0f, 0.0f) - 3.0f * Mathf.Sin(i * 0.4f) - 1.0f;
            float cur_x_add = Random.Range(-deltaX * 0.2f, deltaX * 0.2f);
            //if(brik == null)
            //{
            //    Debug.Log("Brick"+i+" : cur_x = "+ cur_x + "; cur_y = "+ cur_y);
            //}

            // Маленькие деревья
            float curS_h_add = Random.Range(-6.0f, 0.0f) - 4.0f;
            float curS_x_add = Random.Range(-deltaX * 0.8f, deltaX * 0.8f);
            GameObject cur_pineS_down = null;
            switch (Random.Range(0, 3))
            {
                case 0:
                    cur_pineS_down = Instantiate<GameObject>(level1pine1);
                    break;
                case 1:
                    cur_pineS_down = Instantiate<GameObject>(level1pine2);
                    break;
                case 2:
                    cur_pineS_down = Instantiate<GameObject>(level1pine3);
                    break;
            }
            Renderer cur_pineS_down_r = cur_pineS_down.GetComponent<Renderer>();
            float pineS_cur_y = cur_pineS_down_r.bounds.size.y;
            Level1 cur_pineS_instance_down = cur_pineS_down.GetComponent<Level1>();
            cur_pineS_instance_down.SetPosition(cur_x + curS_x_add, -10.0f + pineS_cur_y * 0.5f + curS_h_add, 0.0f);
            cur_pineS_instance_down.xShift = 0.04f;

            if (i % 2 == 0 && (Random.Range(0, 100) < 20))
            {
                centerPhaseGates = 6;
            }
            if (centerPhaseGates > 0)
            {
                centerPhaseGates--;
            }
            if (centerPhaseGates == 0)
            {
                // Большие деревья
                if (Random.Range(0, 100) < probabilityGates)
                {
                    GameObject cur_brik_down = null;
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            cur_brik_down = Instantiate<GameObject>(level1pine1);
                            break;
                        case 1:
                            cur_brik_down = Instantiate<GameObject>(level1pine2);
                            break;
                        case 2:
                            cur_brik_down = Instantiate<GameObject>(level1pine3);
                            break;
                    }
                    Renderer cur_brik_down_r = cur_brik_down.GetComponent<Renderer>();
                    float cur_y = cur_brik_down_r.bounds.size.y;
                    Level1 cur_brik_instance_down = cur_brik_down.GetComponent<Level1>();
                    cur_brik_instance_down.SetPosition(cur_x + cur_x_add, -10.0f + cur_y * 0.5f + cur_h_add, 0.0f);
                    cur_brik_instance_down.xShift = 0.04f;
                    // Облака
                    if (Random.Range(0, 100) < 100)
                    {
                        int j = 0;
                        while (true)
                        {
                            float cur_x_add2 = Random.Range(-2.0f, 2.0f);
                            GameObject cur_cloud_down = null;
                            switch (Random.Range(0, 3))
                            {
                                case 0:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds1);
                                    break;
                                case 1:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds2);
                                    break;
                                case 2:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds3);
                                    break;
                            }
                            Renderer cur_cloud_down_r = cur_cloud_down.GetComponentInChildren<Renderer>();
                            float cloud_h = cur_cloud_down_r.bounds.size.y;
                            Level1 cur_cloud_down_level1 = cur_cloud_down.GetComponent<Level1>();
                            cur_cloud_down_level1.SetPosition(cur_x + cur_x_add + cur_x_add2, -10.0f + cur_y * 1.0f + cloud_h * (j * 1.1f + 0.5f) + gateHeight * 1.2f + cur_h_add, 0.0f);
                            cur_cloud_down_level1.xShift = 0.04f;
                            if (-10.0f + cur_y * 1.0f + cloud_h * (j * 1.1f + 0.5f) + gateHeight * 1.2f + cur_h_add > 10.0f - cloud_h)
                            {
                                break;
                            }
                            j++;
                        }
                    }
                }
                else {
                    //GameObject cur_brik_center = Instantiate<GameObject>(brik2);
                    //Level1 cur_brik2_instance = cur_brik_center.GetComponent<Level1>();
                    //cur_brik2_instance.SetPosition(cur_x, cur_y - cur_h_add, 0.0f);
                    //cur_brik2_instance.xShift = 0.04f;
                }
            }
            if (centerPhaseGates == 3)
            {
                // Тучка по центру
                float cur_x_add2 = Random.Range(-2.0f, 2.0f);
                GameObject cur_cloud_down = null;
                switch (Random.Range(0, 3))
                {
                    case 0:
                        cur_cloud_down = Instantiate<GameObject>(level1clouds1);
                        break;
                    case 1:
                        cur_cloud_down = Instantiate<GameObject>(level1clouds2);
                        break;
                    case 2:
                        cur_cloud_down = Instantiate<GameObject>(level1clouds3);
                        break;
                }
                Renderer cur_cloud_down_r = cur_cloud_down.GetComponentInChildren<Renderer>();
                float cloud_h = cur_cloud_down_r.bounds.size.y;
                Level1 cur_cloud_down_level1 = cur_cloud_down.GetComponent<Level1>();
                cur_cloud_down_level1.SetPosition(cur_x + cur_x_add + cur_x_add2, 2.0f+cur_h_add*0.5f, 0.0f);
                cur_cloud_down_level1.xShift = 0.04f;
            }
        }
        
        float win_x = startShift + (gatesCount-1) * deltaX;
        GameObject cur_win = Instantiate<GameObject>(winBorder);
        WinBorder cur_win_instance = cur_win.GetComponent<WinBorder>();
        cur_win_instance.SetPosition(win_x, 0.0f, 0.0f);
        cur_win_instance.xShift = 0.04f;
    }



    public void GenerateLevel3(float startShift, float deltaX, float gateHeight, int gatesCount, int probabilityGates)
    {
        Color objectsColor = new Color(0.6f, 0.6f, 0.7f, 1.0f);
        //SetColorOfObjects(new Color(0.2f, 0.2f, 0.2f, 1.0f));
        isNight = true;
        isNightNoLight = false;
        objectsColor = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        SetColorOfObjects(objectsColor);

        FlyingBall flyingBall = FindObjectOfType<FlyingBall>();
        flyingBall.GetComponent<SpriteRenderer>().color = objectsColor;

        background.GetComponent<SpriteRenderer>().color = objectsColor;

        // Трава
        Renderer level1layer1r = level1layer1.GetComponent<Renderer>();
        float level1layer1width = level1layer1r.bounds.size.x;
        for (int i = 0; i < 4.0 * (gatesCount + 2) * deltaX / level1layer1width; i++)
        {
            float cur_x = -startShift + i * level1layer1width;
            float cur_y = level1layer1r.bounds.size.y;
            GameObject cur_level1layer1 = Instantiate<GameObject>(level1layer1);
            cur_level1layer1.GetComponent<SpriteRenderer>().color = objectsColor;
            Level1 cur_level1layer1_script = cur_level1layer1.GetComponent<Level1>();
            cur_level1layer1_script.SetPosition(cur_x, -10.0f + cur_y * 0.5f, 0.0f);
            cur_level1layer1_script.xShift = 0.04f * 2.0f;
        }
        // Небо
        Renderer level1layer2r = level1layer2up.GetComponent<Renderer>();
        float level1layer2width = level1layer2r.bounds.size.x;
        for (int i = 0; i < (gatesCount + 2) * deltaX / level1layer1width; i++)
        {
            float cur_x = -startShift + i * level1layer2width;
            float cur_y = level1layer2r.bounds.size.y;
            GameObject cur_level1layer2 = Instantiate<GameObject>(level1layer2up);
            cur_level1layer2.GetComponent<SpriteRenderer>().color = objectsColor;
            Level1 cur_level1layer2_script = cur_level1layer2.GetComponent<Level1>();
            cur_level1layer2_script.SetPosition(cur_x, 10.0f - cur_y * 0.5f, 0.0f);
            cur_level1layer2_script.xShift = 0.04f * 0.5f;
        }

        int centerPhaseGates = 0;
        for (int i = 0; i < gatesCount; i++)
        {

            float cur_x = startShift + i * deltaX;
            //GameObject cur_brik_border_up = Instantiate<GameObject>(brikBorder);
            //Level1 cur_brik_border_instance_up = cur_brik_border_up.GetComponent<Level1>();
            //cur_brik_border_instance_up.SetPosition(cur_x, 11.0f, 0.0f);
            //cur_brik_border_instance_up.xShift = 0.04f;

            if (Random.Range(0, 99) > probabilityGates)
            {
                continue;
            }

            float cur_h_add = Random.Range(-4.0f, 0.0f) - 3.0f * Mathf.Sin(i * 0.4f) - 1.0f;
            float cur_x_add = Random.Range(-deltaX * 0.2f, deltaX * 0.2f);
            //if(brik == null)
            //{
            //    Debug.Log("Brick"+i+" : cur_x = "+ cur_x + "; cur_y = "+ cur_y);
            //}

            // Маленькие деревья
            float curS_h_add = Random.Range(-6.0f, 0.0f) - 4.0f;
            float curS_x_add = Random.Range(-deltaX * 0.8f, deltaX * 0.8f);
            GameObject cur_pineS_down = null;
            switch (Random.Range(0, 3))
            {
                case 0:
                    cur_pineS_down = Instantiate<GameObject>(level1pine1);
                    break;
                case 1:
                    cur_pineS_down = Instantiate<GameObject>(level1pine2);
                    break;
                case 2:
                    cur_pineS_down = Instantiate<GameObject>(level1pine3);
                    break;
            }
            cur_pineS_down.GetComponent<SpriteRenderer>().color = objectsColor;
            Renderer cur_pineS_down_r = cur_pineS_down.GetComponent<Renderer>();
            float pineS_cur_y = cur_pineS_down_r.bounds.size.y;
            Level1 cur_pineS_instance_down = cur_pineS_down.GetComponent<Level1>();
            cur_pineS_instance_down.SetPosition(cur_x + curS_x_add, -10.0f + pineS_cur_y * 0.5f + curS_h_add, 0.0f);
            cur_pineS_instance_down.xShift = 0.04f;

            if (i % 2 == 0 && (Random.Range(0, 100) < 20))
            {
                centerPhaseGates = 10;
            }
            if (centerPhaseGates > 0)
            {
                centerPhaseGates--;
            }
            if (centerPhaseGates == 0)
            {
                // Большие деревья
                if (Random.Range(0, 100) < probabilityGates)
                {
                    GameObject cur_brik_down = null;
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            cur_brik_down = Instantiate<GameObject>(level1pine1);
                            break;
                        case 1:
                            cur_brik_down = Instantiate<GameObject>(level1pine2);
                            break;
                        case 2:
                            cur_brik_down = Instantiate<GameObject>(level1pine3);
                            break;
                    }
                    cur_brik_down.GetComponent<SpriteRenderer>().color = objectsColor;
                    Renderer cur_brik_down_r = cur_brik_down.GetComponent<Renderer>();
                    float cur_y = cur_brik_down_r.bounds.size.y;
                    Level1 cur_brik_instance_down = cur_brik_down.GetComponent<Level1>();
                    cur_brik_instance_down.SetPosition(cur_x + cur_x_add, -10.0f + cur_y * 0.5f + cur_h_add, 0.0f);
                    cur_brik_instance_down.xShift = 0.04f;
                    // Облака
                    if (Random.Range(0, 100) < 100)
                    {
                        int j = 0;
                        while (true)
                        {
                            float cur_x_add2 = Random.Range(-2.0f, 2.0f);
                            GameObject cur_cloud_down = null;
                            switch (Random.Range(0, 3))
                            {
                                case 0:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds1);
                                    break;
                                case 1:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds2);
                                    break;
                                case 2:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds3);
                                    break;
                            }
                            SpriteRenderer[] allChildren = cur_cloud_down.GetComponentsInChildren<SpriteRenderer>();
                            foreach (SpriteRenderer child in allChildren)
                            {
                                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                                sr.color = objectsColor;
                                // do what you want with the transform
                            }
                            Renderer cur_cloud_down_r = cur_cloud_down.GetComponentInChildren<Renderer>();
                            float cloud_h = cur_cloud_down_r.bounds.size.y;
                            Level1 cur_cloud_down_level1 = cur_cloud_down.GetComponent<Level1>();
                            cur_cloud_down_level1.SetPosition(cur_x + cur_x_add + cur_x_add2, -10.0f + cur_y * 1.0f + cloud_h * (j * 1.1f + 0.5f) + gateHeight * 1.2f + cur_h_add, 0.0f);
                            cur_cloud_down_level1.xShift = 0.04f;
                            if (-10.0f + cur_y * 1.0f + cloud_h * (j * 1.1f + 0.5f) + gateHeight * 1.2f + cur_h_add > 10.0f - cloud_h)
                            {
                                break;
                            }
                            j++;
                        }
                    }
                }
                else {
                    //GameObject cur_brik_center = Instantiate<GameObject>(brik2);
                    //Level1 cur_brik2_instance = cur_brik_center.GetComponent<Level1>();
                    //cur_brik2_instance.SetPosition(cur_x, cur_y - cur_h_add, 0.0f);
                    //cur_brik2_instance.xShift = 0.04f;
                }
            }
            if (centerPhaseGates >=7)
            {
                // Высокие деревья
                float cur_h_add2 = Random.Range(-4.0f, 0.0f);
                    GameObject cur_brik_down = null;
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            cur_brik_down = Instantiate<GameObject>(level1pine1);
                            break;
                        case 1:
                            cur_brik_down = Instantiate<GameObject>(level1pine2);
                            break;
                        case 2:
                            cur_brik_down = Instantiate<GameObject>(level1pine3);
                            break;
                    }
                    cur_brik_down.GetComponent<SpriteRenderer>().color = objectsColor;
                    Renderer cur_brik_down_r = cur_brik_down.GetComponent<Renderer>();
                    float cur_y = cur_brik_down_r.bounds.size.y;
                    Level1 cur_brik_instance_down = cur_brik_down.GetComponent<Level1>();
                    cur_brik_instance_down.SetPosition(cur_x + cur_x_add, -10.0f + cur_y * 0.5f + cur_h_add2, 0.0f);
                    cur_brik_instance_down.xShift = 0.04f;
            }

            if (centerPhaseGates == 6)
            {
                // выключаем горелку
                float cur_x_add2 = Random.Range(-2.0f, 2.0f);
                GameObject cur_event_loader = Instantiate<GameObject>(level2eventLighter);
                EventLighterStart cur_event_loader_event = cur_event_loader.GetComponent<EventLighterStart>();
                cur_event_loader_event.SetPosition(cur_x + cur_x_add + cur_x_add2, 2.0f + cur_h_add * 0.5f, 0.0f);
                cur_event_loader_event.xShift = 0.04f;
            }
        }

        float win_x = startShift + (gatesCount - 1) * deltaX;
        GameObject cur_win = Instantiate<GameObject>(winBorder);
        WinBorder cur_win_instance = cur_win.GetComponent<WinBorder>();
        cur_win_instance.SetPosition(win_x, 0.0f, 0.0f);
        cur_win_instance.xShift = 0.04f;
    }


    public void GenerateLevelBoss()
    {

    }

    public void GenerateLevel2_random_1()
    {



        float startShift = 15;
        float deltaX = 1.5f;
        float gateHeight = 6;
        int gatesCount = 50;
        int probabilityGates = 70;
        int[] prob_gates_arr = { 93, 22, 62, 58, 89, 24, 21, 30, 25, 68, 86, 55, 75, 1, 38, 96, 97, 68, 42, 98, 59, 41, 7, 21, 83, 96, 6, 67, 15, 10, 48, 75, 12, 33, 8, 83, 54, 51, 51, 81, 84, 2, 70, 81, 7, 15, 66, 21, 23, 10, };
        float[] cur_h_add_ar = { 0f, -4.344365f, -3.000379f, -1.794549f, 0f, -2.976036f, -1.662166f, -2.868928f, -2.753554f, -3.067905f, 0f, -3.116981f, 0f, -4.694671f, -2.24312f, 0f, 0f, -4.093927f, -5.250569f, 0f, -5.063663f, -3.66209f, -4.677234f, -5.469533f, 0f, 0f, -3.687898f, -5.084968f, -3.897624f, -3.288646f, -5.652876f, 0f, -3.922448f, -6.15552f, -5.589376f, 0f, -3.09056f, -5.325519f, -6.235528f, 0f, 0f, -5.162378f, -6.020396f, 0f, -6.588529f, -3.385019f, -7.359838f, -4.03903f, -4.148219f, -4.310538f, };
        float[] cur_x_add_ar = { 0f, 0.2217028f, 0.2090068f, -0.3637387f, 0f, -0.2087729f, -0.2285437f, -0.2391805f, 0.1126434f, -0.314596f, 0f, -0.1839637f, 0f, 0.1085046f, 0.05147272f, 0f, 0f, 0.3481959f, 0.1150552f, 0f, -0.02202696f, -0.04214492f, -0.09927756f, -0.2745885f, 0f, 0f, -0.3877971f, 0.153977f, 0.2808824f, -0.1203062f, 0.2753173f, 0f, 0.144909f, -0.03723836f, 0.3399842f, 0f, -0.2089703f, 0.3715492f, -0.3693196f, 0f, 0f, 0.1413716f, 0.03447384f, 0f, -0.3772887f, -0.3608682f, -0.1444438f, 0.07180801f, -0.2777447f, -0.06784597f, };
        float[] curS_h_add_ar = { 0f, -5.460908f, -7.628725f, -6.107184f, 0f, -5.743682f, -6.218924f, -7.147325f, -7.455982f, -4.817422f, 0f, -6.676758f, 0f, -8.458107f, -5.870848f, 0f, 0f, -7.536415f, -7.491613f, 0f, -6.948661f, -8.141951f, -4.925595f, -5.374207f, 0f, 0f, -7.905965f, -7.844638f, -7.066452f, -7.082553f, -9.691378f, 0f, -8.937384f, -6.905178f, -5.537158f, 0f, -8.516698f, -4.383152f, -5.297716f, 0f, 0f, -7.880871f, -7.49672f, 0f, -6.014443f, -9.382578f, -8.396792f, -8.756858f, -6.905931f, -7.073373f, };
        float[] curS_x_add_ar = { 0f, -0.7863775f, -0.8830463f, -1.41737f, 0f, -0.004072428f, -1.016564f, 1.332038f, 1.476879f, 0.355121f, 0f, -1.43566f, 0f, 0.2919298f, 0.8989856f, 0f, 0f, 0.5673137f, 0.2524259f, 0f, -0.9589745f, 1.119444f, -1.372054f, 0.4313183f, 0f, 0f, 0.3892336f, -1.463916f, -1.192048f, 1.377116f, 0.6174736f, 0f, 0.4762028f, -1.078479f, -1.365937f, 0f, -1.554899f, 0.4123581f, 0.1772436f, 0f, 0f, 0.6256229f, -1.087838f, 0f, -1.088104f, -0.2811502f, -0.01328111f, 0.1665437f, -0.2423242f, -1.063159f, };
        int[] cur_pineS_down_variant = { 0, 0, 2, 2, 0, 0, 0, 1, 1, 2, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 2, 0, 0, 0, 0, 0, 1, 2, 2, 0, 0, 0, 0, 2, 0, 1, 1, 0, 0, 0, 1, 1, 0, 1, 2, 2, 0, 0, 1, };
        int[] centerPhaseGates_prob = { 0, 90, 23, 13, 0, 91, 18, 68, 58, 62, 0, 2, 0, 4, 43, 0, 0, 91, 7, 0, 33, 36, 86, 79, 0, 0, 18, 83, 50, 46, 66, 0, 56, 63, 46, 0, 82, 60, 67, 0, 0, 89, 15, 0, 29, 15, 81, 93, 49, 83, };

        List<int> prob_gates_arr_l = prob_gates_arr.ToList();
        List<float> cur_h_add_ar_l = cur_h_add_ar.ToList();
        List<float> cur_x_add_ar_l = cur_x_add_ar.ToList();
        List<float> curS_h_add_ar_l = curS_h_add_ar.ToList();
        List<float> curS_x_add_ar_l = curS_x_add_ar.ToList();
        List<int> cur_pineS_down_variant_l = cur_pineS_down_variant.ToList();
        List<int> centerPhaseGates_prob_l = centerPhaseGates_prob.ToList();


        GenerateLevel2_presaved(startShift, deltaX, gateHeight, gatesCount, probabilityGates,
            prob_gates_arr_l, cur_h_add_ar_l, cur_x_add_ar_l, curS_h_add_ar_l, curS_x_add_ar_l, cur_pineS_down_variant_l, centerPhaseGates_prob_l);
    }


    public void GenerateLevel2_random_2()
    {



        float startShift = 15f;
        float deltaX = 1.5f;
        float gateHeight = 6f;
        int gatesCount = 50;
        int probabilityGates = 70;
        int[] prob_gates_arr = { 8, 0, 63, 72, 16, 11, 94, 11, 66, 18, 44, 69, 77, 96, 59, 52, 57, 4, 35, 40, 69, 31, 97, 3, 50, 28, 68, 81, 27, 14, 16, 15, 44, 25, 89, 32, 60, 15, 50, 92, 79, 87, 95, 61, 43, 23, 9, 97, 81, 96, };
        float[] cur_h_add_ar = { -3.240003f, -3.486289f, -4.887797f, 0f, -3.028482f, -3.890493f, 0f, -1.74044f, -1.725325f, -3.424294f, -5.510486f, -4.627355f, 0f, 0f, -5.758112f, -5.765623f, -2.452183f, -2.211242f, -5.310259f, -4.00614f, -2.320818f, -2.345912f, 0f, -3.780819f, -4.442304f, -4.469763f, -4.478243f, 0f, -4.72764f, -5.382503f, -4.773582f, -5.747193f, -4.81776f, -4.437398f, 0f, -6.072583f, -4.113568f, -4.232836f, -3.178873f, 0f, 0f, 0f, 0f, -5.687491f, -6.334169f, -5.855682f, -5.232337f, 0f, 0f, 0f, };
        float[] cur_x_add_ar = { 0.3208987f, 0.05197796f, 0.3793022f, 0f, -0.3519836f, -0.1914481f, 0f, -0.198853f, -0.1396024f, -0.06245789f, 0.3325293f, 0.370998f, 0f, 0f, 0.3004734f, 0.1722586f, 0.3356404f, -0.1495576f, -0.07497659f, 0.3819358f, 0.2072271f, 0.2292409f, 0f, -0.0008150637f, -0.09920755f, 0.2152622f, -0.3055419f, 0f, -0.2806999f, 0.3351127f, 0.02353778f, 0.2819822f, -0.139256f, -0.06101346f, 0f, 0.2415524f, 0.2152682f, -0.2185223f, 0.02318197f, 0f, 0f, 0f, 0f, -0.2261574f, -0.1876674f, -0.2335271f, 0.05338281f, 0f, 0f, 0f, };
        float[] curS_h_add_ar = { -7.396472f, -4.099824f, -9.651635f, 0f, -5.628141f, -9.714013f, 0f, -6.409623f, -5.899763f, -8.925822f, -5.165823f, -8.32979f, 0f, 0f, -5.108446f, -4.485212f, -4.230523f, -9.628798f, -6.639308f, -6.368643f, -4.959374f, -7.564009f, 0f, -7.718027f, -6.525853f, -5.503043f, -6.689813f, 0f, -4.126667f, -6.488545f, -4.594274f, -4.1007f, -9.652061f, -6.005099f, 0f, -9.731809f, -4.082885f, -9.830534f, -7.567549f, 0f, 0f, 0f, 0f, -9.350541f, -4.858605f, -5.902346f, -4.091903f, 0f, 0f, 0f, };
        float[] curS_x_add_ar = { -0.9038807f, -0.9897801f, 0.8519955f, 0f, 0.7744895f, 1.37297f, 0f, 0.2317346f, 0.620018f, -1.338938f, 0.5565074f, 0.7110649f, 0f, 0f, 1.229969f, -0.3580282f, 0.2004865f, -1.312082f, -0.1000872f, -0.6509791f, 0.7465385f, 0.5886021f, 0f, -0.2143294f, -1.200528f, 0.4085639f, -0.8894051f, 0f, 1.089667f, -0.7001952f, 0.9091998f, 1.029942f, -0.07701135f, -0.6573104f, 0f, 0.5702496f, 0.04952645f, 1.271971f, 1.086327f, 0f, 0f, 0f, 0f, -1.27197f, -1.519923f, -0.3518171f, 1.420011f, 0f, 0f, 0f, };
        int[] cur_pineS_down_variant = { 0, 0, 2, 0, 2, 1, 0, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 2, 2, 0, 2, 2, 1, 1, 0, 0, 0, 1, 2, 0, 1, 0, 0, 0, 0, 1, 0, 2, 2, 0, 0, 0, };
        int[] centerPhaseGates_prob = { 83, 78, 24, 0, 39, 40, 0, 95, 33, 7, 76, 55, 0, 0, 4, 25, 1, 39, 53, 60, 61, 22, 0, 0, 10, 4, 54, 0, 97, 29, 65, 71, 40, 22, 0, 61, 73, 59, 66, 0, 0, 0, 0, 82, 49, 93, 2, 0, 0, 0, };

        List<int> prob_gates_arr_l = prob_gates_arr.ToList();
        List<float> cur_h_add_ar_l = cur_h_add_ar.ToList();
        List<float> cur_x_add_ar_l = cur_x_add_ar.ToList();
        List<float> curS_h_add_ar_l = curS_h_add_ar.ToList();
        List<float> curS_x_add_ar_l = curS_x_add_ar.ToList();
        List<int> cur_pineS_down_variant_l = cur_pineS_down_variant.ToList();
        List<int> centerPhaseGates_prob_l = centerPhaseGates_prob.ToList();


        GenerateLevel2_presaved(startShift, deltaX, gateHeight, gatesCount, probabilityGates,
            prob_gates_arr_l, cur_h_add_ar_l, cur_x_add_ar_l, curS_h_add_ar_l, curS_x_add_ar_l, cur_pineS_down_variant_l, centerPhaseGates_prob_l);
    }


    public void GenerateLevel2_random_3()
    {

        float startShift = 15f;
        float deltaX = 1.5f;
        float gateHeight = 6f;
        int gatesCount = 50;
        int probabilityGates = 70;
        int[] prob_gates_arr = { 14, 88, 96, 71, 10, 37, 76, 48, 16, 90, 83, 9, 1, 38, 30, 5, 20, 62, 50, 56, 38, 87, 81, 31, 81, 3, 48, 63, 60, 38, 66, 56, 67, 79, 78, 39, 5, 2, 73, 53, 23, 42, 9, 66, 0, 35, 9, 55, 22, 75, };
        float[] cur_h_add_ar = { -3.777683f, 0f, 0f, 0f, -3.792574f, -4.666326f, 0f, -4.697609f, -4.786847f, 0f, 0f, -4.862851f, -2.829834f, -2.681904f, -4.014845f, -2.018626f, -3.321134f, -5.364237f, -4.394095f, -3.515213f, -6.060116f, 0f, 0f, -5.091168f, 0f, -4.110356f, -4.37919f, -5.718815f, -4.945972f, -5.427063f, -4.164993f, -6.528015f, -6.123781f, 0f, 0f, -4.337028f, -4.11842f, -6.61131f, 0f, -6.971053f, -6.160807f, -3.641321f, -4.35078f, -7.266992f, -3.553824f, -4.905047f, -7.005833f, -5.832521f, -5.443326f, 0f, };
        float[] cur_x_add_ar = { 0.1280301f, 0f, 0f, 0f, -0.04788733f, -0.2548144f, 0f, 0.2304144f, 0.03758121f, 0f, 0f, 0.2845758f, 0.02480316f, 0.2094602f, -0.09544191f, -0.2982934f, -0.2143179f, -0.1367288f, 0.1644253f, -0.01661199f, 0.2814008f, 0f, 0f, -0.1036156f, 0f, -0.2910841f, -0.004270107f, -0.1191777f, 0.1234527f, 0.2239233f, 0.2493002f, 0.1579156f, -0.02168381f, 0f, 0f, 0.2226762f, -0.07184783f, -0.147783f, 0f, 0.2032762f, -0.167037f, 0.2315505f, 0.07929739f, 0.1082056f, -0.1161098f, 0.1268098f, -0.2742208f, -0.09068946f, -0.2263541f, 0f, };
        float[] curS_h_add_ar = { -6.436548f, 0f, 0f, 0f, -4.684859f, -4.829241f, 0f, -8.404461f, -5.481217f, 0f, 0f, -8.581553f, -8.972235f, -6.055012f, -7.082287f, -9.85043f, -8.022909f, -4.898436f, -4.491313f, -6.730933f, -4.682018f, 0f, 0f, -9.98037f, 0f, -9.886614f, -9.319674f, -7.85607f, -4.519899f, -6.302419f, -8.476269f, -7.602688f, -5.901069f, 0f, 0f, -5.695938f, -7.129831f, -5.407654f, 0f, -4.024015f, -5.089247f, -7.440011f, -7.502834f, -5.558019f, -4.49121f, -7.071404f, -8.875028f, -7.933161f, -6.965038f, 0f, };
        float[] curS_x_add_ar = { 0.5756836f, 0f, 0f, 0f, -0.4819177f, -0.8632582f, 0f, -0.9023752f, -0.6852971f, 0f, 0f, -0.5044512f, 0.03273249f, -0.7768304f, 0.1012735f, -0.2527361f, 0.4632894f, 0.05093527f, -1.087203f, 0.5668622f, 0.7966696f, 0f, 0f, -1.014121f, 0f, -0.8738492f, 0.7521398f, -0.2837764f, 0.4366327f, -0.2750088f, -0.01799822f, 0.936862f, -0.6699892f, 0f, 0f, 0.5508943f, -1.165364f, 1.029462f, 0f, 0.4359475f, -0.385719f, 0.4486147f, 0.7779773f, 1.113238f, 0.945631f, -0.02610612f, 1.188342f, 1.1391f, 0.6814745f, 0f, };
        int[] cur_pineS_down_variant = { 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 2, 1, 0, 0, 1, 2, 1, 2, 1, 2, 0, 0, 0, 0, 2, 0, 1, 0, 0, 1, 2, 0, 0, 0, 1, 0, 2, 0, 2, 0, 2, 1, 1, 2, 1, 2, 0, 1, 0, };
        int[] centerPhaseGates_prob = { 70, 0, 0, 0, 5, 95, 0, 12, 29, 0, 0, 73, 83, 52, 9, 19, 76, 52, 97, 53, 4, 0, 0, 47, 0, 23, 15, 38, 17, 66, 30, 5, 85, 0, 0, 40, 14, 74, 0, 71, 66, 41, 10, 77, 83, 50, 98, 19, 79, 0, };


        List<int> prob_gates_arr_l = prob_gates_arr.ToList();
        List<float> cur_h_add_ar_l = cur_h_add_ar.ToList();
        List<float> cur_x_add_ar_l = cur_x_add_ar.ToList();
        List<float> curS_h_add_ar_l = curS_h_add_ar.ToList();
        List<float> curS_x_add_ar_l = curS_x_add_ar.ToList();
        List<int> cur_pineS_down_variant_l = cur_pineS_down_variant.ToList();
        List<int> centerPhaseGates_prob_l = centerPhaseGates_prob.ToList();


        GenerateLevel2_presaved(startShift, deltaX, gateHeight, gatesCount, probabilityGates,
            prob_gates_arr_l, cur_h_add_ar_l, cur_x_add_ar_l, curS_h_add_ar_l, curS_x_add_ar_l, cur_pineS_down_variant_l, centerPhaseGates_prob_l);
    }
    public void GenerateLevel2_presaved(float startShift,
                                        float deltaX,
                                        float gateHeight,
                                        int gatesCount,
                                        int probabilityGates,
                                        List<int> prob_gates_arr,
                                        List<float> cur_h_add_ar,
                                        List<float> cur_x_add_ar,
                                        List<float> curS_h_add_ar,
                                        List<float> curS_x_add_ar,
                                        List<int> cur_pineS_down_variant,
                                        List<int> centerPhaseGates_prob)
    {

        // Трава
        Renderer level1layer1r = level1layer1.GetComponent<Renderer>();
        float level1layer1width = level1layer1r.bounds.size.x;
        for (int i = 0; i < 4.0 * (gatesCount + 2) * deltaX / level1layer1width; i++)
        {
            float cur_x = -startShift + i * level1layer1width;
            float cur_y = level1layer1r.bounds.size.y;
            GameObject cur_level1layer1 = Instantiate<GameObject>(level1layer1);
            Level1 cur_level1layer1_script = cur_level1layer1.GetComponent<Level1>();
            cur_level1layer1_script.SetPosition(cur_x, -10.0f + cur_y * 0.5f, 0.0f);
            cur_level1layer1_script.xShift = worldSpeed * 2.0f;
        }
        // Небо
        Renderer level1layer2r = level1layer2up.GetComponent<Renderer>();
        float level1layer2width = level1layer2r.bounds.size.x;
        for (int i = 0; i < (gatesCount + 2) * deltaX / level1layer1width; i++)
        {
            float cur_x = -startShift + i * level1layer2width;
            float cur_y = level1layer2r.bounds.size.y;
            GameObject cur_level1layer2 = Instantiate<GameObject>(level1layer2up);
            Level1 cur_level1layer2_script = cur_level1layer2.GetComponent<Level1>();
            cur_level1layer2_script.SetPosition(cur_x, 10.0f - cur_y * 0.5f, 0.0f);
            cur_level1layer2_script.xShift = worldSpeed * 0.5f;
        }

        int centerPhaseGates = 0;
        for (int i = 0; i < gatesCount; i++)
        {

            float cur_x = startShift + i * deltaX;
            GameObject cur_brik_border_up = Instantiate<GameObject>(brikBorder);
            Level1 cur_brik_border_instance_up = cur_brik_border_up.GetComponent<Level1>();
            cur_brik_border_instance_up.SetPosition(cur_x, 11.0f, 0.0f);
            cur_brik_border_instance_up.xShift = worldSpeed;

            int prob = prob_gates_arr[i]; // from saved level!

            if (prob > probabilityGates)
            {
                continue;
            }

            //for save
            float cur_h_add = cur_h_add_ar[i];
            float cur_x_add = cur_x_add_ar[i];

            //if(brik == null)
            //{
            //    Debug.Log("Brick"+i+" : cur_x = "+ cur_x + "; cur_y = "+ cur_y);
            //}

            // Маленькие деревья
            //for save
            float curS_h_add = curS_h_add_ar[i];
            float curS_x_add = curS_x_add_ar[i];


            GameObject cur_pineS_down = null;
            //for save
            int variant = cur_pineS_down_variant[i];
            switch (variant)
            {
                case 0:
                    cur_pineS_down = Instantiate<GameObject>(level1pine1);
                    break;
                case 1:
                    cur_pineS_down = Instantiate<GameObject>(level1pine2);
                    break;
                case 2:
                    cur_pineS_down = Instantiate<GameObject>(level1pine3);
                    break;
            }
            Renderer cur_pineS_down_r = cur_pineS_down.GetComponent<Renderer>();
            float pineS_cur_y = cur_pineS_down_r.bounds.size.y;
            Level1 cur_pineS_instance_down = cur_pineS_down.GetComponent<Level1>();
            cur_pineS_instance_down.SetPosition(cur_x + curS_x_add, -10.0f + pineS_cur_y * 0.5f + curS_h_add, 0.0f);
            cur_pineS_instance_down.xShift = worldSpeed;

            //for save
            prob = centerPhaseGates_prob[i];

            if (i % 2 == 0 && (prob < 20))
            {
                centerPhaseGates = 6;
            }
            if (centerPhaseGates > 0)
            {
                centerPhaseGates--;
            }
            if (centerPhaseGates == 0)
            {
                // Большие деревья
                if (Random.Range(0, 100) < probabilityGates)
                {
                    GameObject cur_brik_down = null;
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            cur_brik_down = Instantiate<GameObject>(level1pine1);
                            break;
                        case 1:
                            cur_brik_down = Instantiate<GameObject>(level1pine2);
                            break;
                        case 2:
                            cur_brik_down = Instantiate<GameObject>(level1pine3);
                            break;
                    }
                    Renderer cur_brik_down_r = cur_brik_down.GetComponent<Renderer>();
                    float cur_y = cur_brik_down_r.bounds.size.y;
                    Level1 cur_brik_instance_down = cur_brik_down.GetComponent<Level1>();
                    cur_brik_instance_down.SetPosition(cur_x + cur_x_add, -10.0f + cur_y * 0.5f + cur_h_add, 0.0f);
                    cur_brik_instance_down.xShift = worldSpeed;
                    // Облака
                    //if (Random.Range(0, 100) < 100)
                    if (i % 2 == 0)
                    {
                        int j = 0;
                        while (true)
                        {
                            float cur_x_add2 = Random.Range(-1.0f, 1.0f);
                            GameObject cur_cloud_down = null;
                            switch (Random.Range(0, 3))
                            {
                                case 0:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds1);
                                    break;
                                case 1:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds2);
                                    break;
                                case 2:
                                    cur_cloud_down = Instantiate<GameObject>(level1clouds3);
                                    break;
                            }
                            Renderer cur_cloud_down_r = cur_cloud_down.GetComponentInChildren<Renderer>();
                            float cloud_h = cur_cloud_down_r.bounds.size.y;
                            Level1 cur_cloud_down_level1 = cur_cloud_down.GetComponent<Level1>();
                            cur_cloud_down_level1.SetPosition(cur_x + cur_x_add + cur_x_add2, -10.0f + cur_y * 1.0f + cloud_h * (j * 1.1f + 0.5f) + gateHeight * 1.2f + cur_h_add, 0.0f);
                            cur_cloud_down_level1.xShift = worldSpeed;
                            if (-10.0f + cur_y * 1.0f + cloud_h * (j * 1.1f + 0.5f) + gateHeight * 1.2f + cur_h_add > 10.0f - cloud_h)
                            {
                                break;
                            }
                            j++;
                        }
                    }
                }
                else
                {
                    //GameObject cur_brik_center = Instantiate<GameObject>(brik2);
                    //Level1 cur_brik2_instance = cur_brik_center.GetComponent<Level1>();
                    //cur_brik2_instance.SetPosition(cur_x, cur_y - cur_h_add, 0.0f);
                    //cur_brik2_instance.xShift = 0.04f;
                }
            }
            if (centerPhaseGates == 3)
            {
                // Тучка по центру
                float cur_x_add2 = Random.Range(-2.0f, 2.0f);
                GameObject cur_cloud_down = null;
                switch (Random.Range(0, 3))
                {
                    case 0:
                        cur_cloud_down = Instantiate<GameObject>(level1clouds1);
                        break;
                    case 1:
                        cur_cloud_down = Instantiate<GameObject>(level1clouds2);
                        break;
                    case 2:
                        cur_cloud_down = Instantiate<GameObject>(level1clouds3);
                        break;
                }
                Renderer cur_cloud_down_r = cur_cloud_down.GetComponentInChildren<Renderer>();
                float cloud_h = cur_cloud_down_r.bounds.size.y;
                Level1 cur_cloud_down_level1 = cur_cloud_down.GetComponent<Level1>();
                cur_cloud_down_level1.SetPosition(cur_x + cur_x_add + cur_x_add2, -3.0f - cur_h_add * 1.5f, 0.0f);
                cur_cloud_down_level1.xShift = worldSpeed;
            }
        }

        float win_x = startShift + (gatesCount - 1) * deltaX;
        GameObject cur_win = Instantiate<GameObject>(winBorder);
        WinBorder cur_win_instance = cur_win.GetComponent<WinBorder>();
        cur_win_instance.SetPosition(win_x, 0.0f, 0.0f);
        cur_win_instance.xShift = worldSpeed;
    }

}
