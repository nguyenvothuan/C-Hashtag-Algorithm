var validPalindrome = function (s: string): boolean {
    let l = 0,
      r = s.length - 1;
    let flag = false;
    while (l < r) {
      if (s[l] != s[r]) {
        //try moving left 1 step forward or right one step backward
        if (s[l + 1] == s[r] && !flag) {
          l++;
          flag = true;
        } else if (s[l] == s[r - 1] && !flag) {
          r--;
          flag = true;
        } else return false;
      }
      l++; r--;
    }
    
    return true;
}
function largestPerimeter(nums: number[]): number {
  return 1;
};

function pivotIndex(nums: number[]): number {
  let left =0;
  const sum =nums.reduce((a,b)=>a+b, 0);
  for (let i =0; i<nums.length;i++) {
    if (sum - left -nums[i] == left) return i;
    left +=nums[i];
  }
  return -1;
};

function testPivotIndex(): void {
  console.log(pivotIndex([1,7,3,6,5,6]));
}
testPivotIndex();