using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneracionAle : MonoBehaviour
{

    private int Size_Hori_Cube = 10;
    private int Max_Cubes = 7, Min_Cubes = 3;
    private int Max_NumCubes = 4, Min_NumCubes = 2;

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
                    x_pos = 5;
                    y_pos = -4;
                    Max_Cubes = 5; Min_Cubes = 1;
                    Max_NumCubes = 3; Min_NumCubes = 1;
                    break;
                case 1:
                    x_pos = 7;
                    y_pos = -2;
                    Max_Cubes = 7; Min_Cubes = 5;
                    Max_NumCubes = 4; Min_NumCubes = 2;
                    break;
                case 2:
                    x_pos = 10;
                    y_pos = 0;
                    Max_Cubes = 9; Min_Cubes = 6;
                    Max_NumCubes = 5; Min_NumCubes = 2;
                    break;
                case 3:
                    x_pos = 7;
                    y_pos = 2;
                    Max_Cubes = 7; Min_Cubes = 5;
                    Max_NumCubes = 4; Min_NumCubes = 2;
                    break;
                case 4:
                    x_pos = 5;
                    y_pos = 4;
                    Max_Cubes = 5; Min_Cubes = 1;
                    Max_NumCubes = 3; Min_NumCubes = 1;
                    break;
            }

            int Cubes = Random.Range(Min_Cubes, Max_Cubes - 1);
            int NumSpaces = x_pos - Cubes;
            int NumCubes = Random.Range(Min_NumCubes, Max_NumCubes);
            int NumSpacesTmp = NumSpaces;
            int NumSpaceUsed = 0;

            int IniSpace = 0;
            if (x == 0 || x == 4)
            {
                IniSpace = Random.Range(0, x_pos - Cubes);
            }
            else
            {
                IniSpace = 0;
            }



            GameObject inst_tmp = Instantiate(prefab_Platforme, new Vector3(-(x_pos)/2, y_pos, 0) + new Vector3(IniSpace, 0, 0), Quaternion.identity);
            inst_tmp.transform.parent = transform;

            int Taille_inst = 0;

            if (NumCubes == 1)
            {
                Taille_inst = Cubes;
            }
            else
            {
                Taille_inst = Random.Range(1, Cubes);
                if (Taille_inst > 3)
                {
                    Taille_inst = 3;
                }
            }


            inst_tmp.transform.localScale = new Vector3(Taille_inst, 0.5f, 1);
            inst_tmp.transform.position += new Vector3((Taille_inst - 1) * 0.5f, 0, 0);

            Cubes -= Taille_inst;
            NumCubes--;
            NumSpaces -= IniSpace;

            NumSpaceUsed = Taille_inst + IniSpace;
            
            while (Cubes > 0 && NumCubes > 0)
            {
                int Cubes_tmp = Random.Range(1, Cubes);

                if ((NumCubes == 1 || Cubes_tmp == Cubes) && (x == 0 && x ==4))
                {
                    inst_tmp = Instantiate(prefab_Platforme, new Vector3(-(x_pos/2), y_pos, 0) + new Vector3(x_pos , 0, 0), Quaternion.identity);
                    inst_tmp.transform.parent = transform;

                    Taille_inst = Cubes;

                    inst_tmp.transform.localScale = new Vector3(Taille_inst, 0.5f, 1);
                    inst_tmp.transform.position -= new Vector3((Taille_inst - 1) * 0.5f, 0, 0);

                    Cubes -= Taille_inst;
                    NumCubes--;
                    NumSpaces -= IniSpace;
                }else if ((NumCubes == 1 || Cubes_tmp == Cubes))
                {
                    inst_tmp = Instantiate(prefab_Platforme, new Vector3(-(x_pos / 2), y_pos, 0) + new Vector3(x_pos, 0, 0), Quaternion.identity);
                    inst_tmp.transform.parent = transform;

                    Taille_inst = Cubes;

                    inst_tmp.transform.localScale = new Vector3(Taille_inst, 0.5f, 1);
                    inst_tmp.transform.position -= new Vector3((Taille_inst - 1) * 0.5f, 0, 0);

                    Cubes -= Taille_inst;
                    NumCubes--;
                    NumSpaces -= IniSpace;
                }
                else
                {
                    IniSpace = Random.Range(1, NumSpacesTmp - NumSpaces);

                    inst_tmp = Instantiate(prefab_Platforme, new Vector3(-(x_pos / 2), y_pos, 0) + new Vector3(NumSpaceUsed + IniSpace, 0, 0), Quaternion.identity);
                    inst_tmp.transform.parent = transform;

                    if (NumCubes == 1)
                    {
                        Taille_inst = Cubes;
                    }
                    else
                    {
                        Taille_inst = Random.Range(1, Cubes);
                    }

                    inst_tmp.transform.localScale = new Vector3(Taille_inst, 0.5f, 1);
                    inst_tmp.transform.position += new Vector3(Taille_inst - 1, 0, 0);

                    Cubes -= Taille_inst;
                    NumCubes--;
                    NumSpaces -= IniSpace;

                    NumSpaceUsed = Taille_inst + IniSpace;
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
