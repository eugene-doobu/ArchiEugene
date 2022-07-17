using System.Collections;
using System.Collections.Generic;
using ArchiEugene.Communication;
using UnityEngine;

namespace ArchiEugene.Test
{
    public class Test_TalkContentClick : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << Define.LAYER_NPC))
                {
                    if (hit.collider.TryGetComponent(out NpcController npcController))
                        npcController.TalkTrigger();
                }
            }
        }
    }
}
