using DAL.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteApi.Controllers
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class RoleCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult List(int page = 1, 
            int countPage = 10, string name="",
            string description="")
        {
            int skipElement = (page - 1) * countPage;
            string json = "";
            using (EFContext context = new EFContext())
            {
                var query = context.Roles.AsQueryable();
                if(!string.IsNullOrEmpty(name))
                {
                    query = query.Where(r => r.Name.Contains(name));
                }
                if (!string.IsNullOrEmpty(description))
                {
                    query = query.Where(r => r.Description.Contains(description));
                }
                var roles = query.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                })
                .OrderBy(r=>r.Id)
                .Skip(skipElement)
                .Take(countPage)
                .ToList();
                int total = query.Count();
                json = JsonConvert.SerializeObject(
                        new
                        {
                            Roles = roles,
                            Total= total
                        });
            }
            return Content(json, "application/json");
        }
        [HttpPost]
        public ContentResult Create(RoleCreateViewModel roleModel)
        {
            Role role = new Role
            {
                Name = roleModel.Name,
                Description = roleModel.Description
            };
            string json;
            using (EFContext context = new EFContext())
            {
                context.Roles.Add(role);
                context.SaveChanges();
                json = JsonConvert.SerializeObject(
                        new
                        {
                            Id = role.Id
                        });
            }
            return Content(json, "application/json");

        }
    }
}