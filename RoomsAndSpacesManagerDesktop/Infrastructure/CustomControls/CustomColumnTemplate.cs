using RoomsAndSpacesManagerDataBase.Dto;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace RoomsAndSpacesManagerDesktop.Infrastructure.CustomControls
{
    class CustomColumnTemplate : DataGridTemplateColumn
    {
        public CustomColumnTemplate()
        {
            CustomTextBlockColumnTemplate customTextBlockColumnTemplate = new CustomTextBlockColumnTemplate();
            customTextBlockColumnTemplate.DataContext = BindingDataContext;
            CellTemplate = new System.Windows.DataTemplate(new CustomTextBlockColumnTemplate());
        }

        public RoomDto BindingDataContext
        {
            get
            {
                return (RoomDto)GetValue(BindingDataContextProperty);
            }
            set
            {
                SetValue(BindingDataContextProperty, value);
            }
        }

        public static readonly DependencyProperty BindingDataContextProperty =
           DependencyProperty.Register("BindingDataContext", typeof(RoomDto), typeof(CustomColumnTemplate),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BindingDataContextChanged)));

        private static void BindingDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomColumnTemplate)d).BindingDataContext = ((CustomColumnTemplate)d).BindingDataContext;
        }


    }
}
