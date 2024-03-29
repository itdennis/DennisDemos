﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.Concurrency
{
    class CityInfo : IEqualityComparer<CityInfo>
    {
        public string Name { get; set; }
        public DateTime LastQueryDate { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int[] RecentHighTemperatures { get; set; }

        public CityInfo(string name, decimal longitude, decimal latitude, int[] temps)
        {
            Name = name;
            LastQueryDate = DateTime.Now;
            Longitude = longitude;
            Latitude = latitude;
            RecentHighTemperatures = temps;
        }

        public CityInfo()
        {
        }

        public CityInfo(string key)
        {
            Name = key;
            // MaxValue means "not initialized".
            Longitude = Decimal.MaxValue;
            Latitude = Decimal.MaxValue;
            LastQueryDate = DateTime.Now;
            RecentHighTemperatures = new int[] { 0 };
        }

        public bool Equals(CityInfo x, CityInfo y)
        {
            return x.Name == y.Name && x.Longitude == y.Longitude && x.Latitude == y.Latitude;
        }

        public int GetHashCode(CityInfo obj)
        {
            CityInfo ci = (CityInfo)obj;
            return ci.Name.GetHashCode();
        }
    }
    public class ConcurrentDictionaryDemo
    {
        private void RunAsync()
        {
            string param = "Hi";
            Task.Run(() => MethodWithParameter(param));
        }

        private void MethodWithParameter(string param)
        {
            //Do stuff
        }
        static ConcurrentDictionary<string, CityInfo> cities = new ConcurrentDictionary<string, CityInfo>();

        public static void Run() 
        {
            CityInfo[] data =
            {
                new CityInfo(){ Name = "Boston", Latitude = 42.358769M, Longitude = -71.057806M, RecentHighTemperatures = new int[] {56, 51, 52, 58, 65, 56,53}},
                new CityInfo(){ Name = "Miami", Latitude = 25.780833M, Longitude = -80.195556M, RecentHighTemperatures = new int[] {86,87,88,87,85,85,86}},
                new CityInfo(){ Name = "Los Angeles", Latitude = 34.05M, Longitude = -118.25M, RecentHighTemperatures =   new int[] {67,68,69,73,79,78,78}},
                new CityInfo(){ Name = "Seattle", Latitude = 47.609722M, Longitude =  -122.333056M, RecentHighTemperatures =   new int[] {49,50,53,47,52,52,51}},
                new CityInfo(){ Name = "Toronto", Latitude = 43.716589M, Longitude = -79.340686M, RecentHighTemperatures =   new int[] {53,57, 51,52,56,55,50}},
                new CityInfo(){ Name = "Mexico City", Latitude = 19.432736M, Longitude = -99.133253M, RecentHighTemperatures =   new int[] {72,68,73,77,76,74,73}},
                new CityInfo(){ Name = "Rio de Janiero", Latitude = -22.908333M, Longitude = -43.196389M, RecentHighTemperatures =   new int[] {72,68,73,82,84,78,84}},
                new CityInfo(){ Name = "Quito", Latitude = -0.25M, Longitude = -78.583333M, RecentHighTemperatures =   new int[] {71,69,70,66,65,64,61}}
            };

            // Add some key/value pairs from multiple threads.
            var tasks = new Task[2];
            Action<int> action = (a) => { Console.WriteLine(a); };
            tasks[0] = Task.Run(() =>
            {
                for (int i = 0; i < 2; i++)
                {
                    if (cities.TryAdd(data[i].Name, data[i]))
                        Console.WriteLine($"Added {data[i].Name} on thread {Thread.CurrentThread.ManagedThreadId}");
                    else
                        Console.WriteLine($"Could not add {data[i]}");
                }
            });

            tasks[1] = Task.Run(() =>
            {
                for (int i = 2; i < data.Length; i++)
                {
                    if (cities.TryAdd(data[i].Name, data[i]))
                        Console.WriteLine($"Added {data[i].Name} on thread {Thread.CurrentThread.ManagedThreadId}");
                    else
                        Console.WriteLine($"Could not add {data[i]}");
                }
            });

            // Output results so far.
            Task.WaitAll(tasks);

            // Enumerate collection from the app main thread.
            // Note that ConcurrentDictionary is the one concurrent collection
            // that does not support thread-safe enumeration.
            foreach (var city in cities)
            {
                Console.WriteLine($"{city.Key} has been added.");
            }

            AddOrUpdateWithoutRetrieving();
            RetrieveValueOrAdd();
            RetrieveAndUpdateOrAdd();

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        private static void AddOrUpdateWithoutRetrieving()
        {
            // Sometime later. We receive new data from some source.
            var ci = new CityInfo()
            {
                Name = "Toronto",
                Latitude = 43.716589M,
                Longitude = -79.340686M,
                RecentHighTemperatures = new int[] { 54, 59, 67, 82, 87, 55, -14 }
            };

            // Try to add data. If it doesn't exist, the object ci is added. If it does
            // already exist, update existingVal according to the custom logic in the 
            // delegate.
            cities.AddOrUpdate(ci.Name, ci,
                (key, existingVal) =>
                {
                    // If this delegate is invoked, then the key already exists.
                    // Here we make sure the city really is the same city we already have.
                    // (Support for multiple cities of the same name is left as an exercise for the reader.)
                    if (!ci.Equals(ci, existingVal))
                        throw new ArgumentException($"Duplicate city names are not allowed: {ci.Name}.");

                    // The only updatable fields are the temperature array and LastQueryDate.
                    existingVal.LastQueryDate = DateTime.Now;
                    existingVal.RecentHighTemperatures = ci.RecentHighTemperatures;
                    return existingVal;
                });

            // Verify that the dictionary contains the new or updated data.
            Console.Write($"Most recent high temperatures for {cities[ci.Name].Name} are: ");
            int[] temps = cities[ci.Name].RecentHighTemperatures;
            foreach (var temp in temps) Console.Write($"{temp}, ");
            Console.WriteLine();
        }

        // This method shows how to use data and ensure that it has been
        // added to the dictionary.
        private static void RetrieveValueOrAdd()
        {
            string searchKey = "Caracas";
            CityInfo retrievedValue = null;

            try
            {
                retrievedValue = cities.GetOrAdd(searchKey, GetDataForCity(searchKey));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            // Use the data.
            if (retrievedValue != null)
            {
                Console.Write($"Most recent high temperatures for {retrievedValue.Name} are: ");
                int[] temps = cities[retrievedValue.Name].RecentHighTemperatures;
                foreach (var temp in temps) Console.Write($"{temp}, ");
            }
            Console.WriteLine();
        }

        // This method shows how to retrieve a value from the dictionary,
        // when you expect that the key/value pair already exists,
        // and then possibly update the dictionary with a new value for the key.
        private static void RetrieveAndUpdateOrAdd()
        {
            CityInfo retrievedValue;
            string searchKey = "Buenos Aires";

            if (cities.TryGetValue(searchKey, out retrievedValue))
            {
                // Use the data.
                Console.Write($"Most recent high temperatures for {retrievedValue.Name} are: ");
                int[] temps = retrievedValue.RecentHighTemperatures;
                foreach (var temp in temps) Console.Write($"{temp}, ");

                // Make a copy of the data. Our object will update its LastQueryDate automatically.
                var newValue = new CityInfo(retrievedValue.Name,
                                                retrievedValue.Longitude,
                                                retrievedValue.Latitude,
                                                retrievedValue.RecentHighTemperatures);

                // Replace the old value with the new value.
                if (!cities.TryUpdate(searchKey, newValue, retrievedValue))
                {
                    // The data was not updated. Log error, throw exception, etc.
                    Console.WriteLine($"Could not update {retrievedValue.Name}");
                }
            }
            else
            {
                // Add the new key and value. Here we call a method to retrieve
                // the data. Another option is to add a default value here and 
                // update with real data later on some other thread.
                CityInfo newValue = GetDataForCity(searchKey);
                if (cities.TryAdd(searchKey, newValue))
                {
                    // Use the data.
                    Console.Write($"Most recent high temperatures for {newValue.Name} are: ");
                    int[] temps = newValue.RecentHighTemperatures;
                    foreach (var temp in temps) Console.Write($"{temp}, ");
                }
                else
                    Console.WriteLine($"Unable to add data for {searchKey}");
            }
        }

        //Assume this method knows how to find long/lat/temp info for any specified city.
        static CityInfo GetDataForCity(string name)
        {
            // Real implementation left as exercise for the reader.
            if (String.CompareOrdinal(name, "Caracas") == 0)
                return new CityInfo()
                {
                    Name = "Caracas",
                    Longitude = 10.5M,
                    Latitude = -66.916667M,
                    RecentHighTemperatures = new int[] { 91, 89, 91, 91, 87, 90, 91 }
                };
            else if (String.CompareOrdinal(name, "Buenos Aires") == 0)
                return new CityInfo()
                {
                    Name = "Buenos Aires",
                    Longitude = -34.61M,
                    Latitude = -58.369997M,
                    RecentHighTemperatures = new int[] { 80, 86, 89, 91, 84, 86, 88 }
                };
            else
                throw new ArgumentException($"Cannot find any data for {name}");
        }
    }
}
