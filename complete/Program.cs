using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace complete
{
    class Program
    {
        
            static void Main(string[] args)
            {
                // mainhdhf

                LogoFrame();
                MenuFrame();
                Console.WriteLine("Your Choice: ");

                var y = Convert.ToChar(Console.ReadLine());
                start(y);


            
            }

            static void LogoFrame()
            {
                Console.WriteLine("========================================================================================================================");
                Console.WriteLine("=                                    Welcome To Shopping List Manager                                                  =");
                Console.WriteLine("=                                     You have 0 items in your list                                                    =");
                Console.WriteLine("========================================================================================================================");
            }

            static void MenuFrame()
            {
                List<string> MenuOptions = new List<string>();
                Console.WriteLine("Menu Options: ");
                Console.WriteLine();
                MenuOptions.Add("A   Add a new item");
                MenuOptions.Add("R   Remove an item");
                MenuOptions.Add("E   Edit an item");
                MenuOptions.Add("P   Mark an item as purchased");
                MenuOptions.Add("S   Sort the list");
                MenuOptions.Add("T   Toggle display of purchased items");
                MenuOptions.Add("Q   Quit the program");
                Console.WriteLine();

                foreach (string i in MenuOptions)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("***********************************************************************************************************************");
            }
            static void Priority(string value, string category)
            {
                var temp = "";
                var val = value;
                var cat = category;
                List<string> priorityList = new List<string>();
                Console.WriteLine("Name : " + val);
                Console.WriteLine("Category : " + cat);

                priorityList.Add("L: Low");
                priorityList.Add("M: Medium");
                priorityList.Add("H: High");

                foreach (string i in priorityList)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("Your Choice: ");
                var prior = Console.ReadLine();
                if (prior == "L")
                {
                    temp = "Low";
                }

                if (prior == "M")
                {
                    temp = "Medium";
                }
                if (prior == "H")
                {
                    temp = "High";
                }
                Console.WriteLine(temp);
                Console.WriteLine("Item " + value + " added to list");

                string date = DateTime.UtcNow.ToString("MM-dd-yyyy");

                bool purchased = false;

                Random random = new Random();

                int generatedId1 = random.Next(10);
                int generatedId2 = random.Next(10);
                int generatedId3 = random.Next(10);

                int id = generatedId1 + generatedId2 + generatedId3;


                using (MySqlConnection con = new MySqlConnection())
                {
                    con.ConnectionString = "Server=localhost;Database=shopping;Uid=root;Pwd=hawaibrahB1a1;";


                    Console.WriteLine("New Item Add");

                  
                    string sql = "INSERT INTO shopmanager (id, productName, category,  priority, dateAdded, purchased) VALUES (@id, @Name,@category, @priority, @dateAdded, @purchased)";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Name", value);
                    cmd.Parameters.AddWithValue("@category", category);

                    cmd.Parameters.AddWithValue("@priority", temp);
                    cmd.Parameters.AddWithValue("@dateAdded", date);
                    cmd.Parameters.AddWithValue("@purchased", purchased);


                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();



                }

            }

            static void ListFrame(string value)
            {
                var temp = "";
                List<string> Name = new List<string>();
                Console.WriteLine("Name " + value);
                Console.WriteLine();
                Name.Add("1: Food");
                Name.Add("2: Clothing");
                Name.Add("3: Furniture");
                Name.Add("4: Household");
                Name.Add("5: Jewelry");
                Name.Add("6: Utilities");
                Name.Add("7: Tech");
                Console.WriteLine();

                foreach (string i in Name)
                {
                    Console.WriteLine(i);
                }


                Console.WriteLine("***********************************************************************************************************************");
            }
            static void start(char value)
            {
                switch (value)
                {
                    case 'A':
                        add(value);

                        break;
                    case 'R':
                        remove(value);
                        break;
                    case 'E':
                        edit(value);
                        break;
                    case 'P':
                        purchased(value);
                        break;
                    case 'S':
                        sort(value);
                        break;
                    case 'T':
                        toggle(value);
                        break;
                    case 'Q':
                        quit(value);
                        break;
                    default:
                        Console.WriteLine("Please try again!");
                        break;
                }



            }

             static void quit(char value)
            {
                Console.Write("You  are  about to  quit  the  application please enter  (y/n):");

                string str = Console.ReadLine();

                if (!str.Equals("") && !string.IsNullOrEmpty(str))
                {

                    char n = Convert.ToChar(str);

                    if (n == 'y' || n.ToString().ToUpper() == "Y")
                    {
                        Environment.Exit(0);
                    }

                    if (n == 'n' || n.ToString().ToUpper() == "N")
                    {
                        Console.WriteLine("Please continue   using the  application");
                        
                    }
                }
                else
                {
                    Console.WriteLine("Error please  try again ");
                    
                }
            }

             static void toggle(char value)
            {
                Database data = new Database();

                bool condition = data.togglePurchased();

                if (condition == true)
                {
                    data.displayItems();

                }
                Console.WriteLine("Toggle  complete");

               
            }

             static void sort(char value)
            {
                Console.WriteLine("Enter Item name");
                string str = Console.ReadLine();
            }

            static void purchased(char value)
            {
                Database data = new Database();

                Console.WriteLine("Current items  in  the  database");

                data.displayItems();

                Console.Write("Select  the  item  to  mark  as  purchased by  entering  an  id:");

                string str = Console.ReadLine();


                if (!string.IsNullOrEmpty(str))
                {

                    int tempid = Convert.ToInt32(str);

                    Console.WriteLine("Mark item " + str + "from the  list (y/n)");

                    string n = Console.ReadLine();

                    if ((n.ToUpper() == "Y" || n == "y") && n.Length == 1)
                    {

                        if (data.searchById(tempid) == tempid)
                        {

                            if (data.markAsPurchased(tempid) == true)
                            {
                                Console.WriteLine("Item " + str + " has been  marked  as  purchased");

                                data.displayItems();

                            
                            }

                        }

                        if (data.searchById(tempid) != tempid)
                        {

                            Console.WriteLine("Item " + str + " has already been  marked  as  purchased");

                            
                         
                        }

                    }

                    if (n != "" && (n.ToUpper() == "N" || n == "n") && n.Length == 1)
                    {
                        Console.WriteLine("You  have decided  to  not mark  the  item");

                        
                    }
                }
                else
                {
                    Console.WriteLine("Number " + Convert.ToInt32(str) + " can  not  be  found  or  out  of  range ");

                    
                }
            }

            //complete
            static void edit(char value)
            {
                Database data = new Database();

                Console.Write("Which  item  #  do  you  want to  edit by entering an  id:");
                string str = Console.ReadLine();

                Console.Write("");

                if (data.findById(Convert.ToInt32(str)) == Convert.ToInt32(str))
                {

                    Console.WriteLine("Item Found");

                    

                    Console.WriteLine("");

                    Console.WriteLine("Leave  entries  blank  to  keep current value");

                    Console.Write("New  name:");
                    string newName = Console.ReadLine();

                    if (newName != "" && newName.Length >= 3)
                    {
                        Console.WriteLine("Editing name");
                        data.editName(Convert.ToInt32(str), newName);

                     
                    }

                    Console.WriteLine("1  Food ");
                    Console.WriteLine("2  Clothing ");
                    Console.WriteLine("3  Furniture ");
                    Console.WriteLine("4  Household ");
                    Console.WriteLine("5  Jewelry ");
                    Console.WriteLine("6  utilities ");
                    Console.WriteLine("7  Tech ");

                    Console.Write("New Category:");
                    string newCategory = Console.ReadLine();

                    string cat = "";

                    if (newCategory == "1")
                    {
                        cat = "Food";
                    }

                    if (newCategory == "2")
                    {
                        cat = "Clothing";
                    }

                    if (newCategory == "3")
                    {
                        cat = "Furniture";
                    }

                    if (newCategory == "4")
                    {
                        cat = "household";
                    }

                    if (newCategory == "5")
                    {
                        cat = "utilities";
                    }

                    if (newCategory == "6")
                    {
                        cat = "Jewerly";
                    }

                    if (newCategory == "7")
                    {
                        cat = "Tech";
                    }

                    if (newCategory != "" && cat != "")
                    {
                        Console.WriteLine("Editing Category ");
                        data.editCategory(Convert.ToInt32(str), cat);
                       

                    }

                    Console.WriteLine("L  Low");
                    Console.WriteLine("M  Medium ");
                    Console.WriteLine("H  High ");

                    Console.Write("New Priority");
                    string priority = Console.ReadLine();

                    string prior = "";

                    if (priority == "L")
                    {
                        prior = "Low";
                    }

                    if (prior == "M")
                    {
                        prior = "Medium";
                    }

                    if (prior == "H")
                    {
                        prior = "High";
                    }

                    if (priority != "" && prior != "")
                    {
                        Console.Write("Editing priority ");
                        data.editPrior(Convert.ToInt32(str), prior);

                    }
                    data.displayItems();

                }
                else
                {
                    Console.WriteLine("Item not found");
                }


            }

            static void remove(char value)
            {
                Console.WriteLine("Select Item id to Remove");
                string str = Console.ReadLine();
                RemoveData(Convert.ToInt32(str));

            }

             static void add(char value)
            {
                Console.WriteLine("Enter Item name");
                string str = Console.ReadLine();
                ListFrame(str);
                Console.WriteLine("Your Choice: ");
                var temp = Console.ReadLine();
                string choice = Condition(temp);
                Priority(str, choice);

            }
             static string Condition(string value)
            {
                var temp = "";
                if (value == "1")
                {
                    temp = "Food";
                }

                if (value == "2")
                {
                    temp = "Clothing";
                }
                if (value == "3")
                {
                    temp = "Furniture";
                }
                if (value == "4")
                {
                    temp = "Household";
                }
                if (value == "5")
                {
                    temp = "Jewelry";
                }
                if (value == "6")
                {
                    temp = "Utilities";
                }
                if (value == "7")
                {
                    temp = "Tech";

                }
                return temp;
            }

            static void RemoveData(int id)
            {
                using (MySqlConnection con = new MySqlConnection())
                {
                    Console.WriteLine("Item was removed ");

                    con.ConnectionString = "Server=localhost;Database=shopping;Uid=root;Pwd=hawaibrahB1a1;";

                    string sql = "DELETE  FROM shoppingmanger WHERE id =" + id + "";

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
    
}
