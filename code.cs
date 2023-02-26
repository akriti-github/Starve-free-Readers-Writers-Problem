int readCnt = 0
semaphore accessResource = 1;
semaphore acquiredResource = 1;

void Reader() {
    wait(accessResource);
    readCnt++;
    if (readCnt == 1) {
        wait(acquiredResource); //1st reader, wait for resource in case writer is using
    }
    signal(accessResource);
        /*Reading Operation takes place*/
    wait(accessResource);
    readCnt--;
    if (readCnt == 0) {
        signal(acquiredResource); // last reader, release resource for writer
    }
    signal(accessResource);
}

void Writer() {
    wait(accessResource);
    if (readCnt > 0) {
        wait(acquiredResource); // wait for readers to finish
    }
        /*Writing Operation takes place*/
    signal(acquiredResource);
    signal(accessResource);
}
