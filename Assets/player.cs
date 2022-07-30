using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{
    
    [SerializeField] float MoveSpeed = 2f;
    Vector3 currentvec ;

    //身體部分
    [SerializeField] Transform snackbody ;    
    [SerializeField] GameObject Startbutton ;
    [SerializeField] GameObject Pausebutton ;
    [SerializeField] GameObject Restartbutton ;
    List<Transform> _segments;
 
    // Start is called before the first frame update
    void Start()
    {
        StartGame();       
        currentvec = Vector3.right*MoveSpeed;
        _segments = new List<Transform>();
        _segments.Add(transform);
    }

    private void FixedUpdate() {
        //移動
        for(int i=_segments.Count-1; i>0; i--)
        {
            _segments[i].position = _segments[i-1].position;
        }
        transform.position += currentvec*Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {                   
        //控制
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentvec = Vector3.left*MoveSpeed;
        }else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentvec = Vector3.right*MoveSpeed;
        }else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentvec = Vector3.up*MoveSpeed;
        }else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentvec = Vector3.down*MoveSpeed;
        }

        //Pause
        if(Input.GetKeyDown(KeyCode.Space) && Time.timeScale ==1f ){
            Time.timeScale=0f;
            Pausebutton.SetActive(true);
        }
    }          
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag =="apple"){            
            Grow();
        }else if(other.transform.tag == "wall"){
            Die();
        }
    }

    private void Grow(){        
        Transform segment = Instantiate(snackbody);
        segment.position  = _segments[_segments.Count-1].position;
        _segments.Add(segment);
    }

    public void PlayGame(){     
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;   
        if(button.name == "Re"){            
            SceneManager.LoadScene("SampleScene");
            Restartbutton.SetActive(false);
        }else{
            Startbutton.SetActive(false);
        }
        Time.timeScale = 1f;
        
    }

    public void PauseGame(){
        Time.timeScale = 1f;
        Pausebutton.SetActive(false);
    }
    public void Die(){
        Time.timeScale = 0f;
        Restartbutton.SetActive(true);
    }
    public void StartGame(){
        Time.timeScale = 0f;
        Startbutton.SetActive(true);
    }
}
