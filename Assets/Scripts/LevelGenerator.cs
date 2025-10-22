using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public Vector2Int size;         // Grid size: (columns, rows)
    public Vector2 offset;          // Space between bricks
    public GameObject brickPrefab;  // Brick prefab to spawn
    public Gradient gradient;
    private void Awake()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newBrick = Instantiate(brickPrefab, transform);
                newBrick.transform.position = transform.position + new Vector3((float)((size.x - 1) * .5f-i) * offset.x, j * offset.y, 0);
                newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j / (size.y - 1));
            }
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator countdownending()
    {
        int i = 6;

        while (i != 0)
        {
            Debug.Log(i);
            i--;
            yield return new WaitForSecondsRealtime(1);
        }
        Restart();
    }
}
