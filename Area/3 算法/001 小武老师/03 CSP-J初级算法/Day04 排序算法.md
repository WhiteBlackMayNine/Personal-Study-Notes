---
tags: []
created: ""
---

---
# 关联知识点



---
# 插入排序

将数组中的数据分为两个区间，一个是已排序区间，另一个是为排序区间
开始时，假设已排序区间中元素已被排序，每一次将未排序区间的首个数据放入排序好的区间中
直到未排序空间未空

```C++
void Insertion_Sort(int a[], int n) {  
    for (int i = 2; i <= n; i++) {  
        int key = a[i];  
        int j = i - 1;  
        while (j >= 1 && a[j] > key) {  
            a[j + 1] = a[j];  
            j--;  
        }  
        a[j + 1] = key;   
    }  
}

```

> $O(n^2)$ 稳定
# 选择排序

不断在未排序区间中选择最小的元素，将其放入已排序区间的尾部

```C++
void Selection_Sort(int a[], int n) {  
    for (int i = 1; i < n; i++) {  
        int ith = i;  
        for (int j = i + 1; j <= n; ++j) {  
            if (a[j] < a[ith]) {  
                ith = j;  
            }  
        }  
  
        int t = a[i];  
        a[i] = a[ith];  
        a[ith] = t;  
    }  
}
```

> $o(n^2)$ 不稳定
# 冒泡排序

每次只对相邻两个元素进行操作，每次冒泡操作，都会比较相邻两个元素的大小，若不满足排序要求，就将两者交换
每一次冒泡，会将一个元素移动到它相应的位置，该元素就是未排序元素中最大的元素

```C++
void Bubble_Sort(int a[], int n) {  
    for (int i = 1; i < n; ++i) {  
        for (int j = 1; j < n - i; ++j) {  
            if (a[j] > a[j + 1]) {  
                int t = a[i];  
                a[i] = a[j + 1];  
                a[j + 1] = t;  
            }  
        }  
    }  
}

//优化：如果没有发生移动元素，说明已经有序，无需继续进行排序
void Bubble_Sort(int a[], int n) {  
    for (int i = 1; i < n; ++i) {  
        bool flag = true;  
        for (int j = 1; j < n - i; ++j) {  
            if (a[j] > a[j + 1]) {  
                int t = a[i];  
                a[i] = a[j + 1];  
                a[j + 1] = t;  
                flag = false;  
            }  
        }  
        if (flag) break;  
    }  
}
```







---
