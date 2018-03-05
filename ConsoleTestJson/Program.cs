using DAL.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestJson
{
    class Program
    {
        static void Main(string[] args)
        {
            int action;
            Console.WriteLine("Оберіть опцію");
            Console.WriteLine("1. Додати роль");
            Console.WriteLine("2. Показати список ролей в JSON");
            action = int.Parse(Console.ReadLine());
            
            using (EFContext context = new EFContext())
            {
                if (action == 1)
                {
                    Role role = new Role();
                    Console.WriteLine("Enter role name:");
                    role.Name= Console.ReadLine();
                    context.Roles.Add(role);
                    context.SaveChanges();
                }
                else if(action==2)
                {
                    var roles = context.Roles.Select(r => new RoleViewModel
                    {
                        Id=r.Id,
                        Name=r.Name,
                        Description=r.Description
                    }).ToList();
                    string json = JsonConvert.SerializeObject(
                        new
                        {
                            Roles = roles
                        });
                    Console.WriteLine(json);
                }
            }
        }
    }
}
