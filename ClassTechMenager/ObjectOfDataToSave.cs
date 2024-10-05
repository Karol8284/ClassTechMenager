using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassTechMenager
{
    internal class ObjectOfDataToSave
    {
        public ObjectOfDataToSave() { }
        [JsonInclude]
        protected DateTimeOffset Time { get; set; }
        [JsonInclude]
        protected string ClassName { get; set; }
        [JsonInclude]
        protected int NumberOfLesson { get; set; }
        //[JsonInclude]
        //protected bool[] WorkingStation { get; set; }
        [JsonInclude]
        protected Dictionary<int, string> NotWorkingStation { get; set; }
        /*
        public string SerializeData(DateTimeOffset time, string className,int numberOfLesson, 
            bool[] workingStation, Dictionary<int, string> notWorkingStation) 
        */
        public string SerializeData(DateTimeOffset time, string className,int numberOfLesson,
           Dictionary<int, string> notWorkingStation)
        {
            this.Time = time;
            this.ClassName = className;
            this.NumberOfLesson = numberOfLesson;
            this.NotWorkingStation = notWorkingStation;

            string data = JsonSerializer.Serialize(this);
            //Console.WriteLine("data: "+data);
            return data;
        }
    }
}
