using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RediskaUNT_KroShu
{
    public partial class MainPage : ContentPage
    {
        
        private int _sortTypeByName;
        bool Iosorting = true;
        public ObservableCollection<Equipment> AllItems { get; set; } = new ObservableCollection<Equipment>();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
      
        public class Equipment
        {
            public string Ident { get; set; }
            public string Nameof { get; set; }
            public string Department { get; set; }
            public bool IsPresent { get; set; }
        }

        
       
        private async void SnNameButton_Clicked(object sender, EventArgs e)
        {
            var OrderedList = new List<Equipment>();
            switch (_sortTypeByName)
            {
                case 0:
                    OrderedList = (AllItems.OrderBy(x => x.Nameof)).ToList<Equipment>();
                    break;
                case 1:
                    OrderedList=(AllItems.OrderByDescending(x => x.Nameof)).ToList<Equipment>();
                    break;
                case 2:
                    OrderedList=(AllItems.OrderBy(x => x.Ident)).ToList<Equipment>();
                    break;
                case 3:
                    OrderedList=(AllItems.OrderByDescending(x => x.Ident)).ToList<Equipment>();
                    break;
            }
            _sortTypeByName++;
            if (_sortTypeByName == 4) { _sortTypeByName = 0; }

            AllItems.Clear();
            OrderedList.ForEach(x => AllItems.Add(x));

        }

      
        private void IoBtn_Clicked(object sender, EventArgs e)
        {

        }

        private async void loadbtn_Clicked(object sender, EventArgs e)
        {
            {
                try
                {
                    
                    var result = await FilePicker.PickAsync();
                    var enumLines = File.ReadLines(result.FullPath, Encoding.UTF8);
                    foreach (var line in enumLines)

                    {
                        string[] parts = line.Split(';');
                        Equipment equipment = new Equipment();
                        equipment.Ident = parts[0];
                        equipment.Nameof = parts[1];
                        equipment.Department = parts[2];
                        equipment.IsPresent = bool.Parse(parts[3]);

                       

                        AllItems.Add(equipment);
                       
                    }
                   
                    var AskForAuditorName = DisplayPromptAsync("Так, так, так. Хто тут у нас?", "Введіть свій логін ");
                    
                    AskForAuditorName.Start();
                    string AuditorName = AskForAuditorName.Result;
                    
                    
                }

                catch (Exception)
                {

                }
                

            }
        }

        private void eeditor_Completed(object sender, EventArgs e)
        {
            try
            {
                AllItems.Reverse();
                var indx = AllItems.First(i => i.Ident == eeditor.Text);
                int indexx = AllItems.IndexOf(indx);
                string Namee = AllItems[indexx].Nameof;
                string Identt = AllItems[indexx].Ident;
                string Department = AllItems[indexx].Department;
                AllItems.RemoveAt(indexx);
                Equipment eq = new Equipment();
                eq.IsPresent = true;
                eq.Nameof = Namee;
                eq.Department = Department;
                eq.Ident = Identt;
                AllItems.Add(eq);
                AllItems.Reverse();




            }
            catch
            {
                if (eeditor.Text != "") ;
                {
                    DisplayAlert("Cталася помилка", "Чого? Чому? Навіщо? Відповідей на це питання ми не дізнаємось ніколи, але такого серійника я в списку не знайшов", "Ну і не особливо хотілося");
                }
            }
            eeditor.Text = "";
        }
    }
}

