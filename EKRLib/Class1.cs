using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    [DataContract]
    public class Item : IComparable<Item>
    {
        double weight;

        public Item(double weight)
        {
            Weight = weight;
        }
        [DataMember]
        public double Weight 
        {
            get => weight;
            set 
            {
                if (weight < 0)
                    throw new ArgumentException();
                weight = value;
            }
        }
        public override string ToString()
        {
            return $"Weight: {Weight:f3}";
        }
        public static explicit operator double(Item item) 
        {
            return item.Weight;
        }
        public int CompareTo(Item obj) 
        {
            if (this.Weight > obj.Weight)
                return 1;
            else if (this.Weight < obj.Weight)
                return -1;
            return 0;
        }
    }
    [DataContract, KnownType(typeof(Item))]
    public class Box : Item 
    {
        double a;
        double b;
        double c;

        public Box(double a, double b, double c, double weight):base(weight)
        {
            A = a;
            B = b;
            C = c;
        }
        [DataMember]
        public double A 
        {
            get => a;
            set 
            {
                if (value < 0)
                    throw new ArgumentException();
                a = value;
            }
        }
        [DataMember]
        public double B
        {
            get => b;
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                b = value;
            }
        }
        [DataMember]
        public double C
        {
            get => c;
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                c = value;
            }
        }
        public double GetLongestSideSize() 
        {
            return a > b ? (a > c ? a : c) : b > c ? b : c;
        }
        public override string ToString()
        {
            return base.ToString() + $" A: {A:f3}; B: {B:f3}; C: {C:f3}";
        }
    }
    [DataContract, KnownType(typeof(Item)), KnownType(typeof(Box))]
    public class Collection<T> : IEnumerable<T> where T:Item 
    {
        [DataMember]
        List<T> items = new List<T>();
        public void Add(T item) 
        {
            items.Add(item);
        }
        public IEnumerator<T> GetEnumerator() 
        {
            return new CollectionEnumerator<T>(items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }
    }
    public class CollectionEnumerator<T> : IEnumerator<T> where T: Item
    {
        List<T> items = new List<T>();
        int index = -1;

        public CollectionEnumerator(List<T> items)
        {
            this.items = items;
        }

        public T Current 
        {
            get 
            {
                if(items[index].Weight != 0)
                    return items[index];
                return null;
            }
        }

        object IEnumerator.Current => null;

        public void Dispose()
        { }
                  

        public bool MoveNext()
        {
            if (index < items.Count - 1) 
            {
                index += 1;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            index = -1;
        }
    }



    
}
