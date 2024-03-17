using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChamarInstrucao(GameObject instrucao)
    {
        instrucao.SetActive(true);

    }

    public void FecharInstrucao(GameObject instrucao)
    {
        instrucao.SetActive(false);
    }
}
