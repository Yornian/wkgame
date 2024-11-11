using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//创建一个容器类将我们的combo归类，然后在玩家里面读取我们的容器来了解我们有何种combo
[CreateAssetMenu(fileName = "Combo", menuName = "ComboSO")]
public class ComboSo : ScriptableObject, IEqualityComparer<ComboSo>
{
    [SerializeField]private List<KeyCode> keyCodes = new List<KeyCode>();
    [SerializeField]public List<float> waitTime = new List<float>();
    [SerializeField]Animator animator;
    public string Name;
    public bool isActive;
    private int index = 1;

    public List<KeyCode> GetCombo()
    {
        return keyCodes;
    }
    public List<float> GetTime()
    {
        return waitTime;
    }

    public KeyCode GetFirstKey()
    {
        return keyCodes[0];
    }
    public KeyCode GetNextKey()
    {
        // 增加 index，但不超过 keyCodes 的长度
        if (index < keyCodes.Count - 1)
        {
            index++;
        }

        return keyCodes[index];
    }

    public void ResetKeyIndex()
    {
        index = 0;
    }

    //IEqual microsoft
    //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.contains?view=net-8.0#code-try-1
    public bool Equals(ComboSo x, ComboSo y)
    {
        //Check whether the compared objects reference the same data.
        if (Object.ReferenceEquals(x, y)) return true;

        //Check whether any of the compared objects is null.
        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            return false;

        //Check whether the products' properties are equal.
        return x.Name == y.Name;
    }

    public int GetHashCode(ComboSo combo)
    {
        //Check whether the object is null
        if (Object.ReferenceEquals(combo, null)) return 0;

        //Get hash code for the Name field if it is not null.
        int hashComboName = combo.Name == null ? 0 : combo.Name.GetHashCode();

        //Calculate the hash code for the product.
        return hashComboName;
    }
}