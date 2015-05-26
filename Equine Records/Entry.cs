using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equine_Records
{
    [Table("Entries")]
    public class Entry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // canvas1
    
        public string RiderName { get; set; }
    
        public string Venue { get; set; }
     
        public string Event { get; set; }
        [NotNull]
        public string Date { get; set; }

        // canvas2
    
        public string Horse { get; set; }
    
        public string HorseHeight { get; set; }
   
        public string HorseAge { get; set; }
    
        public string HorseBreed { get; set; }
  
        public string HorseSex { get; set; }


        // canvas3
   
        public double EntryCost { get; set; }
    
        public int Round { get; set; }
    
        public string FinishTime { get; set; }
  
        public string Position { get; set; }


        // canvas3
 
        public double FenceHeight { get; set; }
  
        public string Clears { get; set; }
  
        public int Faults { get; set; }// set default 0
   
        public int Points { get; set; }
   
        public String Comments { get; set; }
   
        public byte[] Image { get; set; }
       

    }
          
}


