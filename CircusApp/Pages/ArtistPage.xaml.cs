using CircusApp.DB;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CircusApp.Pages
{
    /// <summary>
    ///     Логика взаимодействия для ArtistPage.xaml
    /// </summary>
    public partial class ArtistPage : Page
    {
        private User _user;
        private List<Specialization> _free, _owned;
        private List<Skill> _freeSkill, _ownedSkill;
        public ArtistPage(User user)
        {
            InitializeComponent();
            nameTB.Text = user.name;
            surnameTB.Text = user.surname;
            _user = user;
            _owned = _user.UserSpecialization.Select(i => i.Specialization).ToList();
            CurrSpecLB.ItemsSource = _owned;
            var allSpecs = App.DB.Specialization.ToList();
            _free = allSpecs.ToList().Where(i => !_owned.Any(x => x.id == i.id)).ToList();
            FreeSpecs.ItemsSource = _free;
            _ownedSkill = _user.UserSkill.Select(i => i.Skill).ToList();
            CurrSkillLB.ItemsSource = _ownedSkill;
            var allSkill = App.DB.Skill.ToList();
            _freeSkill = allSkill.ToList().Where(i => !_ownedSkill.Any(x => x.id == i.id)).ToList();
            FreeSkill.ItemsSource = _freeSkill;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _user.surname = surnameTB.Text;
            _user.name = nameTB.Text;
            App.DB.SaveChanges();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FreeSpecs.SelectedValue is null)
                return;
            var upSpec = FreeSpecs.SelectedValue as Specialization;
            _free.Remove(_free.FirstOrDefault(i => i.id == upSpec.id));
            _owned.Add(upSpec);
            CurrSpecLB.ItemsSource = null;
            FreeSpecs.ItemsSource = null;
            CurrSpecLB.ItemsSource = _owned;
            FreeSpecs.ItemsSource = _free;
            App.DB.UserSpecialization.Add(new UserSpecialization { specializationId = upSpec.id, userId = _user.id });
            App.DB.SaveChanges();
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CurrSpecLB.SelectedValue is null)
                return;
            var upSpec = CurrSpecLB.SelectedValue as Specialization;
            _owned.Remove(_owned.FirstOrDefault(i => i.id == upSpec.id));
            _free.Add(upSpec);
            CurrSpecLB.ItemsSource = null;
            FreeSpecs.ItemsSource = null;
            CurrSpecLB.ItemsSource = _owned;
            FreeSpecs.ItemsSource = _free;
            App.DB.UserSpecialization.Remove(App.DB.UserSpecialization.FirstOrDefault(i => i.userId == _user.id && i.specializationId == upSpec.id));
            App.DB.SaveChanges();
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FreeSkill.SelectedValue is null)
                return;
            var upSpec = FreeSkill.SelectedValue as Skill;
            _freeSkill.Remove(_freeSkill.FirstOrDefault(i => i.id == upSpec.id));
            _ownedSkill.Add(upSpec);
            CurrSkillLB.ItemsSource = null;
            FreeSkill.ItemsSource = null;
            CurrSkillLB.ItemsSource = _ownedSkill;
            FreeSkill.ItemsSource = _freeSkill;
            App.DB.UserSkill.Add(new UserSkill { skillId = upSpec.id, userId = _user.id });
            App.DB.SaveChanges();
        }

        private void Button_Click_4(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CurrSkillLB.SelectedValue is null)
                return;
            var upSpec = CurrSkillLB.SelectedValue as Skill;
            _ownedSkill.Remove(_ownedSkill.FirstOrDefault(i => i.id == upSpec.id));
            _freeSkill.Add(upSpec);
            CurrSkillLB.ItemsSource = null;
            FreeSkill.ItemsSource = null;
            CurrSkillLB.ItemsSource = _ownedSkill;
            FreeSkill.ItemsSource = _freeSkill;
            App.DB.UserSkill.Remove(App.DB.UserSkill.FirstOrDefault(i => i.userId == _user.id && i.skillId == upSpec.id));
            App.DB.SaveChanges();
        }
    }
}