using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public int poolCount; // 오브젝트를 생성 할 개수
    public string poolName; // 풀링할 오브젝트의 이름

    public int poolLength => pool.Count;

    public GameObject poolObject;
    public Transform parentObject;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public void Enqueue(GameObject _object) => pool.Enqueue(_object);
    public GameObject Dequeue() => pool.Dequeue();
}


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;

    public Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

    public List<Pool> poolList = new List<Pool>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        _Init();
    }

    private void _Init()
    {
        foreach (Pool pool in poolList)
            poolDictionary.Add(pool.poolName, pool);

        foreach (Pool pool in poolDictionary.Values)
        {
            GameObject parent = new GameObject();

            pool.parentObject = parent.transform;

            parent.transform.SetParent(transform);
            parent.name = pool.poolName;

            for (int i = 0; i < pool.poolCount; i++)
            {
                GameObject currentObject = Instantiate(pool.poolObject, parent.transform);
                currentObject.SetActive(false);

                pool.Enqueue(currentObject);
            }
        }
    }

    /// <summary>
    /// 풀에서 가져와 스폰
    /// </summary>
    /// <param name="name">풀링할 오브젝트 이름</param>
    /// <param name="position">위치</param>
    /// <returns></returns>
    public GameObject _SpawnFromPool(string name, Vector3 position)
    {
        Pool currentPool = poolDictionary[name];

        if (currentPool.poolLength <= 0)
        {
            GameObject obj = Instantiate(currentPool.poolObject, currentPool.parentObject);
            obj.SetActive(false);
            currentPool.Enqueue(obj);
        }

        GameObject currentObject = currentPool.Dequeue();
        currentObject.transform.position = position;

        currentObject.SetActive(true);

        return currentObject;
    }

    /// <summary>
    /// 풀에서 가져와 스폰
    /// </summary>
    /// <param name="name">풀링할 오브젝트 이름</param>
    /// <param name="position">위치</param>
    /// <param name="rotate">방향</param>
    /// <returns></returns>
    public GameObject _SpawnFromPool(string name, Vector3 position, Quaternion rotate)
    {
        Pool currentPool = poolDictionary[name];

        if (currentPool.poolLength <= 0)
        {
            GameObject obj = Instantiate(currentPool.poolObject, currentPool.parentObject);
            obj.SetActive(false);
            currentPool.Enqueue(obj);
        }

        GameObject currentObject = currentPool.Dequeue();
        currentObject.transform.position = position;
        currentObject.transform.rotation = rotate;

        currentObject.SetActive(true);

        return currentObject;
    }

    /// <summary>
    /// 오브젝트 풀로 리턴
    /// </summary>
    /// <param name="name">리턴할 이름</param>
    /// <param name="currentObject">리턴할 오브젝트</param>
    public void _ReturnToPool(string name, GameObject currentObject)
    {
        Pool pool = poolDictionary[name];

        currentObject.SetActive(false);
        currentObject.transform.SetParent(pool.parentObject);

        pool.Enqueue(currentObject);
    }
}