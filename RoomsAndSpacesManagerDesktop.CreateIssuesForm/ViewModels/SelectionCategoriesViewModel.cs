using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Infrastructure.Mediators;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.Models.DatabaseModels;
using RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.CreateIssuesForm.ViewModels
{
    class SelectionCategoriesViewModel : ViewModel
    {
        RoomsDbContext roomsContext = new RoomsDbContext();
        public SelectionCategoriesViewModel()
        {
            Categories = roomsContext.GetCategories();
        }

        #region Combobox - Список категорий
        private List<CategoryDto> categories;
        public List<CategoryDto> Categories
        {
            get { return categories; }
            set { Set(ref categories, value); }
        }

        private CategoryDto selectedCategoties;
        public CategoryDto SelectedCategoties
        {
            get { return selectedCategoties; }
            set
            {
                Set(ref selectedCategoties, value);
                SubCategories = roomsContext.GetSubCategotyes(SelectedCategoties);
            }
        }
        #endregion

        #region Combobox - список подкатегорий

        private List<SubCategoryDto> subCategories;
        public List<SubCategoryDto> SubCategories
        {
            get { return subCategories; }
            set
            {
                Set(ref subCategories, value);
            }
        }

        private SubCategoryDto selectedSubCategoties;
        /// <summary>
        /// Выбранная подкатегория помещений
        /// </summary>
        public SubCategoryDto SelectedSubCategoties
        {
            get { return selectedSubCategoties; }
            set
            {
                selectedSubCategoties = value;
                Mediator.NotifyColleagues("ThrowSubCategories", SelectedSubCategoties);
            }
        }

        #endregion
    }
}
