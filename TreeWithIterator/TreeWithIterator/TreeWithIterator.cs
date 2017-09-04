using System;
using System.Collections;
using System.Collections.Generic;

public class TreeWithIterator<T> : IEnumerable<T>
{
    private TreeElement doroot;

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    private class TreeElement
    {
        private TreeElement leftChild;

        private TreeElement rightChild;

        private T value;
    }
}