using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class BankAccountVM : SecureObjectVM
    {
        private string name;
        private string ownerName;
        private string type;
        private int departmentNumber;
        private string bankAccountNumber;
        private string swift;
        private string iban;
        private string pin;

        public BankAccountVM(BankAccount bankAccount) : base(bankAccount.Id,bankAccount.ImageId,bankAccount.CategoryId,bankAccount.UserId,bankAccount.Title, bankAccount.Name,bankAccount.IsFavorit)
        {
            name = bankAccount.Name;    
            ownerName = bankAccount.OwnerName;
            type = bankAccount.Type;
            departmentNumber = bankAccount.DepartmentNumber;
            bankAccountNumber = bankAccount.BankAccountNumber;
            swift = bankAccount.SWIFT; 
            iban = bankAccount.IBAN;
            pin = bankAccount.PIN;
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

        public string OwnerName
        {
            get => ownerName;
            set
            {
                ownerName = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public int DepartmentNumber
        {
            get => departmentNumber;
            set
            {
                departmentNumber = value;
                OnPropertyChanged();
            }
        }

        public string BankAccountNumber
        {
            get => bankAccountNumber;
            set
            {
                bankAccountNumber = value;
                OnPropertyChanged();
            }
        }

        public string SWIFT
        {
            get => swift;
            set
            {
                swift = value;
                OnPropertyChanged();
            }
        }

        public string IBAN
        {
            get => iban;
            set
            {
                iban = value;
                OnPropertyChanged();
            }
        }

        public string PIN
        {
            get => pin;
            set
            {
                pin = value;
                OnPropertyChanged();
            }
        }
    }
}
