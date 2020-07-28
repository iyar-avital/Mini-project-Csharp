using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PLWPF
{
    public static class GroupToList 
    {
        // All  Grouping that run the function are of reference type, so we can do the terms : new()
        public static List<TElement> groupToList<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> elements) 
        {
            List<TElement> listOfelements = new List<TElement>();
            foreach (var item in elements) //item is a Group
            {
                foreach (var elem in item) //elem is a TElement
                {
                    listOfelements.Add(elem);
                }
            }
            return listOfelements;
        }

        public static List<TKey> AllKeysInGroup<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> elements)
        {
            List<TKey> listOfkeys = new List<TKey>();
            foreach (var item in elements) //item is a Group
            {
                listOfkeys.Add(item.Key);
            }
            return listOfkeys;
        }

        public static List<TElement> AllElementsForKey<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> elements, TKey myKey) where TKey: struct //to the equals
        {
            List<TElement> listElementBykey = new List<TElement>();
            foreach (var item in elements) //item is a Group
            {
                if (item.Key.Equals(myKey))
                {
                    foreach (var item1 in item)
                    {
                        listElementBykey.Add(item1);
                    }
                    return listElementBykey;
                }
            }
            return null;
        }

        //public static List<TElement> OneObjectFromAllKey<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> elements)
        //{
        //    List<TElement> listOneElementBykey = new List<TElement>();
        //    foreach (var item in elements) //item is a Group
        //    {
        //        listOneElementBykey.Add(item.ElementAt(0));
        //    }
        //    return listOneElementBykey;
        //}

        //public static List<TElement> AllElementsNonKey<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> elements, TKey myKey) where TKey : struct //to the equals
        //{
        //    List<TElement> listElementBykey = new List<TElement>();
        //    foreach (var item in elements) //item is a Group
        //    {
        //        if (!item.Key.Equals(myKey))
        //        {
        //            foreach (var item1 in item)
        //            {
        //                listElementBykey.Add(item1);
        //            }
        //        }
        //    }
        //    return listElementBykey;
        //}
    }
}
