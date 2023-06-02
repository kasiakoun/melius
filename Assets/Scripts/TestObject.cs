using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [SerializeField] private TestObjectSO testObjectSO;

    private ITestObjectParent testObjectParent;

    public TestObjectSO GeTestObjectSO()
    {
        return testObjectSO;
    }

    public void SetTestObjectParent(ITestObjectParent testObjectParent)
    {
        if (this.testObjectParent != null)
        {
            this.testObjectParent.ClearTestObject();
        }

        this.testObjectParent = testObjectParent;

        testObjectParent.SetTestObject(this);

        transform.parent = testObjectParent.GetHolderTransform();
        transform.localPosition = Vector3.zero;
    }

    public ITestObjectParent GetTestObjectParent()
    {
        return testObjectParent;
    }
}
