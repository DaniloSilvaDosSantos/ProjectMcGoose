using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int minutos = 1;
    public int segundos = 30;
    public bool stop = false;
    public string strContador;
    public TMP_Text contador;
    bool rodarRotina = true;
    bool executarFuncao = true;

    private void Awake()
    {
        contador = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(rodarRotina && !stop)
        {
            StartCoroutine(Contador());
            rodarRotina = false;
        }

        if (stop && executarFuncao)
        {
            OnFinish();
            executarFuncao = false;
        }

        string strMinutos;
        string strSegundos;

        strMinutos = minutos.ToString();
        /*if (minutos <= 9)
        {
            strMinutos = "0" + strMinutos;
        }*/
        strSegundos = segundos.ToString();
        if (segundos < 10)
        {
            strSegundos = "0" + strSegundos;
        }
        contador.text = strMinutos + ":" + strSegundos;
    }

    public void OnFinish()
    {
        //faz algo
    }

    IEnumerator Contador()
    {
        yield return new WaitForSeconds(1);
        segundos -= 1;
        if (segundos <= 0)
        {
            if (minutos > 0)
            {
                minutos -= 1;
                segundos = 59;
            }
            else
            {
                stop = true;
            }
        }
        rodarRotina = true;
        yield return null;
    }
}
