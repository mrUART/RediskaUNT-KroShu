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

        List<Equipment> list = new List<Equipment>();
        
        int sortTypeByName;
        bool iosorting = true;


        public ObservableCollection<Equipment> allItems { get; set; }
        public MainPage()
        {
            InitializeComponent();

        }
        public void shitdontwork(List<Equipment> equipments)
        {
            allItems = new ObservableCollection<Equipment>(equipments);
            listvw.ItemsSource = allItems;
        }
        public class Equipment
        {
            public string ident { get; set; }
            public string Nameof { get; set; }
            public string department { get; set; }
            public bool ispresent { get; set; }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                var result = await FilePicker.PickAsync();

              
                var enumLines = File.ReadLines(result.FullPath, Encoding.UTF8);
              

                    foreach (var line in enumLines)
                {
                    string [] parts = line.Split(';');
                    Equipment equipment = new Equipment();
                    equipment.ident = parts[0];
                    equipment.Nameof = parts[1];
                    equipment.department = parts[2];
                    equipment.ispresent = bool.Parse(parts[3]);
                    
                    list.Add(equipment);

                }
                shitdontwork(list);

            }
            catch (Exception)
            {
                loadbtn.Text = "Помилка завантаження";
            }
            
        }

        private void snNameButton_Clicked(object sender, EventArgs e)
        {
            if (sortTypeByName == 0)
            {
                list = new List<Equipment>(allItems.OrderBy(x => x.Nameof));
                
               
                shitdontwork(list);
            }
            if (sortTypeByName == 1)
            {
                list = new List<Equipment>(allItems.OrderByDescending(x => x.Nameof));
                
                
                shitdontwork(list);
            }
            if (sortTypeByName == 2)
            {
                list = new List<Equipment>(allItems.OrderBy(x => x.ident));
               
                
                shitdontwork(list);
            }
            if (sortTypeByName == 3)
            {
                list = new List<Equipment>(allItems.OrderByDescending(x => x.ident));
                
                
                shitdontwork(list);
            }
            sortTypeByName++;
            if (sortTypeByName == 4) { sortTypeByName = 0; }
            

        }

        private void Editor_Completed(object sender, EventArgs e)
        {
            try
            {
                var indx = allItems.First(i => i.ident == theeditor.Text);
                int indexx = allItems.IndexOf(indx);
                allItems[indexx].ispresent = true;
                list = new List<Equipment>(allItems);


                shitdontwork(list);

            }
            
            catch
            {
                if (theeditor.Text != null)
                {
                    DisplayAlert("Cталася помилка", "Чого? Чому? Навіщо? Відповідей на це питання ми не дізнаємось ніколи, але такого серійника я в списку не знайшов", "Ну і не особливо хотілося");
                }
            }
            theeditor.Text = "";
        }

        private void ioBtn_Clicked(object sender, EventArgs e)
        {
            bool iosorting 
        }
    }
}

