int readCnt = 0
semaphore accessResource = 1;
semaphore acquiredResource = 1;

void Reader() {
    wait(accessResource);
    readCnt++;
    if (readCnt == 1) {
        // First reader, wait for writer to release semaphore
        wait(acquiredResource);
    }
    signal(accessResource);
        /*Reading Operation takes place*/
    wait(accessResource);
    readCnt--;
    if (readCnt == 0) {
        // Last reader, release semaphore for writer
        signal(acquiredResource);
    }
    signal(accessResource);
}

void Writer() {
    wait(accessResource);
    if (readCnt > 0) {
        // Wait for readers to finish
        wait(acquiredResource);
    }
        /*Writing Operation takes place*/
    signal(acquiredResource);
    signal(accessResource);
}
