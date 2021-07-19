using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using RoomAndSpacesOV.Dto;
using RoomAndSpacesOV.Models.RvtHelper;
using RoomAndSpacesOV.ViewModels;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesOV.Models.RvtModels
{
    class AddParametersToModelEvHend : IExternalEventHandler
    {
        /// <summary>
        /// Список помещений. Из Cmd
        /// </summary>
        public static List<Space> Spacies { get; set; }

        /// <summary>
        /// Список помещений из БД. Из vm
        /// </summary>
        public static List<RoomDto> RoomsDto { get; set; }

        MainModelRvtHelper mainModelRvtHelper = new MainModelRvtHelper();
        public static event Action<object> ChangeUI;
        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            List<SpaceDto> spacesDto = new List<SpaceDto>();
            using (Transaction transaction = new Transaction(doc, "Внесение значений параметров"))
            {
                transaction.Start();
                foreach (Space item in Spacies)
                {

                    var rvtRoomNumber = item.LookupParameter("Номер").AsString();

                    var roomDto = RoomsDto.FirstOrDefault(x => x.RoomNumber == rvtRoomNumber);

                    SpaceDto space = new SpaceDto()
                    {
                        Name = item.Name,
                        RoomNumber = rvtRoomNumber
                    };

                    #region Внесение параметров

                    if (roomDto != null)
                    {
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Класс чистоты по СанПиН", "Class_chistoti_SanPin", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Класс чистоты по СП 158", "Class_chistoti_SP_158", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Нагрузка ЭОМ", "El_Nagruzka", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Относительная влажность_Текст", "Ot_vlazhnost", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание АР", "Discription_AR", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ВК", "Equipment_VK", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание КР", "Nagruzki_na_perekririe", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание МГ", "Discription_GSV", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ОВ", "Discription_OV", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ЭОМ", "Discription_EOM", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание СС", "Discription_SS", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ХС", "Discription_HS", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Приток кратность", "Pritok", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("M1_Вытяжка кратность", "Vityazhka", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Расчетная площадь", "Summary_Area", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура максимальная С", "T_max", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура минимальная С", "T_min", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура расчетная С", "T_calc", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Мощность_ТХ", "El_Nagruzka", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Освещенность_ТХ", "Osveshennost_pro_obshem_osvech", item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Количество пациентов", nameof(roomDto.Kolichestvo_personala), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Количество персонала", nameof(roomDto.Kolichestvo_posetitelei), item, roomDto));
                        space.parameters.RemoveAll(x => x == null);
                    }

                    if (roomDto?.Rab_mesta_posetiteli != null & roomDto?.Rab_mesta_posetiteli != "")
                    {
                        var roomDtodeee = roomDto?.Rab_mesta_posetiteli.Split('/');

                        int pac;
                        int.TryParse(roomDtodeee[1], out pac);

                        if (pac != default)
                            item.LookupParameter("М1_Количество пациентов")?.Set(pac);

                        int per;
                        int.TryParse(roomDtodeee[0], out per);

                        if (per != default)
                            item.LookupParameter("М1_Количество персонала")?.Set(per);
                    }
                    #endregion
                    spacesDto.Add(space);
                }
                transaction.Commit();
                MainWindowViewModel.SpaciesList = spacesDto.Where(x => x.parameters.Count != 0).ToList();
                ChangeUI?.Invoke(this);
            }
        }

        public string GetName() => nameof(AddParametersToModelEvHend);
    }
}
