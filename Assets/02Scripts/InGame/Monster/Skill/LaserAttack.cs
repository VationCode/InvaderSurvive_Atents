using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    private LayerMask layerMask;
    private GameObject line;
    private const string lineNode = "03rcParticles/MonsterEffect/Boss/_Prefab/";
    private GameObject[] waringLines;
    private float t = 0;
    private Vector3 endPos;

    private void Start()
    {

    }

    public void CreateWaringLine(Transform _parent)
    {
        if (waringLines == null)
        {
            waringLines = new GameObject[8];
            line = Resources.Load<GameObject>(lineNode + "WaringLine");
            Vector3 dir = Vector3.zero;
            for (int i = 0; i < waringLines.Length; i++)
            {
                waringLines[i] = Instantiate(line);
                waringLines[i].transform.parent = _parent;
                waringLines[i].transform.position = new Vector3(_parent.transform.position.x, _parent.transform.position.y + 0.2f, _parent.transform.position.z);
                waringLines[i].transform.rotation = Quaternion.Euler(new Vector3(-90, dir.y, dir.z));
                dir.z += 45;
            }
        }
    }

    public void Laser()
    {
        /*for (int i = 0; i < waringLines.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(waringLines[i].transform.position, waringLines[i].transform.right, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Monster") | ~LayerMask.NameToLayer("InavderRange")))
            {
                Debug.Log(hit.transform.gameObject.name);
                Vector3.Lerp(waringLines[i].transform.position, hit.point,Time.deltaTime * 3.5f);
            }
        }*/
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
