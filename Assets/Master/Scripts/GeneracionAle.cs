using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneracionAle : MonoBehaviour
{

    private int Size_Hori_Cube = 10;
    private int Max_Cubes = 7, Min_Cubes = 3;
    //private int Max_NumCubes = 4, Min_NumCubes = 2;

    public GameObject prefab_Platforme;

    // Start is called before the first frame update
    void Start()
    {
        GenerationProcedural();
    }

    void GenerationProcedural()
    {
        for (int x = 0; x < 5; x++)
        {
            float y_pos = 0;
            int x_pos = 0;
            switch (x)
            {
                case 0:
                    x_pos = 12 - 2;
                    y_pos = -8;
                    Max_Cubes = 8; Min_Cubes = 6;
                    break;
                case 1:
                    x_pos = 16 - 2;
                    y_pos = -4;
                    Max_Cubes = 12; Min_Cubes = 8;
                    break;
                case 2:
                    x_pos = 20 - 2;
                    y_pos = 0;
                    Max_Cubes = 16; Min_Cubes = 12;
                    break;
                case 3:
                    x_pos = 16 - 2;
                    y_pos = 4;
                    Max_Cubes = 12; Min_Cubes = 8;
                    break;
                case 4:
                    x_pos = 12 - 2;
                    y_pos = 8;
                    Max_Cubes = 8; Min_Cubes = 6;
                    break;
            }

            int Cubes = Random.Range(Min_Cubes, Max_Cubes - 1);
            int NumSpaces = x_pos - Cubes;
            int NumSpacesTmp = NumSpaces;
            int NumSpaceUsed = 0;

            int IniSpace = 0;
            if (x == 0 || x == 4)
            {
                IniSpace = Random.Range(2, x_pos - Cubes);
            }
            else
            {
                IniSpace = 2;
            }

            NumSpaceUsed += IniSpace;

            int Taille_inst = 0;
            Taille_inst = Random.Range(2, Cubes);
            Debug.Log(Taille_inst);

            if (Taille_inst > 5)
            {
                Taille_inst = 5;
            }

            for (int y = 0; y < Taille_inst; y ++)
            {
                GameObject inst_tmp = Instantiate(prefab_Platforme, new Vector3(-(x_pos) / 2, y_pos, 0) + new Vector3(NumSpaceUsed, 0, 0), Quaternion.identity);
                inst_tmp.transform.parent = transform;
                NumSpaceUsed += 1;
            }

            Cubes -= Taille_inst;
            NumSpaces -= IniSpace;

            ////////////////////Right

            Taille_inst = Random.Range(2, Cubes);

            if (Taille_inst > 5)
            {
                Taille_inst = 5;
            }

            for (int y = 0; y < Taille_inst; y++)
            {
                GameObject inst_tmp = Instantiate(prefab_Platforme, new Vector3((x_pos) / 2, y_pos, 0) - new Vector3(y, 0, 0), Quaternion.identity);
                inst_tmp.transform.parent = transform;
            }

            Cubes -= Taille_inst;

            while (Cubes > 0)
            {
                int Cubes_tmp = Random.Range(1, Cubes);

                if ((Cubes_tmp == Cubes) && (x == 0 || x ==4))
                {

                    IniSpace = NumSpaces;

                    NumSpaceUsed += IniSpace;

                    Taille_inst = Cubes;

                    for (int y = 0; y < Taille_inst; y++)
                    {
                        GameObject inst_tmp = Instantiate(prefab_Platforme, new Vector3(-(x_pos / 2), y_pos, 0) + new Vector3(NumSpaceUsed, 0, 0), Quaternion.identity);
                        inst_tmp.transform.parent = transform;
                        NumSpaceUsed += 1;
                    }

                    Cubes -= Taille_inst;
                    NumSpaces -= IniSpace;
                }
                /*else if (Cubes_tmp == Cubes)
                {

                    IniSpace = NumSpaces;

                    NumSpaceUsed += IniSpace;

                    Taille_inst = Cubes;

                    for (int y = 0; y < Taille_inst; y++)
                    {
                        GameObject inst_tmp = Instantiate(prefab_Platforme, new Vector3(-(x_pos / 2), y_pos, 0) + new Vector3(NumSpaceUsed, 0, 0), Quaternion.identity);
                        inst_tmp.transform.parent = transform;
                        NumSpaceUsed += 1;
                    }

                    Cubes -= Taille_inst;
                    NumSpaces -= IniSpace;

                    Debug.Log("Hey");

                }*/
                else
                {
                    IniSpace = Random.Range(1, NumSpaces);
                    if (IniSpace < 3)
                    {
                        if (NumSpaces >=3)
                        {
                            IniSpace = 3;
                        }else if (NumSpaces >= 2)
                        {
                            IniSpace = 2;
                        }
                    }

                    NumSpaceUsed += IniSpace;

                    Taille_inst = Cubes_tmp;

                    if (Taille_inst < 3)
                    {
                        if (Cubes >= 3)
                        {
                            Taille_inst = 3;
                        }
                        else if (Cubes >= 2)
                        {
                            Taille_inst = 2;
                        }

                    }

                    if (Taille_inst > 8)
                    {
                        Taille_inst = 8;
                    }

                    for (int y = 0; y < Taille_inst; y++)
                    {
                        GameObject inst_tmp = Instantiate(prefab_Platforme, new Vector3(-(x_pos / 2), y_pos, 0) + new Vector3(NumSpaceUsed, 0, 0), Quaternion.identity);
                        inst_tmp.transform.parent = transform;
                        NumSpaceUsed += 1;
                    }

                    Cubes -= Taille_inst;
                    NumSpaces -= IniSpace;
                }
            }
        }
    }

    public void New_Generation()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GenerationProcedural();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
