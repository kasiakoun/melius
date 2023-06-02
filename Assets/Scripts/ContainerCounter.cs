using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private TestObjectSO testObjectSO;
    private TestObject testObject;

    public event Action PlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (player.HasTestObject()) return;

        var testObjectTransform = Instantiate(testObjectSO.prefab);
        testObjectTransform.transform.localPosition = Vector3.zero;

        testObject = testObjectTransform.GetComponent<TestObject>();
        Debug.Log(testObject.GeTestObjectSO().objectName);

        testObject.SetTestObjectParent(player);
        PlayerGrabbedObject?.Invoke();
    }
}
