using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CircusApp.DB;

namespace CircusApp.Pages
{
    /// <summary>
    ///     Логика взаимодействия для TalantPage.xaml
    /// </summary>
    public partial class TalantPage : Page
    {
        public TalantPage()
        {
            InitializeComponent();
            App.DB.Specialization.ToList().ForEach(i => SpecsLB.Items.Add(i.name));
            App.DB.Skill.ToList().ForEach(i => SkillLB.Items.Add(i.name));
        }

        private void RemoveSpec(object sender, RoutedEventArgs e)
        {
            var name = (sender as Button).DataContext as string;
            App.DB.Specialization.Remove(App.DB.Specialization.FirstOrDefault(i => i.name == name));
            App.DB.SaveChanges();
            SpecsLB.Items.Clear();
            App.DB.Specialization.ToList().ForEach(i => SpecsLB.Items.Add(i.name));
        }

        private void AddSpec(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SpecNameTB.Text))
                return;
            App.DB.Specialization.Add(new Specialization { name = SpecNameTB.Text });
            App.DB.SaveChanges();
            SpecsLB.Items.Clear();
            App.DB.Specialization.ToList().ForEach(i => SpecsLB.Items.Add(i.name));
        }

        private void RemoveSkill(object sender, RoutedEventArgs e)
        {
            var name = (sender as Button).DataContext as string;
            App.DB.Skill.Remove(App.DB.Skill.FirstOrDefault(i => i.name == name));
            App.DB.SaveChanges();
            SkillLB.Items.Clear();
            App.DB.Skill.ToList().ForEach(i => SkillLB.Items.Add(i.name));
        }

        private void AddSkill(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SkillNameTB.Text))
                return;
            App.DB.Skill.Add(new Skill { name = SkillNameTB.Text });
            App.DB.SaveChanges();
            SkillLB.Items.Clear();
            App.DB.Skill.ToList().ForEach(i => SkillLB.Items.Add(i.name));
        }
    }
}