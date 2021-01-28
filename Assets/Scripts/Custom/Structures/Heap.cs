using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities.Structures
{
    public class Heap<T> where T : IHeapItem<T>
    {
        T[] items;
        int currentItemCount;


        //Formulas
        //parentIndex=(n-1)/2
        //leftChildIndex=n*2+1
        //rightChildIndex=n*2+2

        public Heap(int capacity)
        {
            items = new T[capacity];
        }


        public void Add(T item)
        {
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item);
            currentItemCount++;
        }

        public T Pop()
        {
            T firstItem = items[0];
            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);
            return firstItem;
        }

        public void UpdateItem(T item)
        {
            SortUp(item);
            SortDown(item);
        }


        private void SortUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;

            while (true)
            {
                T parentItem = items[parentIndex];
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else break;

                parentIndex = (item.HeapIndex - 1) / 2;
            }

        }

        private void SortDown(T item)
        {
            while (true)
            {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;
                int swapIndex = 0;

                //Check if it has children
                if (childIndexLeft < currentItemCount)
                {
                    swapIndex = childIndexLeft;
                    if (childIndexRight < currentItemCount)
                    {
                        if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                            swapIndex = childIndexRight;
                    }

                    if (item.CompareTo(items[swapIndex]) < 0) { Swap(item, items[swapIndex]); }
                    else { return; }
                }
                else { return; }

            }
        }




        private void Swap(T a, T b)
        {
            items[a.HeapIndex] = b;
            items[b.HeapIndex] = a;
            int tmp = a.HeapIndex;
            a.HeapIndex = b.HeapIndex;
            b.HeapIndex = tmp;
        }



        public bool Contains(T item)
        {
            return Equals(items[item.HeapIndex], item);
        }


        public int Capacity { get { return items.Length; } }
        public int Count { get { return currentItemCount; } }
    }


    //Compare To higher 1 , same 0, lower -1 
    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}

