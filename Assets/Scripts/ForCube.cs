using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCube : MonoBehaviour
{
    public float maxSpeed;
    public int lives;
    public ForTunnel tunnel;
    [HideInInspector] public float speed;
    private bool pause = false;
    private bool game = true;

    Vector3[][] situations = new Vector3[][] {  new Vector3[] { new Vector3(8.23999977f, -0.879999995f, -0.360000014f), new Vector3(0, 0, 0) }, // [position, rotation]
                                                new Vector3[] { new Vector3(8.23999977f, -0.0390000008f, 1.10599995f), new Vector3(292.388763f, 0, 0) },
                                                new Vector3[] { new Vector3(8.23999977f, 1.324f, 0.55400002f), new Vector3(322.920319f,180,180) },
                                                new Vector3[] { new Vector3(8.23999977f, 1.49399996f, -1.13399994f), new Vector3(38.8687286f,180,180) },
                                                new Vector3[] { new Vector3(8.23999977f, 0.063000001f, -1.94299996f), new Vector3(66.7162933f, 0, 0) } };
    private int position = 0;
    // Start is called before the first frame update
    void Start()
    {
        speed = maxSpeed;
    }
    public Vector3 getPosition()
    {
        return transform.position;
    }
    public int getLives()
    {
        return lives;
    }
    public void changeLives(int newLives)
    {
        lives = newLives;
    }
    public void changeGame(bool newGame)
    {
        game = newGame;
    }
    public void changeSpeed(float newSpeed)
    {
        maxSpeed = newSpeed;
        speed = maxSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            lives -= 1;
            if (lives < 0)
            {
                speed = 0;
            }
            Destroy(other.gameObject);
        }
        else if(other.tag == "Healthy")
        {
            lives += 1;
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && game)
            {
                if (position - 1 < 0) position = situations.Length - 1;
                else position -= 1;

                transform.position = situations[position][0];
                transform.rotation = Quaternion.Euler(situations[position][1]);
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && game)
            {
                if (position + 1 > situations.Length - 1) position = 0;
                else position += 1;

                transform.position = situations[position][0];
                transform.rotation = Quaternion.Euler(situations[position][1]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !game)
        {
            tunnel.StartAgain();
            game = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!game)
            {
                pause = true;
            }
            else
            {
                if (pause)
                {
                    pause = false;
                    speed = maxSpeed;
                }
                else
                {
                    pause = true;
                    speed = 0;
                }
            }
        }
    }
}
