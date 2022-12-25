using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//计分人员
public class ScoreRecorder : System.Object{
    public int score;

    public void Record(GameObject disk){
        score += disk.GetComponent<DiskData>().points;
    }
    
    public void Reset(){
        score = 0;
    }
}