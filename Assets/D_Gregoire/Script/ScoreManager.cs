using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    //Avoir accès a toutes les images pour la barre de score 
    private int globalScore = 0;
    public Image[] imagelist = new Image[13];
    void Start()
    {
        //Get absolument toute les images et ensuite faire un switch pour que en fonction du score les bonnes image s'affichent
        for (int i = 0; i < 12; i++)
        {
            imagelist[i].enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        imagelist[6].enabled = true;
        imagelist[0].enabled = true;
        imagelist[12].enabled = true;
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            globalScore += 1;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            globalScore -= 1;
        }
        switch (globalScore)
        {
            case 1:
                imagelist[5].enabled = true;
                imagelist[1].enabled = false;
                imagelist[2].enabled = false;
                imagelist[3].enabled = false;
                imagelist[4].enabled = false;
                imagelist[7].enabled = false;
                imagelist[8].enabled = false;
                imagelist[9].enabled = false;
                imagelist[10].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                break;
            case 2:
                imagelist[4].enabled = true;
                imagelist[5].enabled = true;
                imagelist[2].enabled = false;
                imagelist[3].enabled = false;
                imagelist[7].enabled = false;
                imagelist[8].enabled = false;
                imagelist[9].enabled = false;
                imagelist[10].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                break;
            case 3:
                imagelist[3].enabled = true;
                imagelist[4].enabled = true;
                imagelist[5].enabled = true;
                imagelist[2].enabled = false;
                imagelist[7].enabled = false;
                imagelist[8].enabled = false;
                imagelist[9].enabled = false;
                imagelist[10].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                break;
            case 4:
                imagelist[2].enabled = true;
                imagelist[3].enabled = true;
                imagelist[4].enabled = true;
                imagelist[5].enabled = true;
                imagelist[7].enabled = false;
                imagelist[8].enabled = false;
                imagelist[9].enabled = false;
                imagelist[10].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                break;
            case 5:
                imagelist[1].enabled = true;
                imagelist[2].enabled = true;
                imagelist[3].enabled = true;
                imagelist[4].enabled = true;
                imagelist[5].enabled = true;
                imagelist[7].enabled = false;
                imagelist[8].enabled = false;
                imagelist[9].enabled = false;
                imagelist[10].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                Debug.Log("Joueur 1 gagné !!");
                Debug.Break();
                break;
            case -1:
                imagelist[7].enabled = true;
                imagelist[1].enabled = false;
                imagelist[2].enabled = false;
                imagelist[3].enabled = false;
                imagelist[4].enabled = false;
                imagelist[5].enabled = false;
                imagelist[8].enabled = false;
                imagelist[9].enabled = false;
                imagelist[10].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                break;
            case -2:
                imagelist[7].enabled = true;
                imagelist[8].enabled = true;
                imagelist[1].enabled = false;
                imagelist[2].enabled = false;
                imagelist[3].enabled = false;
                imagelist[4].enabled = false;
                imagelist[5].enabled = false;
                imagelist[9].enabled = false;
                imagelist[10].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                break;
            case -3:
                imagelist[7].enabled = true;
                imagelist[8].enabled = true;
                imagelist[9].enabled = true;
                imagelist[1].enabled = false;
                imagelist[2].enabled = false;
                imagelist[3].enabled = false;
                imagelist[4].enabled = false;
                imagelist[5].enabled = false;
                imagelist[10].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                break;
            case -4:
                imagelist[7].enabled = true;
                imagelist[8].enabled = true;
                imagelist[9].enabled = true;
                imagelist[10].enabled = true;
                imagelist[1].enabled = false;
                imagelist[2].enabled = false;
                imagelist[3].enabled = false;
                imagelist[4].enabled = false;
                imagelist[5].enabled = false;
                imagelist[11].enabled = false;
                imagelist[12].enabled = false;
                break;
            case -5:
                imagelist[7].enabled = true;
                imagelist[8].enabled = true;
                imagelist[9].enabled = true;
                imagelist[10].enabled = true;
                imagelist[11].enabled = true;
                imagelist[1].enabled = false;
                imagelist[2].enabled = false;
                imagelist[3].enabled = false;
                imagelist[4].enabled = false;
                imagelist[5].enabled = false;
                imagelist[12].enabled = false;
                break;

        }
    }

    void displayScoreView()
    {
        //Vas refresh le score avant l'animation et l'afficher 

    }

    void manageScore(int JoueurGagnant)
    {
        //Si le joueur remportant la manche est le joueur 1
        if (JoueurGagnant == 1)
        {
            globalScore = globalScore + 1;
        }
        else
        {
            globalScore = globalScore - 1;
        }
    }
}
