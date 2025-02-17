using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    #region Singleton
    public static PoolingSystem instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion


    public Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

    static List<GameObject> prefabs = new List<GameObject>();
    static List<Transform> parents = new List<Transform>();

    List<Vector3> defScales = new List<Vector3>();

    static GameObject emptyObject;

    #region Create Pool
    public void CreatePool(GameObject prefab)
    {

        CreatePool(prefab, prefab.name, new GameObject(prefab.name).transform);

        //prefabs.Add(prefab);
        //pools.Add(prefab.name, new List<GameObject>());
        //parents.Add(new GameObject(prefab.name).transform);
    }
    public void CreatePool(GameObject prefab,string _name)
    {
        CreatePool(prefab, _name, new GameObject(prefab.name).transform);
        //prefabs.Add(prefab);
        //pools.Add(_name, new List<GameObject>());
        //parents.Add(new GameObject(prefab.name).transform);
    }

    public void CreatePool(GameObject prefab,Transform parent)
    {
        CreatePool(prefab, prefab.name, parent);
        //prefabs.Add(prefab);
        //pools.Add(prefab.name, new List<GameObject>());
        //parents.Add(parent);
    }    
    public void CreatePool(GameObject prefab,string _name,Transform parent)
    {
        if(pools.ContainsKey(_name))
        {
            Debug.LogWarning("Pool " + _name + " already exists!");
            return;
        }
        if(parent == null)
        {
            parent = new GameObject(prefab.name).transform;
        }
        prefabs.Add(prefab);
        pools.Add(_name, new List<GameObject>());
        parents.Add(parent);
        defScales.Add(prefab.transform.localScale);
    }

    #endregion

    public int FindIndex(string _name)
    {
        List<string> poolKeys = pools.Keys.ToList();
        return poolKeys.FindIndex(0, poolKeys.Count, x => x == _name);
    }


    public GameObject GetObjectFromPool(string _name)
    {

        if (!pools.ContainsKey(_name))
        {
            if(!emptyObject)
            {
                emptyObject = new GameObject("Pool Error");
            }
            Debug.LogError(_name + " Pool not found!");
            return emptyObject;
        }


        List<GameObject> pool = pools[_name];
        for (int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        int index = FindIndex(_name);

        GameObject prefab = prefabs[index];
        Transform parent = parents[index];
        GameObject temp = Instantiate(prefab, parent);
        pool.Add(temp);
        pools[_name] = pool;
        return temp;
    }

    public GameObject GetObjectFromPool(GameObject _prefab)
    {
        string _name = _prefab.name;
        if (!pools.ContainsKey(_prefab.name))
        {
            CreatePool(_prefab);
        }
        return GetObjectFromPool(_name);
    }

    public void ResetParent(GameObject _object, string _name)
    {
        int index = pools.Keys.ToList().IndexOf(_name);
        _object.transform.parent = parents[index];
    }

    public void ResetScale(GameObject _object,string _name)
    {
        int index = pools.Keys.ToList().IndexOf(_name);
        _object.transform.localScale = defScales[index];
    }

    public int GetPoolObjectCount(string _name)
    {
        return pools[_name].Count;
    }
    public int GetPoolObjectCount(GameObject prefab)
    {
        return GetPoolObjectCount(prefab.name);
    }



}
