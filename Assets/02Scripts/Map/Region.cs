using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CellInfo
{
    public int arrIndex;
    public Vector3 centerPos;
}
//�ʵ� ������ ���
public class Region : MonoBehaviour
{
    Vector3 mapSize;
    int row;
    int column;
    float xStartPos;
    float zStartPos;
    float cellxSize;
    float cellzSize;
    Dictionary<int, CellInfo> cellList;

    private void Awake()
    {
        cellList = new Dictionary<int, CellInfo>();
    }


    void Start()
    {
        GetMapSize();
        cellxSize = mapSize.x / (float)column;
        cellzSize = mapSize.z / (float)row;
        xStartPos = transform.position.x - mapSize.x * 0.5f; //����x�� ��ġ�� �ִ� x�Ÿ�
        zStartPos = transform.position.z - mapSize.z * 0.5f;
        SpawanAll();

    }

    public void Initialize()
    {
        int tileCount = row * column;
        Vector3 tmp = Vector3.zero;
        for (int i = 0; i < tileCount; i++)
        {
            int r = i / column;
            //int 
        }
    }

    public void GetMapSize()
    {
        //1. Collider�� �ִ���
        //2. Vertex ���ϱ�

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Vector3[] vertexs =  meshFilter.mesh.vertices;
        float xMin = vertexs[0].x, xMax = vertexs[0].x, zMin = vertexs[0].z, zMax = vertexs[0].z;
        for (int i = 1; i < vertexs.Length; i++) //�ι�° �ε������� �˻�
        {
            if(xMin > vertexs[i].x )
            {
                xMin = vertexs[i].x;
            }
            if (xMax < vertexs[i].x)
            {
                xMax = vertexs[i].x;
            }

            if (zMin > vertexs[i].z)
            {
                zMin = vertexs[i].z;
            }
            if (zMax < vertexs[i].z)
            {
                zMax = vertexs[i].z;
            }
        }
        //xSize = (xMax - xMin)*transform.localScale.x; // ũ�⸦ �÷ȴٰ��ؼ� �������� ���� ���ϴ°��� �ƴϱ⿡ �������� ���ؾ� ������� Vertex�� �����ǰ��� ���� �� �ִ�
        //zSize = zMax - zMin*transform.localScale.z;
        Vector3 tmp1 = new Vector3(xMin, 0, zMin);
        Vector3 tmp2 = new Vector3(xMax, 0, zMin);
        Vector3 wordMin = transform.TransformPoint(tmp1);
        Vector3 wordMax = transform.TransformPoint(tmp2);
        mapSize.x = wordMax.x - wordMin.x;
        mapSize.z = wordMax.z - wordMin.z;
        Collider col =  this.gameObject.AddComponent<Collider>();
        //col.bounds.size = mapSize;
        //Debug.Log("xSize = " + xSize +" , " + "zSize = " + zSize);

    }

    //������ �߾ӿ� ��� ���ӿ�����Ʈ�� ����
    public void SpawanAll()
    {
        int tileIndex = row * column; //2���� �迭 ��� 1���� �迭�� ���
        Vector3 tmp = Vector3.zero;
        for (int i = 0; i < tileIndex; i++)
        {
            int nR = i / column; //���� ��
            int nc = i % column; //���� ��
            Vector3 pos = Vector3.zero;
            pos.x = xStartPos + cellxSize * nc + cellxSize * 0.5f; //cellSize��ŭ ����
            pos.y = 1f;
            pos.z = zStartPos - cellxSize * nR - cellxSize * 0.5f;
            //���ӿ�����Ʈ�� ��ġ ���
            GameObject tmpObj = GameObject.CreatePrimitive(PrimitiveType.Cube); //���߿� ���� ��ġ
            tmpObj.name = "Monster";
            tmpObj.transform.position = pos;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
