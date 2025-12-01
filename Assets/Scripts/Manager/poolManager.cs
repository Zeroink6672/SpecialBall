using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class poolManager : singletonMonoBehaviour<poolManager>
{
    #region Tooltip
    [Tooltip("在此数组中填充你希望加入对象池的预制件，并指定每个预制件需创建的游戏对象数量。")]
    #endregion
    [SerializeField] private Pool[] poolArray = null;
    private Transform objectPoolTransform;
    private Dictionary<int, Queue<Component>> poolDictionary = new Dictionary<int, Queue<Component>>();

    [System.Serializable]
    public struct Pool
    {
        public int poolSize;
        public GameObject prefab;
        public string componentType;
    }

    private void Start()
    {
        //这个单例游戏对象将作为对象池的父容器
        objectPoolTransform = this.gameObject.transform;
        //在开始时创建对象池
        for (int i = 0; i < poolArray.Length; i++)
        {
            CreatePool(poolArray[i].prefab, poolArray[i].poolSize, poolArray[i].componentType); ;
        }
    }

    //使用指定的预制件和每个预制件的池大小创建对象
    private void CreatePool(GameObject prefab, int poolSize, string componentType)
    {
        int poolKey = prefab.GetInstanceID();

        string prefabName = prefab.name;//得到预制件的变量名

        GameObject parentGameObject = new GameObject(prefabName + "Anchor");//创建父游戏对象以将子对象作为父对象

        parentGameObject.transform.SetParent(objectPoolTransform);

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<Component>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab, parentGameObject.transform) as GameObject;
                newObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObject.GetComponent(Type.GetType(componentType)));
            }
        }
    }

    //重用池中的游戏对象组件。“prefab”是包含组件的预制游戏对象。“position”是游戏对象在启用时应出现的世界位置。如果游戏对象需要旋转，则应设置“rotation”。
    public Component ReuseComponent(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey))
        {
            //从队列中获取对象
            Component componentToReuse = GetComponentFromPool(poolKey);
            ResetObject(position, rotation, componentToReuse, prefab);
            return componentToReuse;
        }
        else
        {
            Debug.Log("No object pool for " + prefab);
            return null;
        }
    }

    //用“poolKey”从池中获取一个游戏对象预制件
    private Component GetComponentFromPool(int poolKey)
    { 
        Component componentToReuse=poolDictionary[poolKey].Dequeue();
        poolDictionary[poolKey].Enqueue(componentToReuse);

        if(componentToReuse.gameObject.activeSelf==true)
        {
            componentToReuse.gameObject.SetActive(false);
        }

        return componentToReuse;
    }
    
    //重新设置游戏对象
    private void ResetObject(Vector3 position,Quaternion rotation,Component componentToReuse,GameObject prefab)
    {
        componentToReuse.transform.position = position;
        componentToReuse.transform.rotation = rotation;
        componentToReuse.gameObject.transform.localPosition = prefab.transform.localPosition;
    }


    //验证区域，用于检查数组中是否有数值。如果为空，控制台上获得提示
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(poolArray), poolArray);
    }
#endif
    #endregion
}