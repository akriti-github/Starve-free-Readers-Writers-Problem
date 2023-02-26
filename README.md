# Starve-Free-Reader-Writer-Problem

The Reader-Writer Problem is designed such that while multiple readers can read the shared resource simultaneously, only one writer will be allowed to modify the shared resource at any given time. The two most common approaches to solve this problem using synchromization are:

1) Writers Starve (Readers are given priority)
2) Readers Starve (Writers are given priority)

Both these solutions are not ideal and do not tackle the problem of starvation efficiently. I have attempted to suggest the pseudo code for a starva free solution where both readers and writers have equal priority.

## Explanation : 
 Since multiple readers can enter the Critical Section together, we need a variable "readCnt" to maintain count of number of readerswanted to access the shared resource. 
 From the code studied during lecture class:
 ```
Reader Process :
wait(mutex);
readcount++;
if (readcount == 1) wait(wrt);
signal(mutex);
…
reading is performed
…
wait(mutex);
readcount--;
if (readcount == 0) signal(wrt);
signal(mutex);

```
```
Writer Process :
wait(wrt);
…
writing is performed
…
signal(wrt);

```
