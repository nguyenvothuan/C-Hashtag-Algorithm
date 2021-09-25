using System;
using System.Collections.Generic;

class Tool<T> {
    public Dictionary<int, T> SortDictWithKey(Dictionary<int, T> dict) {
        Dictionary<int,T> newDict=new Dictionary<int, T>();
        List<int> keys = new List<int>(dict.Keys);
        keys.Sort();
        foreach (int key in keys){
            newDict.Add(key, dict[key]);
        }
        return newDict;
    }
}