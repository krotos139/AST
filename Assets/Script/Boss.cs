using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Boss : MonoBehaviour {

    public GameObject audioBossDamage;
    public GameObject audioBossDie;
    public GameObject fadeBlack;
    //public GameObject fadeLose;
    public Tentacle tentacle1;
    public Tentacle tentacle2;
    public Tentacle tentacle3;
    public Tentacle tentacle4;
    public Tentacle tentacle5;
    public Tentacle tentacle6;
    public Tentacle tentacle7;
    public Tentacle tentacle8;
    private float fade;
    private float nextTentacleTime;
    private int tentacleCount;
    private int tentacleCountSpauned;
    public int tentacleMaxCount = 5;
    private bool gameOver;
    private float gameOverFade;
    // Use this for initialization
    void Start () {
        fade = 0.0f;
        setFade();
        gameOverFade = 0.0f;
        //setFadeLose();
        nextTentacleTime = Time.fixedTime+1.0f;
        tentacleCount = 0;
        tentacleCountSpauned = 0;
        gameOver = false;
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        musicManager.StartLevel(1);

        if (true)
        {
            var objects = FindObjectsOfType<MouseDragAndDrop>();
            foreach (MouseDragAndDrop ob in objects)
            {
                //Debug.Log("Staff=" + ob.name);

                int index = 0;
                bool found = false;
                foreach (string name in CommonSingleton.staffNames)
                {
                    //Debug.Log("STAFF SINGLETON NAME : " + name);
                    if (name.Equals(ob.name))
                    {
                        found = true;
                        //Debug.Log("FOUND");
                        break;
                    }
                    index++;
                }



                if (found)
                {

                    //Debug.Log("ALIVE staff!");

                    //ob.transform.rotation = CommonSingleton.staffQuat.ElementAt<Quaternion>(index);
                    //ob.transform.position = CommonSingleton.staffPos.ElementAt<Vector2>(index);
                }
                else {

                    Destroy(ob.gameObject);
                }
            }
        }
    }

    private void setFade()
    {
        SpriteRenderer fadeBlack_sr = fadeBlack.GetComponent<SpriteRenderer>();
        Color fadeBlack_col = fadeBlack_sr.color;
        fadeBlack_col.a = fade;
        fadeBlack_sr.color = fadeBlack_col;
    }
    /*private void setFadeLose()
    {
        SpriteRenderer fadeBlack_sr = fadeLose.GetComponent<SpriteRenderer>();
        Color fadeBlack_col = fadeBlack_sr.color;
        fadeBlack_col.a = gameOverFade;
        fadeBlack_sr.color = fadeBlack_col;
    }*/

    private void setFadeGameOver()
    {
        SpriteRenderer fadeBlack_sr = fadeBlack.GetComponent<SpriteRenderer>();
        Color fadeBlack_col = fadeBlack_sr.color;
        fadeBlack_col.a = fade;
        fadeBlack_sr.color = fadeBlack_col;
    }

    private void addTentacle()
    {
        
        tentacleCount += 1;
        Debug.Log("addTentacle (tentacleCount=" + tentacleCount + ")");
        tentacleCountSpauned += 1;
        while (true)
        {
            Tentacle tentacle;
            switch (Random.Range(1,8))
            {
                case 1:
                    tentacle = tentacle1;
                    break;
                case 2:
                    tentacle = tentacle2;
                    break;
                case 3:
                    tentacle = tentacle3;
                    break;
                case 4:
                    tentacle = tentacle4;
                    break;
                case 5:
                    tentacle = tentacle5;
                    break;
                case 6:
                    tentacle = tentacle6;
                    break;
                case 7:
                    tentacle = tentacle7;
                    break;
                default:
                    tentacle = tentacle8;
                    break;
            }
            if (!tentacle.isFree)
            {
                continue;
            }
            tentacle.Activate();
            break;
        }
    }

    public void DeleteTentacle()
    {
        tentacleCount -= 1;
        audioBossDamage.GetComponent<AudioSource>().Play();
        //Debug.Log("DeleteTentacle (tentacleCount=" + tentacleCount + ")");
    }

    // Update is called once per frame
    public float speedOfGameOver = 0.01f;
    void Update () {
		if (nextTentacleTime < Time.fixedTime && (tentacleCountSpauned < tentacleMaxCount) && (tentacleCount < 8))
        {
            nextTentacleTime = Time.fixedTime + 1.0f;// Random.Range(1.0f, 7.0f);
            addTentacle();
        }
        fade += Time.deltaTime * tentacleCount* speedOfGameOver;
        if (fade >= 0.8)
        {
            gameOver = true;
        }
        setFade();
        if (gameOver)
        {
            gameOverFade += Time.deltaTime * 0.5f;
        }
        //setFadeLose();
        if (gameOverFade >= 1.0f)
        {
            YouLose();
        }
        if (tentacleCount == 0 && (tentacleCountSpauned == tentacleMaxCount))
        {
            YouWon();
        }
    }

    private void YouWon()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("you_won");
        UnityEngine.SceneManagement.SceneManager.LoadScene("final1", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
    private void YouLose()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game_over_3", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
