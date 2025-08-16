using Day6Web.Context;
using Day6Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;


namespace Day6Web.Controllers
{
    public class TreeController : Controller
    {
        List<MyTree> ListofTrees = new List<MyTree>();

        private AppDBContext AppDBContext { get; set; }

        public TreeController(AppDBContext _appDBContext)
        {
            ListofTrees.Add(new MyTree { Id = 1, Name = "Oak", Description = "A large tree with a thick trunk.", TreeCount = 10 });
            //ListofTrees.Add(new MyTree { Id = 2, Name = "Pine", Description = "A coniferous tree with needle-like leaves.", TreeCount = 5 });
            //ListofTrees.Add(new MyTree { Id = 3, Name = "Maple", Description = "A tree known for its vibrant fall colors.", TreeCount = 8 });
            //ListofTrees.Add(new MyTree { Id = 4, Name = "Birch", Description = "A tree with distinctive white bark.", TreeCount = 6 });
            //ListofTrees.Add(new MyTree { Id = 5, Name = "Cherry", Description = "A tree known for its beautiful blossoms.", TreeCount = 4 });
            AppDBContext = _appDBContext;
        }

        public IActionResult Index()
        {
            var allTrees = AppDBContext.myTrees.ToList();
            return View(allTrees);
        }

        [Route("Tree/Details/{name}")]
        //[Route("Tree/details/{name}")]
        public IActionResult Details(string name)
        {
            //var tree1 = ListofTrees.FirstOrDefault(x => x.Name == name);
            var tree = AppDBContext.myTrees.FirstOrDefault(x => x.Name == name);

            if (tree == null)
            {
                return NotFound();
            }

            return View(tree);
        }

        public IActionResult Form(MyTree mytree)
        {
            return View();
        }

        public IActionResult AddTreeResult(MyTree tree)
        {
            if (ModelState.IsValid)
            {
                AppDBContext.myTrees.Add(tree);
                AppDBContext.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form");
            }
        }

        public async Task<JsonResult> GetJsonData()
        {
            var jsonData = await AppDBContext.myTrees.ToListAsync();
            return Json(jsonData);
        }

        public FileResult FileDownload()
        {
            var trees = AppDBContext.myTrees.ToList();
            var json = JsonSerializer.Serialize(trees);
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);

            return File(bytes, "application/text", "tree.txt");
        }

        private List<MyTree> GetDataADO()
        {
            List<MyTree> trees = new List<MyTree>();

            var connectionString = "Data Source=PTPLL419; Initial Catalog=TestDB; Integrated Security=True; TrustServerCertificate=True;";
            const string query = "select Id, Name, Description, TreeCount from myTrees;";

            using (SqlConnection conn = new(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        trees.Add(new MyTree { Id = (int)reader[0], Name = (string)reader[1], Description = (string)reader[2], TreeCount = (int)reader[3] });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                return trees;
            }
        }
    }
}