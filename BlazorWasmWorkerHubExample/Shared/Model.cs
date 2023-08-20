using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmWorkerHubExample.Shared
{
    public class RandmonData<T>
    {
        private Random Random = new Random();
        private int currentIndex = -1; 
        private T[] Values{ get; set; }
        public RandmonData(params T[] values)
        {
            Values = Shuffle( values);
        }
        private T[] Shuffle(T[] values)
        {
            int n = values.Length; 
            while(n >1)
            {
                n--; 
                int k = Random.Next(n + 1);
                T value = values[k];
                values[k] = values[n];
                values[n] = value;
            }

            return values; 
        }
        

        public T Get()
        {
            currentIndex = (currentIndex + 1)%Values.Length;
            return Values[currentIndex];
        }
        
    }
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; } 
        public Model() { }

        private static RandmonData<string> Names = new Shared.RandmonData<string>("Alice", "Bob", "Christina", "David", "Eva");
        private static RandmonData<string> Jobs = new Shared.RandmonData<string>("Angestellter", "Praktikant", "Boss");

        public static Model Create(int id)
        {

            return new Model()
            {
                Id = id,
                Name = Names.Get(),
                Job = Jobs.Get()
            };
        }
    }
}
