using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileData : MonoBehaviour
{
    private string saveFileName;
    public void SetSaveFileName(string saveFileName){
        this.saveFileName = saveFileName;
    }
    public string GetSaveFileName(){
        return this.saveFileName;
    }
}
