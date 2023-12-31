﻿using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class SecureObjectVM : BaseEntityVM
    {
        private int categoryId;
        private string title, info;
        private bool isSelected,isFavorit,isEditable;

        public SecureObjectVM(int id, int imageId, int categoryId, string title, string info,bool isFavorit,int defaultImageId) : base(id, imageId, defaultImageId)
        {
            this.isFavorit = isFavorit;
            this.info = info;
            this.title = title;
            this.categoryId = categoryId;
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

        public int CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditable
        {
            get => isEditable;
            set
            {
                isEditable = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsFavorit
        {
            get => isFavorit;
            set
            {
                isFavorit = value;
                OnPropertyChanged();
            }
        }

        public string Info => info;

        public override void CopyToEntity(BaseEntity entity)
        {
            base.CopyToEntity(entity);
            var temp = (SecureObject)entity;
            temp.Title = title;
            temp.CategoryId = categoryId;
            temp.IsFavorit = isFavorit;
        }
    }
}
