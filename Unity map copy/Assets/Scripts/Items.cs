using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject toClone;
    Vector3 rotate = new Vector3(-90, 0, 0);

    // Start is called before the first frame update
    void Start() {
        CreateSquare(0, 10, 1);
        CreateXLine(-10, -10, 5, 2);
        CreateYLine(12, 6, 3, 3);
        CreateSingle(-2, 5);
    }

    void CreateSquare(int x, int z, int width = 3, int y = 50) {
        if(Physics.Raycast(new Vector3(x, y, z), Vector3.down, out RaycastHit hit1)){
            Instantiate(toClone, hit1.point, Quaternion.Euler(rotate));
        }
        if(Physics.Raycast(new Vector3(x+width, y, z), Vector3.down, out RaycastHit hit2)){
            Instantiate(toClone, hit2.point, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
        if(Physics.Raycast(new Vector3(x, y, z+width), Vector3.down, out RaycastHit hit3)){
            Instantiate(toClone, hit3.point, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
        if(Physics.Raycast(new Vector3(x+width, y, z+width), Vector3.down, out RaycastHit hit4)){
            Instantiate(toClone, hit4.point, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }

    void CreateXLine(int x, int z, int count, int spacing = 3, int y = 50) {
        for (int i = 0; i < count; i++){
            if(Physics.Raycast(new Vector3(x+(i*spacing), y, z), Vector3.down, out RaycastHit hit1)){
                Instantiate(toClone, hit1.point, Quaternion.Euler(rotate));
            }
        }
    }

    void CreateYLine(int x, int z, int count, int spacing = 3, int y = 50) {
        for (int i = 0; i < count; i++){
            if(Physics.Raycast(new Vector3(x, y, z+(i*spacing)), Vector3.down, out RaycastHit hit1)){
                Instantiate(toClone, hit1.point, Quaternion.Euler(rotate));
            }
        }
    }

    void CreateSingle(int x, int z, int y = 50) {
        if(Physics.Raycast(new Vector3(x, y, z), Vector3.down, out RaycastHit hit1)){
            Instantiate(toClone, hit1.point, Quaternion.Euler(rotate));
        }
    }
}
