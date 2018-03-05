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
        public ContentResult List()
        {
            string json = "";
            using (EFContext context = new EFContext())
            {
                var roles = context.Roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                }).ToList();
                json = JsonConvert.SerializeObject(
                        new
                        {
                            Roles = roles
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