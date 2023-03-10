# Starve-Free-Reader-Writer-Problem
By: Akriti Jain (20115008)

The Reader-Writer Problem is designed such that while multiple readers can read the shared resource simultaneously, only one writer will be allowed to modify the shared resource at any given time. The two most common approaches to solve this problem using synchromization are:

1) Writers Starve (Readers are given priority)
2) Readers Starve (Writers are given priority)

Both these solutions are not ideal and do not tackle the problem of starvation efficiently. I have attempted to suggest the pseudo code for a starve free solution where both readers and writers have equal priority.

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
Here, we observe that writers starve, because till the time readers have exclusive access to the shared resource, the writers will have to wait indefinitely. Technically, once all the existing readers have used the resource, they should allow the waiting writer (if any) to acquire exclusive access of the critical section.

The proposed solution makes use of 2 semaphores - "acquiredResource" and "accessResource".
Here, "accessResource" performs the same task as "mutex" from the above code. The other variable "acquiredResource" makes sure that mutual exclusion is present between both readers and writers. If a writer wants to access the resource, it will call wait(acquiredResource) preventing other writers from acquiring it simultaneously. Then, it acquires the "accessResource" semaphore to enter the CS. If a reader is already accessing the resource then, the writer process will be blocked until all the active readers exit the CS. This protects writers from starvation and ensure no writer process is waiting indefinitely because readers have priority access. Similarly, if a reader wants to access the resource, it will also call wait(acquiredResource) to ensure exclusive access for itself assuming no writer process is already accessing it in which case the reader process will get blocked till the writer release the resource. Then, it acquires the "accessResource" semaphore to enter the CS. This is how we also ensure that readers don't starve and get fair access to the shared resource.

Considering a request order given below: 
…. R1, R2, R3, W1, R4, R5 …..

If we implement this using the code covered in lecture class, we will observe that since the value of "wrt" will become 1 only when number of readers is 0, the process W1 will starve indefinitely or atleast until reader processes keep coming. 
```
    wait(accessResource);
    if (readCnt > 0) {
        // Wait for readers to finish
        wait(acquiredResource);
    }
        /*Writing Operation takes place*/
 ```
Whereas, if we use my suggested code then we will observe that as soon as W1 executes this it will enter blocked state first as R1, R2, R3 are in CS, but as soon as they exit the CS, "accessResource" becomes 1 again and the blocked process wakes up (W1 in this case) and executes perfectly.

