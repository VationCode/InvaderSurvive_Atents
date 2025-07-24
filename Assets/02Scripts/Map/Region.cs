using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CellInfo
{
    public int arrIndex;
    public Vector3 centerPos;
}
//필드 영역값 계산
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
        xStartPos = transform.position.x - mapSize.x * 0.5f; //현재x값 위치해 있는 x거리
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
        //1. Collider이 있는지
        //2. Vertex 구하기

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Vector3[] vertexs =  meshFilter.mesh.vertices;
        float xMin = vertexs[0].x, xMax = vertexs[0].x, zMin = vertexs[0].z, zMax = vertexs[0].z;
        for (int i = 1; i < vertexs.Length; i++) //두번째 인덱스부터 검사
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
        //xSize = (xMax - xMin)*transform.localScale.x; // 크기를 늘렸다고해서 포지션의 값이 변하는것은 아니기에 스케일을 곱해야 월드상의 Vertex의 포지션값을 구할 수 있다
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

    //격자의 중앙에 모든 게임오브젝트를 생성
    public void SpawanAll()
    {
        int tileIndex = row * column; //2차원 배열 대신 1차원 배열로 계산
        Vector3 tmp = Vector3.zero;
        for (int i = 0; i < tileIndex; i++)
        {
            int nR = i / column; //현재 열
            int nc = i % column; //현재 행
            Vector3 pos = Vector3.zero;
            pos.x = xStartPos + cellxSize * nc + cellxSize * 0.5f; //cellSize만큼 제작
            pos.y = 1f;
            pos.z = zStartPos - cellxSize * nR - cellxSize * 0.5f;
            //게임오브젝트의 위치 계산
            GameObject tmpObj = GameObject.CreatePrimitive(PrimitiveType.Cube); //나중에 몬스터 배치
            tmpObj.name = "Monster";
            tmpObj.transform.position = pos;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
