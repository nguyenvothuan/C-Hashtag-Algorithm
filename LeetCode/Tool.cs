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

    public int BinarySearch(int[] arr, int key) {//sorted.
        int l=0;int r=arr.Length-1;
        while (r>l) {
            int mid = (l+r)/2;
            if (arr[mid]==key) return mid;
            if (arr[mid]<key) {
                l =mid+1;
            }
            else {
                r= mid-1;
            }
        }
        return arr[r]==key?l:-1;
    }

    public void BinaryInsertSortedList(IList<int> list, int key) {
        //list is sorted
        int l = 0;int r = list.Count-1;
        while (r>l+1){
            int mid = (l+r)/2;
            if (list[mid]==key) {
                list.Insert(mid, key);
            }
            if (list[mid]<key) {
                l =mid+1;
            }
            else {
                r= mid-1;
            }
        }
        if (list[l]>key)
            list.Insert(l, key);
        if (list[l]<key) {
            if (l+1>=list.Count)
                list.Add(key);
            else 
                list.Insert(l+1, key);
        }
    } 


}
