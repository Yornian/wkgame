using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    //首先我们创建了一个类来管理我们的combo（keycode的顺序），我们可以自行配置combo的按键和最后的出招动画
    //我们在这个数据类里面设置了几个函数使得我们能够找到对应的combo表

    //在玩家中我们创建了一个comboSO数据类的列表

    //在一系列激活的combo中我们继续判断下一个要被激活的combo 直到最后只剩一个combo的时候

    //for loop ：当玩家输入一个按键的时候从进入combo模式，从index 0开始查看激活的一系列combo 直到只剩一个combo位置 
    //其间如果输入时间过了 那就从退出combo模式 然后在一段冷却过后等待下一次输入（注意判断combo的覆盖）
public class ComboController : MonoBehaviour
{
    public List<ComboSo> comboSos = new List<ComboSo>();
    // List<ComboSo> activeCombos = new List<ComboSo>();
    List<KeyCode> initKey = new List<KeyCode>();
    float timer = 10;

    private void Awake() {
        GatherInitKey();
    }


    private void Update() {
        UpdateComboStatus();
    }

    /// <summary>
    /// 方法一获得每个combo的第一个按键，并放入一个列表之内
    /// </summary>
    private void GatherInitKey()
    {
        foreach(ComboSo comboSo in comboSos)
        {
            if(!initKey.Contains(comboSo.GetFirstKey()))
                initKey.Add(comboSo.GetFirstKey());
        }
    }
    /// <summary>
    /// 方法二 当玩家输入按键之后，如果和initkey匹配
    /// 那么就激活所有包含当前keycode的combo
    /// </summary>
    void UpdateComboStatus()
    {
        foreach(var KeyCode in initKey) 
            if (Input.GetKey(KeyCode))
            {
                foreach (ComboSo comboSo in comboSos)
                {
                    if(comboSo.GetFirstKey() == KeyCode)
                    {
                        if(!comboSo.isActive)
                        {
                            StartCoroutine(ActiveCombo(comboSo));
                        }
                        // if (!activeCombos.Contains(comboSo))
                        // {
                        //     Debug.Log(comboSo.Name);
                        //     //激活combo
                        //     if(!comboSo.isActive)
                        //     activeCombos.Add(comboSo);
                        // }
                    }
                }
            }
    }

    /// <summary>
    /// 方法三 激活当前combo
    /// 如果时间到了就改为不激活状态，如果键位输错了也改为不激活状态
    /// 时间一直在减少 并且在每次正确按键后重置
    /// </summary>
    /// <param name="comboSo"></param>
    /// 
    private IEnumerator ActiveCombo(ComboSo comboSo)
    {
        comboSo.isActive = true;
        float time = timer;
        Debug.Log("Combo activated, starting timer: " + time);

        while (time > 0)
        {
            var nextKey = comboSo.GetNextKey();
            // Update time and check for the next key press
            if (Input.GetKey(nextKey))
            {
                nextKey = comboSo.GetNextKey();
                time = timer; // Reset timer if next key in combo is pressed
            }
            
            time -= Time.deltaTime;
            Debug.Log("Time remaining: " + time);
            yield return null; // Wait for the next frame
        }
        comboSo.isActive = false;
        Debug.Log("Combo deactivated");
    }
    
    //     var time = 0.2f;
    //     while (time> 0)
    //     {
    //         if(Input.GetKeyDown(comboSo.GetNextKey()))
    //         {
    //             timer = 0.2f;
    //         }
    //         else
    //         {
    //             comboSo.isActive = false;
    //         }
    //         time -= Time.deltaTime;
}
