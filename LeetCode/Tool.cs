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
            if (arr[mid]>key) {
                l =mid+1;
            }
            else {
                r= mid-1;
            }
        }
        return arr[r]==key?l:-1;
    }

    public int evenSubarray(List<int> numbers, int k)
    {
        if (k==0) return 0;
        bool[] IsOdd = new bool[numbers.Count];
        foreach (int i in numbers) {
            IsOdd[i] = numbers[i]%2!=0;
        }
        int count=0;
        for (int i =0;i<numbers.Count;i++){
            int odd =0;
            for (int j=i;j<numbers.Count;j++){
                if (IsOdd[j]){
                    odd++;
                    if (odd>k){
                        break;
                    }
                }
                count++;
            }
        }
        return count;
    } 
}
