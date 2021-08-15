def filt(str):
    arr =[]
    for char in str:
        if char=='Y':
            arr.append(1)
        elif char=='N':
            arr.append(0)
        else: 
            print('false input')
            return -1
    return arr

testnum = input()
for testcase in range(int(testnum)):
    mat = []
    firstline = input()
    mat.append(filt(firstline))
    testsize = len(firstline)  -1
    for other in range(testsize):
        cur = input()
        mat.append(filt(cur))
    print(mat)


        
