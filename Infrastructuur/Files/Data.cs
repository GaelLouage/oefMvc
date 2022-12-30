using Infrastructuur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Files
{
    public class Data
    {
     
        public IEnumerable<StudentEntity> GetStudentsFromFile()
        {
            var id = 1;
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "Studenten.txt");
            if (!File.Exists(fileName))
            {
                throw new Exception("File not found");
            }
            var students = File.ReadAllLines(fileName);
            for (int i = 0; i < students.Length; i++)
            {
                string[] splitted = students[i].Split(',');
                yield return new StudentEntity() { Id= id, FirstName = splitted[0], LastName = splitted[1], Points = double.Parse(splitted[2]) };
                id++;
            }
        }
    }
}
