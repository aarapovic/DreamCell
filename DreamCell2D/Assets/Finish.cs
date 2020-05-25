using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Finish : MonoBehaviour
{
    public Item ring1;
    public Item ring2;
    public Item ring3;
    public GameObject VideoPlayer;
    public int timeToEnd;
    public GameObject canvas;
    // Update is called once per frame
    public void Start()
    {
        VideoPlayer.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
       if(ring1.isSet && ring2.isSet && ring3.isSet && other.CompareTag("Player"));
        {
            canvas.SetActive(false);
            VideoPlayer.SetActive(true);
            Destroy(VideoPlayer, timeToEnd);
           
    }
        SceneManager.LoadScene("GameOver");
    }
}
