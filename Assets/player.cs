using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{
    //速度
    [SerializeField] float MoveSpeed ;
    [SerializeField] Text speed ;
    [SerializeField] GameObject plus;
    [SerializeField] GameObject de;
    Vector3 currentvec ;

    //身體部分
    [SerializeField] Transform snackbody ;    
    List<Transform> _segments;
    //遊戲系統
    [SerializeField] GameObject Startbutton ;
    [SerializeField] GameObject Pausebutton ;
    [SerializeField] GameObject Restartbutton ;

    //分數相關
    [SerializeField] Text n_score ;
    int score ;
    
    // Start is called before the first frame update
    void Start()
    {       
        Time.timeScale = 0f;
        score = 0;  
        MoveSpeed = 2f ;      
        currentvec = Vector3.right;
        _segments = new List<Transform>();
        _segments.Add(transform);
        speed.text = "SPEED:"+ ((int)MoveSpeed).ToString();
    }

    private void FixedUpdate() {
        //移動
        for(int i=_segments.Count-1; i>0; i--)
        {
            _segments[i].position = _segments[i-1].position;
        }
        transform.position += currentvec*MoveSpeed*Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {                   
        
        //控制
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentvec = Vector3.left;
        }else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentvec = Vector3.right;
        }else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentvec = Vector3.up;
        }else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentvec = Vector3.down;
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
            UpdateScore(10);
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
    void UpdateScore(int num){
        score += num ;        
        n_score.text = "score:"+ score.ToString();
    }

    public void ChangeSpeed(){
        var s  = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;        
        if(s == de){      
            if(MoveSpeed>1f){MoveSpeed -= 1f;}            
        }else if(s == plus ){            
            if(MoveSpeed<8f){MoveSpeed += 1f;}            
        }
        speed.text = "SPEED:"+ ((int)MoveSpeed).ToString();
    }
}   
