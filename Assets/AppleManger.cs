using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManger : MonoBehaviour
{
    [SerializeField] GameObject prefabapple ;

    [SerializeField] Transform borderTop;
    [SerializeField] Transform borderBottom;
    [SerializeField] Transform borderLeft;
    [SerializeField] Transform borderRight;
    public void SpwanApple(Vector3 vec){
        
        float x = Random.Range(borderLeft.position.x,borderRight.position.x);
        float y = Random.Range(borderBottom.position.y,borderTop.position.y);
        while(Mathf.Abs(x - vec.x)<0.3 || Mathf.Abs(y - vec.y)<0.3 ){
            x = Random.Range(borderLeft.position.x,borderRight.position.x);
            y = Random.Range(borderBottom.position.y,borderTop.position.y);
        }
        GameObject pre_apple =  Instantiate(prefabapple, transform);
        pre_apple.transform.position = new Vector3(x, y, 0f);
    }
}
