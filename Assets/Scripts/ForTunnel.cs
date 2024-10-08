using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForTunnel : MonoBehaviour
{
    public ForCube cube;
    public Text lives;
    public Text score;
    public Text gameOver;
    public Chunk[] chunksPrefabs;
    private List<Chunk> spawnedChunks = new List<Chunk>();
    private int Length = 100;
    private int times = 0;
    private float S = 0;
    private float StartSpeed;
    private int StartLives;

    // Start is called before the first frame update
    void Start()
    {
        
        Chunk newChunk = Instantiate(chunksPrefabs[0]);
        newChunk.transform.position = new Vector3(0, 0, 0);
        spawnedChunks.Add(newChunk);
        for (int i=1; i< chunksPrefabs.Length; i++)
        {
            newChunk = Instantiate(chunksPrefabs[Random.Range(0, chunksPrefabs.Length)]);
            newChunk.transform.position = new Vector3(-i * Length, 0, 0);
            spawnedChunks.Add(newChunk);
        }
        StartSpeed = cube.speed;
        StartLives = cube.lives;

    }

    public void StartAgain()
    {
        for (int i = 0; i < spawnedChunks.Count; i++)
        {
            spawnedChunks[i].toBeDestroy();
            
        }
        spawnedChunks.Clear();
        Chunk newChunk = Instantiate(chunksPrefabs[0]);
        newChunk.transform.position = new Vector3(0, 0, 0);
        spawnedChunks.Add(newChunk);
        times = 0;
        S = 0;
        gameOver.text = "";
        cube.changeLives(StartLives);
 
    }
    // Update is called once per frame
    void Update()
    {
        lives.text = cube.getLives().ToString();
        score.text = Mathf.Round(S/500).ToString();
        //Debug.Log("A=" + Vector3.Distance(spawnedChunks[spawnedChunks.Count-1].transform.position, cube.getPosition() ));
        //Debug.Log(spawnedChunks[spawnedChunks.Count - 1].transform.position);
        //Debug.Log("LENGTH="+Length * spawnedChunks.Count);
        //transform.position += new Vector3(speed, 0, 0);
        if(cube.lives < 0)
        {
            cube.changeGame(false);
            gameOver.text = "ÈÃÐÀ ÎÊÎÍ×ÅÍÀ\nÍאזלטעו ןנמבוכ, קעמבû םאקאעü חאםמגמ";
        }
        for (int i=0; i<spawnedChunks.Count; i++)
        {
            try
            {
                spawnedChunks[i].transform.position += new Vector3(cube.speed, 0, 0) * Time.deltaTime;
            }
            catch { }
            S += cube.speed*Time.deltaTime;

        }

        
        if (Mathf.Abs(spawnedChunks[spawnedChunks.Count-1].transform.position.x) < 100 )
        {
            
            cube.changeSpeed(StartSpeed + times*3/10);
           
            float pos = spawnedChunks[spawnedChunks.Count - 1].transform.position.x;

            for (int i = 1; i < chunksPrefabs.Length + 1; i++)
            {
                Chunk newChunk = Instantiate(chunksPrefabs[Random.Range(0, chunksPrefabs.Length)]);
                newChunk.transform.position = new Vector3(pos - i * Length, 0, 0);
                spawnedChunks.Add(newChunk);
                times += 1;
            }
            
        }
    }
}
