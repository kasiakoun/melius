using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, ITestObjectParent
{
    [SerializeField] private TestObjectSO testObjectSO;
    [SerializeField] private Transform topContainer;

    private TestObject testObject;

    public override void Interact(Player player)
    {
        if (player.HasTestObject())
        {
            if (!HasTestObject())
            {
                var playerTestObject = player.GetTestObject();
                SetTestObject(playerTestObject);
                playerTestObject.SetTestObjectParent(this);
            }
        }
        else
        {
            if (HasTestObject())
            {
                testObject.SetTestObjectParent(player);
            }
        }
        //if (testObject == null)
        //{
        //    var testObjectTransform = Instantiate(testObjectSO.prefab, topContainer);
        //    testObjectTransform.transform.localPosition = Vector3.zero;

        //    testObject = testObjectTransform.GetComponent<TestObject>();
        //    Debug.Log(testObject.GeTestObjectSO().objectName);
        //}
        //else
        //{
        //    testObject.SetTestObjectParent(player);
        //}
    }

    public void SetTestObject(TestObject testObject)
    {
        this.testObject = testObject;
    }

    public TestObject GetTestObject()
    {
        return testObject;
    }

    public void ClearTestObject()
    {
        testObject = null;
    }

    public bool HasTestObject()
    {
        return testObject != null;
    }

    public Transform GetHolderTransform()
    {
        return topContainer;
    }
}
