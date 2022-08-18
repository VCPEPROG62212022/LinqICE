using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinqICE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> listOutput = new List<string>();
        //Starting data , does not matter if its in a text file 
        List<Data> testData = new List<Data> ();
        public MainWindow()
        {
            InitializeComponent();
            ReadData();
            ViewList();
        }

        public void ViewList()
        {
            listOutput.Clear();
            List<Data> Sortme = testData.OrderBy(e => e.Id).ToList();
            testData = Sortme;
            foreach (Data data in Sortme)
            {
                String strOutput = "User ID: " + data.Id + " " +
                    "\n Name: " + data.Name;
                listOutput.Add(strOutput);  
            }

            lstOutput.ItemsSource = null;
            lstOutput.ItemsSource = listOutput;
        }
        public void ViewList(Data[] inputData)
        {
            listOutput.Clear(); 
            List<Data> Sortme = inputData.OrderBy(e => e.Id).ToList();
            testData = Sortme;
            foreach (Data data in Sortme)
            {
                String strOutput = "User ID: " + data.Id + " " +
                    "\n Name: " + data.Name;
                listOutput.Add(strOutput);
            }

            lstOutput.ItemsSource = null;
            lstOutput.ItemsSource = listOutput;
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int iID = Convert.ToInt32(txtSearch.Text);
                Data[] Search = testData.Where(x => x.Id == iID).ToArray();

                ViewList(Search);
            }
            catch(Exception ex)
            {
                ViewList();
            }
        }

        private void lstOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Selected = lstOutput.SelectedIndex;
            String strName =testData[Selected].Name;
            String strID = testData[Selected].Id+"";
            txtName.Text = strName;
            txtID.Text = strID;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int Selected = lstOutput.SelectedIndex;
            testData[Selected].Name= txtName.Text;
            testData[Selected].Id = Convert.ToInt32(txtID.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //subscription.Add(new Subscription { Type = "Foo", Type2 = "Bar", Period = 1 });
            testData.Add(new Data(Convert.ToInt32(txtID.Text), txtName.Text));
            txtName.Clear();
            txtID.Clear();
            UpdateData();
            ViewList();
        }


        private void ReadData()
        {
            String TextFile = "DataFile.txt";

                
                if (File.Exists(TextFile))
                {
                testData.Clear();
                    // Read file using StreamReader. Reads file line by line  
                    using (StreamReader file = new StreamReader(TextFile))
                    {
                        string ln;

                        while ((ln = file.ReadLine()) != null)
                        {
                            String strID = ln;
                            String strName = file.ReadLine();
                            testData.Add(new Data(Convert.ToInt32(strID), strName));   
                        }
                        file.Close();
                          }
                }
            else
            {
                List<Data> AddData = new List<Data> {
            new Data(9,"Josh") ,
            new Data(8,"Lee") ,
            new Data(3,"Wade") ,
            new Data(1,"Keely") ,
            new Data(2,"Gabe") ,
            new Data(30,"Rob")};

               
                using (StreamWriter sw = File.AppendText(@TextFile))
                {
                    foreach(Data data in AddData)
                    {
                        sw.WriteLine(data.Id);
                        sw.WriteLine(data.Name);
                    }
                    sw.Close();
                }

                ReadData();
            }

            }


        private void UpdateData()
        {
            String TextFile = "DataFile.txt";
            File.Delete(TextFile);
            using (StreamWriter sw = File.AppendText(@TextFile))
            {
                foreach (Data data in testData)
                {
                    sw.WriteLine(data.Id);
                    sw.WriteLine(data.Name);
                }
            }
            ReadData();
        }
        }
    }

