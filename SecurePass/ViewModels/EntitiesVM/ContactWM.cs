using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class ContactWM :  SecureObjectVM
    {
        private string title;
        private string name;
        private string surname;
        private string gender;
        private DateTime birthday;
        private string workPLace;
        private string company;
        private string position;

        public ContactWM(Contact contact) : base(contact.Id,contact.ImageId,contact.CategoryId,contact.UserId,contact.Title,contact.Name, contact.IsFavorit)
        {
            title = contact.Title;
            name = contact.Name;
            surname = contact.Surname;
            gender = contact.Gender;
            birthday = contact.Birthday;
            workPLace = contact.WorkPLace;
            company = contact.Company;
            position = contact.Position;
        }

        public string Title 
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                OnPropertyChanged();
            }
        }

        public string WorkPLace
        {
            get => workPLace;
            set
            {
                workPLace = value;
                OnPropertyChanged();
            }
        }

        public string Company
        {
            get => company;
            set
            {
                company = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }
    }
}
