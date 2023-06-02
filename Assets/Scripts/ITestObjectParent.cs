using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITestObjectParent
{
    void SetTestObject(TestObject testObject);
    TestObject GetTestObject();
    void ClearTestObject();
    bool HasTestObject();
    Transform GetHolderTransform();
}
