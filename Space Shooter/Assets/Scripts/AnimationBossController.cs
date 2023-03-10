using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBossController : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBoss()
    {
        Instantiate(boss, transform.position, transform.rotation);
        var father = transform.parent.gameObject;
        Destroy(father);
    }
}
